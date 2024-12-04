using CurrentBillApp.Data;
using CurrentBillApp.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace CurrentBillApp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class BillsController : Controller
  {
    private readonly AppDbContext _context;

    public BillsController(AppDbContext context)
    {
      _context = context;
    }

    // Get all bills
    [HttpGet]
    public IActionResult GetBills()
    {
      var bills = _context.Bills.ToList();
      return Ok(bills); // Return JSON data
    }

    // Create a new bill
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Bill bill)
    {
      if (ModelState.IsValid)
      {
        _context.Bills.Add(bill);
        _context.SaveChanges();
        return RedirectToAction(nameof(GetBills));
      }
      return BadRequest(ModelState);
    }

    // Edit a bill
    [HttpPut("{id}")]
    public IActionResult Edit(int id, [FromBody] Bill bill)
    {
      if (id != bill.Id) return BadRequest("Mismatched Bill ID");

      if (ModelState.IsValid)
      {
        _context.Bills.Update(bill);
        _context.SaveChanges();
        return Ok(bill);
      }
      return BadRequest(ModelState);
    }

    // Delete a bill
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var bill = _context.Bills.Find(id);
      if (bill == null) return NotFound();

      _context.Bills.Remove(bill);
      _context.SaveChanges();
      return Ok();
    }

    // Export filtered bills to Excel
    [HttpGet("report/excel")]
    public IActionResult ExportFilteredBillsToExcel(DateTime? startDate, DateTime? endDate, decimal? minAmount, decimal? maxAmount, string? description)
    {
      ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

      // Filter the bills
      var bills = _context.Bills.AsQueryable();
      if (startDate.HasValue) bills = bills.Where(b => b.CreatedDate >= startDate.Value);
      if (endDate.HasValue) bills = bills.Where(b => b.CreatedDate <= endDate.Value);
      if (minAmount.HasValue) bills = bills.Where(b => b.Amount >= minAmount.Value);
      if (maxAmount.HasValue) bills = bills.Where(b => b.Amount <= maxAmount.Value);
      if (!string.IsNullOrEmpty(description)) bills = bills.Where(b => b.Description.Contains(description));

      using (var package = new ExcelPackage())
      {
        var worksheet = package.Workbook.Worksheets.Add("Bills Report");

        // Add headers
        worksheet.Cells[1, 1].Value = "Bill Number";
        worksheet.Cells[1, 2].Value = "Description";
        worksheet.Cells[1, 3].Value = "Amount";
        worksheet.Cells[1, 4].Value = "Created Date";

        var billsList = bills.ToList();

        // Populate rows with data
        for (int i = 0; i < billsList.Count; i++)
        {
          worksheet.Cells[i + 2, 1].Value = billsList[i].BillNumber;
          worksheet.Cells[i + 2, 2].Value = billsList[i].Description;
          worksheet.Cells[i + 2, 3].Value = billsList[i].Amount;
          worksheet.Cells[i + 2, 4].Value = billsList[i].CreatedDate.ToString("yyyy-MM-dd");
        }

        var stream = new MemoryStream();
        package.SaveAs(stream);
        stream.Position = 0;

        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BillsReport.xlsx");
      }
    }

    // Export bills to PDF
    [HttpGet("report/pdf")]
    public IActionResult ExportBillsToPdf()
    {
      var bills = _context.Bills.ToList();

      using (var stream = new MemoryStream())
      {
        var document = new Document();
        var writer = PdfWriter.GetInstance(document, stream);
        document.Open();

        // Add a title
        document.Add(new Paragraph("Bills Report"));

        // Create a table with 4 columns
        var table = new PdfPTable(4)
        {
          WidthPercentage = 100
        };
        table.AddCell("Bill Number");
        table.AddCell("Description");
        table.AddCell("Amount");
        table.AddCell("Created Date");

        // Add rows
        foreach (var bill in bills)
        {
          table.AddCell(bill.BillNumber);
          table.AddCell(bill.Description);
          table.AddCell(bill.Amount.ToString("F2"));
          table.AddCell(bill.CreatedDate.ToString("yyyy-MM-dd"));
        }

        document.Add(table);
        document.Close();

        stream.Position = 0;
        return File(stream, "application/pdf", "BillsReport.pdf");
      }
    }

    // Filter bills by various criteria
    [HttpGet("filter")]
    public IActionResult FilterBills([FromQuery] BillFilterModel filters)
    {
      var query = _context.Bills.AsQueryable();

      if (!string.IsNullOrEmpty(filters.StartDate) && DateTime.TryParse(filters.StartDate, out var startDate))
      {
        query = query.Where(b => b.CreatedDate >= startDate);
      }

      if (!string.IsNullOrEmpty(filters.EndDate) && DateTime.TryParse(filters.EndDate, out var endDate))
      {
        query = query.Where(b => b.CreatedDate <= endDate);
      }

      if (filters.MinAmount.HasValue)
      {
        query = query.Where(b => b.Amount >= filters.MinAmount.Value);
      }

      if (filters.MaxAmount.HasValue)
      {
        query = query.Where(b => b.Amount <= filters.MaxAmount.Value);
      }

      if (!string.IsNullOrEmpty(filters.Description))
      {
        query = query.Where(b => b.Description.Contains(filters.Description));
      }

      var result = query.ToList();
      return Ok(result);
    }
  }
}
