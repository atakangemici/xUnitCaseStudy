using Microsoft.Extensions.DependencyInjection;
using xUnitCaseStudy.App;

namespace MyApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<IUserInput, UserInput>()
                .AddTransient<IUserOutput, UserOutput>()
                .AddTransient<IUserRegister, UserRegister>()
                .BuildServiceProvider();

            var userRegistration = serviceProvider.GetService<IUserRegister>();
            userRegistration.RegisterUser();
        }
    }

    public class UserInput : IUserInput
    {
        public string GetInput()
        {
            return Console.ReadLine();
        }
    }

    public class UserOutput : IUserOutput
    {
        public void WriteOutput(string message)
        {
            Console.Write(message);
        }
    }
}
