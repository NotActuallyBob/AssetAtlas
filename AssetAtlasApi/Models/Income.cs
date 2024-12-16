using System.ComponentModel.DataAnnotations;

namespace AssetAtlasApi.Models {
    public class Income {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Source {  get; set; }
        public DateTime PaymentTime { get; set; }
    }
}
