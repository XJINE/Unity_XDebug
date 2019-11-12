using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public static partial class Debug
{
    // WARNING:
    // This utility is must be used as dll.
    // If you need more detail, check comments in Log().

    // NOTE:
    // This utility doesn't consider any rich text in l(L)ogs.

    #region Field

    // NOTE:
    // To make a ReadOnlyCollection, it need to use List instead of Queue.
    // Because Queue does't inherit IList.

    public static bool NullTagIsEnabled = true;

    public static bool UnregisteredTagIsEnabled = true;

    private static int maxLogCount = 100;

    private static List<LogData> logs;

    private static Dictionary<string, bool> tags;

    #endregion Field

    #region Property

    public static int MaxLogCount
    {
        get
        {
            return Debug.maxLogCount;
        }
        set
        {
            Debug.maxLogCount = value;
            TrimLogs();
        }
    }

    public static ReadOnlyCollection<LogData> Logs
    {
        get;
        private set;
    }

    public static ReadOnlyDictionary<string, bool> Tags 
    {
        get;
        private set;
    }

    public static bool UnityLoggerEnabled
    {
        get { return UnityEngine.Debug.unityLogger.logEnabled;  }
        set { UnityEngine.Debug.unityLogger.logEnabled = value; }
    }
    
    #endregion Property

    #region Constructor

    static Debug()
    {
        Debug.logs = new List<Debug.LogData>();
        Debug.Logs = new ReadOnlyCollection<Debug.LogData>(Debug.logs);

        Debug.tags = new Dictionary<string, bool>();
        Debug.Tags = new ReadOnlyDictionary<string, bool>(Debug.tags);
    }

    #endregion Constructor

    #region Method

    public static void EnableTag(string tag)
    {
        if (Debug.tags.ContainsKey(tag))
        {
            Debug.tags[tag] = true;
        }
        else
        {
            Debug.tags.Add(tag, true);
        }
    }

    public static void DisableTag(string tag)
    {
        if (Debug.tags.ContainsKey(tag))
        {
            Debug.tags[tag] = false;
        }
        else
        {
            Debug.tags.Add(tag, false);
        }
    }

    public static bool TagIsEnabled(string tag)
    {
        if (tag == null)
        {
            return Debug.NullTagIsEnabled;
        }

        if (Debug.tags.ContainsKey(tag))
        {
            return Debug.tags[tag];
        }

        Debug.tags.Add(tag, Debug.UnregisteredTagIsEnabled);

        return Debug.UnregisteredTagIsEnabled;
    }

    public static void LogError(object message, string tag = null)
    {
        LogError(message, null, tag);
    }

    public static void LogError(object message, Object context, string tag = null)
    {
        Log(message, context, tag, LogType.Error);
    }

    public static void LogAssert(object message, string tag = null)
    {
        LogAssert(message, null, tag);
    }

    public static void LogAssert(object message, Object context, string tag = null)
    {
        Log(message, context, tag, LogType.Assert);
    }

    public static void LogWarning(object message, string tag = null)
    {
        LogWarning(message, null, tag);
    }

    public static void LogWarning(object message, Object context, string tag = null)
    {
        Log(message, context, tag, LogType.Warning);
    }

    public static void LogException(object message, string tag = null)
    {
        LogException(message, null, tag);
    }

    public static void LogException(object message, Object context, string tag = null)
    {
        Log(message, context, tag, LogType.Exception);
    }

    public static void Log(object message, string tag = null)
    {
        Log(message, null, tag);
    }

    public static void Log(object message, Object context, string tag = null)
    {
        Log(message, context, tag, LogType.Log);
    }

    private static void Log(object message, Object context, string tag, LogType logType)
    {
        // WARNING:
        // When use standard UnityEditor & Debug.Log,
        // we can jump to the call-point in source code with console click.
        // However in this case, the call-point becomming following "Debug.unityLogger.Log".
        // Unfortunately, there are no solution to avoid this problem from any code.
        // So we have to make a ".dll".
        // https://answers.unity.com/questions/176422/debug-wrapper-class.html
        // https://answers.unity.com/questions/1226230/how-to-properly-call-debuglogstring-in-a-custom-lo.html

        if (!TagIsEnabled(tag))
        {
            return;
        }

        LogData logData;

        bool messageIsLogData = message.GetType() == typeof(LogData);

        if (messageIsLogData)
        {
            logData = (LogData)message;
        }
        else
        {
            logData = new LogData(message, context, tag, logType);

            Debug.logs.Add(logData);

            TrimLogs();
        }

        // NOTE:
        // In following case, the output text gets "tag:message" format.
        // UnityEngine.Debug.unityLogger.Log(logType, tag, logData.ToString(), context);

        const string UNITY_LOGGER_FORMAT = "{0}"; 

        UnityEngine.Debug.unityLogger.LogFormat(logType, context, UNITY_LOGGER_FORMAT, logData);
    }

    private static void TrimLogs() 
    {
        int diff = Debug.logs.Count - Debug.maxLogCount;

        for (int i = 0; i < diff; i++)
        {
            Debug.logs.RemoveAt(0);
        }
    }

    public static void ClearLogs() 
    {
        Debug.logs.Clear();
    }

    public static void ClearTags() 
    {
        Debug.tags.Clear();
    }

    #endregion Method
}