using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using Gauss.Investment.Application.UseCases.User.ChangePassword;
using Gauss.Investment.Communication.Requests;
using Gauss.Investment.Exceptions;
using Gauss.Investment.Exceptions.ExceptionsBase;

namespace UseCase.Test.User.ChangePassword
{
    public class ChangePasswordUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, var password) = UserBuilder.Build();

            var request = RequestChangePasswordBuilder.Build();
            request.Password = password;

            var useCase = CreateUseCase(user);

            Func<Task> act = async () => await useCase.Execute(request);

            await act.Should().NotThrowAsync();

            var passwordEncripter = PasswordEncripterBuilder.Build();

            user.Password.Should().Be(passwordEncripter.Encrypt(request.NewPassword));
        }

        [Fact]
        public async Task Error_New_Passwor_Empty()
        {
            (var user, var password) = UserBuilder.Build();

            var request = new RequestChangePassword
            {
                Password = password,
                NewPassword = string.Empty
            };

            var useCase = CreateUseCase(user);

            Func<Task> act = async () => { await useCase.Execute(request); };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMessages.Count == 1 &&
                e.ErrorMessages.Contains(ResourceMesssagesException.PASSWORD_EMPTY));

            var passwordEncripter = PasswordEncripterBuilder.Build();

            user.Password.Should().Be(passwordEncripter.Encrypt(password));
        }

        [Fact]
        public async Task Error_Current_Different()
        {
            (var user, var password) = UserBuilder.Build();

            var request = RequestChangePasswordBuilder.Build();

            var useCase = CreateUseCase(user);

            Func<Task> act = async () => { await useCase.Execute(request); };

            await act.Should().ThrowAsync<ErrorOnValidationException>()
                .Where(e => e.ErrorMessages.Count == 1 &&
                e.ErrorMessages.Contains(ResourceMesssagesException.PASSWORD_DIFFERENT_CURRENT_PASSWORD));

            var passwordEncripter = PasswordEncripterBuilder.Build();

            user.Password.Should().Be(passwordEncripter.Encrypt(password));
        }

        private static ChangePasswordUseCase CreateUseCase(Gauss.Investment.Domain.Entities.User user)
        {
            var unitOfWork = UnitOfWorkBuilder.Build();
            var userUpdateRepository = new UserUpdateOnlyRepositoryBuilder().GetById(user).Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var passwordEncripter = PasswordEncripterBuilder.Build();

            return new ChangePasswordUseCase(loggedUser, userUpdateRepository, unitOfWork, passwordEncripter);
        }
    }
}
