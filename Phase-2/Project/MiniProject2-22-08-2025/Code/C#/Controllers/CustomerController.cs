using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniProject2.Context;
using MiniProject2.DTO;
using MiniProject2.Models;
using MiniProject2.Security;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MiniProject2.Controllers
{

    public class CustomerController : Controller
    {

        MyAppDbContext appDbContext;
        private readonly HttpClient _httpClient;
        Microsoft.Extensions.Options.IOptions<JwtOptions> jwtoptions;
        public CustomerController(MyAppDbContext DbContext, HttpClient httpClient, Microsoft.Extensions.Options.IOptions<JwtOptions> jwtoptions)
        {
            appDbContext = DbContext;
            _httpClient = httpClient;
            this.jwtoptions = jwtoptions;
        }


        public class LoginDto
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        public class FastApiLoginResponse
        {
            [JsonPropertyName("login")]
            public bool Login { get; set; }

            [JsonPropertyName("Role")]
            public string Role { get; set; }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var url = "http://localhost:5000/login/";

            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(loginDto),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(url, content);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, $"FastAPI error: {body}");
            }

            var fastApiResult = System.Text.Json.JsonSerializer.Deserialize<FastApiLoginResponse>(body);

            if (fastApiResult == null || !fastApiResult.Login)
            {
                return Unauthorized("Invalid username or password");
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, loginDto.username),
        new Claim(ClaimTypes.Role, fastApiResult.Role)
    };

            var token = JwtService.CreateJWTToken(jwtoptions.Value, claims);

            return Ok(new { token });
        }

        [Authorize(Roles ="admin")]
        [HttpPost("AddCustomer")]
        public IActionResult AddCustomer(CustomerDtoInput dto)
        {
            if (dto == null)
            {
                return BadRequest("Customer data is null");
            }

            var customer = new CustomerModel
            {
                Name = dto.Name,
                Age = dto.Age
            };

            appDbContext.customerDetails.Add(customer);
            appDbContext.SaveChanges();

            return Ok(new
            {
                Message = "Customer Added",
                CustomerId = customer.CustId,
                customer.Name,
                customer.Age
            });
        }

        [Authorize(Roles ="admin")]
        [HttpGet("GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            var customers = appDbContext.customerDetails
                .Include(c => c.BankAccounts)
                .Select(c => new CustomerDto
                {
                    CustId = c.CustId,
                    Name = c.Name,
                    Age = c.Age,
                    BankAccounts = c.BankAccounts.Select(b => new AccountDto
                    {
                        AccountNum = b.AccountNum,
                        AccountBalance = b.AccountBalance,
                        CreatedDate = b.CreatedDate
                    }).ToList()
                }).Where(x =>x.Age >17)
                .ToList();

            return Ok(customers);
        }
 
        [HttpGet("GetCustomer/{id}")]
 
        public IActionResult GetCustomer(int id)
        {
            var customer = appDbContext.customerDetails
                .Include(c => c.BankAccounts)
                .Where(c => c.CustId == id)
                .Select(c => new CustomerDto
                {
                    CustId = c.CustId,
                    Name = c.Name,
                    Age = c.Age,
                    BankAccounts = c.BankAccounts.Select(b => new AccountDto
                    {
                        AccountNum = b.AccountNum,
                        AccountBalance = b.AccountBalance,
                        CreatedDate = b.CreatedDate
                    }).ToList()
                })
                .FirstOrDefault();

            if (customer == null)
            {
                return NotFound("Customer not found");
            }

            return Ok(customer);
        }
    }
}
