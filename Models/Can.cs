namespace EnergyScanVerwaltung.Models
{
    public class Can
    {
        public string id { get; set; }
        public string manufacturerId { get; set; }
        public string tasteId { get; set; }
        public string countryId { get; set; }
        public string contentamount { get; set; }
        public string mhd { get; set; }
        public bool deposit { get; set; }
        public string closure { get; set; }
        public string description { get; set; }
        public bool damaged { get; set; }
        public string coffeincontent { get; set; }
        public string statusId { get; set; }
        public string creationDate { get; set; }
        public string changedLast { get; set; }
        public string changedById { get; set; }

        public Can()
        {

        }
    }
}
