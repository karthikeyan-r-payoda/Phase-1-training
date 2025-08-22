using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniProject2.Context;
using MiniProject2.DTO;
using MiniProject2.Models;

namespace MiniProject2.Controllers
{
    public class BankController : Controller
    {

        MyAppDbContext appDbContext;

        public BankController(MyAppDbContext DbContext)
        {
            appDbContext = DbContext;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("AddAccount")]
        public IActionResult AddAccount(AccountDtoInput dto)
        {
            if (dto == null)
            {
                return BadRequest("Bank data is null");
            }

            var customer = appDbContext.customerDetails.Find(dto.CustId);
            if (customer == null)
            {
                return NotFound("Customer not found");
            }

            var bank = new BankModel
            {
                AccountBalance = dto.AccountBalance,
                CreatedDate = dto.CreatedDate,
                CustId = dto.CustId
            };

            appDbContext.BankDetails.Add(bank);
            appDbContext.SaveChanges();
            return Ok("Account Added");
        }


        [HttpGet("account/balance/{username}")]
        public IActionResult GetAccountBalance(string username)
        {
            var customer = appDbContext.customerDetails
                .FirstOrDefault(c => c.Name == username);

            if (customer == null)
            {
                return NotFound($"Customer '{username}' not found.");
            }

            var totalBalance = appDbContext.BankDetails
                .Where(a => a.CustId == customer.CustId)
                .Sum(a => a.AccountBalance);

            return Ok(new
            {
                Username = username,
                TotalBalance = totalBalance
            });
        }


        [HttpGet("GetMyBalance")]
        public IActionResult GetMyResult()
        {
            var name = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
            var result = appDbContext.BankDetails.Where(m => m.Customer.Name == name).Sum(m => m.AccountBalance);
            return Ok(new
            {
                MyBalance = result
            });
        }
    }
}
