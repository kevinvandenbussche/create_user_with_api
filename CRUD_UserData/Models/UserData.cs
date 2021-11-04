using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CRUD_UserData.Models
{
    public class UserData
    {
        public int Id { get; set; }

    
        public string Name { get; set; }
        
        public string FirstName { get; set; }

        public UserData()
        {

        }
    }

    
}
