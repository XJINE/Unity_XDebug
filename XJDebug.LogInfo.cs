using System.Collections;
using UnityEngine;

public static partial class XJDebug
{
    #region Class

    public class LogInfo
    {
        #region Field

        // HOWTO:
        // You can replace XJDebug.LogInfo.StringFormat as you like.
        // 
        // {0} : LogType.
        // {1} : Time in Time.sinceLevelLoad.
        // {2} : Time in System.DateTime.Now.
        // {3} : Message.
        // {4} : Context.
        // {5} : Tag.

        public static readonly string DefaultStringFormat = "{3}";
        public static readonly string DefaultStringFormatTimeInSinceLevelLoad = "{1} - {3}";
        public static readonly string DefaultStringFormatTimeInSystem = "{2} - {3}";
        public static string StringFormat = XJDebug.LogInfo.DefaultStringFormat;

        public static readonly string DefaultStringFormatSplitter = ", ";
        public static string StringFormatSplitter = XJDebug.LogInfo.DefaultStringFormatSplitter;

        public static readonly string DefaultStringFormatNullMessage = "NO_MESSAGE";
        public static readonly string DefaultStringFormatNullContext = "NO_CONTEXT";
        public static readonly string DefaultStringFormatNullTag     = "NO_TAG";
        public static string StringFormatNullMessage = XJDebug.LogInfo.DefaultStringFormatNullMessage;
        public static string StringFormatNullContext = XJDebug.LogInfo.DefaultStringFormatNullContext;
        public static string StringFormatNullTag     = XJDebug.LogInfo.DefaultStringFormatNullTag;

        // NOTE:
        // "message" and "context" must be keep as string
        // because "object message" and "Object context" are may be null when this instance is referenced.

        public readonly LogType logType;
        public readonly float timeInSinceLevelLoad;
        public readonly System.DateTime timeInSystem;
        public readonly string message;
        public readonly string context;
        public readonly string tag;

        #endregion Field

        #region Constructor

        public LogInfo(LogType logType, object message, Object context, string tag)
        {
            this.logType = logType;
            this.timeInSinceLevelLoad = Time.timeSinceLevelLoad;
            this.timeInSystem = System.DateTime.Now;
            this.tag = tag;

            ICollection messages = message as ICollection;

            if (messages != null)
            {
                this.message = "";

                foreach (var item in messages)
                {
                    this.message += item + LogInfo.StringFormatSplitter;
                }
            }
            else
            {
                this.message = message == null ? XJDebug.LogInfo.StringFormatNullMessage : message.ToString();
            }

            this.context = context == null ? XJDebug.LogInfo.StringFormatNullContext : context.ToString();
        }

        #endregion Constructor

        #region Method

        public override string ToString()
        {
            return string.Format(LogInfo.StringFormat,
                                 this.logType,
                                 this.timeInSinceLevelLoad,
                                 this.timeInSystem,
                                 this.message,
                                 this.context,
                                 this.tag == null ? XJDebug.LogInfo.StringFormatNullTag : tag);
        }

        #endregion Method
    }

    #endregion Class
}