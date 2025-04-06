using Domain.Dto.DTO.response;
using Domain.Interfaces.IServices;
using Entities.Templates;
using Microsoft.AspNetCore.Mvc;
using RegistrationApi.Examples;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace RegistrationApi;

[ApiController]
[Route("api/Register")]
public class ControllerApi : ControllerBase
{
    private readonly IRegistrService _registrService;

    public ControllerApi(IRegistrService registrService)
    {
        _registrService = registrService;
    }

    /// <summary>
    /// Регистрирует нового пользователя.
    /// </summary>
    [HttpPost("RegistrationUser")]
    [SwaggerOperation(Summary = "Регистрация пользователя", Description = "Регистрирует нового пользователя с указанными данными.")]
    [SwaggerRequestExample(typeof(RegisterDto), typeof(RegisterInAppExample))]
    [SwaggerResponse(200, "Пользователь успешно зарегистрирован")]
    [SwaggerResponse(400, "Ошибка при регистрации")] 
    [SwaggerResponse(409, "Пользователь уже существует")] 
    [SwaggerResponse(500, "Неожиданная ошибка")] 
    public async Task<IActionResult> RegisterUser([FromBody] RegisterDto dto)
    {
        RegisterAnswers result = await _registrService.RegistrUserToApp(dto);
        return result switch
        {
            RegisterAnswers.UserHasBeenRegistered => Ok(new { message = "User registered successfully" }),
            RegisterAnswers.UserHasNotBeenRegistered => BadRequest(new { message = "Registration failed" }),
            RegisterAnswers.UserExist => Conflict(new { message = "User already exists" }),
            _ => StatusCode(500, new { message = "Unexpected error occurred" })
        };
    }
}