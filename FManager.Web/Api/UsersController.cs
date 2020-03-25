using AutoMapper;
using System.Web.Http;
using FManager.Core.Services;
using System.Threading.Tasks;
using System;
using FManager.Core.Helpers;
using FManager.Core.Dtos.Users;
using FManager.Data.Entities.Users;
using FManager.Core.Providers;
using System.Collections.Generic;
using System.Linq;

namespace FManager.Web.Api
{
    public class UsersController : ApiController
    {
        private readonly ISessionHelper sessionHelper;
        private readonly IUsersService usersService;
        private readonly IPaymentPlansService paymentPlansService;
        private readonly ICriptographyProvider criptographyProvider;
        private readonly IMapper mapper;

        public UsersController(
            ISessionHelper sessionHelper,
            IUsersService usersService,
            ICriptographyProvider criptographyProvider,
            IPaymentPlansService paymentPlansService,
            IMapper mapper)
        {
            this.sessionHelper = sessionHelper;
            this.usersService = usersService;
            this.paymentPlansService = paymentPlansService;
            this.criptographyProvider = criptographyProvider;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<bool> Post(UserDto userDto)
        {
            var existingUser = await this.usersService.GetByCriteriaAsync(w => w.Email == userDto.Email);

            if (existingUser != null)
            {
                return false;
            }

            var user = this.mapper.Map<User>(userDto);

            user.UserId = Guid.NewGuid();
            user.SessionToken = Guid.NewGuid();
            user.CreationDate = DateTime.Now;
            user.Change = DateTime.Now;
            user.Password = this.criptographyProvider.Apply(user.Password);

            user = await this.usersService.AddAsync(user);

            this.sessionHelper.SetToSession("Token", user.SessionToken.ToString());
            this.sessionHelper.SetToSession("UserId", user.UserId);

            return true;
        }

        [HttpGet, Route("api/Users/GetCurrentUserName")]
        public async Task<string> GetCurrentUserName()
        {
            return (await this.usersService.GetUserByCurrentSessionToken())?.Name ?? string.Empty;
        }

        [HttpGet, Route("api/Users/GetPaymentPlans")]
        public async Task<List<PaymentPlanDto>> GetPaymentPlans()
        {
            var paymentPlans = (await this.paymentPlansService.GetAllAsync())
                .OrderBy(w => w.Description);

            var paymentPlanDtos = this.mapper.Map<List<PaymentPlanDto>>(paymentPlans);

            return paymentPlanDtos;
        }
    }
}