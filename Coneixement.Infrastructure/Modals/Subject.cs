using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace Coneixement.Infrastructure.Modals
{
     [Serializable]
     [XmlRoot("Subject")]
   public class Subject
    {
       public int ID { get; set; }
       public string Title { get; set; }
       public string SubjectDataPath { get; set; }
       public DateTime CreatedON { get; set; }
       public User CreatedBy { get; set; }
       public Boolean IsSelected { get; set; }
       public string IconPath { get; set; }
       public List<Concept> Concepts { get; set; }
    }
}
