using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee351.Models.Models
{
    public class RegisterModel
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        //[Required]
        //public string? Contact { get; set; }
        //[Required]
        //public DateTime? DateOfBirth { get; set; }
        //[Required]
        //public string? Gender { get; set; }
        //[Required]
        //public string? Address { get; set; }
        //[Required]
        //public int? DepartmentId { get; set; }
        //[Required]
        //public int? CountryId { get; set; }
        //[Required]
        //public int? StateId { get; set; }
        //[Required]
        //public int? CityId { get; set; }
        //public string? ProfilePic { get; set; }
        //[NotMapped]
        //public IFormFile Image { get; set; }
    }
}
