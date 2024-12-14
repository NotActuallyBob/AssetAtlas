using AssetAtlasApi.Models;
using AssetAtlasApi.Services;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AssetAtlasApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase {

        AtlasDbContext context;
        ExpenseService expenseService;
        public ExpenseController(AtlasDbContext context, ExpenseService expenseService) {
            this.context = context;
            this.expenseService = expenseService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadCsv(IFormFile file) {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try {
                var csvRecords = new List<CsvExpense>();

                using (var stream = file.OpenReadStream())
                using (var reader = new StreamReader(stream))
                using (var csv = new CsvReader(reader, CultureInfo.GetCultureInfo("fi-FI"))) // Use Finnish culture
                {
                    csv.Context.RegisterClassMap<CsvExpenseMap>(); // Register the mapping
                    csvRecords = csv.GetRecords<CsvExpense>().ToList();
                }

                // Map CsvExpense to Expense model
                IEnumerable<Expense> expenses = csvRecords.Select(record => new Expense {
                    Amount = (int)(decimal.Parse(record.Summa.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture) * -100),
                    SpendTime = DateTime.SpecifyKind(DateTime.ParseExact(record.Kirjauspäivä, "dd.MM.yyyy", CultureInfo.InvariantCulture), DateTimeKind.Utc),
                    Recipient = record.SaajanNimi
                });

                expenses = expenses.Where(x => x.Amount > 0).ToList();
                List<Expense> expenseList = expenses.Where(x => x.Amount > 0).ToList();

                context.Expenses.AddRange(expenses);
                await context.SaveChangesAsync();

                return Ok(new { Message = "CSV uploaded and processed successfully.", Count = expenseList.Count });
            } catch (Exception ex) {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        // GET: api/<ExpenseController>
        [HttpGet]
        public List<Tuple<Category, int>> Get() {
            DateTime end = DateTime.Now;
            DateTime start = DateTime.Now.AddDays(-30);


            return expenseService.GetPeriodExpences(start, end);
        }

        // GET api/<ExpenseController>/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return "value";
        }

        //// POST api/<ExpenseController>
        //[HttpPost]
        //public void Post([FromBody] string value) {
        //}

        // PUT api/<ExpenseController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE api/<ExpenseController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
