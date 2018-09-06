using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class XJDebugSample : MonoBehaviour
{
    void Start ()
    {
        // SAMPLE:1
        // Log are able to control with tag.

        XJDebug.EnableTag("TAG_A");
        XJDebug.DisableTag("TAG_B");

        XJDebug.Log("MESSAGE_1", "TAG_A");
        XJDebug.Log("MESSAGE_2", this, "TAG_B");

        // SAMPLE:2
        // If tag is omitted, it becomes null.
        // null is enabled tag in default, but able to switch the setting.

        XJDebug.Log("MESSAGE_3", this);
        XJDebug.Log("MESSAGE_4", this, null);

        XJDebug.NullIsEnableTag = false;

        XJDebug.Log("MESSAGE_5");

        XJDebug.NullIsEnableTag = true;

        // SAMPLE:3
        // null message or context shows "NULL".

        XJDebug.Log(null);

        // SAMPLE:4
        // There are logTypes such as Warning, Exception or any others.
        
        XJDebug.LogWarning("MESSAGE_6");

        // SAMPLE:5
        // Output format is able to set with "XJDebug.LogInfo.StringFormat".
        // 
        // {0} : LogType.
        // {1} : Time in Time.sinceLevelLoad.
        // {2} : Time in System.DateTime.Now.
        // {3} : Message.
        // {4} : Context.
        // {5} : Tag.

        XJDebug.LogInfo.StringFormat = "{2}({1}) {3}";

        XJDebug.Log("MESSAGE_13");

        // SAMPLE:6
        // ICollection such as Array, List or any others are splitted to each item.
        // Splitter format is able to set with "XJDebug.LogInfo.StringFormatSplitter".

        string[] stringArray = new string[]{ "MESSAGE_7", "MESSAGE_8", "MESSAGE_9" };
        XJDebug.Log(stringArray);

        XJDebug.LogInfo.StringFormatSplitter = "/";

        List<int> intList = new List<int>() { 10, 11, 12 };
        XJDebug.Log(intList);

        // SAMPLE:7
        // Logs are keeped in XJDebug.Logs upto XJDebug.MaxLogCount.
        // Related settings are there.
        // - XJDebug.KeepDisableTagLog
        // - XJDebug.MaxLogCount

        foreach (XJDebug.LogInfo log in XJDebug.Logs.Where<XJDebug.LogInfo>(log => log.tag == null))
        {
            Debug.Log("[HISTORY] " + log.ToString());
        }
    }
}