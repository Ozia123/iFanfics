using AutoMapper;
using iFanfics.DAL.Entities;
using iFanfics.Web.Models;
using System.Collections.Generic;
using System.Linq;
using iFanfics.BLL.DTO;

namespace iFanfics.Web.Util {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile() {
            CreateMapsForUser();
            CreateMapsForFanfic();
            CreateMapsForChapter();
            CreateMapsForChapterRating();
            CreateMapsForComment();
            CreateMapsForCommentRating();
            CreateMapsForTag();
            CreateMapsForGenre();

            CreateMap<ResultSetFromElastic, FanficsFromElastic>()
                .ReverseMap();

            //CreateMap<Fanfic, FullInfoFanfic>()
            //    .ForMember(dto => dto.Username, opt => opt.MapFrom(src => src..ClientProfile.Name))
            //    .ForMember(dto => dto.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            //    .ReverseMap();

            CreateMap<IQueryable<Fanfic>, List<FullInfoFanfic>>();
        }

        private void CreateMapsForUser() {
            CreateMap<UserDTO, LoginModel>()
                .ReverseMap();

            CreateMap<RegisterModel, UserDTO>()
                .ReverseMap();

            CreateMap<EditCurrentUser, UserDTO>()
                .ReverseMap();

            CreateMap<CurrentUser, UserDTO>()
                .ReverseMap();

            CreateMap<UserDTO, CurrentUser>()
                .ReverseMap();

            CreateMap<ClientProfile, UserDTO>()
                .ReverseMap();

            CreateMap<UserDTO, ClientProfile>()
                .ReverseMap();
        }

        private void CreateMapsForFanfic() {
            CreateMap<Fanfic, FanficDTO>()
               .ReverseMap();

            CreateMap<CreateFanfic, FanficDTO>()
                .ReverseMap();

            CreateMap<FanficDTO, Fanfic>()
                .ReverseMap();

            CreateMap<FullInfoFanfic, FanficForElastic>()
                .ReverseMap();

            CreateMap<FullInfoFanfic, FanficForElastic>()
                .ReverseMap();
        }

        private void CreateMapsForChapter() {
            CreateMap<Chapter, ChapterDTO>()
               .ReverseMap();

            CreateMap<ChapterModel, ChapterDTO>()
                .ReverseMap();

            CreateMap<ChapterDTO, ChapterModel>()
                .ReverseMap();

            CreateMap<ChapterDTO, Chapter>()
                .ReverseMap();
        }

        private void CreateMapsForChapterRating() {
            CreateMap<ChapterRating, ChapterRatingDTO>()
               .ReverseMap();

            CreateMap<ChapterRatingModel, ChapterRatingDTO>()
                .ReverseMap();

            CreateMap<EditChapterRating, ChapterRatingDTO>()
                .ReverseMap();

            CreateMap<ChapterRatingDTO, ChapterRating>()
                .ReverseMap();
        }

        private void CreateMapsForComment() {
            CreateMap<Comment, CommentDTO>()
               .ReverseMap();

            CreateMap<CommentDTO, Comment>()
                .ReverseMap();
        }

        private void CreateMapsForCommentRating() {
            CreateMap<CommentRating, CommentRatingDTO>()
               .ReverseMap();

            CreateMap<CommentRatingModel, CommentRatingDTO>()
                .ReverseMap();

            CreateMap<EditCommentRating, CommentRatingDTO>()
                .ReverseMap();

            CreateMap<CommentRatingDTO, CommentRating>()
                .ReverseMap();
        }

        private void CreateMapsForTag() {
            CreateMap<Tag, TagDTO>()
                .ReverseMap();

            CreateMap<Models.TagModel, TagDTO>()
                .ReverseMap();

            CreateMap<FanficTags, FanficTagsDTO>()
                .ReverseMap();

            CreateMap<FanficTagsDTO, FanficTags>()
                .ReverseMap();

            CreateMap<FanficTags, TagDTO>()
                .ReverseMap();
        }

        private void CreateMapsForGenre() {
            CreateMap<Genre, GenreDTO>()
                .ReverseMap();

            CreateMap<GenreModel, GenreDTO>()
                .ReverseMap();
        }
    }
}
