using Employee351.Repository.DBRepository.Employee;
using Employee351.Repository.DBRepository.Login;
using Employee351.Repository.DBRepository.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee351.Repository
{
    public static class DataRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            Dictionary<Type, Type> types = new Dictionary<Type, Type>
            {
                {typeof(ILoginRepository),typeof(LoginRepository) },
                {typeof(IRegisterRepository),typeof(RegisterRepository) },
                {typeof(IEmployeeRepo),typeof(EmployeeRepo) },
            };
            return types;
        }
    }
}
