using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayNGo.FraceMarteja.DAL
{
    public class DbResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }
    }
}
