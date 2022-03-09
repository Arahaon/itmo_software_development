using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final.Models
{
    public class DepositAccount
    {
        public int Id { get; set; }
        
        public string Owner { get; set; }
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        [Required]
        public string Currency { get; set; }
    }
}