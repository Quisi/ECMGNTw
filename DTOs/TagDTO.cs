using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EnergyScanVerwaltung.DTOs
{
    [JsonObject(MemberSerialization.OptOut)]
    public class TagDTO
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

        public TagDTO()
        {

        }

    }
}
