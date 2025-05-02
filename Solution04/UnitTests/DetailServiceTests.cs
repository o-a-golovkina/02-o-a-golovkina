using DataAccessLayer;
using DomainTables;
using Microsoft.EntityFrameworkCore;

namespace UnitTests
{
    public class DetailServiceTests
    {
        private AppDbContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // унікальна база
                .Options;

            return new AppDbContext(options);
        }


        [Fact]
        public void AddDetail_Should_Add_One_Record()
        {
            using var context = GetInMemoryContext();
            var service = new DetailService(context);

            var detail = new Detail { Name = "Test", Brand = "A1", Mass = 1.234 };
            service.AddDetail(detail);

            Assert.Single(context.Details.ToList());
        }

        [Fact]
        public void GetAllDetails_Should_Return_All()
        {
            using var context = GetInMemoryContext();
            var service = new DetailService(context);

            context.Details.Add(new Detail { Name = "D1", Brand = "X", Mass = 1 });
            context.Details.Add(new Detail { Name = "D2", Brand = "Y", Mass = 2 });
            context.SaveChanges();

            var list = service.GetAllDetails();
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void UpdateDetail_Should_Change_Name()
        {
            using var context = GetInMemoryContext();
            var service = new DetailService(context);

            var detail = new Detail { Name = "Old", Brand = "M", Mass = 3 };
            context.Details.Add(detail);
            context.SaveChanges();

            detail.Name = "New";
            service.UpdateDetail(detail);

            var updated = context.Details.First();
            Assert.Equal("New", updated.Name);
        }

        [Fact]
        public void DeleteDetail_Should_Remove()
        {
            using var context = GetInMemoryContext();
            var service = new DetailService(context);

            var detail = new Detail { Name = "DeleteMe", Brand = "D", Mass = 5 };
            context.Details.Add(detail);
            context.SaveChanges();

            service.DeleteDetail(detail.CodeDetail);
            Assert.Empty(context.Details.ToList());
        }
    }
}
