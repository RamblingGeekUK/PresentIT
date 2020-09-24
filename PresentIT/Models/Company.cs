using System;

namespace PresentIT.Models
{
    public class Company
    {
        private DateTime? _dateCreated = null;
        public string Id { get; set; }       
        public string CompanyName { get; set; }
     
        public DateTime DateCreated {
            get
            {
                return this._dateCreated.HasValue
                   ? this._dateCreated.Value
                   : DateTime.Now;
            }

            set { this._dateCreated = value; }
        }
        public Boolean Active { get; set; }
    }
}
