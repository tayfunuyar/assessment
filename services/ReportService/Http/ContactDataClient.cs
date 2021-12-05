using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ReportService.Dtos;

namespace ReportService.Http
{
    public class ContactDataClient : IContactDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ContactDataClient(HttpClient client, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = client;
        }
        public async Task<IEnumerable<ContactInformationReadDto>> GetContactInformations()
        {
            List<ContactInformationReadDto> contactInformationList = new List<ContactInformationReadDto>();
            var response = await _httpClient.GetAsync($"{_configuration["ContactService"]}/api/ContactInformation/GetAllContactInformation");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                contactInformationList = JsonConvert.DeserializeObject<List<ContactInformationReadDto>>(responseData);
            }
            return contactInformationList;
        }
    }
}