using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public  class World:BaseEntity
    {
        public string Title {get;set;}
        public string Subtitle { get;set;}
        public string? ImageUrl { get;set;}
        [NotMapped]
        public IFormFile? ImageFile { get;set;}
    }
}
