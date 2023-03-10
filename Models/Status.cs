namespace EnergyScanVerwaltung.Models
{
    public class Status
    {
        public string id { get; set; }
        public string name { get; set; }

        public Status()
        {

        }

        public Status(string id, string name)
        {
            this.id = id;
            this.name = name;
        }



    }
}
