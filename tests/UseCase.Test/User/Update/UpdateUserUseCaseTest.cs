using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using Gauss.Investment.Application.UseCases.User.Update;
using Gauss.Investment.Domain.Extensions;
using Gauss.Investment.Exceptions;
using Gauss.Investment.Exceptions.ExceptionsBase;
using UseCase.Test.LoggedUser;
using LoggedUserBuilder = CommonTestUtilities.LoggedUser.LoggedUserBuilder;

namespace UseCase.Test.User.Update
{
    public class UpdateUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, _) = UserBuilder.Build();

            var request = RequestUpdateUserBuilder.Build();

            var useCase = CreateUseCase(user);

            Func<Task> act = async () => await useCase.Execute(request);

            await act.Should().NotThrowAsync();

            user.Name.Should().Be(request.Name);
            user.Email.Should().Be(request.Email);
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            (var user, _) = UserBuilder.Build();

            var request = RequestUpdateUserBuilder.Build();
            request.Name = string.Empty;

            var useCase = CreateUseCase(user);

            Func<Task> act = async () => { await useCase.Execute(request); };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMessages.Count == 1 &&
                e.ErrorMessages.Contains(ResourceMesssagesException.NAME_EMPTY));

            user.Name.Should().NotBe(request.Name);
            user.Email.Should().NotBe(request.Name);
        }

        [Fact]
        public async Task Error_Email_Alread_Registered()
        {
            (var user, _) = UserBuilder.Build();

            var request = RequestUpdateUserBuilder.Build();

            var useCase = CreateUseCase(user, request.Email);

            Func<Task> act = async () => { await useCase.Execute(request); };

            await act.Should().ThrowAsync<ErrorOnValidationException>()
                .Where(e => e.ErrorMessages.Count == 1 &&
                e.ErrorMessages.Contains(ResourceMesssagesException.EMAIL_ALREADY_REGISTERED));

            user.Name.Should().NotBe(request.Name);
            user.Email.Should().NotBe(request.Email);
        }


        private static UpdateUserUseCase CreateUseCase(Gauss.Investment.Domain.Entities.User user, string? email = null)
        {
            var unitOfWork = UnitOfWorkBuilder.Build();
            var userUpdateRepository = new UserUpdateOnlyRepositoryBuilder().GetById(user).Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            var userReadOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
            if (string.IsNullOrEmpty(email).IsFalse())
                userReadOnlyRepositoryBuilder.ExistActiveUserWithEmail(email!);

            return new UpdateUserUseCase(loggedUser, userUpdateRepository, userReadOnlyRepositoryBuilder.Build(), unitOfWork);
        }

    }
}
