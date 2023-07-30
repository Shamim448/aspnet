using Autofac.Extras.Moq;
using Crud.Application.Features.Training.Repositories;
using Crud.Application.Features.Training.Services;
using Moq;

namespace Crud.Application.Tests
{
    public class AccountServiceTests
    { 
        private AutoMock _mock;
        private Mock<IAccountRepository> _accountRepositoryMock;
        private Mock<IEmailService> _emailServiceMock;
        private AccountService _accountService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mock = AutoMock.GetLoose();
        }
        [SetUp]
        public void SetUp()
        {
            _accountRepositoryMock = _mock.Mock<IAccountRepository>();
            _emailServiceMock = _mock.Mock<IEmailService>();
            _accountService  = _mock.Create<AccountService>();
        }
        [TearDown]
        public void Teardown() 
        {
            _accountRepositoryMock.Reset();
            _emailServiceMock.Reset();
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }
        [Test]
        public void CreateUser_LargeUsername_TruncateUsername()
        {
            const string username = @"mynameisshamimhosenmynameisshamimhosen
                                    mynameisshamimhosenmynameisshamimhosen";
            string expectedResult = username.Substring(0, 30);
            const string password = "fgdhgfhgngdfjkgj";

            _accountService.CreateAccount(username, password);
        }
    }
}