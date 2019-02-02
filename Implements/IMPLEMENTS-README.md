# Implements

## Purpose
Reuseable library for: AES Encryption, Config Deserializer and Logging.

## Components
- implements-library
    - Library, sample config file

---

# How to use the Deserializer
## 1. Add references

```csharp
using Implements;
```

## 2. Create Config object to import data into, and then query

```csharp
Config cfg = new Config();
```

## 3. Inside a using statement, deserializer the config.ini file, loading the content into the config. Please note, the __execute__ method will take options for logging and validation. In this example, they're both skipping using the default values of false.

```csharp
using (Deserializer deserializer = new Deserializer())
{
    cfg.Collection = deserializer.Execute(@"C:\Temp\MyConfig\MyConfigFile.txt");
}
```

## 4. Use the Config object to extract the contents. Two methods on extraction: (a) single value and (b) list of values.

### Get Value

```csharp
// test1 == "first" (string)
var test1 = cfg.GetValue("app_first", "appname");
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

### Get Values

```csharp
// var test3 = "first","second","third" (List<string>)
var test3 = cfg.GetValues("apps_index", "app");
```

Config Source:

```ini
; all apps
[apps_index]
app=first
app=second
app=third
```