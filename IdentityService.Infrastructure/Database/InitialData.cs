using IdentityService.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.Database
{
    internal static class InitialData
    {
        public static void CreateInitalData(DbContext context)
        {
            var user = createAnUser();
            context.Add<User>(user);
            context.SaveChanges();
        }

        private static User createAnUser()
        {
            return new User("Huy", "Huynh", "huy.huynhducquang@plenti.com.au", "huyhuynhducquang" ,"abc1232131", "");
        }
    }
}
