using System.ComponentModel.DataAnnotations;

namespace CurrentBillApp.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string BillNumber { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
