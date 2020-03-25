using AutoMapper;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using FManager.Core.Dtos.ManageBO;
using FManager.Core.Services;
using System.Collections.Generic;
using System.Linq;

namespace FManager.Web.Api
{
    public class ParitiesController : BaseApiController
    {
        private readonly IParitiesService paritiesService;
        private readonly IMapper mapper;

        public ParitiesController(
            IParitiesService paritiesService,
            IMapper mapper)
        {
            this.paritiesService = paritiesService;
            this.mapper = mapper;
        }

        [HttpGet, Route("api/Parities/paginate")]
        public async Task<List<ParityDto>> Paginate()
        {
            var parities = (await this.paritiesService
                .GetAllByCriteriaAsync(w => !w.Deleted))
                .OrderBy(w => w.Name);

            return this.mapper.Map<List<ParityDto>>(parities);
        }
    }
}