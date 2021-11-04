using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CRUD_UserData.Service
{
    public class Repository
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
