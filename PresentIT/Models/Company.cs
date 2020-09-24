using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PresentIT.Models
{
    public class Company
    {
        private DateTime? _dateCreated = null;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }   
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
