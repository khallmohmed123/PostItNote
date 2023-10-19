namespace PostItNote.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public ICategoryRepository category { get; }
        public INoteRepository Note { get; }
        public ITicketRepository Ticket { get; }
        void save();
    }
}
