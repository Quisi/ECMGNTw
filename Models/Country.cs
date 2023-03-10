namespace EnergyScanVerwaltung.Models
{
    public class Country
    {
        public string id { get; set; }
        public string name { get; set; }

        public Country()
        {

        }

        public Country(string id, string name)
        {
            this.id = id;
            this.name = name;
        }



    }
}
