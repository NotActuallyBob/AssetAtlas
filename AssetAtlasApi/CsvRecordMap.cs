namespace AssetAtlasApi {
    using AssetAtlasApi.Models;
    using CsvHelper.Configuration;

    public class CsvRecordMap : ClassMap<CsvRecord> {
        public CsvRecordMap() {
            Map(m => m.Kirjauspäivä).Name("Kirjauspäivä");
            Map(m => m.Maksupäivä).Name("Maksupäivä");
            Map(m => m.Summa).Name("Summa");
            Map(m => m.Tapahtumalaji).Name("Tapahtumalaji");
            Map(m => m.Maksaja).Name("Maksaja");
            Map(m => m.SaajanNimi).Name("Saajan nimi");
            Map(m => m.SaajanTilinumero).Name("Saajan tilinumero");
            Map(m => m.SaajanBIC).Name("Saajan BIC-tunnus");
            Map(m => m.Viitenumero).Name("Viitenumero");
            Map(m => m.Viesti).Name("Viesti");
            Map(m => m.Arkistointitunnus).Name("Arkistointitunnus");
        }
    }
}
