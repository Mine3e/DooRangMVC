using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
    public class FileSizeException : Exception
    {
        public string propertyname {  get; set; }
        public FileSizeException(string propertyName,string? message) : base(message)
        {
            propertyname = propertyName;
        }
    }
}
