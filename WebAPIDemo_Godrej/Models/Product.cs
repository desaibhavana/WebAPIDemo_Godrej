using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPIDemo_Godrej.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }

        [Column(TypeName ="varchar(20)")]
        public string ProductName { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        public double? Price { get; set; }

        [Column(TypeName = "numeric(5)")]
        public int? Qty { get; set; }

    }
}
