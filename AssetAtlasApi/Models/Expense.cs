using Microsoft.EntityFrameworkCore;

namespace AssetAtlasApi.Models {

    [PrimaryKey(nameof(Id))]
    public class Expense {
        int Id { get; set; }
    }
}
