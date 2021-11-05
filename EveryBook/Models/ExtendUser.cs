using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EveryBook.Models
{
    public class ExtendUser : IdentityUser
    {
        public string Name { get; set; }
        //public bool IsAdmin { get; set; }
    }
}
