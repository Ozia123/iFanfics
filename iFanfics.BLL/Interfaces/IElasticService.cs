using iFanfics.BLL.DTO;

namespace iFanfics.BLL.Interfaces {
    public interface IElasticService {
        FanficsFromElastic GetFanficsWithPaging(int from, int count);
        FanficsFromElastic GetFanifcsByGenre(string genre, int from, int count);
        FanficsFromElastic GetFanficsBySearchTerm(string searchTerm, int from, int count);
        FanficsFromElastic GetByGenreAndSearchTerm(string genre, string searchTerm, int from, int count);
    }
}
