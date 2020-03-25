namespace FManager.Web.Mappers
{
    using AutoMapper;
    using FManager.Core.Dtos.ManageBO;
    using FManager.Data.Entities.ManageBO;

    public class AccountsMapper : Profile
    {
        public AccountsMapper()
        {
            CreateMap<Account, AccountDto>()
                .ForMember(dest => dest.CurrencyName, opt => opt.MapFrom(w => w.Currency.Name));

            CreateMap<AccountDto, Account>()
                .ForMember(dest => dest.Currency, opt => opt.Ignore());
        }
    }
}
