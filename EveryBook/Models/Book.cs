using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EveryBook.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string PictureUrl { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Author { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}.")]
        [DefaultValue(0)]
        [DisplayName("Original Price")]
        public float OriginalPrice { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1} and grater than the \"Original Price\".")]
        [DefaultValue(0)]
        public float Price { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        [DisplayName("Available Quantity")]
        [DefaultValue(0)]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}.")]
        public int AvailableQuantity { get; set; } = 1;

        [DisplayName("Genre")]
        [ForeignKey("Genre")]
        public long GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
