
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PresentIT.Models
{
    public class Candidate
    {
        private DateTime? _dateCreated = null;
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public string Auth0 { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public DateTime Created
        {
            get
            {
                return _dateCreated.HasValue
                    ? this._dateCreated.Value
                    : DateTime.Now;
            }

            set { this._dateCreated = value; }
        }
        public Boolean Accepted { get; set; }
        public string VideoURL { get; set; }

    }
}
