using iFanfics.BLL.DTO;
using iFanfics.BLL.Interfaces;
using iFanfics.DAL.Interfaces;
using AutoMapper;
using iFanfics.DAL.Entities;

namespace iFanfics.BLL.Services {
    public class ElasticService : IElasticService {
        private readonly IElasticRepository _elastic;
        private readonly IMapper _mapper;

        public ElasticService(IElasticRepository elastic, IMapper mapper) {
            _elastic = elastic;
            _mapper = mapper;
        }

        public FanficsFromElastic GetByGenreAndSearchTerm(string genre, string searchTerm, int skip, int top) {
            if (!(string.IsNullOrEmpty(genre) || string.IsNullOrEmpty(searchTerm))) {
                var body = new {
                    query = new {
                        @bool = new {
                            must = new object[]
                            {
                                new {
                                    match = new
                                    {
                                        genrename = genre
                                    }
                                },
                                new {
                                    multi_match = new
                                    {
                                        fields = new[] { "title", "genrename", "username", "description"},
                                        query = searchTerm
                                    }
                                }
                            }
                        }
                    },

                    from = skip,
                    size = top
                };

                return _mapper.Map<ResultSetFromElastic, FanficsFromElastic>(_elastic.GetFanficsFromBody(body));
            }

            return new FanficsFromElastic();
        }

        public FanficsFromElastic GetFanficsBySearchTerm(string searchTerm, int skip, int top) {
            var body = new {
                query = new {
                    multi_match = new {
                        fields = new[] { "title", "chaptersfromelastic", "username", "description", "genrename" },
                        query = searchTerm
                    }
                },
                from = skip,
                size = top
            };

            return _mapper.Map<ResultSetFromElastic, FanficsFromElastic>(_elastic.GetFanficsFromBody(body));
        }

        public FanficsFromElastic GetFanficsWithPaging(int skip, int top) {
            var body = new {
                from = skip,
                size = top
            };
            return _mapper.Map<ResultSetFromElastic, FanficsFromElastic>(_elastic.GetFanficsFromBody(body));
        }

        public FanficsFromElastic GetFanifcsByGenre(string genre, int skip, int top) {
            if (!string.IsNullOrEmpty(genre)) {
                var body = new {
                    from = skip,
                    size = top,
                    query = new {
                        multi_match = new {
                            fields = new[] { "genrename" },
                            query = genre
                        }
                    }
                };

                return _mapper.Map<ResultSetFromElastic, FanficsFromElastic>(_elastic.GetFanficsFromBody(body));
            }

            return new FanficsFromElastic();
        }
    }
}
