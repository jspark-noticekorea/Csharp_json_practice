# Csharp_json_practice
This is about how to use Nuget Package NewtonSoft.Json in C# (.Net, .Net Framework)

## Nuget Download
Download the package `NewtonSoft.Json`

## Serialize & dump on a file
```
string serial = JsonConvert.SerializeObject(obj, Formatting.Indented);
File.WriteAllText(filePath, serial);
```

## Deserialize & read from
```
T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath)); 
```

## Ignore specific field
write `[JsonIgnore]` on the declaration of the field.
```
[JsonIgnore]
public int mySecretField;
```

## Enum
write `[JsonConverter(typeof(StringEnumConverter))]` on the declaration of the enum field
(not declaration of the enum)
```
[JsonConverter(typeof(StringEnumConverter))]
public myEnum myEnumField = myEnum.HELLO;
```

## Get UnIgnored Field
```
foreach(var fileInfo in typeof(MyClass).GetFields().Where(field => !Attribute.IsDefine(field, typeof(JsonIgnoreAttribute))))
    {...}
```
