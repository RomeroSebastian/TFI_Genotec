using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions.Domain
{
    public class BLLException : Exception
    {
        public bool IsBusinessException { get; private set; }
        
        public BLLException(bool IsBusinessException = false)
        {
            this.IsBusinessException = IsBusinessException;
        }

        public BLLException(String message, bool IsBusinessException = false) : base(message)
        {
            this.IsBusinessException = IsBusinessException;
        }

        public BLLException(Exception ex, bool IsBusinessException = false) : base("BLL Exception", ex)
        {
            this.IsBusinessException = IsBusinessException;
        }

        public BLLException(String message, Exception ex, bool IsBusinessException = false) : base(message, ex)
        {
            this.IsBusinessException = IsBusinessException;
        }
    }
}
