using System.Collections.Generic;
using System.Threading.Tasks;
using ReportService.Dtos;

namespace ReportService.Http {
     
     public interface IContactDataClient {
           Task<IEnumerable<ContactInformationReadDto>> GetContactInformations();
     }
}