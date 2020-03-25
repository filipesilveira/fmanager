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
    public class DayBooksController : BaseApiController
    {
        private readonly IDayBooksService dayBooksService;
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public DayBooksController(
            IDayBooksService dayBooksService,
            IUsersService usersService,
            IMapper mapper)
        {
            this.dayBooksService = dayBooksService;
            this.usersService = usersService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<DayBookDto> Get(Guid id)
        {
            var dayBook = await this.dayBooksService.GetByCriteriaAsync(w => w.DayBookId == id && !w.Deleted);

            if (dayBook == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return this.mapper.Map<DayBookDto>(dayBook);
        }

        [HttpPost]
        public async Task Post(DayBookDto DayBookDto)
        {
            var dayBook = this.mapper.Map<DayBook>(DayBookDto);
            var user = await this.usersService.GetUserByCurrentSessionToken();

            dayBook.DayBookId = Guid.NewGuid();
            dayBook.CreationDate = DateTime.Now;
            dayBook.Change = DateTime.Now;
            dayBook.UserId = user.UserId;

            await this.dayBooksService.AddAsync(dayBook);
        }

        [HttpPut]
        public async Task Put(DayBookDto dayBookDto)
        {
            var dayBook = this.mapper.Map<DayBook>(dayBookDto);

            dayBook.Change = DateTime.Now;

            await this.dayBooksService.UpdateAsync(dayBook);
        }

        [HttpDelete]
        public async Task Delete(Guid id)
        {
            var dayBook = await this.dayBooksService.GetByCriteriaAsync(w => w.DayBookId == id);

            dayBook.Deleted = true;

            await this.dayBooksService.UpdateAsync(dayBook);
        }

        [HttpGet, Route("api/DayBooks/paginate")]
        public async Task<List<DayBookDto>> Paginate()
        {
            var user = await this.usersService.GetUserByCurrentSessionToken();

            var dayBooks = (await this.dayBooksService
                .GetAllByCriteriaAsync(w => w.UserId == user.UserId && !w.Deleted))
                .OrderByDescending(w => w.CreationDate);

            return this.mapper.Map<List<DayBookDto>>(dayBooks);
        }
    }
}