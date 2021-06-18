using Homuai.Infrastructure.DataAccess;

namespace WebApi.Test
{
    public class ContextSeedInMemory
    {
        public static void Seed(HomuaiContext context)
        {
            context.SaveChanges();
        }
    }
}
