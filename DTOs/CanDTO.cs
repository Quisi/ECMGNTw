using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace EnergyScanVerwaltung.DTOs
{
    [DataContract]
    [JsonObject(MemberSerialization.OptOut)]
    public partial class CanDTO : IEquatable<CanDTO>
    {
        public string Id { get; set; }
        [Required]
        public List<BarcodeDTO> Barcodes { get; set; }
        [Required]
        public ManufacturerDTO Manufacturer { get; set; }
        [Required]
        public TasteDTO Taste { get; set; }
        public CountryDTO Country { get; set; }
        public StatusDTO Status { get; set; }
        public List<TagDTO> Tags { get; set; }
        public string Contentamount { get; set; }
        public string Mhd { get; set; }
        public bool Deposit { get; set; }
        public string Closure { get; set; }
        public string Description { get; set; }
        public bool Damaged { get; set; }
        public string Coffeincontent { get; set; }

        public CanDTO()
        {

        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class CanDTO {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Barcodes: ").Append(Barcodes).Append("\n");
            sb.Append("  Manufacturer: ").Append(Manufacturer).Append("\n");
            sb.Append("  Taste: ").Append(Taste).Append("\n");
            sb.Append("  Country ").Append(Country).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  Tags: ").Append(Tags).Append("\n");
            sb.Append("  Contentamount: ").Append(Contentamount).Append("\n");
            sb.Append("  Mhd: ").Append(Mhd).Append("\n");
            sb.Append("  Deposit: ").Append(Id).Append("\n");
            sb.Append("  Closure: ").Append(Closure).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Damaged: ").Append(Damaged).Append("\n");
            sb.Append("  Coffeincontent: ").Append(Coffeincontent).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType() && Equals((CanDTO)obj);
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashName = 41;
                // Suitable nullity checks etc, of course :)
                if (Id != null)
                {
                    hashName = hashName * 59 + Id.GetHashCode();
                }

                return hashName;
            }
        }

        public bool Equals(CanDTO other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return
                (
                    Id == other.Id ||
                    Id != null &&
                    Id.Equals(other.Id)

                );
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(CanDTO left, CanDTO right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CanDTO left, CanDTO right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
