namespace EnergyScanVerwaltung.Models
{
    public class ChangeRequest
    {
        public string id { get; set; }
        public string table { get; set; }
        public string field { get; set; }
        public string pkField { get; set; }
        public string pk { get; set; }
        public string userId { get; set; }
        public string timestamp { get; set; }
        public string stateId { get; set; }
        public string change { get; set; }

        public ChangeRequest()
        {

        }
    }
}
