namespace EnergyScanVerwaltung.Models
{
    public class Tag
    {
        public string id { get; set; }
        public string name { get; set; }

        public Tag()
        {

        }

        public Tag(string id, string name)
        {
            this.id = id;
            this.name = name;
        }



    }
}
