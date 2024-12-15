using AssetAtlasApi.Models;

namespace AssetAtlasApi.Dtos {
    public class CategorizationDto {
        public List<int> ids {  get; set; }
        public Category category { get; set; }
    }
}
