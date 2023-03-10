namespace EnergyScanVerwaltung.Models
{
    public class Taste
    {
        public string id { get; set; }
        public string name { get; set; }

        public Taste()
        {

        }

        public Taste(string id, string name)
        {
            this.id = id;
            this.name = name;
        }



    }
}
