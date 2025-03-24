using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using Gauss.Investment.Application.UseCases.Login.DoLogin;
using Gauss.Investment.Communication.Requests;
using Gauss.Investment.Domain.Entities;
using Gauss.Investment.Exceptions;
using Gauss.Investment.Exceptions.ExceptionsBase;

namespace UseCase.Test.Login.DoLogin
{
    public class DoLoginUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, var password) = UserBuilder.Build();
            var useCase = CreateUseCase(user);

            var result = await useCase.Execute(new RequestLogin
            {
                Email = user.Email,
                Password = password
            });

            result.Should().NotBeNull();
            result.Name.Should().NotBeNullOrWhiteSpace().And.Be(user.Name);
        }

        [Fact]
        public async Task Error_Invalid_User()
        {
            var request = RequestLoginBuilder.Build();
            var useCase = CreateUseCase();

            Func<Task> act = async () => { await useCase.Execute(request); };

            await act.Should().ThrowAsync<InvalidLoginException>()
                .Where(e => e.Message.Equals(ResourceMesssagesException.EMAIL_OR_PASSWORD_INVALID));

        }

        private static DoLoginUseCase CreateUseCase(Gauss.Investment.Domain.Entities.User? user = null)
        {
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var userReadOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();

            if (user is not null)
                userReadOnlyRepositoryBuilder.GetUserByEmailAndPassword(user);

            return new DoLoginUseCase(userReadOnlyRepositoryBuilder.Build(), passwordEncripter);
        }
    }
}
