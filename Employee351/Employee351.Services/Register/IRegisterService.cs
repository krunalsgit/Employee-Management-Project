using Employee351.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee351.Services.Register
{
    public interface IRegisterService
    {
        Task<int> Register(RegisterModel registerData);
        Task<int> VerifyEmail(string email);
    }
}
