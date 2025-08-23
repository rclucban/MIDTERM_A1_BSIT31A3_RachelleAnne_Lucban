using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            var books = BookService.Instance.GetBooks();
            return View(books);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            BookService.Instance.AddBook(model);
            return RedirectToAction("Index");
        }

     
        public IActionResult EditModal(Guid id)
        {
            var editBookViewModel = BookService.Instance.GetBookById(id);
            if (editBookViewModel == null) return NotFound();

          
            return PartialView("_EditBookPartial", editBookViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditBookViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                // If model state is not valid, you can return a view with validation errors
                return BadRequest(ModelState);
            }

            // Assuming BookService has a method to update the book
            BookService.Instance.UpdateBook(vm);

            return Ok();
        }

        public IActionResult DeleteModal(Guid id)
        {
            ViewBag.BookId = id;
            return PartialView("_DeleteBookPartial");
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            // Assuming BookService has a method to delete the book
            BookService.Instance.DeleteBook(id);
            return Ok();
        }

        public IActionResult Details(Guid id)
        {
            var book = BookService.Instance.GetBooks().First(b => b.BookId == id);
            return View(book);
        }

        public IActionResult AddBookCopyModal(Guid id)
        {
            var book = BookService.Instance.GetBooks().FirstOrDefault(b => b.BookId == id);
            if (book == null) return NotFound();

            var viewModel = new AddBookCopyViewModel
            {
                BookId = id,
                BookTitle = book.Title
            };

            return PartialView("_AddBookCopyPartial", viewModel);
        }

        [HttpPost]
        public IActionResult AddBookCopy(AddBookCopyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BookService.Instance.AddBookCopy(model.BookId, model.CoverImageUrl!, model.Condition!, model.Source!);
            return Ok();
        }


    }
}
