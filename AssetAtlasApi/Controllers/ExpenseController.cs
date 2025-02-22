﻿using AssetAtlasApi.Dtos;
using AssetAtlasApi.Models;
using AssetAtlasApi.Services;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpPost("/api/ExpensesUpload")]
        public async Task<IActionResult> UploadCsv(IFormFile file) {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try {
                var csvRecords = new List<CsvRecord>();

                using (var stream = file.OpenReadStream())
                using (var reader = new StreamReader(stream))
                using (var csv = new CsvReader(reader, CultureInfo.GetCultureInfo("fi-FI"))) // Use Finnish culture
                {
                    csv.Context.RegisterClassMap<CsvRecordMap>(); // Register the mapping
                    csvRecords = csv.GetRecords<CsvRecord>().ToList();
                }

                // Map CsvExpense to Expense model
                IEnumerable<Expense> expenses = csvRecords.Where(x => x.Summa.StartsWith("-") && !x.Tapahtumalaji.Contains("OMA")).Select(record => new Expense {
                    Amount = (int)(decimal.Parse(record.Summa.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture) * -100),
                    SpendTime = DateTime.SpecifyKind(DateTime.ParseExact(record.Maksupäivä, "dd.MM.yyyy", CultureInfo.InvariantCulture), DateTimeKind.Utc),
                    Recipient = record.SaajanNimi
                }).ToList();

                IEnumerable<Income> incomes = csvRecords.Where(x => x.Summa.StartsWith("+") && !x.Tapahtumalaji.Contains("OMA")).Select(record => new Income {
                    Amount = (int)(decimal.Parse(record.Summa.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture) * 100),
                    PaymentTime = DateTime.SpecifyKind(DateTime.ParseExact(record.Maksupäivä, "dd.MM.yyyy", CultureInfo.InvariantCulture), DateTimeKind.Utc),
                    Source = record.Maksaja
                }).ToList();
;
                expenseService.CategorizeExpenses(expenses);

                context.Incomes.AddRange(incomes);
                context.Expenses.AddRange(expenses);
                await context.SaveChangesAsync();

                return Ok();
            } catch (Exception ex) {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        // GET: api/<ExpenseController>
        [HttpGet]
        public List<Tuple<Category, int>> Get(string start, string end) {
            if(string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end)) return new List<Tuple<Category, int>>();

            DateTime startDate = DateTime.SpecifyKind(DateTime.Parse(start), DateTimeKind.Utc);
            DateTime endDate = DateTime.SpecifyKind(DateTime.Parse(end), DateTimeKind.Utc);


            return expenseService.GetPeriodExpences(startDate, endDate);
        }

        [HttpGet("/api/ExpensesUncategorized")]
        public List<Expense> GetUncateorized() {
            return expenseService.GetUncategorized();
        }

        [HttpPost("/api/ExpensesCategorize")]
        public void Categorize([FromBody] CategorizationDto categorizationDto) {
            if(categorizationDto == null) return;

            foreach (int id in categorizationDto.ids) {
                var expense = context.Expenses.FirstOrDefault(x => x.Id == id);
                if (expense == null) {
                    continue;
                }

                expense.ExpenseCategory = categorizationDto.category;
            }

            context.SaveChanges();

            return;
        }

        [HttpGet("/api/XY")]
        public List<List<Tuple<DateTime, int>>> GetXY(string start, string end) {

            DateTime startDate = DateTime.SpecifyKind(DateTime.Parse(start), DateTimeKind.Utc);
            DateTime endDate = DateTime.SpecifyKind(DateTime.Parse(end), DateTimeKind.Utc);

            var list = new List<List<Tuple<DateTime, int>>>();
            foreach (Category category in Enum.GetValues(typeof(Category))) {
				DateTime iterator = startDate;
				var listInner = new List<Tuple<DateTime, int>>();
				while (iterator <= endDate) {
					int sum = context.Expenses.Where(x => x.ExpenseCategory == category && x.SpendTime.Year == iterator.Year && x.SpendTime.Month == iterator.Month && x.SpendTime.Day == iterator.Day).Sum(x => x.Amount);
					listInner.Add(Tuple.Create(iterator, sum));
					iterator = iterator.AddDays(1);
				}

                list.Add(listInner);
			}
            
            return list;
        }
    }
}
