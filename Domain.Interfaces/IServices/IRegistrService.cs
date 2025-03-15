using Entities.Templates;

namespace Domain.Interfaces.IServices;

public interface IRegistrService
{
    Task<bool> RegistrUserToApp(RegisterDto registerDto);
}