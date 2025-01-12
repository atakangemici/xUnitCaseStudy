using static xUnitCaseStudy.App.IUserRegister;

namespace xUnitCaseStudy.App
{
    public class UserRegister : IUserRegister
    {
        private readonly IUserInput _userInput;
        private readonly IUserOutput _userOutput;

        public UserRegister(IUserInput userInput, IUserOutput userOutput)
        {
            _userInput = userInput;
            _userOutput = userOutput;
        }

        public void RegisterUser()
        {
            _userOutput.WriteOutput("Enter your name:");
            string name = _userInput.GetInput();

            if (string.IsNullOrEmpty(name) || name.Length < 3)
            {
                _userOutput.WriteOutput("Name is invalid.");
                return;
            }

            _userOutput.WriteOutput("Enter your surname:");
            string surname = _userInput.GetInput();

            if (string.IsNullOrEmpty(surname) || surname.Length < 3)
            {
                _userOutput.WriteOutput("Surname is invalid.");
                return;
            }

            _userOutput.WriteOutput("Enter your phone number:");
            string phone = _userInput.GetInput();

            if (phone.Length < 10)
            {
                _userOutput.WriteOutput("Phone number is invalid.");
                return;
            }

            _userOutput.WriteOutput("Enter your password:");
            string password = _userInput.GetInput();

            if (password.Length < 8)
            {
                _userOutput.WriteOutput("Password is invalid.");
                return;
            }

            _userOutput.WriteOutput("Registration successful.");
        }
    }
}
