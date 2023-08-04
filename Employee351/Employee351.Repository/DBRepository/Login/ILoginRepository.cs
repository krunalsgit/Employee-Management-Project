using Employee351.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee351.Repository.DBRepository.Login
{
    public interface ILoginRepository
    {
        Task<int> Login(LoginModel loginData);
    }
}
