using Employee351.Services.Employee;
using Employee351.Services.Login;
using Employee351.Services.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee351.Services
{
    public static class ServiceRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            Dictionary<Type, Type> dictionary = new Dictionary<Type, Type>
            {
                { typeof(ILoginService), typeof(LoginService) },
                { typeof(IRegisterService), typeof(RegisterService) },
                { typeof(IEmployeeService), typeof(EmployeeService) }
            };
            return dictionary;
        }
    }
}
