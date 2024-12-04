namespace CurrentBillApp.Models
{
  public class BillFilterModel
  {
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }
    public string? Description { get; set; }
  }
}
