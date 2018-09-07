using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class XJDebugSample : MonoBehaviour
{
    void Start ()
    {
        // SAMPLE:1
        // Log are able to control with tag. Default tag value is null and it is enabled.
        // You can switch the setting with XJDebug.NullIsEnableTag if you need.
        // XJDebug.NullIsEnableTag = true;

        XJDebug.EnableTag("TAG_A");
        XJDebug.DisableTag("TAG_B");

        XJDebug.Log("MESSAGE_1", "TAG_A");
        XJDebug.Log("MESSAGE_2", this, "TAG_B");
        XJDebug.LogWarning("MESSAGE_3", this);


        // SAMPLE:2
        // Output format is able to set with "XJDebug.LogInfo.StringFormat".
        // 
        // {0} : LogType.
        // {1} : Time in Time.sinceLevelLoad.
        // {2} : Time in System.DateTime.Now.
        // {3} : Message.
        // {4} : Context.
        // {5} : Tag.

        XJDebug.LogInfo.StringFormat = "{2}({1}) {3}";
        XJDebug.Log("MESSAGE_4");

        // SAMPLE:3
        // ICollection such as Array, List or any others are splitted to each item.
        // Splitter format is able to set with "XJDebug.LogInfo.StringFormatSplitter".
        // XJDebug.LogInfo.StringFormatSplitter = "/";

        List<int> intList = new List<int>() { 5, 6, 7 };
        XJDebug.Log(intList);

        // SAMPLE:4
        // Logs are keeped in XJDebug.Logs upto XJDebug.MaxLogCount.

        foreach (XJDebug.LogInfo log in XJDebug.Logs.Where<XJDebug.LogInfo>(log => log.tag == null))
        {
            Debug.Log("[HISTORY] " + log.ToString());
        }
    }
}