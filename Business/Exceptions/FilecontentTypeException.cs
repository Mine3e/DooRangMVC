using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
    public class FilecontentTypeException : Exception
    {
        public  string PropertyName {get ; set;}
        public FilecontentTypeException(string propertyname,string? message) : base(message)
        {
            PropertyName = propertyname;
        }
    }
}
