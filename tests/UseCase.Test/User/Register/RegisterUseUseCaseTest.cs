using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using Gauss.Investment.Application.UseCases.User.Register;
using Gauss.Investment.Exceptions;
using Gauss.Investment.Exceptions.ExceptionsBase;

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

        [Fact]
        public async Task Error_Email_already_registered()
        {
            var request = RequestRegisterUserBuilder.Build();
            var useCase = CreateUseCase(request.Email);

            Func<Task> act = async () => await useCase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMesssagesException.EMAIL_ALREADY_REGISTERED));
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestRegisterUserBuilder.Build();
            request.Name = string.Empty;

            var useCase = CreateUseCase();

            Func<Task> act = async () => await useCase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMesssagesException.NAME_EMPTY));
        }

        private RegisterUseUseCase CreateUseCase(string? email = null)
        {
            var mapper = MapperBuilder.Build();
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();

            if (string.IsNullOrEmpty(email) ==  false)
                readRepositoryBuilder.ExistActiveUserWithEmail(email);

            return new RegisterUseUseCase(writeRepository, readRepositoryBuilder.Build(), unitOfWork, passwordEncripter, mapper);
        }
    }
}
