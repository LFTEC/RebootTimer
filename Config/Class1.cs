using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;

namespace Config
{
    public class Data
    {
        public DateTime StartTime { get; set; }

        public string Type { get; set; }

        public string Span { get; set; }
    }

    public static class DataSerializor
    {
        public static Data GetData()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (File.Exists(path + "\\config.xml"))
            {
                Stream fs = new FileStream(path + "\\config.xml", FileMode.Open, FileAccess.Read);
                XmlSerializer serializer = new XmlSerializer(typeof(Data));
                Data data = (Data)serializer.Deserialize(fs);
                fs.Close();
                fs.Dispose();
                return data;
            }
            else
                return null;
        }

        public static void SetData(Data data)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Stream fs = new FileStream(path + "\\config.xml", FileMode.Create, FileAccess.Write);
            XmlSerializer serializer = new XmlSerializer(typeof(Data));
            serializer.Serialize(fs, data);
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }
    }
}
