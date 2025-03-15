using System.Net.Http.Json;
using Domain.Dto.DTO.response;
using Domain.Interfaces.IDBPROXIES;
using Entities.Templates;
using Serilog;

namespace Infrastructure.Data.DBProxies;

public class RegistrProxy: IRegistrProxy
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;

    public RegistrProxy(HttpClient httpClient, ILogger logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    
    public async Task<bool> Registr(RegisterDto registerDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/register/create", registerDto);

            if (!response.IsSuccessStatusCode)
            {
                _logger.Warning("RegisterUser request failed with status code {StatusCode}", response.StatusCode);
                return false;
            }

            var result = await response.Content.ReadFromJsonAsync<RegisterResponse>();
            return result?.Message == "User registered successfully";
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Exception in RegisterUser");
            return false;
        }
    }
}