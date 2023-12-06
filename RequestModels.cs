using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attendanceApiIntergration.RequestModel
{
    public class RequestModels
    {
        public int IndexNo { get; set; }
        public string UserId { get; set; }
        public int DID { get; set; }    
        public string DeviceId { get; set; }
        public bool IsPublished { get; set; }
    }
}
