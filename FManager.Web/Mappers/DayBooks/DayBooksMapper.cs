namespace FManager.Web.Mappers
{
    using AutoMapper;
    using FManager.Core.Dtos.DayBooks;
    using FManager.Data.Entities.DayBooks;

    public class DayBooksMapper : Profile
    {
        public DayBooksMapper()
        {
            CreateMap<DayBook, DayBookDto>().ReverseMap();
        }
    }
}
