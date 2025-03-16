using Domain.Dto.DTO.response;
using Domain.Interfaces.IDBPROXIES;
using Domain.Interfaces.IServices;
using Entities.Templates;
using Serilog;

namespace Infrastrucutre.Buisnes.Services;

public class RegistrService:IRegistrService
{
    private readonly ILogger _logger;
    private readonly ICheckExistProxy _checkExistProxy;
    private readonly IRegistrProxy _registrProxy;

    public RegistrService(ILogger logger, ICheckExistProxy checkExistProxy, IRegistrProxy registrProxy)
    {
        _logger = logger;
        _checkExistProxy = checkExistProxy;
        _registrProxy = registrProxy;
    }
    public async Task<RegisterAnswers> RegistrUserToApp(RegisterDto registerDto)
    {
        
        if (await _checkExistProxy.CheckExist(new CheckExistDto { Email = registerDto.Login }))//тут тупейшая ошибка пошедшая с тупым дто
        {
            if (await _registrProxy.Registr(registerDto))
            {
                return RegisterAnswers.UserHasBeenRegistered;
            }
            return RegisterAnswers.UserHasNotBeenRegistered;
        }
        return RegisterAnswers.UserExist;
    }
}