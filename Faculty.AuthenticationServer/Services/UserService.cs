using System;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using Faculty.Common.Dto.User;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Faculty.AuthenticationServer.Models;
using Faculty.AuthenticationServer.Services.Interfaces;

namespace Faculty.AuthenticationServer.Services
{
    /// <summary>
    /// User service.
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// User manager for executing operation on identity user.
        /// </summary>
        private readonly UserManager<CustomUser> _userManager;
        /// <summary>
        /// Role manager for executing operation on identity role.
        /// </summary>
        private readonly RoleManager<IdentityRole> _roleManager;
        /// <summary>
        /// Mapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for init identity means.
        /// </summary>
        /// <param name="userManager">User manager.</param>
        /// <param name="roleManager">Role manager.</param>
        /// <param name="mapper">Mapper.</param>
        public UserService(UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Method for getting all user list.
        /// </summary>
        /// <returns>An instance of the Task class typed by IEnumerable interface of user for display.</returns>
        public IEnumerable<UserDto> GetUsers(out IdentityResult identityResult)
        {
            var customUsers = _userManager.Users;
            if (customUsers == null)
            {
                identityResult = IdentityResult.Failed();
                return default;
            }

            if (!customUsers.Any())
            {
                identityResult = IdentityResult.Failed();
                return new List<UserDto>();
            }

            var userDto = _mapper.Map<IEnumerable<CustomUser>, IEnumerable<UserDto>>(customUsers);
            identityResult = IdentityResult.Success;
            return userDto;
        }

        /// <summary>
        /// Method for getting user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>An instance of the Task class typed by UserModify class.</returns>
        public async Task<Tuple<UserDto, IdentityResult>> GetUser(string id)
        {
            var customUser = await _userManager.FindByIdAsync(id);
            if (customUser == null)
            {
                return new Tuple<UserDto, IdentityResult>(default, IdentityResult.Failed());
            }

            var userDto = _mapper.Map<CustomUser, UserDto>(customUser);
            return new Tuple<UserDto, IdentityResult>(userDto, IdentityResult.Success);
        }

        /// <summary>
        /// Method for creating user.
        /// </summary>
        /// <param name="userAddDto">Model user for add.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<Tuple<UserDto, IdentityResult>> CreateUser(UserAddDto userAddDto)
        {
            var customUser = _mapper.Map<UserAddDto, CustomUser>(userAddDto);
            var result = await _userManager.CreateAsync(customUser, userAddDto.Password);
            if (!result.Succeeded)
            {
                return new Tuple<UserDto, IdentityResult>(default, IdentityResult.Failed());
            }

            await _userManager.AddToRolesAsync(customUser, userAddDto.Roles);
            userAddDto.Id = customUser.Id;
            return new Tuple<UserDto, IdentityResult>(userAddDto, IdentityResult.Success);
        }

        /// <summary>
        /// Method for deleting user.
        /// </summary>
        /// <param name="id">Model user for delete.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<IdentityResult> DeleteUser(string id)
        {
            var customUser = await _userManager.FindByIdAsync(id);
            if (customUser == null)
            {
                return IdentityResult.Failed();
            }

            await _userManager.DeleteAsync(customUser);
            return IdentityResult.Success;
        }

        /// <summary>
        /// Method for editing user.
        /// </summary>
        /// <param name="userDto">Model user for modify.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<IdentityResult> EditUser(UserDto userDto)
        {
            var customUser = await _userManager.FindByIdAsync(userDto.Id);
            if (customUser == null)
            {
                return IdentityResult.Failed();
            }

            customUser = _mapper.Map(userDto, customUser);
            UpdateUserRoles(customUser, userDto, out var identityResult);
            if (!identityResult.Succeeded)
            {
                return IdentityResult.Failed();
            }

            var resultUpdate = await _userManager.UpdateAsync(customUser);
            if (!resultUpdate.Succeeded)
            {
                return IdentityResult.Failed();
            }

            return IdentityResult.Success;
        }

        /// <summary>
        /// Method for update user roles.
        /// </summary>
        /// <param name="customUser">Custom identity user.</param>
        /// <param name="userDto">User data transfer object.</param>
        /// <param name="identityResult"></param>
        private void UpdateUserRoles(CustomUser customUser, UserDto userDto, out IdentityResult identityResult)
        {
            var roles = _userManager.GetRolesAsync(customUser).Result;
            var resultRemove = _userManager.RemoveFromRolesAsync(customUser, roles).Result;
            if (!resultRemove.Succeeded)
            {
                identityResult = IdentityResult.Failed();
                return;
            }

            var resultAddRoles = _userManager.AddToRolesAsync(customUser, userDto.Roles).Result;
            if (!resultAddRoles.Succeeded)
            {
                identityResult = IdentityResult.Failed();
                return;
            }

            identityResult = IdentityResult.Success;
        }

        /// <summary>
        /// Method for editing user password.
        /// </summary>
        /// <param name="userModifyPasswordDto">User data transfer object.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<IdentityResult> EditPasswordUser(UserModifyPasswordDto userModifyPasswordDto)
        {
            var customUser = await _userManager.FindByIdAsync(userModifyPasswordDto.Id);
            if (customUser == null)
            {
                return IdentityResult.Failed();
            }

            var resultValidate = await _userManager.PasswordValidators[0].ValidateAsync(_userManager, customUser, userModifyPasswordDto.NewPassword);
            if (!resultValidate.Succeeded)
            {
                return IdentityResult.Failed();
            }

            customUser.PasswordHash = _userManager.PasswordHasher.HashPassword(customUser, userModifyPasswordDto.NewPassword);
            var resultUpdate = await _userManager.UpdateAsync(customUser);
            if (!resultUpdate.Succeeded)
            {
                return IdentityResult.Failed();
            }

            return IdentityResult.Success;
        }

        /// <summary>
        /// Method for getting existing roles.
        /// </summary>
        /// <returns>An instance of the Task class typed by IEnumerable interface of strings.</returns>
        public IEnumerable<string> GetRoles(out IdentityResult identityResult)
        {
            var namesRole = _roleManager.Roles.Select(x => x.Name).ToList();
            if (!namesRole.Any())
            {
                identityResult = IdentityResult.Failed();
                return default;
            }

            identityResult = IdentityResult.Success;
            return namesRole;
        }
    }
}
