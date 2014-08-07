using System.Collections.Generic;
using System.Management;
namespace Coneixement.Infrastructure.Helpers.SystemInformationHelper
{
    public class MotherBoardInfo
    {
        public Dictionary<string , string> Details;
        public MotherBoardInfo()
        {
            Details = new Dictionary<string , string>();
            ManagementObjectSearcher searcher =
                              new ManagementObjectSearcher("SELECT Product, SerialNumber FROM Win32_BaseBoard");
            ManagementObjectCollection information = searcher.Get();
            foreach (ManagementObject obj in information)
            {
                foreach (PropertyData data in obj.Properties)
                    Details.Add(data.Name , data.Value.ToString());
            }
            searcher.Dispose();
        }
    }
}
