using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Payroll
{
    public class Payroll
    {
        public int NetPay { get; internal set; }
        public int taxablepay { get; internal set; }
        public int incometax { get; internal set; }
        public int deductions { get; internal set; }
        public string payrollId { get; internal set; }
        public int basicPay { get; internal set; }
    }
}
