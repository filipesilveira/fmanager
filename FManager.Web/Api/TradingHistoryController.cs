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
    public class TradingHistoryController : BaseApiController
    {
        private readonly IEntriesService entriesService;
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public TradingHistoryController(
            IEntriesService entriesService,
            IUsersService usersService,
            IMapper mapper)
        {
            this.entriesService = entriesService;
            this.usersService = usersService;
            this.mapper = mapper;
        }

        [HttpPost, Route("api/TradingHistory/paginate")]
        public async Task<IHttpActionResult> Paginate(TradingHistoryModel tradingHistoryModel)
        {
            var user = await this.usersService.GetUserByCurrentSessionToken();
            var entriesQuery = await this.entriesService.GetAllQueryableByCriteriaAsync(w => w.Section.Account.UserId == user.UserId);

            if (tradingHistoryModel.AccountId.HasValue)
            {
                entriesQuery = entriesQuery.Where(w => w.Section.AccountId == tradingHistoryModel.AccountId);
            }

            if (tradingHistoryModel.StartDate.HasValue)
            {
                entriesQuery = entriesQuery.Where(w => DbFunctions.TruncateTime(w.DateAndTime) >= DbFunctions.TruncateTime(tradingHistoryModel.StartDate));
            }

            if (tradingHistoryModel.EndDate.HasValue)
            {
                entriesQuery = entriesQuery.Where(w => DbFunctions.TruncateTime(w.DateAndTime) <= DbFunctions.TruncateTime(tradingHistoryModel.EndDate));
            }

            if (tradingHistoryModel.ParityId.HasValue)
            {
                entriesQuery = entriesQuery.Where(w => w.ParityId == tradingHistoryModel.ParityId);
            }

            if (tradingHistoryModel.DayOfWeek != null)
            {
                var dayOfWeek = (int) tradingHistoryModel.DayOfWeek;
                entriesQuery = entriesQuery.Where(w => SqlFunctions.DatePart("dw", w.DateAndTime) == dayOfWeek);
            }

            entriesQuery = entriesQuery.Where(w => !w.Deleted && !w.Section.Deleted && !w.Section.Account.Deleted);

            var total = entriesQuery.Count();

            var entries = await entriesQuery
                .OrderByDescending(w => w.DateAndTime)
                .Skip((tradingHistoryModel.Page - 1) * tradingHistoryModel.Count)
                .Take(tradingHistoryModel.Count)
                .Include(w => w.Parity)
                .ToListAsync();

            var tradingHistoryDtos = new List<TradingHistoryDto>();

            foreach (var entry in entries)
            {
                tradingHistoryDtos.Add(new TradingHistoryDto()
                {
                    DateAndTime = entry.DateAndTime,
                    ParityId = entry.ParityId,
                    ParityName = entry.Parity.Name,
                    AccountId = entry.Section.AccountId,
                    Value = entry.Value,
                    CurrencyName = entry.Section.Account.Currency.Name,
                    Result = entry.Win ? (entry.Value * (entry.Payout / 100)) : - entry.Value,
                    Equity = entry.Win ? ((entry.Value * (entry.Payout / 100)) + entry.Value) : 0
                });
            }

            if (tradingHistoryDtos.Count() == 0)
            {
                return this.Ok();
            }

            var totalProfit = entriesQuery.Sum(entry => entry.Win ? (entry.Value * (entry.Payout / 100)) : - entry.Value);
            var totalEquity = entriesQuery.Sum(entry => entry.Win ? ((entry.Value * (entry.Payout / 100)) + entry.Value) : 0);
            var totalInvestment = entriesQuery.Sum(entry => entry.Value);
            var currencyName = tradingHistoryDtos.FirstOrDefault()?.CurrencyName;

            return this.Ok(new
            {
                Results = tradingHistoryDtos,
                Profit = totalProfit,
                Equity = totalEquity,
                Investment = totalInvestment,
                CurrencyName = currencyName,
                Total = total
            });
        }
    }
}