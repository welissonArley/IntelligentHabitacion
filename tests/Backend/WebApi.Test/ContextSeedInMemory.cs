using Homuai.Infrastructure.DataAccess;
using WebApi.Test.Builder;

namespace WebApi.Test
{
    public class ContextSeedInMemory
    {
        public static void Seed(HomuaiContext context)
        {
            EntityBuilder.Start();

            context.Users.Add(EntityBuilder.UserWithoutHome);

            context.SaveChanges();
        }
    }
}
