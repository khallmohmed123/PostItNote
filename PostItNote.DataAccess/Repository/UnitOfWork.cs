using PostItNote.DataAccess.Data;
using PostItNote.DataAccess.Repository.IRepository;
namespace PostItNote.DataAccess.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository category { get; private set;}
        public INoteRepository Note { get; private set; }
        public ITicketRepository Ticket { get; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            category = new CategoryRepository(_db);
            Note = new NoteRepository(_db);
            Ticket = new TicketRepository(_db);
        }
        public void save()
        {
            _db.SaveChanges();
        }
    }
}
