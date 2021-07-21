using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.WebApi
{
    public class ApiResult<T>
    {
        public T Data { get; set; }

        public string Message { get; set; }
    }
}
