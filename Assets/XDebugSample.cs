using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class XDebugSample : MonoBehaviour
{
    void Start ()
    {
        // NOTE:
        // Log are able to control with tag. Default tag value is null and it is enabled.
        // You can switch the setting with Debug.NullIsEnableTag if you need.
        // Debug.NullIsEnableTag = true;

        Debug.EnableTag("TAG_A");
        Debug.DisableTag("TAG_B");

        Debug.Log("Message01", "TAG_A");
        Debug.Log("Message02", "TAG_B");
        Debug.LogWarning("Message03", this);

        // NOTE:
        // Output format is able to set with "Debug.LogData.StringFormat".
        // 
        // {0} : Message.
        // {1} : Context.
        // {2} : Tag.
        // {3} : LogType.
        // {4} : Time in Time.sinceLevelLoad.
        // {5} : Time in System.DateTime.Now.
        // 
        // Also there are some related settings.
        // 
        // Debug.LogData.StringFormatNullMessage = "";
        // Debug.LogData.StringFormatNullContext = "";
        // Debug.LogData.StringFormatNullTag     = "";
        // Debug.LogData.StringFormatDateTimeNow = "HH:mm:ss";

        Debug.LogData.StringFormat = "[{5}] {0}";
        Debug.Log("Message04");

        // NOTE:
        // ICollection such as Array, List or any others are splitted to each item.
        // Splitter format is able to set with "Debug.LogData.StringFormatSeparator".

        List<object> list = new List<object>() { "Message05", 6, 7 };
        Debug.Log(list, "TAG_A");

        // NOTE:
        // Logs are keeped in Debug.Logs upto Debug.MaxLogCount.

        // Take null tagged logs with LINQ.
        foreach (Debug.LogData log in Debug.Logs.Where<Debug.LogData>(log => log.tag == null))
        {
            Debug.Log(log);
        }

        // NOTE:
        // Debug.UnityLoggerEnabled equals UnityEngine.Debug.unitylogger.enabled.

        Debug.UnityLoggerEnabled = false;

        Debug.Log("Message08");

        Debug.UnityLoggerEnabled = true;

        foreach (Debug.LogData log in Debug.Logs)
        {
            Debug.Log(log);
        }
    }
}