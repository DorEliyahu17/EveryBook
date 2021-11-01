using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EveryBook.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        [DisplayName("Purchase Time")]
        public DateTime PurchaseTime { get; set; }

        [ForeignKey("ExtendUser")]
        public string ExtendUserId { get; set; }

        public virtual ExtendUser ExtendUser { get; set; }
        /*
        [ForeignKey("Store")]
        public long StoreId { get; set; }

        public virtual Store Store { get; set; }
        */
    }
}
