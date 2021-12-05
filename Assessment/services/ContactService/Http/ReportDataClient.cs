using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ContactService.Dtos;
using ContactService.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

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
        public async Task<GenericResponse<string>> SendReportRequest(ReportRequestDto reportRequestDto)
        {
               var httpContent = new StringContent(
                JsonConvert.SerializeObject(reportRequestDto),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync($"{_configuration["ReportService"]}/api/Report", httpContent);
            if (response.IsSuccessStatusCode)
            {
                 var data = await response.Content.ReadAsStringAsync();
                 var genericData = JsonConvert.DeserializeObject<GenericResponse<string>>(data);
                 return GenericResponse<string>.SuccessResponse("",genericData.Response);
            }
            else
            {
                return GenericResponse<string>.ErrorResponse(response.ReasonPhrase);
            }

           
        }

      
    }
}