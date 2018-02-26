using AutoMapper;
using iFanfics.Web.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iFanfics.BLL.DTO;
using iFanfics.BLL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using iFanfics.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace iFanfics.Web.Controllers {
    public class TagController : Controller {
        private readonly ITagService _tagService;
        private readonly SignInManager<ApplicationUser> _authenticationManager;
        private readonly IFanficService _fanficService;
        private readonly IFanficTagsService _fanficTagsService;
        private readonly IMapper _mapper;

        public TagController(
            ITagService tagService,
            IFanficTagsService fanficTagsService,
            IFanficService fanficService,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper) {
            _tagService = tagService;
            _fanficService = fanficService;
            _authenticationManager = signInManager;
            _fanficTagsService = fanficTagsService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/tags")]
        public IActionResult GetAll() {
            List<TagDTO> tags = _tagService.GetAll();
            if (tags == null) {
                return NotFound();
            }

            return Ok(GetTagModelsFromListDTO(tags).ToList().GetRange(0, 8));
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

        [HttpPut]
        [Route("api/tag/add/{id}")]
        public async Task<IActionResult> Put([Required]string id, [FromBody]TagModel item) {
            if (ModelState.IsValid && User.Identity.IsAuthenticated) {
                ApplicationUser user = await _authenticationManager.UserManager.FindByNameAsync(User.Identity.Name);
                FanficDTO fanfic = await _fanficService.GetById(id);
                if (fanfic.ApplicationUserId == user.Id || await _authenticationManager.UserManager.IsInRoleAsync(user, "Admin")) {
                    TagDTO tag = _tagService.GetTagByName(item.tagName);
                    if (tag == null) {
                        tag = await _tagService.Create(new TagDTO() { TagName = item.tagName, Uses = 1 });
                    }
                    await _fanficTagsService.Create(new FanficTagsDTO() { TagId = tag.Id, FanficId = id });
                    return Ok();
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("api/tag/delete/{id}")]
        public async Task<IActionResult> Delete([Required]string id, [FromBody]TagModel item) {
            if (ModelState.IsValid && User.Identity.IsAuthenticated) {
                ApplicationUser user = await _authenticationManager.UserManager.FindByNameAsync(User.Identity.Name);
                FanficDTO fanfic = await _fanficService.GetById(id);
                if (fanfic.ApplicationUserId == user.Id || await _authenticationManager.UserManager.IsInRoleAsync(user, "Admin")) {
                    TagDTO tag = _tagService.GetTagByName(item.tagName);
                    if (tag != null) {
                        FanficTagsDTO fanficTag = _fanficTagsService.GetFanficTagsByFanficId(id).FirstOrDefault(x => x.TagId == tag.Id);
                        await _fanficTagsService.Delete(fanficTag.Id);
                    }
                    return Ok();
                }
            }
            return BadRequest(ModelState);
        }
    }
}