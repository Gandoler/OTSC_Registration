using Entities.Templates;

namespace Domain.Interfaces.IDBPROXIES;

public interface IRegistrProxy
{
    Task<bool> Registr(RegisterDto registerDto);
}