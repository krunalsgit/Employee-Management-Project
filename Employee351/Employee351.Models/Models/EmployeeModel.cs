using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee351.Models.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Contact { get; set; }
        public decimal Salary { get; set; }
        public string Department { get; set; }
        public string Skills { get; set; }
        public string Address { get; set; }
        public string ProfilePic { get; set; }
    }

    public class AddEmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Contact { get; set; }
        public decimal Salary { get; set; }
        public string Department { get; set; }
        public string Skills { get; set; }
        public List<string> Addresses { get; set; }
        public string ProfilePic { get; set; }
        public IFormFile Image { get; set; }

    }
}
