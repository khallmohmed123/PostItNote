namespace PostItNote.Models.ViewModels
{
    public class NoteVm
    {
        public Note note { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; }
    }
}
