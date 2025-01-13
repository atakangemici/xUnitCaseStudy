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

        //Bu Test Ne Yapıyor?
        //Test, kullanıcı kaydı sırasında adı çok kısa olan bir giriş için doğru hatanın yazdırıldığını doğruluyor.

        //Bu Test Neden Önemli?
        //Kullanıcıdan alınan ismin minimum uzunluk kontrolü yapılır.
        //Sistem, geçersiz girişleri doğru şekilde tespit edip, kullanıcıyı bilgilendirir.
        //Mock nesneler sayesinde, kullanıcı girdisi ve çıktısı gibi dış bağımlılıklar test ortamında izole edilir.

        //Unit Test Method Name : 3 Kısma Ayrılabilir.
        //1. Kısım : Test Edilen Metot İsmi (RegisterUser)
        //2. Kısım : Beklenen Davranış (Should Print Name Invalid)
        //3. Kısım : Testin Şartı - Condition (When Name Is Too Short)

        //Fact Attribute, xUnit test çerçevesinde kullanılan bir özelliktir ve bir metodu bir test metodu olarak işaretler.
        [Fact]
        public void RegisterUser_ShouldPrintNameInvalid_WhenNameIsTooShort()
        {
            //3A Pattern
            //Arrange ile gerekli düzenlemeler yapılır (mock nesneler ve test ortamı hazırlanır).
            //Act ile test edilen kod çalıştırılır.       
            //Assert ile beklenen sonuçların elde edilip edilmediği kontrol edilir.


            ////Arrange

            //Bu aşamada test senaryosu için gerekli olan tüm bağımlılıklar ve başlangıç durumu ayarlanır.
            //Dummy Data ile Mock Nesnelerinin Ayarlanması

            _mockUserInput.SetupSequence(input => input.GetInput())
                     .Returns("A")  // Name'in kısa olması
                     .Returns("Gemici")
                     .Returns("5368672145")
                     .Returns("12345678");

            var userRegistration = new UserRegister(_mockUserInput.Object, _mockUserOutput.Object);

            //Amaç: SetupSequence GetInput metoduna yapılan her çağrının sıralı bir girdiyi döndürmesini sağlamak.
            //Kullanıcı, adı, soyadı, telefon numarası ve şifre bilgilerini sırasıyla giriyor gibi simüle edilir.

            //Şu durumu kontrol eder:
            //Kullanıcı adı "A" olduğunda(çok kısa),
            //WriteOutput("Name is invalid.") validasyonunun çağrılıp çağrılmadığını kontrol eder.


            ////Act

            //Bu satır, test edilen kodun çağrılmasını sağlar.
            //Act bölümü, bir birim testinde işlem adımını ifade eder. İşlem, testin amacına uygun olarak
            //bir metodun çağrılmasını veya bir işlemin gerçekleştirilmesini içerir.
            userRegistration.RegisterUser();

            //// Assert 

            //Bu satır, Yazılan testin doğru şekilde çalışıp çalışmadığını doğrulamak için kullanılır.
            //output.WriteOutput "Name is invalid." mesajını, metodun çağrıldığı sırada gönderilen mesaj olduğunu ifade eder.
            _mockUserOutput.Verify(output => output.WriteOutput("Name is invalid."));

            //Eğer: Metod tam olarak doğru argümanla çağrılmışsa: Test başarılı olur.





            //Doğru Test Prensipleri:
            //Pozitif ve Negatif Senaryoları Test Etme:

            //Pozitif Senaryo: Doğru verilerle doğru çıktıyı kontrol etme.
            //Negatif Senaryo: Hatalı veya eksik verilerle beklenen hata durumunu kontrol etme.
        }

        [Fact]
        public void RegisterUser_ShouldPrintSurnameInvalid_WhenSurnameIsTooShort()
        {
            // Arrange
            _mockUserInput.SetupSequence(input => input.GetInput())
                     .Returns("Atakan")
                     .Returns("G")  // Surname'in kısa olması
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
                     .Returns("536867")  // Geçersiz telefon numarası
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
                     .Returns("1234567");  // Zayıf şifre

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
