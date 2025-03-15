using Domain.Dto.DTO.response;
using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;

namespace RegistrationApi;
[ApiController]
[Route("api")]
public class ControllerApi: ControllerBase
{
    private readonly IRegistrService _registrService;

    public ControllerApi(IRegistrService registrService)
    {
        _registrService = registrService;
    }
    
    [HttpPost("RegistrationUser")] 
    public async Task<IActionResult> RegisterUser([FromBody] RegisterDto dto)
    {
        RegisterAnswers result = await _registrService.RegistrUserToApp(dto);
        return result switch
        {
            RegisterAnswers.UserHasBeenRegistered => Ok(new { message = "User registered successfully" }),
            RegisterAnswers.UserHasNotBeenRegistered => BadRequest(new { message = "Registration failed" }),
            RegisterAnswers.UserExist => Conflict(new { message = "User already exists" }),
            _ => StatusCode(500, new { message = "Unexpected error occurred" }) // на всякий случай
        };
       
    }

}