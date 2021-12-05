using AutoMapper;
using ContactService.Dtos;
using ContactService.Entities;

namespace ContactService.Profiles
{
    public class ContactsProfile : Profile
    {
        public ContactsProfile()
        {
            CreateMap<Contact, ContactReadDto>();
            CreateMap<ContactReadDto, Contact>();
            CreateMap<ContactCreateDto, Contact>();
            CreateMap<Contact, ContactCreateDto>();
            CreateMap<ContactDto, Contact>();
            CreateMap<ContactInformationDto, ContactInformation>();
            CreateMap<ContactInformation, ContactInformationReadDto>()
            .ForMember(x => x.InformationType, opt => opt.Ignore());
            CreateMap<ContactInformationCreateDto, ContactInformation>();
        }
    }
}