namespace EnergyScanVerwaltung.Models
{
    public class Policy
    {
        public string id { get; set; }
        public string name { get; set; }

        public Policy()
        {

        }

        public Policy(string id, string name)
        {
            this.id = id;
            this.name = name;
        }



    }
}
