using Gauss.Investment.Domain.Services.LoggedUser;
using Moq;

namespace UseCase.Test.LoggedUser
{
    public class LoggedUserBuilder
    {
        public static ILoggedUser Build(Gauss.Investment.Domain.Entities.User user)
        {
            var mock = new Mock<ILoggedUser>();

            mock.Setup(x => x.User()).ReturnsAsync(user);

            return mock.Object;

        }
    }
}
