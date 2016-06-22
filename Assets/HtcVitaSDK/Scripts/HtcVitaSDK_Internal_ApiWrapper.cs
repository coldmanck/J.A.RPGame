using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

using Htc.Vita.Core;
using LitJson;
using PublicKeyConvert;

namespace Htc.Vita.Internal
{
    class ApiWrapper
    {
        private static readonly List<InitCallback> INIT_CALLBACKS = new List<InitCallback>();

        [DllImport("vita_api", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void init(string identity, get_license callback);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int get_license([MarshalAs(UnmanagedType.LPStr)] string message, [MarshalAs(UnmanagedType.LPStr)] string signature);

        private static string sAppId = "";
        private static string sAppKey = "";

        public ApiWrapper()
        {
        }

        public void Init(string appId, string appKey, InitCallback initCallback)
        {
            sAppId = appId;
            sAppKey = appKey;
            if (initCallback != null)
            {
                INIT_CALLBACKS.Add(initCallback);
            }

            init(sAppId, new get_license(GetLicenseHandler));
        }

        /*
         * Responsed license JSON format:
         * {
         *   "issueTime": 1442301893123, // epoch time in milliseconds, Long
         *   "expirationTime": 1442451893123, // epoch time in milliseconds, Long
         *   "latestVersion": 1001, // versionId, Integer
         *   "updateRequired": true // Boolean
         * }
         */
        private static int GetLicenseHandler([MarshalAs(UnmanagedType.LPStr)] string message, [MarshalAs(UnmanagedType.LPStr)] string signature)
        {
            // Logger.Log("Raw Message: " + message);
            // Logger.Log("Raw Signature: " + signature);

            bool isVerified = (message != null && message.Length > 0);
            if (!isVerified)
            {
                for (int i = INIT_CALLBACKS.Count - 1; i >= 0; i--)
                {
                    InitCallback initCallback = INIT_CALLBACKS[i];
                    initCallback.OnFailure(90003, "License message is empty");
                    INIT_CALLBACKS.Remove(initCallback);
                }
                return 0;
            }

            isVerified = (signature != null && signature.Length > 0);
            if (!isVerified) // signature is empty - error code mode
            {
                JsonData jsonData = JsonMapper.ToObject(message);
                int errorCode = 99999;
                string errorMessage = "";

                try
                {
                    errorCode = Int32.Parse((string)jsonData["code"]);
                }
                catch
                {
                }
                try
                {
                    errorMessage = (string)jsonData["message"];
                }
                catch
                {
                }

                for (int i = INIT_CALLBACKS.Count - 1; i >= 0; i--)
                {
                    InitCallback initCallback = INIT_CALLBACKS[i];
                    initCallback.OnFailure(errorCode, errorMessage);
                    INIT_CALLBACKS.Remove(initCallback);
                }
                return 0;
            }

            isVerified = VerifyMessage(sAppId + "\n" + message, signature, sAppKey);
            if (!isVerified)
            {
                for (int i = INIT_CALLBACKS.Count - 1; i >= 0; i--)
                {
                    InitCallback initCallback = INIT_CALLBACKS[i];
                    initCallback.OnFailure(90001, "License verification failed");
                    INIT_CALLBACKS.Remove(initCallback);
                }
                return 0;
            }

            string decodedLicense = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(message.Substring(message.IndexOf("\n") + 1)));
            JsonData jsonData2 = JsonMapper.ToObject(decodedLicense);
            Logger.Log("License: " + decodedLicense);

            long issueTime = -1;
            long expirationTime = -1;
            int latestVersion = -1;
            bool updateRequired = false;

            try
            {
                issueTime = (long)jsonData2["issueTime"];
            }
            catch
            {
            }
            try
            {
                expirationTime = (long)jsonData2["expirationTime"];
            }
            catch
            {
            }
            try
            {
                latestVersion = unchecked((int)jsonData2["latestVersion"]);
            }
            catch
            {
            }
            try
            {
                updateRequired = (bool)jsonData2["updateRequired"];
            }
            catch
            {
            }

            for (int i = INIT_CALLBACKS.Count - 1; i >= 0; i--)
            {
                InitCallback initCallback = INIT_CALLBACKS[i];
                initCallback.OnSuccess(issueTime, expirationTime, latestVersion, updateRequired);
                INIT_CALLBACKS.Remove(initCallback);
            }
            return 0;
        }

        private static bool VerifyMessage(string message, string signature, string publicKey)
        {
            try
            {
                RSACryptoServiceProvider provider = PEMKeyLoader.CryptoServiceProviderFromPublicKeyInfo(publicKey);
                byte[] decodedSignature = System.Convert.FromBase64String(signature);
                SHA1Managed sha = new SHA1Managed();
                byte[] data = System.Text.Encoding.UTF8.GetBytes(message);

                return provider.VerifyData(data, sha, decodedSignature);
            }
            catch (Exception e)
            {
                Logger.Log(e.ToString());
            }
            return false;
        }

        private class Identity
        {
            public string appId;
            public string hmdId;
            public List<string> ecTags;
            public int versionId;

            public Identity(string appId, string hmdId, List<string> ecTags, int versionId)
            {
                this.appId = appId;
                this.hmdId = hmdId;
                this.ecTags = ecTags;
                this.versionId = versionId;
            }
        }

        abstract public class InitCallback
        {
            abstract public void OnSuccess(long issueTime, long expirationTime, int latestVersion, bool updateRequired);
            abstract public void OnFailure(int errorCode, string errorMessage);
        }
    }
}
