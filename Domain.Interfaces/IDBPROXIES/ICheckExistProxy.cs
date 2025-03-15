using Entities.Templates;

namespace Domain.Interfaces.IDBPROXIES;

public interface ICheckExistProxy
{
    Task<bool> CheckExist(CheckExistDto checkExistDto);
}