using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetAtlasApi.Models {
    public enum Category {
        Uncategorized = 0,
        Groceries = 1,
        Housing = 2,
        Restaurants = 3,
        Travel = 4,
        Entertainment = 5,
        Hobbies = 6,
        Clothing = 7,
        Health = 8,
        Other = 9,
    }

    public class Expense {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime SpendTime { get; set; }
        public string Recipient { get; set; }
        public Category ExpenseCategory { get; set; } = Category.Uncategorized;
    }
}
