using DomainTables;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class ProductionService
    {
        private readonly AppDbContext _context;

        public ProductionService(AppDbContext context)
        {
            _context = context;
        }

        public void AddProduction(Production production)
        {
            _context.Productions.Add(production);
            _context.SaveChanges();
        }

        public List<Production> GetAllProductions()
        {
            return _context.Productions
                .Include(p => p.Detail)
                .Include(p => p.Operation)
                .ToList();
        }

        public void UpdateProduction(Production updated)
        {
            _context.Productions.Update(updated);
            _context.SaveChanges();
        }

        public void DeleteProduction(int detailId, int operationNumber)
        {
            var production = _context.Productions
                .FirstOrDefault(p => p.DetailId == detailId && p.OperationNumber == operationNumber);

            if (production != null)
            {
                _context.Productions.Remove(production);
                _context.SaveChanges();
            }
        }
    }
}
