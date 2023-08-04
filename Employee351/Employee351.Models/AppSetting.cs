using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee351.Models
{
    public class AppSetting
    {
        public string JWT_Secret { get; set; }
        public object MailSettings { get; set; }
    }

}
