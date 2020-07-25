using System;
using System.Collections.Generic;
using System.Linq;
using CodeNest.Simple.NETWebProject.Data;
using CodeNest.Simple.NETWebProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeNest.Simple.NETWebProject.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MoviesController:ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public MoviesController(ApplicationDbContext _dbContext)
        {
            dbContext=_dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Movie>> GetMovies(){
            var Movies=dbContext.Movies;
            return Movies;
        }
        [HttpGet("{id}")]
        public ActionResult<Movie> GetMovie(int id){
            var Movie=dbContext.Movies.FirstOrDefault(option=>option.ID==id);
            if (Movie!=null)
                return Movie;
            else
                return NotFound();
        }
        [HttpPost]
        public ActionResult<Movie> CreateMovie([FromBody]AddMovie addMovie){
            var MovieToCreate=new Movie(){
            Title=addMovie.Title,
            Link=addMovie.Link,
            CreatedDate=DateTime.Now
            };
           var CreatedMovie= dbContext.Movies.Add(MovieToCreate).Entity;
            dbContext.SaveChanges();
            return CreatedMovie;
        }
        [HttpPut("{id}")]
        public ActionResult<Movie> EditMovie(int id, [FromBody] EditMovie editMovie)
        {
          var MovieToEdit=dbContext.Movies.FirstOrDefault(option => option.ID == id);
          if(MovieToEdit==null)
            return NotFound();
        MovieToEdit.Title=editMovie.Title;
        MovieToEdit.Link=editMovie.Link;
        dbContext.SaveChanges();
        return MovieToEdit;        
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var MovieToDelete = dbContext.Movies.FirstOrDefault(option => option.ID == id);
            if (MovieToDelete == null)
                return BadRequest();
            dbContext.Movies.Remove(MovieToDelete);
            dbContext.SaveChanges();
            return NoContent();
        }
    }
}