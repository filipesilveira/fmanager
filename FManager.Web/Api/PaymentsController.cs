using AutoMapper;
using System.Web.Http;
using System.Threading.Tasks;
using FManager.Core.Providers;
using FManager.Core.Services;
using FManager.Core.Dtos.Users;
using System;

namespace FManager.Web.Api
{
    public class PaymentsController : ApiController
    {
        private readonly IPaymentsProvider paymentsProvider;
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public PaymentsController(
            IPaymentsProvider paymentsProvider,
            IUsersService usersService,
            IMapper mapper)
        {
            this.paymentsProvider = paymentsProvider;
            this.usersService = usersService;
            this.mapper = mapper;
        }

        [HttpGet, Route("api/Payments/CreatePaymentUrl")]
        public async Task<IHttpActionResult> CreatePaymentUrl(Guid paymentPlanId)
        {
            var user = await this.usersService.GetUserByCurrentSessionToken();
            var userDto = this.mapper.Map<UserDto>(user);

            if (user == null)
            {
                return this.Unauthorized();
            }

            var paymentUrl = this.paymentsProvider.Checkout(userDto, paymentPlanId);

            if (string.IsNullOrEmpty(paymentUrl))
            {
                return this.BadRequest();
            }

            return this.Ok(paymentUrl);
        }

        [HttpGet, Route("api/Payments/GetPaymentUrl")]
        public async Task<IHttpActionResult> GetPaymentUrl()
        {
            var user = await this.usersService.GetUserByCurrentSessionToken();
            var paymentUrl = this.paymentsProvider.GetPaymentUrl(user);

            return Ok(paymentUrl);

        }
    }
}