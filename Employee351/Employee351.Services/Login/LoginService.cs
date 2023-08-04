
using Employee351.Models.Models;
using Employee351.Repository.DBRepository.Login;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee351.Services.Login
{
    public class LoginService: ILoginService
    {
        private readonly ILoginRepository _loginRepo;
        public LoginService(ILoginRepository login){ 
            _loginRepo = login;
        }
        public async Task<int> Login(LoginModel loginData)
        {
            var result = await _loginRepo.Login(loginData);
            return result;
        }
    }
}
