using WebApi.Test.Builder.Entities;

namespace WebApi.Test.Builder
{
    public static class EntityBuilder
    {
        public static Homuai.Domain.Entity.User UserWithoutHome { get; set; }

        public static void Start()
        {
            if (UserWithoutHome == null)
            {
                UserWithoutHome = UserBuilder.Instance().WithoutHome();
            }
        }
    }
}
