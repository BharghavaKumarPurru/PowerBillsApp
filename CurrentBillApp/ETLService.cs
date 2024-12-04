using System;
using System.Linq;
using CurrentBillApp.Data;
using CurrentBillApp.Models;
namespace CurrentBillApp
{
  public class ETLService
  {
    private readonly AppDbContext _context;

    public ETLService(AppDbContext context)
    {
      _context = context;
    }

    // Run ETL process
    public void RunETL()
    {
      try
      {
        // Extract: Fetch all raw bill data
        var rawData = _context.Bills.ToList();

        // Transform: Filter, clean, and transform the data
        var transformedData = rawData
            .Where(b => b.Amount > 0) // Exclude invalid or zero-amount bills
            .Select(b => new
            {
              b.BillNumber,
              b.Description,
              TransformedAmount = b.Amount * 1.1M, // Ensure proper decimal handling
              b.CreatedDate
            });

        // Load: Save transformed data to a new table (TransformedBills)
        foreach (var data in transformedData)
        {
          _context.TransformedBills.Add(new TransformedBill
          {
            BillNumber = data.BillNumber,
            Description = data.Description,
            Amount = data.TransformedAmount,
            CreatedDate = data.CreatedDate
          });
        }

        _context.SaveChanges();
      }
      catch (Exception ex)
      {
        // Log the error to the console or a file
        Console.WriteLine($"Error during ETL process: {ex.Message}");
        throw; // Re-throw the exception to be caught in the controller if necessary
      }
    }
  }
}



