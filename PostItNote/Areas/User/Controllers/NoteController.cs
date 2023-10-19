using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PostItNote.DataAccess.Repository.IRepository;
using PostItNote.Models;
using PostItNote.Models.ViewModels;
using System.Net.Sockets;
using System.Security.Claims;

namespace PostItNote.Areas.User.Controllers
{
    [Area("User")]
    public class NoteController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public NoteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var ClaimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var user_id = claim.Value;
            IEnumerable<Note> notes = _unitOfWork.Note.GetAll(u => u.User_id == user_id, includeproperties: "User,Category");
            List<NoteVm> noteVms = new List<NoteVm>();
            NoteVm noteVm;
            foreach (var item in notes)
            {
                noteVm = new NoteVm()
                {
                    note = item,
                    Tickets = _unitOfWork.Ticket.GetAll(u => u.Note_id == item.Id)
                };
                noteVms.Add(noteVm);
            }
            return View(noteVms);
        }
        public IActionResult create()
        {
            IEnumerable<SelectListItem> CategorList = _unitOfWork.category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return View(CategorList);
        }
        [HttpPost]
        public IActionResult create_data()
        {
            //Request.
            {
                MemoryStream stream = new MemoryStream();
                Request.Body.CopyToAsync(stream);
                stream.Position = 0;
                var ClaimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                var user_id = claim.Value;
                using (StreamReader reader = new StreamReader(stream))
                {
                    string requestBody = reader.ReadToEnd();
                    if (requestBody.Length > 0)
                    {
                        var obj = JsonConvert.DeserializeObject<NoteVm>(requestBody);
                        if (obj != null)
                        {
                            obj.note.User_id = user_id;
                            _unitOfWork.Note.Add(obj.note);
                            int id = obj.note.Id;
                            foreach (Ticket ticket in obj.Tickets)
                            {
                                ticket.Note_id = id;
                                _unitOfWork.Ticket.Add(ticket);
                            }
                            _unitOfWork.save();
                        }
                    }
                    else
                    {
                        return NotFound("error data");
                    }
                }
            }
            return Ok("hello okay");
        }
        public IActionResult NoteView(int id)
        {
            var ClaimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var user_id = claim.Value;
            Note notes = _unitOfWork.Note.GetFirstOrDefault(u => u.User_id == user_id && u.Id == id, includeproperties: "User,Category");
            NoteVm noteVm = new NoteVm()
            {
                note = notes,
                Tickets = _unitOfWork.Ticket.GetAll(u => u.Note_id == notes.Id)
            };
            return View(noteVm);
        }
        public IActionResult NoteEdit()
        {
            return View();
        }
        public IActionResult NoteDelete()
        {
            return View();
        }
        [HttpPost]
        public IActionResult create_data_single()
        {
            {
                MemoryStream stream = new MemoryStream();
                Request.Body.CopyToAsync(stream);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    string requestBody = reader.ReadToEnd();
                    if (requestBody.Length > 0)
                    {
                        var obj = JsonConvert.DeserializeObject<Ticket>(requestBody);
                        if (obj != null)
                        {
                            if(obj.Id>0)
                            _unitOfWork.Ticket.update(obj);
                            _unitOfWork.save();
                        }
                        return Ok("hello okay");
                    }
                    else
                    {
                        return NotFound("error data");
                    }

                }
            }
            
        }
        [HttpPost("{ticket_id:int}")]
        public IActionResult delete_data_single(int ticket_id) { 
        
        return Ok("hello okay");
        }
    }
}
