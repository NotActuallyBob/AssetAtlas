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
            var expensesInDb = context.Expenses.Where(x => x.SpendTime <= end && x.SpendTime >= start);
            foreach (Category category in Enum.GetValues(typeof(Category))) {
                var expensesOfCategory = expensesInDb.Where(x => x.ExpenseCategory == category);

                expenses.Add(Tuple.Create(category, expensesOfCategory.Sum(x => x.Amount)));
            }

            return expenses;
        }

        static string[] keywordsEntertainment = ["baribal", "help.max", "finnkino"];
        static string[] keywordsGrocieries = ["alepa", "s-market", "prisma", "compass group finland", "k-market"];
        static string[] keywordsRestaurants = ["ravintola", "restaurant"];
        static string[] keywordsHousing = ["verbo"];
        static string[] keywordsTravel = ["bolt", "helsingin seudun"];

        public void CategorizeExpenses(IEnumerable<Expense> expenses) {
            foreach (var expense in expenses) {
                if(expense.ExpenseCategory != Category.Uncategorized) {
                    continue;
                }

                string recipient = expense.Recipient.ToLower();

                if(keywordsEntertainment.Any(x => recipient.Contains(x))) {
                    expense.ExpenseCategory = Category.Entertainment;
                }

                if (keywordsGrocieries.Any(x => recipient.Contains(x))) {
                    expense.ExpenseCategory = Category.Groceries;
                }

                if(keywordsRestaurants.Any(x => recipient.Contains(x))) {
                    expense.ExpenseCategory = Category.Restaurants;
                }

                if(keywordsHousing.Any(x => recipient.Contains(x))) {
                    expense.ExpenseCategory = Category.Housing;
                }

                if(keywordsTravel.Any(x => recipient.Contains(x))) {
                    expense.ExpenseCategory = Category.Travel;
                }
            }
        }

        public List<Expense> GetUncategorized() {
            return context.Expenses.Where(x => x.ExpenseCategory == Category.Uncategorized).ToList();
        }
    }
}
