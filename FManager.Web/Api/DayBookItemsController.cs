using AutoMapper;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using FManager.Core.Services;
using System.Collections.Generic;
using System.Linq;
using FManager.Core.Dtos.DayBooks;
using FManager.Data.Entities.DayBooks;

namespace FManager.Web.Api
{
    public class DayBookItemsController : BaseApiController
    {
        private readonly IDayBookItemsService dayBookItemsService;
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public DayBookItemsController(
            IDayBookItemsService dayBookItemsService,
            IUsersService usersService,
            IMapper mapper)
        {
            this.dayBookItemsService = dayBookItemsService;
            this.usersService = usersService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<DayBookItemDto> Get(Guid id)
        {
            var dayBookItem = await this.dayBookItemsService.GetByCriteriaAsync(w => w.DayBookItemId == id && !w.Deleted);

            if (dayBookItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return this.mapper.Map<DayBookItemDto>(dayBookItem);
        }

        [HttpPost]
        public async Task Post(DayBookItemDto dayBookItemDto)
        {
            var dayBookItem = this.mapper.Map<DayBookItem>(dayBookItemDto);
            var user = await this.usersService.GetUserByCurrentSessionToken();

            dayBookItem.DayBookItemId = Guid.NewGuid();
            dayBookItem.CreationDate = DateTime.Now;
            dayBookItem.Change = DateTime.Now;
            dayBookItem.UserId = user.UserId;

            await this.dayBookItemsService.AddAsync(dayBookItem);
        }

        [HttpPut]
        public async Task Put(DayBookItemDto dayBookItemDto)
        {
            var dayBookItem = this.mapper.Map<DayBookItem>(dayBookItemDto);

            dayBookItem.Change = DateTime.Now;

            await this.dayBookItemsService.UpdateAsync(dayBookItem);
        }

        [HttpDelete]
        public async Task Delete(Guid id)
        {
            var dayBookItem = await this.dayBookItemsService.GetByCriteriaAsync(w => w.DayBookItemId == id);

            dayBookItem.Deleted = true;

            await this.dayBookItemsService.UpdateAsync(dayBookItem);
        }

        [HttpGet, Route("api/DayBookItems/paginate")]
        public async Task<List<DayBookItemDto>> Paginate(Guid dayBookId)
        {
            var user = await this.usersService.GetUserByCurrentSessionToken();

            var dayBookItems = (await this.dayBookItemsService
                .GetAllByCriteriaAsync(w => w.DayBookId == dayBookId && w.UserId == user.UserId && !w.Deleted))
                .OrderByDescending(w => w.CreationDate);

            return this.mapper.Map<List<DayBookItemDto>>(dayBookItems);
        }
    }
}