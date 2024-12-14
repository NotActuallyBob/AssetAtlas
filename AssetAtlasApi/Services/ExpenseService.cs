using AssetAtlasApi.Models;
using System.Collections.Generic;

namespace AssetAtlasApi.Services {
    public class ExpenseService {
        AtlasDbContext context;

        public ExpenseService(AtlasDbContext dbContext) {
            this.context = dbContext;
        }

        public void AddExpense(Expense expense) {
            context.Add(expense);
        }

        public List<Tuple<Category, int>> GetPeriodExpences(DateTime start, DateTime end) {
            List <Tuple<Category, int>> expenses = new List <Tuple<Category, int>>();
            foreach (Category category in Enum.GetValues(typeof(Category))) {
                var expensesOfCategory = context.Expenses.Where(x => x.ExpenseCategory == category);

                expenses.Add(Tuple.Create(category, expensesOfCategory.Sum(x => x.Amount)));
            }

            return expenses;
        }
    }
}
