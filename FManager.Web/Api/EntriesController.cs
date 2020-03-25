using AutoMapper;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using FManager.Core.Dtos.ManageBO;
using FManager.Core.Services;
using FManager.Data.Entities.ManageBO;
using System.Collections.Generic;
using System.Linq;

namespace FManager.Web.Api
{
    public class EntriesController : BaseApiController
    {
        private readonly IEntriesService entriesService;
        private readonly IParitiesService paritiesService;
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public EntriesController(
            IEntriesService entriesService,
            IParitiesService paritiesService,
            IUsersService usersService,
            IMapper mapper)
        {
            this.entriesService = entriesService;
            this.paritiesService = paritiesService;
            this.usersService = usersService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<EntryDto> Get(Guid id)
        {
            var entry = await this.entriesService.GetByCriteriaAsync(w => w.EntryId == id && !w.Deleted);

            if (entry == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return this.mapper.Map<EntryDto>(entry);
        }

        [HttpPost]
        public async Task Post(EntryDto entryDto)
        {
            var entry = this.mapper.Map<Entry>(entryDto);

            entry.EntryId = Guid.NewGuid();
            entry.CreationDate = DateTime.Now;
            entry.Change = DateTime.Now;

            await this.entriesService.AddAsync(entry);
        }

        [HttpPut]
        public async Task Put(EntryDto entryDto)
        {
            var entry = this.mapper.Map<Entry>(entryDto);

            entry.Change = DateTime.Now;

            await this.entriesService.UpdateAsync(entry);
        }

        [HttpDelete]
        public async Task Delete(Guid id)
        {
            var entry = await this.entriesService.GetByCriteriaAsync(w => w.EntryId == id);

            entry.Deleted = true;

            await this.entriesService.UpdateAsync(entry);
        }

        [HttpGet, Route("api/Entries/paginate")]
        public async Task<List<EntryDto>> Paginate(Guid sectionId)
        {
            var user = await this.usersService.GetUserByCurrentSessionToken();

            var entries = (await this.entriesService
                .GetAllByCriteriaAsync(w => w.SectionId == sectionId && w.Section.Account.UserId == user.UserId && !w.Deleted))
                .OrderByDescending(w => w.DateAndTime);

            return this.mapper.Map<List<EntryDto>>(entries);
        }

        [HttpPost, Route("api/Entries/CreateIQOptionEntries")]
        public async Task CreateIQOptionEntriesAsync(List<IQOptionEntryDto> iQOptionEntryDtos)
        {
            iQOptionEntryDtos = iQOptionEntryDtos.Where(w => w.Investments != w.Equity).OrderBy(w => w.OpeningDateTime).ToList();

            foreach (var iQOptionEntryDto in iQOptionEntryDtos)
            {
                if (iQOptionEntryDto.Instrument != "Binary Options" && iQOptionEntryDto.Instrument != "Digital Options")
                {
                    continue;
                }

                var parity = await this.paritiesService.GetByCriteriaAsync(w => w.Name == iQOptionEntryDto.Asset);

                if (parity == null)
                {
                    parity = new Parity()
                    {
                        ParityId = Guid.NewGuid(),
                        Name = iQOptionEntryDto.Asset,
                        Change = DateTime.Now,
                        CreationDate = DateTime.Now
                    };

                    parity = await this.paritiesService.AddAsync(parity);
                }

                if (iQOptionEntryDto.GrossPnL.HasValue)
                {
                    iQOptionEntryDto.TotalPnL = iQOptionEntryDto.GrossPnL.Value;
                }

                var entry = new Entry
                {
                    EntryId = Guid.NewGuid(),
                    DateAndTime = iQOptionEntryDto.OpeningDateTime,
                    ParityId = parity.ParityId,
                    Win = iQOptionEntryDto.TotalPnL > 0,
                    Value = iQOptionEntryDto.Investments,
                    CreationDate = DateTime.Now,
                    Change = DateTime.Now,
                    Payout = iQOptionEntryDto.TotalPnL > 0 ? ((iQOptionEntryDto.TotalPnL * 100) / iQOptionEntryDto.Investments) : 0,
                    SectionId = iQOptionEntryDto.SectionId
                };

                await this.entriesService.AddAsync(entry);
            }
        }
    }
}