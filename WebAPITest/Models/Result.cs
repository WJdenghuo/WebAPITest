using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPITest.Models
{
    public class Result<T>
    {
        public string R { get; set; } //0:表示失败；1:表示成功
        public IEnumerable<T> Data { get; set; }
        public string M { get; set; }//message
        public int Total { get; set; }//条数
    }

    public class Result
    {
        public string R { get; set; }
        public object Data { get; set; }
        public string M { get; set; }
        public int Total { get; set; }
    }
}