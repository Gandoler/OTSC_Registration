using Entities.Templates;

namespace Domain.Interfaces.IDBPROXIES;

public interface IRegistr
{
    Task<bool> Registr(RegisterDto registerDto);
}