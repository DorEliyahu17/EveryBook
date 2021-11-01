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

        [ForeignKey("DistributionUnit")]
        [DisplayName("Favorite Distribution Unit")]
        public long DistributionUnitId { get; set; }

        public virtual DistributionUnit DistributionUnit { get; set; }
    }
}
