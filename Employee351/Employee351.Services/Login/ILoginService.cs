﻿using Employee351.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee351.Services.Login
{
    public interface ILoginService
    {
        Task<int> Login(LoginModel loginData);
    }
}
