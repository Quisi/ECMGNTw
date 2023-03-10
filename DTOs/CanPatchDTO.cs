using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EnergyScanVerwaltung.DTOs
{
    /// <summary>
    /// Data-Transfer-Object: Barcode
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class CanPatchDTO
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        [Required]
        public string FieldName { get; set; }
        /// <summary>
        /// Barcode-Value
        /// </summary>
        [Required]
        public string NewValue { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public CanPatchDTO()
        {

        }

    }
}
