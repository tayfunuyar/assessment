using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ContactService.Dtos;
using ContactService.Models;
using Microsoft.Extensions.Configuration;

namespace ContactService.Http {
    public class ReportDataClient : IReportDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ReportDataClient(IConfiguration configuration , HttpClient client)
        {
            _configuration = configuration;
            _httpClient = client;
        }
        public async Task<GenericResponse<Guid>> SendReportRequest(ReportRequestDto reportRequestDto)
        {
               var httpContent = new StringContent(
                JsonSerializer.Serialize(reportRequestDto),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync($"{_configuration["ReportService"]}/api/Report", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync Post CommandService was OK!");
            }
            else
            {
                Console.WriteLine("--> Sync Post CommandService was NOT OK!");
            }

            return GenericResponse<Guid>.ErrorResponse("");
        }

      
    }
}