namespace AssetAtlasApi.Models {
    public class CsvExpense {
        public string Kirjauspäivä { get; set; } // Booking Date
        public string Maksupäivä { get; set; }   // Payment Date
        public string Summa { get; set; }        // Amount
        public string Tapahtumalaji { get; set; } // Transaction Type
        public string Maksaja { get; set; }      // Payer
        public string SaajanNimi { get; set; }   // Payee Name
        public string SaajanTilinumero { get; set; } // Payee Account Number
        public string SaajanBIC { get; set; }    // Payee BIC
        public string Viitenumero { get; set; }  // Reference Number
        public string Viesti { get; set; }       // Message
        public string Arkistointitunnus { get; set; } // Archive ID
    }
}
