using DomainTables;

namespace DataAccessLayer
{
    public class OperationService
    {
        private readonly AppDbContext _context;

        public OperationService(AppDbContext context)
        {
            _context = context;
        }

        public void AddOperation(Operation operation)
        {
            _context.Operations.Add(operation);
            _context.SaveChanges();
        }

        public List<Operation> GetAllOperations()
        {
            return _context.Operations.ToList();
        }

        public void UpdateOperation(Operation updated)
        {
            _context.Operations.Update(updated);
            _context.SaveChanges();
        }

        public void DeleteOperation(int id)
        {
            var operation = _context.Operations.Find(id);
            if (operation != null)
            {
                _context.Operations.Remove(operation);
                _context.SaveChanges();
            }
        }
    }
}
