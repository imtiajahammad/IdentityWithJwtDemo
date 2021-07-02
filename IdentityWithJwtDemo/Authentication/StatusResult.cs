using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityWithJwtDemo.Authentication
{
    public class StatusResult<T>
    {
        public StatusResult()
        {
            Status = ResponseStatus.Failed;
            Message = String.Empty;
        }
        public ResponseStatus Status { get;set; }
        public string Message { get; set; }
        public T Result { get;set; }
    }
    public enum ResponseStatus{
        Failed=0,
        Success=1,
        NotFound=2

    }
}
