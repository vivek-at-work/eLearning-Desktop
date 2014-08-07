using System.Collections.Generic;
namespace Coneixement.Infrastructure.Modals
{
   public class Concept
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string DataPath { get; set; }
        public string IconPath { get; set; }
        public bool IsSelecetd { get; set; }
        public ContentType Type { get; set; }
        public List<Topic> Topics { get; set; }
    }
   public enum ContentType { 
   Image,
   PDF,
   Video ,
       Question
   }
   public class Topic
   {
       public int ID { get; set; }
       public string Title { get; set; }
       public string DataPath { get; set; }
       public string IconPath { get; set; }
       public bool IsSelecetd { get; set; }
       public ContentType Type { get; set; }
   }
}
