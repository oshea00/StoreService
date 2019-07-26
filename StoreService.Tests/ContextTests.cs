using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StoreService.Web.Models;
using System.Threading.Tasks;

namespace Tests
{
    public class ContextTests
    {
        [Test]
        public async Task TopicsAreAvailable()
        {
            var builder = new DbContextOptionsBuilder<StoreContext>();
            builder.UseInMemoryDatabase<StoreContext>("topics");
            using (var context = new StoreContext(builder.Options))
            {
                context.Database.EnsureCreated();
                var topiclist = await context.Topics.ToListAsync();
                Assert.Greater(topiclist.Count, 0);
            }
        }

        [Test]
        public void IFail()
        {
            Assert.Pass();
        }
    }
}