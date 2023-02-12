using Api.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ApiTests.Controllers
{
    public class DatabaseConnectionFactory
    {
        /// <summary>
        /// Returns an in memory database. If <paramref name="deleteExistingDatabase"/> is
        /// true, then the returned context will have an empty database. If <paramref name="deleteExistingDatabase"/> is false, then
        /// the returned context will use an existing database if there is one.
        /// If there is no existing database, then a new database will be created.
        /// </summary>
        public static BodyFitTrackerContext GetInMemoryDatabase(bool deleteExistingDatabase = true)
        {
            var options = new DbContextOptionsBuilder<BodyFitTrackerContext>()
                .UseInMemoryDatabase(databaseName: "BodyFitContext")
                .Options;

            BodyFitTrackerContext bodyFitTrackerContext = new BodyFitTrackerContext(options);

            if (deleteExistingDatabase)
            {
                bodyFitTrackerContext.Database.EnsureDeleted();
                bodyFitTrackerContext.Database.EnsureCreated();
            }

            return bodyFitTrackerContext;
        }
    }
}
