using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.MacExpress.Presentation.Core.ViewModel
{
    public class StatusResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public T Data { get; set; }
        public List<T> Result { get; set; }
    }
}
