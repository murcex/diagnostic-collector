# Implements

## Purpose
Reuseable library for: AES Encryption, Config Deserializer and Logging.

## Components
- implements-library
    - Library, sample config file

---

# Deserializer
## 1. Add references

```csharp
using Implements;
```

## 2. Example "Global.cs"

Example Global.cs

```csharp
static Global()
{
    GetConfigs();
    SetConfig();
}

// backing fields
string _example;
Dictionary<string, List<KeyValuePair<string, string>>> _myConfigGroup;

// collect config dictionary by tag
GetConfigs()
{
    using (Deserializer deserilaizer = new Deserializer())
    { 
        _myConfigGroup = deserilaizer.GetTag("myConfig"); 
    }
}

// sort config dictionary by kvp into backing fields
SetConfig()
private static void SetKFlowConfig()
{
  foreach (var kvp in MyConfigGroup)
  {
    switch (kvp.Key.ToString())
    {
      case "example":
       _example = kvp.Value;
       break;
    }
  }
}

// public readonly properties, calling backing fields -- this is used by your service
string Example { get { return _example; } }
Dictionary<string, List<KeyValuePair<string, string>>> MyConfigGroup{ get { return _myConfigGroup; } }
```

## 3. Inside a using statement, deserializer the config.ini file. Use the Get methods to load the contents. 

**Please note, the __execute__ method will take options for logging and validation. In this example, they're both skipping using the default values of false.**

```csharp
using (Deserializer deserializer = new Deserializer())
{
    deserializer.Execute(@"C:\Temp\MyConfig\MyConfigFile.txt");

    myCollection = deserializer.GetCollection();
    myTag = deserializer.GetTag("tagName");
    myValue = deserializer.GetValue("tagName", "keyName");
    myValues = deserializer.GetValues("tagName", "keyName");
}
```

## Supported Extraction Methods

---

### 1. Get Collection

```csharp
Dictionary<string, List<KeyValuePair<string, string>>> myCollection = deserializer.GetCollection();
// myCollection == tag1 with kvp, tag2 with kvp -- the whole config.ini file ..
```

Config Source:

```ini
[tag1]
key1=value
key2=value
key2=value

[tag2]
key1=value
key2=value
key3=value
```

---

### 2. Get Tag

```csharp
List<KeyValuePair<string, string>> myTag = deserializer.GetTag("tagName");
// myTag == [appname="first"],[collection="logs"] ..
```

Config Source:

```ini
[app_first]
appname=first
collection=logs
type=json
retention=30
index=serviceid
index=status
```

---

### 3. Get Value

```csharp
string myValue = deserializer.GetValue("app_first", "appname");
// myValue == "first"
```

Config Source:

```ini
; first app
[app_first]
appname=first
collection=logs
type=json
retention=30
index=serviceid
index=status
```

---

### 4. Get Values

```csharp
List<string> myValues = deserializer.GetValues("apps_index", "app");
// myValues == "first","second","third"
```

Config Source:

```ini
; all apps
[apps_index]
app=first
app=second
app=third
```