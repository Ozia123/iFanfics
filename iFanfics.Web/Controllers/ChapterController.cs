using AutoMapper;
using iFanfics.Web.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using iFanfics.BLL.DTO;
using iFanfics.BLL.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using iFanfics.DAL.Entities;
using System.Linq;
using System;

namespace iFanfics.Web.Controllers {
    public class ChapterController : Controller {
        private readonly IFanficService _fanficService;
        private readonly IChapterService _chapterService;
        private readonly SignInManager<ApplicationUser> _authenticationManager;
        private readonly IMapper _mapper;

        public ChapterController(
            IFanficService fanficService,
            IChapterService chapterService,
            SignInManager<ApplicationUser> authManager,
            IMapper mapper)
        {
            _fanficService = fanficService;
            _chapterService = chapterService;
            _authenticationManager = authManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/chapters/{id}")]
        public IActionResult GetFanficChapters([Required]string id) {
            if (ModelState.IsValid) {
                IEnumerable<ChapterDTO> chapters = _chapterService.GetFanficChapters(id);
                return Ok(_mapper.Map<IEnumerable<ChapterDTO>, IEnumerable<ChapterModel>>(chapters));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("api/chapter/create/{id}")]
        public async Task<IActionResult> CreateChapterForFanfic([Required]string id, [FromBody]ChapterModel item) {
            if (ModelState.IsValid && User.Identity.IsAuthenticated) {
                ApplicationUser user = await _authenticationManager.UserManager.FindByNameAsync(User.Identity.Name);
                FanficDTO fanfic = await _fanficService.GetById(id);

                if (fanfic == null) {
                    return NotFound();
                }
                if (fanfic.ApplicationUserId == user.Id || await _authenticationManager.UserManager.IsInRoleAsync(user, "Admin")) {
                    ChapterDTO newChapter = _mapper.Map<ChapterModel, ChapterDTO>(item);
                    newChapter.Id = null;
                    newChapter.FanficId = id;
                    newChapter = await _chapterService.Create(newChapter);
                    return Ok(_mapper.Map<ChapterDTO, ChapterModel>(newChapter));
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("api/chapter/edit/{id}")]
        public async Task<IActionResult> EditChapterForFanfic([Required]string id, [FromBody]ChapterModel item) {
            if (ModelState.IsValid && User.Identity.IsAuthenticated) {
                ApplicationUser user = await _authenticationManager.UserManager.FindByNameAsync(User.Identity.Name);
                FanficDTO fanfic = await _fanficService.GetById(id);

                if (fanfic == null) {
                    return NotFound();
                }
                if (fanfic.ApplicationUserId == user.Id || await _authenticationManager.UserManager.IsInRoleAsync(user, "Admin")) {
                    ChapterDTO newChapter = _mapper.Map<ChapterModel, ChapterDTO>(item);
                    newChapter.FanficId = id;
                    newChapter = await _chapterService.Update(newChapter);
                    return Ok(_mapper.Map<ChapterDTO, ChapterModel>(newChapter));
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("api/delete/{id}")]
        public async Task<IActionResult> DeleteChapterFromFanfic([Required]string id) {
            if (ModelState.IsValid && User.Identity.IsAuthenticated) {
                ApplicationUser user = await _authenticationManager.UserManager.FindByNameAsync(User.Identity.Name);
                ChapterDTO chapter = await _chapterService.GetById(id);
                FanficDTO fanfic = await _fanficService.GetById(chapter.FanficId);

                if (fanfic == null) {
                    return NotFound();
                }
                if (fanfic.ApplicationUserId == user.Id || await _authenticationManager.UserManager.IsInRoleAsync(user, "Admin")) {
                    ChapterDTO deletedChapter = await _chapterService.Delete(id);
                    return Ok(_mapper.Map<ChapterDTO, ChapterModel>(deletedChapter));
                }
            }
            return BadRequest(ModelState);
        }
    }
}
