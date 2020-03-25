using AutoMapper;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using FManager.Data.Entities.ManageBO;
using FManager.Core.Services;
using FManager.Core.Dtos.ManageBO;

namespace FManager.Web.Api
{
    public class SectionsController : BaseApiController
    {
        private readonly ISectionsService sectionsService;
        private readonly IEntriesService entriesService;
        private readonly IParitiesService paritiesService;
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public SectionsController(
            ISectionsService sectionsService,
            IEntriesService entriesService,
            IParitiesService paritiesService,
            IUsersService usersService,
            IMapper mapper)
        {
            this.sectionsService = sectionsService;
            this.entriesService = entriesService;
            this.paritiesService = paritiesService;
            this.usersService = usersService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<SectionDto> Get(Guid? id, Guid accountId)
        {
            Section section;

            if (id == null)
            {
                section = await this.sectionsService.CreateNewSection(accountId);
            }
            else
            {
                section = await this.sectionsService.GetByCriteriaAsync(w => w.SectionId == id && !w.Deleted);
            }

            if (section == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return this.mapper.Map<SectionDto>(section);
        }

        [HttpPost]
        public async Task Post(SectionDto sectionDto)
        {
            var section = this.mapper.Map<Section>(sectionDto);

            section.SectionId = Guid.NewGuid();
            section.CreationDate = DateTime.Now;
            section.Change = DateTime.Now;

            await this.sectionsService.AddAsync(section);
        }

        [HttpPut]
        public async Task Put(SectionDto sectionDto)
        {
            var section = this.mapper.Map<Section>(sectionDto);

            section.Change = DateTime.Now;

            await this.sectionsService.UpdateAsync(section);
        }

        [HttpDelete]
        public async Task Delete(Guid id)
        {
            var section = await this.sectionsService.GetByCriteriaAsync(w => w.SectionId == id);

            section.Deleted = true;

            await this.sectionsService.UpdateAsync(section);
        }

        [HttpGet, Route("api/Sections/paginate")]
        public async Task<IHttpActionResult> Paginate(Guid accountId, int page, int count)
        {
            var user = await this.usersService.GetUserByCurrentSessionToken();

            var sections = (await this.sectionsService
                .GetAllByCriteriaAsync(w => w.AccountId == accountId && w.Account.UserId == user.UserId && !w.Deleted))
                .OrderByDescending(w => w.CreationDate);

            var total = sections.Count();

            var sectionsPaginated = sections
                .Skip((page - 1) * count)
                .Take(count);

            return this.Ok(new
            {
                Total = total,
                Results = this.mapper.Map<List<SectionDto>>(sectionsPaginated)
            });
        }

        [HttpPost, Route("api/Sections/CreateIQOptionSections")]
        public async Task CreateIQOptionSectionsAsync(List<IQOptionEntryDto> iQOptionEntryDtos)
        {
            iQOptionEntryDtos = iQOptionEntryDtos.Where(w => w.Investments != w.Equity).OrderBy(w => w.OpeningDateTime).ToList();

            Section currentSection = null;
            Entry lastEntry = null;

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

                if (currentSection == null || iQOptionEntryDto.OpeningDateTime.Subtract(lastEntry.DateAndTime).Hours >= 2)
                {
                    currentSection = new Section
                    {
                        SectionId = Guid.NewGuid(),
                        AccountId = iQOptionEntryDto.SectionId,
                        Change = DateTime.Now,
                        CreationDate = DateTime.Now
                    };

                    await this.sectionsService.AddAsync(currentSection);
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
                    SectionId = currentSection.SectionId
                };

                lastEntry = await this.entriesService.AddAsync(entry);
            }
        }
    }
}