using Newtonsoft.Json;
using System;

namespace PresentIT.Models
{
    public class Candidate
    {
        private DateTime? _dateCreated = null;

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "Firstname")]
        public string Firstname { get; set; }
        
        [JsonProperty(PropertyName = "Surname")]
        public string Surname { get; set; }
        [JsonProperty(PropertyName = "Created")]
        public DateTime Created
        {
            get
            {
                return this._dateCreated.HasValue
                    ? this._dateCreated.Value
                    : DateTime.Now;
            }

            set { this._dateCreated = value; }
        }

        [JsonProperty(PropertyName = "Accepted")]
        public Boolean Accepted { get; set; }

        [JsonProperty(PropertyName = "VideoURL")]
        public string VideoURL { get; set; }

    }
}
