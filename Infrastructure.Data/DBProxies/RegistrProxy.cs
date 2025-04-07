using System.Net.Http.Json;
using Domain.DTO.DTO.MailComp;
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
    
    public async Task<Guid?> Registr(RegisterDto registerDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/register/create", registerDto);

            if (!response.IsSuccessStatusCode)
            {
                _logger.Warning("RegisterUser request failed with status code {StatusCode}", response.StatusCode);
                return null;
            }

            Guid? result = await response.Content.ReadFromJsonAsync<Guid>();
            return result;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Exception in RegisterUser");
            return null;
        }
    }
    public async Task<bool> Addmail(ADDMailDto mailDto)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync("api/register/Mail", mailDto);

            if (!response.IsSuccessStatusCode)
            {
                _logger.Warning("Mail regist failed with {StatusCode}", response.StatusCode);
                return false;
            }

            var result = await response.Content.ReadFromJsonAsync<RegisterResponse>();
            return result?.Message == "Mail regist successfully";
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Exception in Mail regist");
            return false;
        }
    }
}