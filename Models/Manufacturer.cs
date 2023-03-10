namespace EnergyScanVerwaltung.Models
{
    public class Manufacturer
    {
        public string id { get; set; }
        public string name { get; set; }

        public Manufacturer()
        {

        }

        public Manufacturer(string id, string name)
        {
            this.id = id;
            this.name = name;
        }



    }
}
