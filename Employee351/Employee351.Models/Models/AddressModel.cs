using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee351.Models.Models
{
    public class AddressModel
    {
        public int AddressId { get; set; }
        public string Address { get; set; }
        public int EmployeeId { get; set; }
    }
}
