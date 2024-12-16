using AssetAtlasApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetAtlasApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase {

        AtlasDbContext context {  get; set; }

        public IncomeController(AtlasDbContext context) {
            this.context = context;
        }

        [HttpGet]
        public List<Tuple<string, int>> GetIncomes(string start, string end) {
            if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end)) return new List<Tuple<string, int>>();

            DateTime startDate = DateTime.SpecifyKind(DateTime.Parse(start), DateTimeKind.Utc);
            DateTime endDate = DateTime.SpecifyKind(DateTime.Parse(end), DateTimeKind.Utc);

            var incomes = new List<Tuple<string, int>>();

            var incomesInDb = context.Incomes.Where(x => x.PaymentTime <= endDate && x.PaymentTime >= startDate);
            var uniqueSources = incomesInDb.Select(x => x.Source).Distinct().ToList();
            foreach (string source in uniqueSources) {
                var incomesOfSource = incomesInDb.Where(x => x.Source == source);

                incomes.Add(Tuple.Create(source, incomesOfSource.Sum(x => x.Amount)));
            }


            return incomes;
        }
    }
}
