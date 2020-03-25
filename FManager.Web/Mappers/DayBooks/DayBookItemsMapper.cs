namespace FManager.Web.Mappers
{
    using AutoMapper;
    using FManager.Core.Dtos.DayBooks;
    using FManager.Data.Entities.DayBooks;

    public class DayBookItemsMapper : Profile
    {
        public DayBookItemsMapper()
        {
            CreateMap<DayBookItem, DayBookItemDto>();

            CreateMap<DayBookItemDto, DayBookItem>()
                .ForMember(dest => dest.DayBook, opt => opt.Ignore());
        }
    }
}
