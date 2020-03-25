using AutoMapper;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using FManager.Core.Dtos.ManageBO;
using FManager.Core.Services;
using System.Collections.Generic;

namespace FManager.Web.Api
{
    public class CurrenciesController : BaseApiController
    {
        private readonly ICurrenciesService currenciesService;
        private readonly IMapper mapper;

        public CurrenciesController(
            ICurrenciesService currenciesService,
            IMapper mapper)
        {
            this.currenciesService = currenciesService;
            this.mapper = mapper;
        }

        [HttpGet, Route("api/Currencies/paginate")]
        public async Task<List<CurrencyDto>> Paginate()
        {
            var currencies = await this.currenciesService.GetAllByCriteriaAsync(w => !w.Deleted);

            return this.mapper.Map<List<CurrencyDto>>(currencies);
        }
    }
}