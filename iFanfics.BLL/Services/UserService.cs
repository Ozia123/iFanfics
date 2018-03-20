using AutoMapper;
using iFanfics.BLL.DTO;
using iFanfics.BLL.Infrastucture;
using iFanfics.BLL.Interfaces;
using iFanfics.DAL.Entities;
using iFanfics.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iFanfics.BLL.Services {
    public class UserService : IUserService {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper) {
            _database = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetUserById(string id) {
            ClientProfile profile = await _database.ProfileRepository.GetById(id);
            return _mapper.Map<ClientProfile, UserDTO>(profile);
        }

        public async Task<bool> UserInRole(string userName, string roleName) {
            ApplicationUser user = await _database.UserManager.FindByIdAsync(await GetUserId(userName));
            return await _database.UserManager.IsInRoleAsync(user, roleName);
        }

        public async Task<string> GetUsernameById(string id) {
            ClientProfile profile = await _database.ProfileRepository.GetById(id);
            return profile.Username;
        }

        public List<string> GetAllUsersIds() {
            return _database.ProfileRepository.GetAllUsersId();
        }

        public async Task<OperationDetails> Create(UserDTO item) {
            OperationDetails resultOperation;
            string userId = await RegistrationUser(item.Email, item.Username, item.Password);
            if (userId != string.Empty) {
                await _database.UserManager.AddToRoleAsync(await _database.UserManager.FindByIdAsync(userId), item.Role);
                await CreateProfileForUser(userId, item.Username, item.PictureURL);
                await _database.SaveAsync();
                resultOperation = new OperationDetails(true, string.Empty, string.Empty);
            }
            else {
                resultOperation = new OperationDetails(false, string.Empty, string.Empty);
            }
            return resultOperation;
        }

        public async Task<OperationDetails> Delete(UserDTO item) {
            OperationDetails resultOperation;
            ApplicationUser user = await _database.UserManager.FindByIdAsync(item.Id);
            var result = await _database.UserManager.DeleteAsync(user);
            if (result.Succeeded) {
                await _database.ProfileRepository.Delete(item.Id);
                await _database.SaveAsync();
                resultOperation = new OperationDetails(true, string.Empty, string.Empty);
            }
            else {
                resultOperation = new OperationDetails(false, string.Empty, string.Empty);
            }
            return resultOperation;
        }

        public async Task<OperationDetails> Update(UserDTO item) {
            OperationDetails resultOperation;
            ClientProfile clientProfile = _mapper.Map<UserDTO, ClientProfile>(item);
            clientProfile = await _database.ProfileRepository.Update(clientProfile);
            if (clientProfile == null) {
                resultOperation = new OperationDetails(false, string.Empty, string.Empty);
            }
            else {
                resultOperation = new OperationDetails(true, string.Empty, string.Empty);
            }
            return resultOperation;
        }

        public async Task<string> GetUserId(string username) {
            return (await _database.UserManager.FindByNameAsync(username)).Id;
        }

        private async Task<string> RegistrationUser(string email, string username, string password) {
            string userId = string.Empty;
            ApplicationUser user = await _database.UserManager.FindByEmailAsync(email);
            if (user == null) {
                user = new ApplicationUser { Email = email, UserName = username, EmailConfirmed = false };
                var result = await _database.UserManager.CreateAsync(user, password);
                userId = result.Succeeded ? user.Id : string.Empty;
            }
            return userId;
        }

        private async Task CreateProfileForUser(string userId, string username, string pictureURL) {
            ClientProfile clientProfile = new ClientProfile {
                Id = userId,
                Username = username,
                DateOfCreation = DateTime.Now,
                PictureURL = pictureURL
            };
            await _database.ProfileRepository.Create(clientProfile);
        }

        public async Task SeedDatabse() {
            await _database.RoleManager.CreateAsync(new IdentityRole { Name = "User" });
            await _database.RoleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        }
    }
}