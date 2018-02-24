using AutoMapper;
using iFanfics.Web.Models;
using Microsoft.AspNetCore.Mvc;
using iFanfics.BLL.DTO;
using iFanfics.BLL.Interfaces;
using System.Collections.Generic;

namespace iFanfics.Web.Controllers {
    public class GenreController : Controller {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreController(
            IGenreService genreService,
            IMapper mapper) {
            _genreService = genreService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/genres")]
        public IActionResult GetAll() {
            List<GenreDTO> genres = _genreService.GetAll();
            if (genres == null) {
                return NotFound();
            }

            return Ok(GetGenreModelsFromListDTO(genres));
        }

        private IEnumerable<GenreModel> GetGenreModelsFromListDTO(List<GenreDTO> genres) {
            List<GenreModel> items = new List<GenreModel>();

            foreach (var genre in genres) {
                items.Add(new GenreModel() { genreName = genre.GenreName });
            }
            return items;
        }
    }
}