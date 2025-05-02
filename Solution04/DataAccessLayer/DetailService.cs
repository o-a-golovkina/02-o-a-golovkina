using DomainTables;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class DetailService
    {
        private readonly AppDbContext _context;

        public DetailService(AppDbContext context)
        {
            _context = context;
        }

        public void AddDetail(Detail detail)
        {
            _context.Details.Add(detail);
            _context.SaveChanges();
        }

        public List<Detail> GetAllDetails()
        {
            return _context.Details.Include(d => d.Productions).ToList();
        }

        public void UpdateDetail(Detail updated)
        {
            _context.Details.Update(updated);
            _context.SaveChanges();
        }

        public void DeleteDetail(int id)
        {
            var detail = _context.Details.Find((short)id);
            if (detail != null)
            {
                _context.Details.Remove(detail);
                _context.SaveChanges();
            }
        }
    }
}
