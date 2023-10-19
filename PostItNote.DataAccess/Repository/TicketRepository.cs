using PostItNote.DataAccess.Data;
using PostItNote.DataAccess.Repository.IRepository;
using PostItNote.Models;
namespace PostItNote.DataAccess.Repository
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        private readonly ApplicationDbContext _db;
        public TicketRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void update(Ticket obj)
        {
            _db.Tickets.Update(obj);
        }
    }
}
