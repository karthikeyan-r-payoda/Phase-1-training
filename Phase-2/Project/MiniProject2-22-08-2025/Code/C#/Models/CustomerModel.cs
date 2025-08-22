using System.ComponentModel.DataAnnotations;

namespace MiniProject2.Models
{
    public class CustomerModel
    {
        [Key]
        public int CustId { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
        public ICollection<BankModel> BankAccounts { get; set; }

    }
}
