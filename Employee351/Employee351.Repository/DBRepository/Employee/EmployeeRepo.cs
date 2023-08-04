using Employee351.Models;
using Employee351.Models.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee351.Repository.DBRepository.Employee
{
    public class EmployeeRepo:BaseRepository, IEmployeeRepo
    {
        IConfiguration _configuration;
        public EmployeeRepo(IConfiguration configuration, IOptions<DataConfig> dataConfig) : base(dataConfig)
        {
            _configuration = configuration;
        }
        public async Task<int> CreateEmployee(AddEmployeeModel model)
        {
            var parameters = new { Name=model.Name,Email=model.Email,Gender=model.Gender,Contact=model.Contact,Salary=model.Salary,DepartmentId=model.Department,Skills=model.Skills,ProfilePic=model.ProfilePic};
            var addEmp = await QueryFirstOrDefaultAsync<int>("[dbo].[sp_AddOrUpdateEmployee]", parameters, commandType: System.Data.CommandType.StoredProcedure);
            foreach (var item in model.Addresses)
            {
                var param = new { Address = item, EmpId = addEmp };
                await QueryFirstOrDefaultAsync<int>("[dbo].[sp_AddUpdateAddress]", param, commandType: System.Data.CommandType.StoredProcedure);
            }
            return addEmp;
        }
        public async Task<int> UpdateEmployee(AddEmployeeModel model)
        {
            var parameters = new {Id=model.Id, Name=model.Name,Email=model.Email,Gender=model.Gender,Contact=model.Contact,Salary=model.Salary,DepartmentId=model.Department,Skills=model.Skills,ProfilePic=model.ProfilePic};
            var addEmp= await QueryFirstOrDefaultAsync<int>("[dbo].[sp_AddOrUpdateEmployee]", parameters, commandType: System.Data.CommandType.StoredProcedure);
            foreach (var item in model.Addresses)
            {
                var param = new { Address = item, EmpId = addEmp };
                await QueryFirstOrDefaultAsync<int>("[dbo].[sp_AddUpdateAddress]", param, commandType: System.Data.CommandType.StoredProcedure);
            }
            return addEmp;
        }

        public async Task<EmployeeModel> GetEmployeeDetails(int id)
        {
            var parameters = new { Id=id};
            return await QueryFirstOrDefaultAsync<EmployeeModel>("[dbo].[sp_GetEmployeeDetails]", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<List<EmployeeModel>> GetAllEmployeeDetails(int? pageSize, int? pageIndex, string? search, string? sortColumn, string? sortOrder)
        {
            var parameters=new { pageSize=pageSize, pageIndex=pageIndex,search=search, sortColumn = sortColumn, sortOrder = sortOrder };
            IEnumerable<EmployeeModel> result = await QueryAsync<EmployeeModel>("[dbo].[sp_GetEmployeeDetails]", parameters, commandType: System.Data.CommandType.StoredProcedure);
            return result.ToList();
        }
        public async Task<int> DeleteEmployee(int id)
        {
            var parameters = new { Id = id };
            return await QueryFirstOrDefaultAsync<int>("[dbo].[sp_DeleteEmployee]", parameters, commandType: System.Data.CommandType.StoredProcedure);  
        }
    }
}
