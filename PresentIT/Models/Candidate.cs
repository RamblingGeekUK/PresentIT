
using System;

namespace PresentIT.Models
{
    public class Candidate
    {
        private DateTime? _dateCreated = null;
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
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
        public Boolean Accepted { get; set; }
        public string VideoURL { get; set; }

    }
}
