using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProject2.Models
{
    public class BankModel
    {
        [Key]
        public  int AccountNum { get; set; }
        public int AccountBalance { get; set; }
        public DateOnly CreatedDate { get; set; }
        public int CustId { get; set; }

        [ForeignKey("CustId")]
        public CustomerModel Customer { get; set; }
    }
}
