using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace Coneixement.Infrastructure.Modals
{
    [Serializable]
    public class Category
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string CategoryDataPath { get; set; }
        public DateTime CreatedON { get; set; }
        public User CreatedBy { get; set; }
        public string IconPath { get; set; }
        public Boolean IsSelected { get; set; }
       [XmlArray("Subjects"), XmlArrayItem("Subject")]
        public List<Subject> Subjects { get; set; }
       public List<Category> SubCategories { get; set; }
       public List<ExaminationType> RelatedExaminationsTypes { get; set; }
    }
}
