namespace FManager.Web.Mappers
{
    using AutoMapper;
    using FManager.Core.Dtos.ManageBO;
    using FManager.Data.Entities.ManageBO;
    using System.Linq;

    public class SectionsMapper : Profile
    {
        public SectionsMapper()
        {
            CreateMap<Section, SectionDto>()
                .ForMember(dest => dest.AccountBroker, opt => opt.MapFrom(w => w.Account.Broker))
                .ForMember(dest => dest.CurrencyName, opt => opt.MapFrom(w => w.Account.Currency.Name))
                .ForMember(dest => dest.DateAndTime, opt => opt.MapFrom(w => w.Entries.OrderBy(x => x.DateAndTime).FirstOrDefault().DateAndTime))
                .ForMember(dest => dest.TotalEntries, opt => opt.MapFrom(w => w.Entries.Where(x => !x.Deleted).Count()));

            CreateMap<SectionDto, Section>()
                .ForMember(dest => dest.Account, opt => opt.Ignore());
        }
    }
}
