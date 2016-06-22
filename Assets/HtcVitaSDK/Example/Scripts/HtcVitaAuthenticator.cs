using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Htc.Vita.Core;
using Htc.Vita.Internal;

namespace Htc.Vita.Example
{
    public class HtcVitaAuthenticator : MonoBehaviour
    {
        // Vendor should own these values:
        public static string APP_ID = "931c62d8-469f-4360-8d92-0df7ac8ed434";
        public static string APP_KEY = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCiOIjhHke5"
                                     + "RluTbh/891hMlK8cHgHXkzfk3ZXbMUSg+CqYu6UXumvthSwc"
                                     + "JxBo4jrkJH/kVXA4DXctEMSZ9ECRj0a9zrbDpe4QqhOaOC9q"
                                     + "KkhgZAXTufnln5kqQRvNIy54DGYFMsWA37gZnlreKOvucBp1"
                                     + "elbJhHVLqZPQcxJFjQIDAQAB";

        private static int sGuiUpdateTimes = 0;
        private static HtcVitaAuthenticator sInstance = null;

        private bool mAuthChecked = false;
        private long mIssueTime = 0;
        private long mExpirationTime = 0;
        private int mErrorCode = 0;
        private string mErrorMessage = "";

        void Awake()
        {
            sInstance = this;
        }

        void Start()
        {
            Auth.Init(APP_ID, APP_KEY, new MyAuthCallback());
        }

        void Update()
        {
            if (!sInstance.IsAuthChecked())
            {
                if (sInstance.GetIssueTime() > 0)
                {
                    sInstance.SetAuthChecked(true);
#if UNITY_5_3
                    int nextLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
                    int levelCount = UnityEngine.SceneManagement.SceneManager.sceneCount;
                    if (nextLevel < levelCount)
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevel);
                    }
                    else
                    {
                        Htc.Vita.Core.Logger.Log("Level " + nextLevel + " is not found. Skipped.");
                    }
#else
                    int nextLevel = Application.loadedLevel + 1;
                    if (nextLevel < Application.levelCount)
                    {
                        Application.LoadLevel(nextLevel);
                    }
                    else
                    {
                        Htc.Vita.Core.Logger.Log("Level " + nextLevel + " is not found. Skipped.");
                    }
#endif
                }
                if (sInstance.GetErrorCode() > 0 && sInstance.GetErrorMessage().Length > 0)
                {
                    sInstance.SetAuthChecked(true);
                    Htc.Vita.Core.Logger.Log("Error occured. Licence checking is failed.");
                    Application.Quit();
                }
            }
        }

        void OnGUI()
        {
            sGuiUpdateTimes++;
            GUI.Label(new Rect(10, 10, Screen.width, Screen.height), "OnGUI(): " + sGuiUpdateTimes);
            if (sInstance.IsAuthChecked())
            {
                long issueTime = sInstance.GetIssueTime();
                long expirationTime = sInstance.GetExpirationTime();
                int errorCode = sInstance.GetErrorCode();
                string errorMessage = sInstance.GetErrorMessage();
                if (issueTime > 0 || expirationTime > 0)
                {
                    GUI.Label(new Rect(10, 30, Screen.width, Screen.height), "issueTime: " + issueTime);
                    GUI.Label(new Rect(10, 50, Screen.width, Screen.height), "expirationTime: " + expirationTime);
                }
                else if (errorCode > 0 || errorMessage.Length > 0)
                {
                    GUI.Label(new Rect(10, 30, Screen.width, Screen.height), "errorCode: " + errorCode);
                    GUI.Label(new Rect(10, 50, Screen.width, Screen.height), "errorMessage: " + errorMessage);
                }
            }
        }

        public int GetErrorCode()
        {
            return mErrorCode;
        }

        public HtcVitaAuthenticator SetErrorCode(int errorCode)
        {
            mErrorCode = errorCode;
            return this;
        }

        public string GetErrorMessage()
        {
            return mErrorMessage;
        }

        public HtcVitaAuthenticator SetErrorMessage(string errorMessage)
        {
            if (errorMessage != null)
            {
                mErrorMessage = errorMessage;
            }
            return this;
        }

        public long GetExpirationTime()
        {
            return mExpirationTime;
        }

        public HtcVitaAuthenticator SetExpirationTime(long expirationTime)
        {
            mExpirationTime = expirationTime;
            return this;
        }

        public long GetIssueTime()
        {
            return mIssueTime;
        }

        public HtcVitaAuthenticator SetIssueTime(long issueTime)
        {
            mIssueTime = issueTime;
            return this;
        }

        public bool IsAuthChecked()
        {
            return mAuthChecked;
        }

        public HtcVitaAuthenticator SetAuthChecked(bool authChecked)
        {
            mAuthChecked = authChecked;
            return this;
        }

        private class MyAuthCallback : Auth.Callback
        {
            public override void OnSuccess(long issueTime, long expirationTime, int latestVersion, bool updateRequired)
            {
                sInstance.SetIssueTime(issueTime)
                        .SetExpirationTime(expirationTime);
                Htc.Vita.Core.Logger.Log("[MyAuthCallback.OnSuccess] latestVersion: " + latestVersion);
                Htc.Vita.Core.Logger.Log("[MyAuthCallback.OnSuccess] updateRequired: " + updateRequired);
            }

            public override void OnFailure(int errorCode, string errorMessage)
            {
                sInstance.SetErrorCode(errorCode)
                        .SetErrorMessage(errorMessage);
            }
        }
    }
}
