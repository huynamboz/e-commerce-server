using e_commerce_server.src.Core.Modules.User.Service;
using e_commerce_server.src.Core.Modules.User;
using e_commerce_server.src.Packages.HttpExceptions;
using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Modules.Product.Service;
using e_commerce_server.src.Core.Modules.Product;
using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Core.Modules.Report.Dto;
using e_commerce_server.src.Core.Modules.Review.Service;
using e_commerce_server.src.Core.Modules.Review;
using CloudinaryDotNet.Actions;
using e_commerce_server.src.Core.Common.Enum;

namespace e_commerce_server.src.Core.Modules.Report.Service
{
    public class ReportService
    {
        private readonly ProductRepository productRepository;
        private readonly UserRepository userRepository;
        private readonly ReportRepository reportRepository;
        public ReportService(MyDbContext context)
        {
            reportRepository = new ReportRepository(context);
            productRepository = new ProductRepository(context);
            userRepository = new UserRepository(context);
        }

        public object CreateOrUpdateReport(int productId, int userId, ReportProductDto model)
        {
            var product = productRepository.GetProductById(productId);

            if (product == null)
            {
                throw new BadRequestException(ProductEnum.PRODUCT_NOT_FOUND);
            }

            var user = userRepository.GetUserById(userId);

            if (user == null)
            {
                throw new BadRequestException(UserEnum.USER_NOT_FOUND);
            }

            if (product.user_id == userId)
            {
                throw new ForbiddenException(ReportEnum.CANNOT_REPORT_OWN_PRODUCT);
            }

            var report = reportRepository.GetReportByIds(userId, productId);


            if (report != null)
            {
                report.description = model.description;
                report.update_at = DateTime.Now;

                reportRepository.UpdateReport(report);

                return new
                {
                    message = ReportEnum.UPDATE_REPORT_SUCCESS,
                    data = new
                    {
                        report.description,
                        report.update_at
                    }
                };
            }

            var newReport = new ReportData
            {
                description = model.description,
                product_id = productId,
                user_id = userId,
                create_at = DateTime.Now,
                update_at = DateTime.Now,
            };

            reportRepository.CreateReport(newReport);

            return new
            {
                message = ReportEnum.CREATE_REPORT_SUCCESS,
                data = new
                {
                    newReport.description,
                    newReport.update_at
                }
            };
        }

        public object GetReportsByUserId(int page, int roleId)
        { 
            if (roleId != RoleEnum.ADMIN)
            {
                throw new ForbiddenException(ReportEnum.GET_ALL_REPORTS_DENY);
            }

            var reports = reportRepository.GetReports();

            var paginatedReports = reportRepository.GetReportsByPage(page);

            int total = (int)Math.Ceiling((double)reports.Count() / PageSizeEnum.PAGE_SIZE); //calculate total pages

            return new
            {
                data = paginatedReports,
                meta = new
                {
                    totalPages = total,
                    totalCount = reports.Count(),
                    currentPage = page
                }
            };
        }
    }
}
