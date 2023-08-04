using Employee351.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee351.Repository.DBRepository.Employee
{
    public interface IEmployeeRepo
    {
        Task<int> CreateEmployee(AddEmployeeModel model);
        Task<int> UpdateEmployee(AddEmployeeModel model);
        Task<EmployeeModel> GetEmployeeDetails(int id);
        Task<List<EmployeeModel>> GetAllEmployeeDetails(int? pageSize, int? pageIndex, string? search, string? sortColumn, string? sortOrder);
        Task<int> DeleteEmployee(int id);
        
    }
}
