using Employee351.Common;
using Employee351.Models.Models;
using Employee351.Services.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee351.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public AdminController(IEmployeeService employeeService) { 
            _employeeService = employeeService;
        }

        [HttpGet("GetEmployeeDetails")]
        public async Task<ApiResponse<EmployeeModel>> GetEmployeeDetails(int? pageSize, int? pageIndex, string? search, string? sortColumn, string? sortOrder)
        {
            try
            {
                ApiResponse<EmployeeModel> response = new ApiResponse<EmployeeModel>() { Data = new List<EmployeeModel>() };
                var result = await _employeeService.GetAllEmployeeDetails(pageSize, pageIndex, search, sortColumn, sortOrder);
                if (result.Count > 0)
                {
                    response.Data = result;
                    response.Success = true;
                    response.Message = "Success";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Not Found";
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("DeleteEmployee")]
        public async Task<ApiPostResponse<int>> DeleteStudent(int id)
        {
            try
            {
                ApiPostResponse<int> response = new ApiPostResponse<int>();
                var result = await _employeeService.DeleteEmployee(id);
                if (result != 1)
                {
                    response.Success = false;
                    response.Message = "No Data Found";
                }
                else
                {
                    response.Success = true;
                    response.Message = "Success";
                    response.Data = result;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
