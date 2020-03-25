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
    public class AssertivenessController : BaseApiController
    {
        private readonly IEntriesService entriesService;
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public AssertivenessController(
            IEntriesService entriesService,
            IUsersService usersService,
            IMapper mapper)
        {
            this.entriesService = entriesService;
            this.usersService = usersService;
            this.mapper = mapper;
        }

        [HttpPost, Route("api/Assertiveness/paginate")]
        public async Task<IHttpActionResult> Paginate(AssertivenessModel assertivenessModel)
        {
            var user = await this.usersService.GetUserByCurrentSessionToken();
            var entries = await this.entriesService.GetAllQueryableByCriteriaAsync(w => w.Section.Account.UserId == user.UserId);

            if (assertivenessModel.AccountId.HasValue)
            {
                entries = entries.Where(w => w.Section.AccountId == assertivenessModel.AccountId);
            }

            if (assertivenessModel.StartDate.HasValue)
            {
                entries = entries.Where(w => DbFunctions.TruncateTime(w.DateAndTime) >= DbFunctions.TruncateTime(assertivenessModel.StartDate));
            }

            if (assertivenessModel.EndDate.HasValue)
            {
                entries = entries.Where(w => DbFunctions.TruncateTime(w.DateAndTime) <= DbFunctions.TruncateTime(assertivenessModel.EndDate));
            }

            if (assertivenessModel.ParityId.HasValue)
            {
                entries = entries.Where(w => w.ParityId == assertivenessModel.ParityId);
            }

            if (assertivenessModel.DayOfWeek != null)
            {
                var dayOfWeek = (int)assertivenessModel.DayOfWeek;
                entries = entries.Where(w => SqlFunctions.DatePart("dw", w.DateAndTime) == dayOfWeek);
            }

            entries = entries.Where(w => !w.Deleted && !w.Section.Deleted && !w.Section.Account.Deleted);

            var parities = await entries
                .Include(w => w.Parity)
                .GroupBy(w => w.ParityId)
                .Select(w => w.FirstOrDefault())
                .ToListAsync();

            var reportDtos = new List<AssertivenessDto>();

            foreach (var parity in parities)
            {
                var totalWin = entries.Where(w => w.ParityId == parity.ParityId && w.Win).Count();
                var totalLoss = entries.Where(w => w.ParityId == parity.ParityId && !w.Win).Count();
                var totalEntry = totalWin + totalLoss;
                var percentage = (totalWin * 100) / totalEntry;

                reportDtos.Add(new AssertivenessDto()
                {
                    ParityId = parity.ParityId,
                    ParityName = parity.Parity.Name,
                    Percentage = percentage,
                    TotalWin = totalWin,
                    TotalLoss = totalLoss,
                    TotalEntries = totalEntry
                });
            }

            if (reportDtos.Count() == 0)
            {
                return this.Ok();
            }

            var totalPercentage = (int) reportDtos.Sum(w => w.Percentage) / reportDtos.Count();
            var totalWins = reportDtos.Sum(w => w.TotalWin);
            var totalLosses = reportDtos.Sum(w => w.TotalLoss);
            var totalEntries = reportDtos.Sum(w => w.TotalEntries);

            return this.Ok(new
            {
                Results = reportDtos,
                Percentage = totalPercentage,
                Wins = totalWins,
                Losses = totalLosses,
                Entries = totalEntries
            });
        }
    }
}