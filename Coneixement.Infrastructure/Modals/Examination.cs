using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Coneixement.Infrastructure.Modals
{
    [Serializable]
   public class ExaminationType
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string CategoryDataPath { get; set; }
        public DateTime CreatedON { get; set; }
        public User CreatedBy { get; set; }
        public string IconPath { get; set; }
        public List<Examination> Examinations { get; set; }
        public Boolean IsSelected { get; set; }
    }
    [Serializable]
    public class Examination
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string CategoryDataPath { get; set; }
        public DateTime CreatedON { get; set; }
        public User CreatedBy { get; set; }
        public string IconPath { get; set; }
        public Boolean IsSelected { get; set; }
    }
}
