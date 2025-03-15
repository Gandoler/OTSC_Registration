using System.Net.Http.Json;
using Domain.Dto.DTO.response;
using Domain.Interfaces.IDBPROXIES;
using Entities.Templates;
using Serilog;

namespace Infrastructure.Data.DBProxies;

public class CheckExistProxy: ICheckExistProxy
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;

    public CheckExistProxy(HttpClient httpClient, ILogger logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    
    public async Task<bool> CheckExist(CheckExistDto checkExistDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/register/exists", checkExistDto);
            
            if (!response.IsSuccessStatusCode)
            {
                _logger.Warning("CheckExist request failed with status code {StatusCode}", response.StatusCode);
                return false;
            }

            var result = await response.Content.ReadFromJsonAsync<CheckExistResponse>();
            return result?.Exists ?? false;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Exception in CheckExist");
            return false;
        }
    }
}