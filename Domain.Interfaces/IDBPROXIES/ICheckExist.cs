using Entities.Templates;

namespace Domain.Interfaces.IDBPROXIES;

public interface ICheckExist
{
    Task<bool> CheckExist(CheckExistDto checkExistDto);
}