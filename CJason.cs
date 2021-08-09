using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Csharp_json_practice
{

    class CJason
    {
        public void SerializeTest()
        {
            //var p = new Person { Id = 1, Name = "Alex" };
            var p = new Person(1, "Alex");
            string jsonString = JsonConvert.SerializeObject(p);
            Debug.WriteLine(jsonString);

            Person pobj = JsonConvert.DeserializeObject<Person>(jsonString);
            Debug.WriteLine(pobj.ToString());
        }

        public void WriteTest()
        {
            Person p = new Person(2, "Elizabeth");
            string serial = JsonConvert.SerializeObject(p, Formatting.Indented);
            //serial = JValue.Parse(serial).ToString(Formatting.Indented);
            File.WriteAllText("output.json", serial);

        }
        public void WriteTest2()
        {
            Person p = new Person(2, "Elizabeth");
            string serial = JsonConvert.SerializeObject(p);
            using (StreamWriter file = File.CreateText("output2.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, p);
            }

        }

        public void ReadTest()
        {
            Person p1 = JsonConvert.DeserializeObject<Person>(File.ReadAllText("output2.json"));
            Debug.WriteLine(p1.ToString());
        }

        public void SettingsWriteTest()
        {
            CSetup settings = new CSetup(0);
            string jsonVal = JsonConvert.SerializeObject(settings);
           jsonVal = JValue.Parse(jsonVal).ToString(Formatting.Indented);
            File.WriteAllText("setting.json", jsonVal);

        }
        public void SettingsReadTest()
        {
            CSetup settings = JsonConvert.DeserializeObject<CSetup>(File.ReadAllText("setting.json"));
            Type setType = settings.GetType();
            foreach(var info in setType.GetFields())
                Debug.WriteLine(info.Name +" "+info.GetValue(settings));
            foreach(var x in settings.lst)
            Debug.WriteLine(x                );
        }

        public void CopyTest<T>(T target, string filename)
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(filename));
            foreach(var field in typeof(T).GetFields().Where(f => !Attribute.IsDefined(f, typeof(JsonIgnoreAttribute))))
                field.SetValue(target, field.GetValue(data));

        }
    }

    class Person
    {
        public int Id;
        public string Name = "";
        public int[] arr = { 1,2,3,4,5,6,7,8};
        public Person(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return string.Format("ID: {0}, Name: {1}, arr: {2}", Id, Name,arr.Length);
        }
    }

    public class CSettingJson
    {
        public float pldCur = 130.0F;
        public float pldTmp = 25.0F;
        public float sldCur = 300f;
        public float sldTmp = 35.0F;
        public uint pulseWidth = 4u;


        public uint offsetStokes = 9000u;
        public uint offsetAStokes = 23800u;
        public uint ApdbStokes = 47u;
        public uint ApdbAStokes = 47u;
        public uint detGain = 2u; //Tia Gain
        public float detTmp1 = 35.0F;
        public float detTmp2 = 35.0F;

        public int[] sequences = { 1, 0, 0, 0, 0, 0, 0, 0 };

        public float gain = 1.15f;
        public float offset = 0f;

        public uint distance = 2;

        public int refBeginMeter = 110;
        public int refEndMeter = 290;
        public int dataBeginMeter = 300;
        public int dataEndMeter = 15000;

        public bool offsetFbkEn = false;
        public bool gainFbkEn = false;
        public bool movingAvgEn = false;
        public bool tempStableEn = false;

    }

    public enum myEnum {HELLO=0, WORLD=1, HAPPY=2, CODING=3 };
    public class MyClass
    {
        public int a, b;
    }
    public class CSetup
    {        
        public List<int> lst = new List<int>();
        [JsonIgnore]
       public  MyClass myclass1 = new MyClass();
        [JsonConverter(typeof(StringEnumConverter))]
        public myEnum myenum_ = myEnum.HAPPY;
        public CSetup() { }
        public CSetup(int x)
        {
            lst.Add(0);
            lst.Add(10);
            lst.Add(100);
        }
    }

}
