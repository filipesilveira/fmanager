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
    public class AccountsController : BaseApiController
    {
        private readonly IAccountsService accountsService;
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public AccountsController(
            IAccountsService accountsService,
            IUsersService usersService,
            IMapper mapper)
        {
            this.accountsService = accountsService;
            this.usersService = usersService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var account = await this.accountsService.GetByCriteriaAsync(w => w.AccountId == id && !w.Deleted);

            if (account == null)
            {
                this.NotFound();
            }

            return this.Ok(this.mapper.Map<AccountDto>(account));
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(AccountDto accountDto)
        {
            var account = this.mapper.Map<Account>(accountDto);
            var user = await this.usersService.GetUserByCurrentSessionToken();

            account.AccountId = Guid.NewGuid();
            account.CreationDate = DateTime.Now;
            account.Change = DateTime.Now;
            account.UserId = user.UserId; ;

            await this.accountsService.AddAsync(account);

            return this.Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(AccountDto accountDto)
        {
            var account = this.mapper.Map<Account>(accountDto);

            account.Change = DateTime.Now;

            await this.accountsService.UpdateAsync(account);

            return this.Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var account = await this.accountsService.GetByCriteriaAsync(w => w.AccountId == id);

            account.Deleted = true;

            await this.accountsService.UpdateAsync(account);

            return this.Ok();
        }

        [HttpGet, Route("api/Accounts/paginate")]
        public async Task<IHttpActionResult> Paginate()
        {
            var user = await this.usersService.GetUserByCurrentSessionToken();

            var accounts = (await this.accountsService
                .GetAllByCriteriaAsync(w => w.UserId == user.UserId && !w.Deleted))
                .OrderByDescending(w => w.CreationDate);

            return this.Ok(this.mapper.Map<List<AccountDto>>(accounts));
        }

        [HttpGet, Route("api/Accounts/combobox")]
        public async Task<IHttpActionResult> GetCombobox()
        {
            var user = await this.usersService.GetUserByCurrentSessionToken();

            var accounts = (await this.accountsService
                .GetAllByCriteriaAsync(w => w.UserId == user.UserId && !w.Deleted))
                .OrderByDescending(w => w.CreationDate);

            var result = accounts.Select(w => new
            {
                w.AccountId,
                w.Broker
            }).ToList();

            return this.Ok(result);
        }
    }
}