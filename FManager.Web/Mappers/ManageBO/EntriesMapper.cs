namespace FManager.Web.Mappers
{
    using AutoMapper;
    using FManager.Core.Dtos.ManageBO;
    using FManager.Data.Entities.ManageBO;

    public class EntriesMapper : Profile
    {
        public EntriesMapper()
        {
            CreateMap<Entry, EntryDto>()
                .ForMember(dest => dest.ParityName, opt => opt.MapFrom(w => w.Parity.Name));

            CreateMap<EntryDto, Entry>()
                .ForMember(dest => dest.Section, opt => opt.Ignore());
        }
    }
}
