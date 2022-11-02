using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ComicBookAPI.Models;
using ComicBookAPI.Data;
namespace ComicBookAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ComicBookController:ControllerBase
    {
        private readonly ApiContext _context;

        public ComicBookController(ApiContext context)
        {
            _context = context;
        }

        // Create/Update Request
        [HttpPost]
        public JsonResult CreateEdit(ComicBook comicBook)
        {
            if(comicBook.Id == 0)
            {
                _context.ComicBooks.Add(comicBook);
            }else
            {
                var comicInDB = _context.ComicBooks.Find(comicBook.UPC);
                if(comicInDB == null )
                    return new JsonResult(NotFound());
                comicInDB = comicBook;
            }
            _context.SaveChanges();
            return new JsonResult(Ok(comicBook));
        }
        // Get All
        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _context.ComicBooks.ToList();

            return new JsonResult(Ok(result));
        }

        // Get a ComicBook based on UPC 
        [HttpGet("upc")]
        public JsonResult Get(long upc)
        {
            var result = _context.ComicBooks.FirstOrDefault(comicbook =>comicbook.UPC == upc);
            return new JsonResult(Ok (result));
        }

        // Delete a ComicBook based on UPC
        [HttpDelete("upc")]
        public JsonResult Delete(long upc)
        {
            var result = _context.ComicBooks.FirstOrDefault(comicBook => comicBook.UPC == upc);
            if (result != null)
                _context.ComicBooks.Remove(result);
            else
                return new JsonResult(NotFound());
            return new JsonResult(Ok(NoContent()));
        }

    }
}