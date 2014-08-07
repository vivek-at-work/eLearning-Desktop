using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 namespace Coneixement.Infrastructure.Modals
{
    [Serializable]
    public class LicencingHistory
    {
        public String LicenceKey { get; set; }
        public String MotherBoardID { get; set; }
        public String LicencingUrl { get; set; }
        public DateTime LicencedOn { get; set; }
        public String ServiceResponse { get; set; }
        public bool IsValidLicence { get; set; }
        public User User { get; set; }
    }
}
