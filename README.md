# Unity_XDebug

XDebug has Debug class and it will replace UnityEngine.Debug.
The (X)Debug class will keep logs with these time and the tag.

## Import to Your Project

You can import this asset from UnityPackage.

- [XDebug.unitypackage](https://github.com/XJINE/Unity_XDebug/blob/master/XDebug.unitypackage)

## How to Use

### Log Control with Tag

Log and the output are able to control with tag.

Default tag value is ``null`` and it is enabled.
You can switch the setting with ``Debug.NullIsEnableTag`` if you need.

EX:
```csharp
Debug.EnableTag("TAG_A");
Debug.DisableTag("TAG_B");

Debug.Log("Message01", "TAG_A");
Debug.Log("Message02", "TAG_B");
Debug.LogWarning("Message03", this);
```
RESULT:
```
Message01
Message03
```

### Output Format

Output format is able to customize with ``Debug.LogData.StringFormat``.
This format is used as ```String.Format``` when the message will be generated.

Then, following KEYs are replaced with ``Debug.LogData`` field values.

| KEY | FIELD                       |
|:----|:----------------------------|
| {0} | Message                     |
| {1} | Context                     |
| {2} | Tag                         |
| {3} | LogType                     |
| {4} | Time in Time.sinceLevelLoad |
| {5} | Time in System.DateTime.Now |

EX:
```csharp
 Debug.LogInfo.StringFormat = "[{5}] {0}";
 Debug.Log("MESSAGE04");
```

RESULT:
```
[14:28:59] Message04
```

Some of these formats are able to set with the following settings.

```csharp
Debug.LogData.StringFormatNullMessage = "";
Debug.LogData.StringFormatNullContext = "";
Debug.LogData.StringFormatNullTag     = "";
Debug.LogData.StringFormatDateTimeNow = "HH:mm:ss";
```

### Output IEnumerable

IEnumerable message such as Array, List and any others are separated into each item with ``Debug.LogData.StringFormatSeparator``.

EX:
```csharp
List<int> list = new List<int>() { "Message05", 6, 7 };
Debug.Log(list, "TAG_A");
```

RESULT:
```
Message05, 6, 7
```

### Keep Logs

Logs are keeped in ``Debug.Logs`` upto ``Debug.MaxLogCount``.

EX:
```csharp
// Take null tagged logs with LINQ.
foreach (Debug.LogData log in Debug.Logs.Where<Debug.LogData>(log => log.tag == null))
{
    Debug.Log(log);
}
```

### Disable Output

``Debug.UnityLoggerEnabled`` equals ``UnityEngine.Debug.unitylogger.enabled.``.
When it gets false, no messages are output but the log will be kept in ``(X)Debug.Logs``.

EX:
```csharp
Debug.UnityLoggerEnabled = false;

Debug.Log("Message08");

Debug.UnityLoggerEnabled = true;

foreach (Debug.LogData log in Debug.Logs)
{
    Debug.Log(log);
}
```

RESULT:
```
~
Message08
```

## Limitation

### Reference of Message and Context

"message" and "context" are keeped as string in ``Debug.LogData`` because "object message" and "Object context" are may be null.

### To Keep Consle Click Action

In standard Unity editor, we can jump to the call-point of ``Debug.Log`` in source code with console click.

However if make a wrapper function of these, the call-point is set inside the wrapper.

Unfortunately, there are no way to solve this problem from any code. 
To avoid this problem, we have to make a ".dll".

This is the reason why assets directory doesn't include any source code of ``XDebug``.

- Reference
    - https://answers.unity.com/questions/176422/debug-wrapper-class.html
    - https://answers.unity.com/questions/1226230/how-to-properly-call-debuglogstring-in-a-custom-lo.html
