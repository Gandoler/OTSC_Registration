using Entities.Templates;
using Swashbuckle.AspNetCore.Filters;

namespace RegistrationApi.Examples;

public class RegisterInAppExample : IExamplesProvider<RegisterDto>
{
    public RegisterDto GetExamples()
    {
        return new RegisterDto
        {
          
            Login = "jenka228337",
            Password = "234567password",
            Email = "jeneka228337@gmail.com",
        };
    }
}