using Employee351.Models;
using Employee351.Models.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee351.Repository.DBRepository.Register
{
    public class RegisterRepository:BaseRepository,IRegisterRepository
    {
        IConfiguration _configuration;
        public RegisterRepository(IConfiguration configuration, IOptions<DataConfig> dataConfig) : base(dataConfig) 
        { 
            _configuration = configuration;
        }
        public async Task<int> Register(RegisterModel registerData)
        {
            var parameters=new {Name=registerData.Name,Email=registerData.Email,Password=registerData.Password};
            var result = await QueryFirstOrDefaultAsync<int>("[dbo].[sp_RegisterEmployee]", parameters,commandType:System.Data.CommandType.StoredProcedure);
            return result;
        }
        public async Task<int> VerifyEmail(string email)
        {
            var parameters=new {Email=email};
            var result = await QueryFirstOrDefaultAsync<int>("[dbo].[sp_isVariables]", parameters,commandType:System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}
