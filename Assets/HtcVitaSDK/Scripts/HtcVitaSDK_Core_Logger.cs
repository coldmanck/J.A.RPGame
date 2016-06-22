using System;
using System.Collections;
using System.Reflection;

namespace Htc.Vita.Core
{
    public class Logger
    {
        private const string LOGGER_TYPE_NAME_UNITY = "UnityEngine.Debug";

        private static bool sHasDetected = false;
        private static bool sUsingUnityLog = true;
        private static Type sUnityLogType = null;

        public static void Log(string message)
        {
            if (!sHasDetected || sUsingUnityLog)
            {
                UnityLog(message);
            }
            else
            {
                ConsoleLog(message);
            }
        }

        private static void ConsoleLog(string message)
        {
            Console.WriteLine(message);
            sHasDetected = true;
        }

        private static void UnityLog(string message)
        {
            MethodInfo methodInfo = null;
            try
            {
                if (sUnityLogType == null)
                {
                    sUnityLogType = GetType(LOGGER_TYPE_NAME_UNITY);
                }
                methodInfo = sUnityLogType.GetMethod("Log", new Type[] { typeof(string) });
                methodInfo.Invoke(null, new object[] { message });
                sUsingUnityLog = true;
            }
            catch (Exception)
            {
                ConsoleLog(message);
                sUsingUnityLog = false;
            }
            sHasDetected = true;
        }

        private static Type GetType(string typeName)
        {
            Type type = Type.GetType(typeName);
            if (type != null)
            {
                return type;
            }
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = assembly.GetType(typeName);
                if (type != null)
                {
                    return type;
                }
            }
            return null;
        }
    }
}
