using Employee351.Common;
using Employee351.Models;
using Employee351.Models.Models;
using Employee351.Services.Employee;
using Employee351.Services.Login;
using Employee351.Services.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Employee351.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("GetEmployeeDetailById")]
        public async Task<ApiPostResponse<EmployeeModel>> GetEmployeeDetails(int id)
        {
            ApiPostResponse<EmployeeModel> response = new ApiPostResponse<EmployeeModel>();
            try
            {
                var result = await _employeeService.GetEmployeeDetails(id);
                if (result != null)
                {
                    response.Data = new EmployeeModel();
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

        [HttpPost("AddEmployee")]
        public async Task<ApiPostResponse<int>> CreateEmployee([FromForm] AddEmployeeModel model)
        {
            try
            {
                ApiPostResponse<int> response = new ApiPostResponse<int>();
                var file = model.Image;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Asset/Images");
                string destinationPath = Path.Combine(path, file.FileName);
                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                model.ProfilePic = file.FileName;
                //var file = Request.Form.Files[0];

                //string path = Path.Combine(Directory.GetCurrentDirectory(), "Asset/Images");
                //string destinationPath = Path.Combine(path, file.FileName);
                //using (var stream = new FileStream(destinationPath, FileMode.Create))
                //{
                //    await file.CopyToAsync(stream);
                //}

                //var model = new RegisterModel()
                //{
                //    FirstName = Request.Form["FirstName"],
                //    LastName = Request.Form["LastName"],
                //    Email = Request.Form["Email"],
                //    Gender = Request.Form["Gender"],
                //    Contact = Request.Form["Contact"],
                //    DateOfBirth = Convert.ToDateTime(Request.Form["DateOfBirth"]),
                //    Address = Request.Form["Address"],
                //    DepartmentId = Convert.ToInt32(Request.Form["Department"]),
                //    CountryId = Convert.ToInt32(Request.Form["Country"]),
                //    StateId = Convert.ToInt32(Request.Form["State"]),
                //    CityId = Convert.ToInt32(Request.Form["City"]),
                //    ProfilePic = file.FileName,
                //    Password = Request.Form["Password"]
                //};
                var result = await _employeeService.CreateEmployee(model);
                if (result > 0)
                {
                    response.Data = result;
                    response.Success = true;
                    response.Message = "User Created Successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Unsuccessfully";
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("UpdateEmployee")]
        public async Task<ApiPostResponse<int>> UpdateEmployee([FromForm] AddEmployeeModel model)
        {
            try
            {
                ApiPostResponse<int> response = new ApiPostResponse<int>();
                var file = model.Image;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Asset/Images");
                string destinationPath = Path.Combine(path, file.FileName);
                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                model.ProfilePic = file.FileName;
                //var file = Request.Form.Files[0];

                //string path = Path.Combine(Directory.GetCurrentDirectory(), "Asset/Images");
                //string destinationPath = Path.Combine(path, file.FileName);
                //using (var stream = new FileStream(destinationPath, FileMode.Create))
                //{
                //    await file.CopyToAsync(stream);
                //}

                //var model = new RegisterModel()
                //{
                //    FirstName = Request.Form["FirstName"],
                //    LastName = Request.Form["LastName"],
                //    Email = Request.Form["Email"],
                //    Gender = Request.Form["Gender"],
                //    Contact = Request.Form["Contact"],
                //    DateOfBirth = Convert.ToDateTime(Request.Form["DateOfBirth"]),
                //    Address = Request.Form["Address"],
                //    DepartmentId = Convert.ToInt32(Request.Form["Department"]),
                //    CountryId = Convert.ToInt32(Request.Form["Country"]),
                //    StateId = Convert.ToInt32(Request.Form["State"]),
                //    CityId = Convert.ToInt32(Request.Form["City"]),
                //    ProfilePic = file.FileName,
                //    Password = Request.Form["Password"]
                //};
                var result = await _employeeService.UpdateEmployee(model);
                if (result > 0)
                {
                    response.Data = result;
                    response.Success = true;
                    response.Message = "User Updated Successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Unsuccessfully";
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
