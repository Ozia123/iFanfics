using System.Threading.Tasks;

namespace iFanfics.BLL.Interfaces {
    public interface IService<T, TK> {
        Task<T> Create(T item);
        Task<T> Delete(TK id);
        Task<T> Update(T item);
        Task<T> GetById(TK id);
    }
}
