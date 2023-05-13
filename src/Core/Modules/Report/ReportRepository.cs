using e_commerce_server.src.Core.Common.Enum;
using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Packages.HttpExceptions;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_server.src.Core.Modules.Report
{
    public class ReportRepository
    {
        private readonly AppDbContext _context;
        public ReportRepository(AppDbContext context)
        {
            _context = context;
        }
        public ReportData? GetReportByIds(int userId, int productId)
        {
            try
            {
                return _context.Reports.SingleOrDefault(r => r.user_id == userId && r.product_id == productId);
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public void CreateReport(ReportData report) 
        {
            try
            {
                _context.Reports.Add(report);
                _context.SaveChanges();
            }

            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public void UpdateReport(ReportData report)
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
        public List<ReportData> GetReports()
        {
            try
            {
                return _context.Reports.ToList();
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }

        public List<ReportData> GetReportsByPage(int page)
        {
            try
            {
                return _context.Reports
                    .Skip((page - 1) * 10)
                    .Take(PageSizeEnum.PAGE_SIZE)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
    }
}
