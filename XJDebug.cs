using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public static partial class XJDebug
{
    // WARNING:
    // This utility is must be used as dll.
    // If you need more detail, check comments in "XJDebug.Log".

    // NOTE:
    // This utility doesn't consider any rich text in l(L)ogs.

    #region Field

    // NOTE:
    // To make a ReadOnlyCollection, it need to use List instead of Queue.
    // Because Queue does't inherit IList.

    public static bool KeepDisableTagLog = false;

    public static bool NullIsEnableTag = true;

    private static int maxLogCount = 1000;

    private static List<LogInfo> logs;

    private static Dictionary<string, bool> tags;

    #endregion Field

    #region Property

    public static int MaxLogCount
    {
        get
        {
            return XJDebug.maxLogCount;
        }
        set
        {
            XJDebug.maxLogCount = value;
            TrimLogs();
        }
    }

    public static ReadOnlyCollection<LogInfo> Logs
    {
        get;
        private set;
    }

    public static ReadOnlyDictionary<string, bool> Tags 
    {
        get;
        private set;
    }

    #endregion Property

    #region Constructor

    static XJDebug()
    {
        XJDebug.logs = new List<XJDebug.LogInfo>();
        XJDebug.Logs = new ReadOnlyCollection<XJDebug.LogInfo>(XJDebug.logs);

        XJDebug.tags = new Dictionary<string, bool>();
        XJDebug.Tags = new ReadOnlyDictionary<string, bool>(XJDebug.tags);
    }

    #endregion Constructor

    #region Method

    public static void EnableTag(string tag)
    {
        if (XJDebug.tags.ContainsKey(tag))
        {
            XJDebug.tags[tag] = true;
        }
        else
        {
            XJDebug.tags.Add(tag, true);
        }
    }

    public static void DisableTag(string tag)
    {
        if (XJDebug.tags.ContainsKey(tag))
        {
            XJDebug.tags[tag] = false;
        }
        else
        {
            XJDebug.tags.Add(tag, false);
        }
    }

    public static bool TagIsEnable(string tag)
    {
        if (tag == null)
        {
            return XJDebug.NullIsEnableTag;
        }

        if (XJDebug.tags.ContainsKey(tag))
        {
            return XJDebug.tags[tag];
        }

        return false;
    }

    public static void LogError(object message, string tag = null)
    {
        LogError(message, null, tag);
    }

    public static void LogError(object message, Object context, string tag = null)
    {
        Log(LogType.Error, tag, message, context);
    }

    public static void LogAssert(object message, string tag = null)
    {
        LogAssert(message, null, tag);
    }

    public static void LogAssert(object message, Object context, string tag = null)
    {
        Log(LogType.Assert, tag, message, context);
    }

    public static void LogWarning(object message, string tag = null)
    {
        LogWarning(message, null, tag);
    }

    public static void LogWarning(object message, Object context, string tag = null)
    {
        Log(LogType.Warning, tag, message, context);
    }

    public static void LogException(object message, string tag = null)
    {
        LogException(message, null, tag);
    }

    public static void LogException(object message, Object context, string tag = null)
    {
        Log(LogType.Exception, tag, message, context);
    }

    public static void Log(object message, string tag = null)
    {
        Log(message, null, tag);
    }

    public static void Log(object message, Object context, string tag = null)
    {
        Log(LogType.Log, tag, message, context);
    }

    private static void Log(LogType logType, string tag, object message, Object context)
    {
        // WARNING:
        // When use standard UnityEditor & Debug.Log,
        // we can jump to the call-point in source code with console click.
        // However in this case, the call-point becomming following "Debug.unityLogger.Log".
        // Unfortunately, there are no solution to avoid this problem from any code.
        // So we have to make a ".dll".
        // https://answers.unity.com/questions/176422/debug-wrapper-class.html
        // https://answers.unity.com/questions/1226230/how-to-properly-call-debuglogstring-in-a-custom-lo.html

        // NOTE:
        // Arguments order is same as "Debug.unity.logger.Log".

        LogInfo logInfo = new LogInfo(logType, message, context, tag);

        bool tagIsEnable = TagIsEnable(tag);

        if (tagIsEnable)
        {
            Debug.unityLogger.Log(logType, tag, logInfo.ToString(), context);
        }

        if (tagIsEnable || XJDebug.KeepDisableTagLog)
        {
            UpdateLogs(logInfo);
        }
    }

    private static void UpdateLogs(LogInfo logInfo) 
    {
        XJDebug.logs.Add(logInfo);
        TrimLogs();
    }

    private static void TrimLogs() 
    {
        int logsCount = XJDebug.logs.Count;

        while (logsCount > XJDebug.maxLogCount)
        {
            XJDebug.logs.RemoveAt(0);
            logsCount = XJDebug.logs.Count;
        }
    }

    public static void ClearLogs() 
    {
        XJDebug.logs.Clear();
    }

    public static void ClearTags() 
    {
        // NOTE:
        // All of tags will shows disable.

        XJDebug.tags.Clear();
    }

    #endregion Method
}