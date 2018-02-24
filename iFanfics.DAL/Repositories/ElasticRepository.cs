using iFanfics.DAL.Entities;
using iFanfics.DAL.Interfaces;
using Elasticsearch.Net;
using Newtonsoft.Json.Linq;

namespace iFanfics.DAL.Repositories {
    public class ElasticRepository : IElasticRepository {
        private readonly IElasticLowLevelClient _elasticClient;

        public ElasticRepository(IElasticLowLevelClient elasticClient) {
            _elasticClient = elasticClient;
        }

        public ResultSetFromElastic GetFanficsFromBody(object body) {
            ResultSetFromElastic fanfics = new ResultSetFromElastic();

            string response = SendFanficRequest(body);

            dynamic responseJson = JObject.Parse(response);
            fanfics.Total = responseJson["hits"]["total"];
            dynamic elementsWithMetaData = responseJson["hits"]["hits"];

            foreach (var element in elementsWithMetaData) {
                fanfics.Data.Add(MapFanficFromElasticResponse(element["_source"]));
            }

            return fanfics;
        }

        private string SendFanficRequest(object body) {
            return _elasticClient.Search<StringResponse>("fullinfofanfic", "fullinfofanfic", PostData.Serializable(body)).Body;
        }

        private FanficForElastic MapFanficFromElasticResponse(dynamic item) {
            FanficForElastic fanfic = new FanficForElastic {
                Id = item["id"],
                ApplicationUserId = item["applicationuserid"],
                Title = item["title"],
                Description = item["description"],
                CreationDate = item["creationdate"],
                Username = item["username"],
                GenreId = item["genreid"],
                GenreName = item["genrename"],
                ChaptersForElastic = item["chaptersforelastic"],
                CommentsForElastic = item["commentsforelastic"],
                TagsForElastic = item["tagsforelastic"],
            };
            return fanfic;
        }
    }
}
