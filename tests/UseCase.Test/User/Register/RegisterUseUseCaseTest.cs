using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using Gauss.Investment.Application.UseCases.User.Register;

namespace UseCase.Test.User.Register
{
    public class RegisterUseUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserBuilder.Build();
            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Name.Should().Be(request.Name);
        }

        private RegisterUseUseCase CreateUseCase()
        {
            var mapper = MapperBuilder.Build();
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var readRepository = new UserReadOnlyRepositoryBuilder().Build();

            return new RegisterUseUseCase(writeRepository, readRepository, unitOfWork, passwordEncripter, mapper);
        }
    }
}
