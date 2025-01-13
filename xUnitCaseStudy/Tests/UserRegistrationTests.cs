using Moq;
using System;
using System.Diagnostics.Metrics;
using Xunit;
using xUnitCaseStudy.App;

namespace xUnitCaseStudy.Tests
{
    public class UserRegistrationTests
    {
        //Mocking işlemleri,Mock(Sahte) Instance yaratma
        private readonly Mock<IUserInput> _mockUserInput;
        private readonly Mock<IUserOutput> _mockUserOutput;

        public UserRegistrationTests()
        {
            _mockUserInput = new Mock<IUserInput>();
            _mockUserOutput = new Mock<IUserOutput>();
        }

        [Fact]
        public void RegisterUser_ShouldPrintNameInvalid_WhenNameIsTooShort()
        {
            //Arrange
            _mockUserInput.SetupSequence(input => input.GetInput())
                     .Returns("A") //Name is short.
                     .Returns("Gemici")
                     .Returns("5368672145")
                     .Returns("12345678");

            var userRegistration = new UserRegister(_mockUserInput.Object, _mockUserOutput.Object);

            //Act
            userRegistration.RegisterUser();

            //Assert 
            _mockUserOutput.Verify(output => output.WriteOutput("Name is invalid."));
        }

        [Fact]
        public void RegisterUser_ShouldPrintSurnameInvalid_WhenSurnameIsTooShort()
        {
            // Arrange
            _mockUserInput.SetupSequence(input => input.GetInput())
                     .Returns("Atakan")
                     .Returns("G") //Surname is short.
                     .Returns("5368672145")
                     .Returns("12345678");

            var userRegistration = new UserRegister(_mockUserInput.Object, _mockUserOutput.Object);

            // Act
            userRegistration.RegisterUser();

            // Assert
            _mockUserOutput.Verify(output => output.WriteOutput("Surname is invalid."));
        }

        [Fact]
        public void RegisterUser_ShouldPrintPhoneInvalid_WhenPhoneIsIncorrect()
        {
            // Arrange
            _mockUserInput.SetupSequence(input => input.GetInput())
                     .Returns("Atakan")
                     .Returns("Gemici")
                     .Returns("536867") //Invalid telephone number.
                     .Returns("12345678");

            var userRegistration = new UserRegister(_mockUserInput.Object, _mockUserOutput.Object);

            // Act
            userRegistration.RegisterUser();

            // Assert
            _mockUserOutput.Verify(output => output.WriteOutput("Phone number is invalid."));
        }

        [Fact]
        public void RegisterUser_ShouldPrintPasswordInvalid_WhenPasswordIsTooWeak()
        {
            // Arrange
            _mockUserInput.SetupSequence(input => input.GetInput())
                     .Returns("Atakan")
                     .Returns("Gemici")
                     .Returns("5368672145")
                     .Returns("1234567"); // Weak password

            var userRegistration = new UserRegister(_mockUserInput.Object, _mockUserOutput.Object);

            // Act
            userRegistration.RegisterUser();

            // Assert
            _mockUserOutput.Verify(output => output.WriteOutput("Password is invalid."));
        }

        [Fact]
        public void RegisterUser_ShouldPrintRegistrationSuccessful_WhenAllInputsAreValid()
        {
            // Arrange
            _mockUserInput.SetupSequence(input => input.GetInput())
                     .Returns("John")
                     .Returns("Doe")
                     .Returns("+1234567890")
                     .Returns("Password123");

            var userRegistration = new UserRegister(_mockUserInput.Object, _mockUserOutput.Object);

            // Act
            userRegistration.RegisterUser();

            // Assert
            _mockUserOutput.Verify(output => output.WriteOutput("Registration successful."));
        }
    }
}
