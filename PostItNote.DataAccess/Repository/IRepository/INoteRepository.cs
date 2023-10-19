using PostItNote.Models;
namespace PostItNote.DataAccess.Repository.IRepository
{
    public interface INoteRepository : IRepository<Note>
    {
        void update(Note obj);
    }
}
