using System;
using System.Collections.Generic;
using UnityEngine;

public static partial class Debug
{
    #region Class

    public class LogData
    {
        #region Field

        // NOTE:
        // You can replace XDebug.LogInfo.StringFormat as you like.
        // 
        // {0} : Message.
        // {1} : Context.
        // {2} : Tag.
        // {3} : LogType.
        // {4} : Time in Time.sinceLevelLoad.
        // {5} : Time in System.DateTime.Now.

        public static string StringFormat            = "{0}";
        public static string StringFormatNullMessage = "";
        public static string StringFormatNullContext = "";
        public static string StringFormatNullTag     = "";
        public static string StringFormatDateTimeNow = "HH:mm:ss";
        public static string StringFormatSeparator   = ", ";

        // NOTE:
        // "message" and "context" must be keep as string
        // because "object message" and "Object context" are may be null when this instance is referenced.

        public readonly string   message;
        public readonly string   context;
        public readonly string   tag;
        public readonly LogType  logType;
        public readonly float    timeInSinceLevelLoad;
        public readonly DateTime timeInSystemDateTime;

        #endregion Field

        #region Constructor

        public LogData(object message, UnityEngine.Object context, string tag, LogType logType)
        {
            // NOTE:
            // message and context.ToString() make some garbage that will call GC.
            // However, when keeping these references directly,
            // these are kept in here even if these are removed in the other process.
            // So it can't avoid making the string :-(

            this.tag                  = tag;
            this.logType              = logType;
            this.timeInSinceLevelLoad = Time.timeSinceLevelLoad;
            this.timeInSystemDateTime = DateTime.Now;

            if (message is IEnumerable<object> messages)
            {
                // NOTE:
                // This makes boxing but accepts. Use StringBuilder instead if needed.

                this.message = string.Join(LogData.StringFormatSeparator, messages);
            }
            else
            {
                this.message = message?.ToString();
            }

            this.context = context?.ToString();
        }

        #endregion Constructor

        #region Method

        public override string ToString()
        {
            // NOTE:
            // It cannot make some string cache
            // because of the format like a LogData.StringFormat may be changed.

            return string.Format(LogData.StringFormat,
                                 this.message ?? Debug.LogData.StringFormatNullMessage,
                                 this.context ?? Debug.LogData.StringFormatNullContext,
                                 this.tag     ?? Debug.LogData.StringFormatNullTag,
                                 this.logType,
                                 this.timeInSinceLevelLoad,
                                 this.timeInSystemDateTime.ToString(StringFormatDateTimeNow));
        }

        #endregion Method
    }

    #endregion Class
}