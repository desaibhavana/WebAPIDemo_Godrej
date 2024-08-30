using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCoreApp_Client_Godrej.Models
{
    public class Product
    {
        public int ProductId { get; set; }
       
        public string ProductName { get; set; }

        public double? Price { get; set; }
        
        public int? Qty { get; set; }
    }
}
