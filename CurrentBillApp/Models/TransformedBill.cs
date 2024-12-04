using System;
using System.ComponentModel.DataAnnotations;

namespace CurrentBillApp.Models
{
  public class TransformedBill
  {
    [Key]
    public int Id { get; set; }
    public string BillNumber { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedDate { get; set; }
  }
}
