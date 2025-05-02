using DataAccessLayer;
using DomainTables;
using Microsoft.EntityFrameworkCore;

namespace UnitTests
{
    public class ProductionServiceTests
    {
        private AppDbContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "ProductionTestDb")
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public void AddProduction_Should_Add()
        {
            using var context = GetInMemoryContext();
            var service = new ProductionService(context);

            context.Details.Add(new Detail { CodeDetail = 1, Name = "Detail", Brand = "A", Mass = 1.0 });
            context.Operations.Add(new Operation { CodeOperation = 10, ShopNumber = 1, Time = 2, Price = 100 });
            context.SaveChanges();

            var production = new Production { CodeDetail = 1, CodeOperation = 10, OperationNumber = 1 };
            service.AddProduction(production);

            Assert.Single(context.Productions.ToList());
        }

        [Fact]
        public void GetAllProductions_Should_Return_All()
        {
            using var context = GetInMemoryContext();
            var service = new ProductionService(context);

            context.Details.Add(new Detail { CodeDetail = 1, Name = "D", Brand = "X", Mass = 1.0 });
            context.Operations.Add(new Operation { CodeOperation = 2, ShopNumber = 2, Time = 2, Price = 20 });
            context.Productions.Add(new Production { CodeDetail = 1, OperationNumber = 1, CodeOperation = 2 });
            context.SaveChanges();

            var result = service.GetAllProductions();
            Assert.Single(result);
        }

        [Fact]
        public void DeleteProduction_Should_Remove()
        {
            using var context = GetInMemoryContext();
            var service = new ProductionService(context);

            context.Details.Add(new Detail { CodeDetail = 1, Name = "D", Brand = "X", Mass = 1.0 });
            context.Operations.Add(new Operation { CodeOperation = 2, ShopNumber = 2, Time = 2, Price = 20 });
            context.Productions.Add(new Production { CodeDetail = 1, OperationNumber = 1, CodeOperation = 2 });
            context.SaveChanges();

            service.DeleteProduction(1, 1);
            Assert.Empty(context.Productions.ToList());
        }
    }
}
