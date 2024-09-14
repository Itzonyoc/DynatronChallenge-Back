using Microsoft.AspNetCore.Mvc;
using DynatronChallenge.Model;
using Newtonsoft.Json;
using System.Text;
using DynatronChallenge.DAC;

namespace DynatronChallenge_Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CustomerController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomerDAC _customerDAC;

        public List<CustomerModel> dataset { get; set; }

        public CustomerController(IConfiguration config, ILogger<CustomerController> logger, IHttpContextAccessor httpContextAccessor, ICustomerDAC customerDAC)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _customerDAC = customerDAC;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            dataset = _customerDAC.GetCustomers();
            return Ok(dataset);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerModel customer)
        {
            _customerDAC.CreateCustomer(customer);
            return Ok(true);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CustomerModel customer)
        {
            _customerDAC.UpdateCustomer(customer);
            return Ok(true);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            _customerDAC.DeleteCustomer(id);
            return Ok(true);
        }
    }
}
