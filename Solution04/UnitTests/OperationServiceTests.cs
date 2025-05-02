using DataAccessLayer;
using DomainTables;
using Microsoft.EntityFrameworkCore;

namespace UnitTests
{
    public class OperationServiceTests
    {
        private AppDbContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public void AddOperation_Should_Add_One_Record()
        {
            using var context = GetInMemoryContext();
            var service = new OperationService(context);

            var operation = new Operation
            {
                ShopNumber = 5,
                Time = 2,
                Price = 150.50
            };

            service.AddOperation(operation);
            Assert.Single(context.Operations.ToList());
        }

        [Fact]
        public void GetAllOperations_Should_Return_All()
        {
            using var context = GetInMemoryContext();
            var service = new OperationService(context);

            context.Operations.Add(new Operation { ShopNumber = 1, Time = 1, Price = 10 });
            context.Operations.Add(new Operation { ShopNumber = 2, Time = 2, Price = 20 });
            context.SaveChanges();

            var result = service.GetAllOperations();
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void UpdateOperation_Should_Change_Duration()
        {
            using var context = GetInMemoryContext();
            var service = new OperationService(context);

            var op = new Operation { CodeOperation = 1, ShopNumber = 1, Time = 1, Price = 10 };
            context.Operations.Add(op);
            context.SaveChanges();

            op.Time = 4;
            service.UpdateOperation(op);

            var updated = context.Operations.First();
            Assert.Equal(4, updated.Time);
        }

        [Fact]
        public void DeleteOperation_Should_Remove()
        {
            using var context = GetInMemoryContext();
            var service = new OperationService(context);

            var op = new Operation { ShopNumber = 1, Time = 1, Price = 10 };
            context.Operations.Add(op);
            context.SaveChanges();

            service.DeleteOperation(1);
            Assert.Empty(context.Operations.ToList());
        }
    }
}
