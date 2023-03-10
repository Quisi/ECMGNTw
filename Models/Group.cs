namespace EnergyScanVerwaltung.Models
{
    public class Group
    {
        public string id { get; set; }
        public string name { get; set; }

        public Group()
        {

        }

        public Group(string id, string name)
        {
            this.id = id;
            this.name = name;
        }



    }
}
