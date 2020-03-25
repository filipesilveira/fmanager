using AutoMapper;
using System.Threading.Tasks;
using System.Web.Http;
using FManager.Core.Dtos.ManageBO;
using FManager.Core.Services;
using System.Collections.Generic;
using FManager.Core.Models.ManageBO;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace FManager.Web.Api
{
    public class ReviewsController : BaseApiController
    {
        private readonly ISectionsService sectionsService;
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public ReviewsController(
            ISectionsService sectionsService,
            IUsersService usersService,
            IMapper mapper)
        {
            this.sectionsService = sectionsService;
            this.usersService = usersService;
            this.mapper = mapper;
        }

        [HttpPost, Route("api/Reviews/paginate")]
        public async Task<IHttpActionResult> Paginate(ReviewModel reviewModel)
        {
            var user = await this.usersService.GetUserByCurrentSessionToken();

            var sectionsQuery = await this.sectionsService
                .GetAllQueryableByCriteriaAsync(w => w.Account.UserId == user.UserId && w.Entries.Any(x => !x.Deleted));

            if (reviewModel.StartDate.HasValue)
            {
                sectionsQuery = sectionsQuery.Where(w => DbFunctions.TruncateTime(w.CreationDate) >= DbFunctions.TruncateTime(reviewModel.StartDate));
            }

            if (reviewModel.EndDate.HasValue)
            {
                sectionsQuery = sectionsQuery.Where(w => DbFunctions.TruncateTime(w.CreationDate) <= DbFunctions.TruncateTime(reviewModel.EndDate));
            }

            if (reviewModel.AccountId.HasValue)
            {
                sectionsQuery = sectionsQuery.Where(w => w.AccountId == reviewModel.AccountId);
            }

            if (reviewModel.DayOfWeek != null)
            {
                var dayOfWeek = (int)reviewModel.DayOfWeek;
                sectionsQuery = sectionsQuery.Where(w => SqlFunctions.DatePart("dw", w.CreationDate) == dayOfWeek);
            }

            sectionsQuery = sectionsQuery.Where(w => !w.Deleted && !w.Account.Deleted);

            var total = sectionsQuery.Count();

            var sectionsPaginated = sectionsQuery
                .OrderByDescending(w => w.CreationDate)
                .Skip((reviewModel.Page - 1) * reviewModel.Count)
                .Take(reviewModel.Count);

            var sections = await sectionsPaginated
                .Include(w => w.Entries)
                .ToListAsync();

            var sectionDtos = new List<ReviewDto>();

            foreach (var section in sections)
            {
                var entries = section.Entries
                    .Where(w => !w.Deleted)
                    .OrderBy(w => w.DateAndTime)
                    .ToList();

                var dateAndTime = entries.FirstOrDefault().DateAndTime;
                var summaries = reviewModel.Operations ? entries.Select(w => w.Summary).ToList() : new List<string>();
                var summary = section.Summary?.Replace("\r\n", "<br />").Replace("\n", "<br />").Replace("\r", "<br />");

                if (string.IsNullOrEmpty(summary) && !summaries.Any(w => !string.IsNullOrEmpty(w)))
                {
                    continue;
                }

                sectionDtos.Add(new ReviewDto()
                {
                    DateAndTime = dateAndTime,
                    Summary = summary,
                    EntriesSummaries = summaries
                });
            }

            return this.Ok(new
            {
                Total = total,
                Results = sectionDtos
            });
        }
    }
}