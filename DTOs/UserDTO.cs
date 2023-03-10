using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace EnergyScanVerwaltung.DTOs
{
    [JsonObject(MemberSerialization.OptOut)]
    public class UserDTO
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }

        public GroupDTO Group { get; set; }

        public bool Verified { get; set; }

        public UserDTO()
        {

        }
    }
}
