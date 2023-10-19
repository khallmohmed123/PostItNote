using PostItNote.Models;
namespace PostItNote.DataAccess.Repository.IRepository
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        void update(Ticket obj);
    }
}
