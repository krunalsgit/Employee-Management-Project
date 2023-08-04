using Dapper;
using Employee351.Models;
using Employee351.Models.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee351.Repository.DBRepository.Login
{
    public class LoginRepository: BaseRepository,ILoginRepository
    {
        IConfiguration _configuration;
        public LoginRepository(IConfiguration configuration, IOptions<DataConfig> dataConfig) : base(dataConfig) { 
            _configuration = configuration;
        }

        public async Task<int> Login(LoginModel loginData)
        {
            var parameters=new {Email=loginData.Email,Password=loginData.Password};
            return await QueryFirstOrDefaultAsync<int>("[dbo].[sp_LoginEmployee351]", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
