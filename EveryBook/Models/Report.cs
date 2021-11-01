using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EveryBook.Models
{
    public class Report
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [DisplayName("Create Time")]
        public DateTime CreateTime { get; set; }

        [Required]
        [DisplayName("Start Period")]
        public DateTime StartPeriod { get; set; }

        [Required]
        [DisplayName("End Period")]
        public DateTime EndPeriod { get; set; }

        [Required]
        public float Total { get; set; }
    }
}
