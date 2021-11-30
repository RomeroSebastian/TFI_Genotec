using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Logger.Domain
{
    public partial class Log
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public severityType Severity { get; set; }
        public logType LogType { get; set; }
    }
}
