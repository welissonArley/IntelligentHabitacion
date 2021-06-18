using Homuai.Application.Services.Token;
using Homuai.Infrastructure.DataAccess;
using Useful.ToTests.Builders.TokenController;
using WebApi.Test.Builder;

namespace WebApi.Test
{
    public class ContextSeedInMemory
    {
        public static void Seed(HomuaiContext context)
        {
            EntityBuilder.Start();
            var tokenController = TokenControllerBuilder.Instance().Build();

            AddUserWithoutHome(context, tokenController);

            context.SaveChanges();
        }

        private static void AddUserWithoutHome(HomuaiContext context, TokenController tokenController)
        {
            context.Users.Add(EntityBuilder.UserWithoutHome);
            context.Tokens.Add(new Homuai.Domain.Entity.Token
            {
                Id = 1,
                UserId = EntityBuilder.UserWithoutHome.Id,
                Value = tokenController.Generate(EntityBuilder.UserWithoutHome.Email)
            });
        }
    }
}
