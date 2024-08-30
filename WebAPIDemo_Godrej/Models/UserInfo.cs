using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIDemo_Godrej.Models
{
    [Table("UserInfo")]
    public class UserInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(TypeName = "varchar(50)")]
        public string Username { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Password { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Role { get; set; }
    }
}
