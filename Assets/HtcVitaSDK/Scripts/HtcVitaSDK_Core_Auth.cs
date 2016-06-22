using System;
using System.Collections.Generic;
using System.Text;

using Htc.Vita.Internal;

namespace Htc.Vita.Core
{
    class Auth
    {
        public static void Init(string appId, string appKey, Auth.Callback callback)
        {
            if (appId == null || appKey == null || callback == null)
            {
                throw new InvalidOperationException("publicKey == null || appId == null || callback == null");
            }
            ApiWrapper apiWrapper = new ApiWrapper();
            ApiWrapper.InitCallback initCallback = new MyInitCallback(callback);
            apiWrapper.Init(appId, appKey, initCallback);
        }

        private class MyInitCallback : ApiWrapper.InitCallback
        {
            Auth.Callback mAuthCallback = null;

            public MyInitCallback(Auth.Callback authCallback)
            {
                mAuthCallback = authCallback;
            }

            public override void OnSuccess(long issueTime, long expirationTime, int latestVersion, bool updateRequired)
            {
                if (mAuthCallback != null)
                {
                    mAuthCallback.OnSuccess(issueTime, expirationTime, latestVersion, updateRequired);
                }
            }

            public override void OnFailure(int errorCode, string errorMessage)
            {
                if (mAuthCallback != null)
                {
                    mAuthCallback.OnFailure(errorCode, errorMessage);
                }
            }
        }

        abstract public class Callback
        {
            abstract public void OnSuccess(long issueTime, long expirationTime, int latestVersion, bool updateRequired);
            abstract public void OnFailure(int errorCode, string errorMessage);
        }
    }
}
