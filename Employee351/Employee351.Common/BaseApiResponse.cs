using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee351.Common
{
    public class BaseApiResponse
    {
        public BaseApiResponse()
        {

        }

        public bool Success { get; set; }

        public string Message { get; set; }
    }
    public class ApiResponse<T> : BaseApiResponse
    {
        public virtual IList<T> Data { get; set; }
    }

    public class ApiPostResponse<T> : BaseApiResponse
    {
        public virtual T Data { get; set; }

    }
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public long TAID { get; set; }
        public string StringData { get; set; }
        public string StringDataCompress { get; set; }

    }
}
