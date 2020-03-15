// <Service>Manager.cs

public static bool Initialize(List<KeyValuePair<string, string>> appConfig, List<KeyValuePair<string, string>> kirokuConfig)
{
    Configuration.SetConfigs(appConfig, kirokuConfig);
}

Execute()
{

}

// Configuration.cs

public static bool SetConfigs(List<KeyValuePair<string, string>> appConfig, List<KeyValuePair<string, string>> kirokuConfig)
{
    _kcopyTagList = appConfig;

    _kirokuTagList = kirokuConfig;

    if (SetAppConfig())
    {
        return SetKirokuConfig();
    }

    return false;
}

private static bool SetAppConfig()
{
    foreach (var kvp in AppTagList)
    {
        switch (kvp.Key.ToString())
        {
            case "item":
                _item_ = kvp.Value;
                break;
            default:
                { }
                break;
        }
    }

    return true;
}

private static bool SetKirokuConfig()
{
    KManager.Configure(KirokuTagList);

    return true;
}

// Configs
private static List<KeyValuePair<string, string>> _kcopyTagList;
private static List<KeyValuePair<string, string>> _kirokuTagList;

private static List<KeyValuePair<string, string>> KCopyTagList { get { return _kcopyTagList; } }
public static List<KeyValuePair<string, string>> KirokuTagList { get { return _kirokuTagList; } }