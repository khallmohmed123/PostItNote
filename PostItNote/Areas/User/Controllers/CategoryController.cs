using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostItNote.DataAccess.Repository.IRepository;
using PostItNote.Models;
using System.Security.Claims;
namespace PostItNote.Areas.User.Controllers
{
    [Area("User")]
    public class CategoryController : Controller
    {
        readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        public IActionResult Index()
        {
            var ClaimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var user_id=claim.Value;
            IEnumerable<Category> categories= _unitOfWork.category.GetAll(u=>u.user_id==user_id);
            return View(categories);
        }
        public IActionResult create() 
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.category.Add(obj);
                _unitOfWork.save();
            }
            else
            {

            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult update(int? id)
        {
            var category= _unitOfWork.category.GetFirstOrDefault(u => u.Id == id);
            return View(category);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult update(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.category.update(obj);
                _unitOfWork.save();
            }
            else
            {

            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult delete(int?id) {
            Category category = _unitOfWork.category.GetFirstOrDefault(u => u.Id == id);
            return View(category);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult delete(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.category.Remove(category);
                _unitOfWork.save();
            }
            else
            {

            }
            return RedirectToAction(nameof(Index));
        }
    }
}
