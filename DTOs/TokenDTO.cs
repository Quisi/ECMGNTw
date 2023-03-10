using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyScanVerwaltung.DTOs
{
    public class TokenDTO : UserDTO
    {
        public string access_token { get; set; }
        public TokenDTO()
        {

        }
    }
}
