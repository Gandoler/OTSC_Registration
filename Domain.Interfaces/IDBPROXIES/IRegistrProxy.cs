using Domain.DTO.DTO.MailComp;
using Entities.Templates;

namespace Domain.Interfaces.IDBPROXIES;

public interface IRegistrProxy
{
    Task<Guid?> Registr(RegisterDto registerDto);
    Task<bool> Addmail(ADDMailDto registerDto);
}