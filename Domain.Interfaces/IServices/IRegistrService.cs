using Domain.Dto.DTO.response;
using Entities.Templates;

namespace Domain.Interfaces.IServices;

public interface IRegistrService
{
    Task<RegisterAnswers> RegistrUserToApp(RegisterDto registerDto);
}