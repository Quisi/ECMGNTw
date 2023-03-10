using System;

namespace EnergyScanVerwaltung.DTOs
{
    public class ChangeRequestDTO
    {
        public string id { get; set; }
        public string table { get; set; }
        public string field { get; set; }
        public string pkField { get; set; }
        public string pk { get; set; }
        public UserDTO user { get; set; }
        public DateTime timestamp { get; set; }
        public StatusDTO status { get; set; }
        public string changeoldValue { get; set; }
        public string changenewValue { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime changedLast { get; set; }
        public UserDTO changedBy { get; set; }
        public ChangeRequestDTO()
        {

        }
    }
}