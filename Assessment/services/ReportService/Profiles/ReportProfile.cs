using AutoMapper;
using ReportService.Dtos;
using ReportService.Entities;

namespace ReportService.Profiles {
     public class ReportProfile : Profile {
            public ReportProfile()
            {
                CreateMap<ReportRequestDto,Report>();
                 CreateMap<Report,ReportReadDto>();
            }
     }
}