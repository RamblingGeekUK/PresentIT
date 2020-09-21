using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PresentIT.Models
{
    public class Company
    {
        private DateTime? _dateCreated = null;

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        
        [JsonProperty(PropertyName = "Company Name")] // Think as this as our Tenant
        public string CompanyName { get; set; }
        
        [JsonProperty(PropertyName = "Created")]
        public DateTime DateCreated {
            get
            {
                return this._dateCreated.HasValue
                   ? this._dateCreated.Value
                   : DateTime.Now;
            }

            set { this._dateCreated = value; }
        }

        [JsonProperty(PropertyName = "Active")]
        public Boolean Active { get; set; }

    }
}
