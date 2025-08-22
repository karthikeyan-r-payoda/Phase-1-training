namespace MiniProject2.DTO
{
    public class CustomerDto
    {
        public int CustId { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }
        public List<AccountDto> BankAccounts { get; set; }
    }
}
