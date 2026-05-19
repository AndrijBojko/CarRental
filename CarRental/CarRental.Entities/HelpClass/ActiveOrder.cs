using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entities.HelpClass
{
    public class ActiveOrder
    {
        public string ManagerLName { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public string CustomerLName { get; set; }
        public string CustomerFName { get; set; }
        public DateTime FinishDateTime { get; set; }
    }
}
