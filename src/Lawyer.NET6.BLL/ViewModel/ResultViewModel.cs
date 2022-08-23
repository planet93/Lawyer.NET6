using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawyer.NET6.BLL.ViewModel
{
    public class ResultViewModel
    {
        public bool Error { get; set; } = false;
        public string? ErrorMessage { get; set; }
    }
}
