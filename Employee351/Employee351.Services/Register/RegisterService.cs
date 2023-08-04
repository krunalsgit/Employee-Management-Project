
using Employee351.Models.Models;
using Employee351.Repository.DBRepository.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee351.Services.Register
{
    public class RegisterService: IRegisterService
    {
        IRegisterRepository _RegisterRepo;
        public RegisterService(IRegisterRepository repository) {
            _RegisterRepo = repository;
        }

        public async Task<int> Register(RegisterModel registerData)
        {
            return await _RegisterRepo.Register(registerData);
        }
        public async Task<int> VerifyEmail(string email)
        {
            return await _RegisterRepo.VerifyEmail(email);
        }
    }
}
