using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class AuditTrail
    {

        [Key]
        public int id { get; set; }
        public string userid { get; set; }
        
        public string operationtype { get; set; }
        public DateTime datetime { get; set; }

        public string description { get; set; }

        public string primarykey { get; set; }


    }
}
