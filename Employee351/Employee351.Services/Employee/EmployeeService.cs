using Employee351.Models.Models;
using Employee351.Repository.DBRepository.Employee;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee351.Services.Employee
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepo _employeeRepo;
        public EmployeeService(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public async Task<int> CreateEmployee(AddEmployeeModel model)
        {
            return await _employeeRepo.CreateEmployee(model);
        }
        public async Task<int> UpdateEmployee(AddEmployeeModel model)
        {
            return await _employeeRepo.UpdateEmployee(model);
        }
        public async Task<EmployeeModel> GetEmployeeDetails(int id)
        {
            return await _employeeRepo.GetEmployeeDetails(id);
        }
        public async Task<List<EmployeeModel>> GetAllEmployeeDetails(int? pageSize, int? pageIndex, string? search, string? sortColumn, string? sortOrder)
        {
            return await _employeeRepo.GetAllEmployeeDetails(pageSize, pageIndex, search, sortColumn, sortOrder);
        }
        public async Task<int> DeleteEmployee(int id)
        {
            return await _employeeRepo.DeleteEmployee(id);
        }
    }
}
