using iFanfics.BLL.DTO;
using iFanfics.BLL.Infrastucture;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iFanfics.BLL.Interfaces {
    public interface IUserService {
        Task<UserDTO> GetUserById(string id);
        Task<string> GetUserId(string username);
        Task<OperationDetails> Create(UserDTO userDTO);
        Task<OperationDetails> Delete(UserDTO userDTO);
        Task<OperationDetails> Update(UserDTO userDTO);
        Task<bool> UserInRole(string username, string roleName);
        List<string> GetAllUsersIds();
        Task<string> GetUsernameById(string id);
        Task SeedDatabse();
    }
}
