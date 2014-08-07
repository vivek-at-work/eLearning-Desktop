using System;
using System.IO;
using System.Xml.Serialization;
namespace Coneixement.Infrastructure.Helpers
{
    public class SerializationHelper<T>
    {
        public static bool Serialize<t>(T value , String filename)
        {
            if (value == null)
            {
                return false;
            }
            try
            {
                XmlSerializer _xmlserializer = new XmlSerializer(typeof(T));
                Stream stream = new FileStream(filename , FileMode.Create);
                _xmlserializer.Serialize(stream , value);
                stream.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static T Deserialize<t>(String filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return default(T);
            }
            try
            {
                XmlSerializer _xmlSerializer = new XmlSerializer(typeof(T));
                Stream stream = new FileStream(filename , FileMode.Open , FileAccess.Read);
                var result = (T)_xmlSerializer.Deserialize(stream);
                stream.Close();
                return result;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
