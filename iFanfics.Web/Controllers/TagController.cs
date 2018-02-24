using AutoMapper;
using iFanfics.Web.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iFanfics.BLL.DTO;
using iFanfics.BLL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace iFanfics.Web.Controllers {
    public class TagController : Controller {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public TagController(
            ITagService tagService,
            IMapper mapper) {
            _tagService = tagService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/tags")]
        public IActionResult GetAll() {
            List<TagDTO> tags = _tagService.GetAll();
            if (tags == null) {
                return NotFound();
            }

            return Ok(GetTagModelsFromListDTO(tags));
        }

        private IEnumerable<TagModel> GetTagModelsFromListDTO(List<TagDTO> tags) {
            tags.OrderByDescending(o => o.Uses);
            List<TagModel> items = new List<TagModel>();

            foreach (var tag in tags) {
                items.Add(new TagModel() { tagName = tag.TagName });
            }
            return items;
        }

        [HttpPost]
        [Route("api/tag/create")]
        public async Task<IActionResult> Post([FromBody]TagModel item) {
            if (ModelState.IsValid && User.Identity.IsAuthenticated) {
                TagDTO tag = _tagService.GetTagByName(item.tagName);
                if (tag == null) {
                    await _tagService.Create(new TagDTO() { TagName = item.tagName, Uses = 1 });
                    return Ok();
                }
                tag.Uses++;
                await _tagService.Update(tag);
                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}