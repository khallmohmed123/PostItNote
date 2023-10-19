using PostItNote.DataAccess.Data;
using PostItNote.DataAccess.Repository.IRepository;
using PostItNote.Models;
namespace PostItNote.DataAccess.Repository
{
    public class NoteRepository: Repository<Note>, INoteRepository
    {
        private readonly ApplicationDbContext _db;
        public NoteRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void update(Note obj)
        {
            _db.Notes.Update(obj);
        }
    }
}
