using Furion;
using Furion.DatabaseAccessor;
using Furion.DataEncryption;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using JoyAdmin.Core; 
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Threading.Tasks;

namespace JoyAdmin.Application
{
    /// <summary>
    /// 角色管理服务
    /// </summary>
    
    public class RBACService : IDynamicApiController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<RoleSecurity> _roleSecurityRepository;
        private readonly IRepository<Security> _securityRepository;
        private readonly IRepository<IPLog> _iplogRepository;
        private readonly IAuthorizationManager _authorizationManager;

        public RBACService(IHttpContextAccessor httpContextAccessor
            , IRepository<User> userRepository
            , IRepository<Role> roleRepository
            , IRepository<UserRole> userRoleRepository
            , IRepository<RoleSecurity> roleSecurityRepository
            , IRepository<Security> securityRepository
            , IRepository<IPLog> iplogRepository
            , IAuthorizationManager authorizationManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _roleSecurityRepository = roleSecurityRepository;
            _securityRepository = securityRepository;
            _iplogRepository = iplogRepository;
            _authorizationManager = authorizationManager;
        }

        /// <summary>
        /// 登录（免授权）
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>管理员：admin/654321；</remarks>
        /// <returns></returns>
        [AllowAnonymous]
        public UserDto Login(LoginInput input)
        {
            // 验证用户名和密码
            var user = _userRepository.FirstOrDefault(u => u.Account.Equals(input.Account)) ?? throw Oops.Oh(ErrorCode.WrongUser).StatusCode(ErrorStatus.ValidationFaild);
            if (!user.Password.Equals(MD5Encryption.Encrypt(input.Password)))
            {
                throw Oops.Oh(ErrorCode.WrongPwd).StatusCode(ErrorStatus.ValidationFaild);
            }
            var output = user.Adapt<UserDto>();
              
            output.Token = JWTEncryption.Encrypt(new Dictionary<string, object>()
            {

                { ClaimConst.CLAINM_USERID, user.Id },
                { ClaimConst.CLAINM_ACCOUNT,user.Account },
                { ClaimConst.CLAINM_NAME, user.Name }, 
          
            });


            _httpContextAccessor.HttpContext.Response.Headers["access-token"] = output.Token;

            return output;
        }

        /// <summary>
        /// 查看用户信息
        /// </summary>
        public UserDto GetUserInfo()
        {
            // 获取用户Id
            var userId = _authorizationManager.GetUserId();
            var entity = _userRepository
                .Include(u => u.Roles)
                    .ThenInclude(u => u.Securities)
                .Where(u => u.Id == userId).FirstOrDefault();

            UserDto user = entity.Adapt<UserDto>();
            user.Access = new List<string>();
            entity.Roles.SelectMany(r => r.Securities).ToList().ForEach(s =>
            {
                user.Access.Add(s.UniqueCode);
            });
            return user;
        } 
       

        #region 权限管理
        /// <summary>
        /// 权限列表
        /// </summary> 
        public List<SecurityDto> GetAuth()
        {
            return _securityRepository.AsEnumerable().OrderBy(m=>m.Sort).Adapt<List<SecurityDto>>();
        }

        /// <summary>
        /// 保存权限
        /// </summary>
        [SecurityDefine("auth")]
        public Task SaveAuth(SecurityDto input)
        {
            if (input.Id == 0)
            {
                var hasData= _securityRepository.Where(m =>m.UniqueCode == input.UniqueCode || m.UniqueName == input.UniqueName).FirstOrDefault();
                if (hasData!=null)
                {
                    if (hasData.UniqueCode==input.UniqueCode)
                    {
                        throw Oops.Oh(ErrorCode.WrongValidation, "已存在此权限编码").StatusCode(ErrorStatus.ValidationFaild);
                    }
                    else if (hasData.UniqueName == input.UniqueName)
                    {
                        throw Oops.Oh(ErrorCode.WrongValidation, "已存在此权限名称").StatusCode(ErrorStatus.ValidationFaild);
                    }
                }
                return _securityRepository.InsertAsync(input.Adapt<Security>());
            }
            else {
                var hasData = _securityRepository.Where(m =>m.Id!=input.Id && (m.UniqueCode == input.UniqueCode || m.UniqueName == input.UniqueName)).FirstOrDefault();
                if (hasData != null)
                {
                    if (hasData.UniqueCode == input.UniqueCode)
                    {
                        throw Oops.Oh(ErrorCode.WrongValidation, "已存在此权限编码").StatusCode(ErrorStatus.ValidationFaild);
                    }
                    else if (hasData.UniqueName == input.UniqueName)
                    {
                        throw Oops.Oh(ErrorCode.WrongValidation, "已存在此权限名称").StatusCode(ErrorStatus.ValidationFaild);
                    }
                }
                return _securityRepository.UpdateAsync(input.Adapt<Security>());
            }
           
           
        }
        /// <summary>
        /// 删除权限
        /// </summary>
        [SecurityDefine("auth")]
        public Task DeleteAuth(long id)
        { 
            return _securityRepository.FakeDeleteAsync(id); 
        }
        #endregion

        #region 角色管理
        /// <summary>
        /// 角色列表
        /// </summary> 
        public List<RoleDto> GetRole()
        {
            return _roleRepository.AsEnumerable().OrderBy(m=>m.Sort).Adapt<List<RoleDto>>();
        }
        /// <summary>
        /// 获取某个角色的权限
        /// </summary>
        [SecurityDefine("role")]
        public List<SecurityDto> GetAuthByRoleId(long id)
        {
            var securities = _roleRepository.Include(u => u.Securities)
              .Where(u => u.Id == id).SelectMany(u => u.Securities); 
            return securities.Adapt<List<SecurityDto>>();
        }
        /// <summary>
        /// 保存角色
        /// </summary>
        [SecurityDefine("role")]
        public Task SaveRole(RoleDto input)
        {
            if (input.Id == 0)
            {

                var hasData = _roleRepository.Where(m => m.Name==input.Name).FirstOrDefault();
                if (hasData != null)
                {
                    throw Oops.Oh(ErrorCode.WrongValidation, "已存在此角色名称").StatusCode(ErrorStatus.ValidationFaild);
                }
                return _roleRepository.InsertAsync(input.Adapt<Role>());
            }
            else {
                var hasData = _roleRepository.Where(m =>m.Name == input.Name&& m.Id!=input.Id).FirstOrDefault();
                if (hasData != null)
                {
                    throw Oops.Oh(ErrorCode.WrongValidation, "已存在此角色名称").StatusCode(ErrorStatus.ValidationFaild);
                }
                return _roleRepository.UpdateAsync(input.Adapt<Role>());
            }
           

        }
        /// <summary>
        /// 删除角色
        /// </summary>
        [SecurityDefine("role")]
        public Task DeleteRole(long id)
        { 
            return _roleRepository.FakeDeleteAsync(id); 
        }
        

        /// <summary>
        /// 为角色分配权限
        /// </summary>
        [SecurityDefine("role")]
        public Task GiveRoleSecurity(RoleSecurityInput input)
        {
            input.SecurityIds ??= Array.Empty<long>();
            _roleSecurityRepository.Delete(_roleSecurityRepository.Where(u => u.RoleId == input.RoleId).ToList());

            var list = new List<RoleSecurity>();
            foreach (var securityId in input.SecurityIds)
            {
                list.Add(new RoleSecurity { RoleId = input.RoleId, SecurityId = securityId });
            }

            return _roleSecurityRepository.InsertAsync(list);
        }
        #endregion

        #region 员工管理

        /// <summary>
        /// 查询员工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [SecurityDefine("employee")] 
        public PagedList<UserDto> SearchEmployee(UserInput input)
        {
            var search = _userRepository.AsQueryable();
           
            if (!string.IsNullOrEmpty(input.keyword))
            {
                search = search.Where(m => m.Name.Contains(input.keyword) ||m.Account.Contains(input.keyword));
            }
            var users = search.ToPagedList(input.page, input.size).Adapt<PagedList<UserDto>>();
            return users;
        }
        
        /// <summary>
        /// 保存员工
        /// </summary>
        [SecurityDefine("employee")]
        public Task SaveEmployee(UserDto input)
        {
          
            if (input.Id<=0)
            {
                var oldUser = _userRepository.Where(m => m.Account == input.Account).FirstOrDefault();
                if (oldUser != null)
                {
                    throw Oops.Oh(ErrorCode.WrongValidation, "该账号已存在").StatusCode(ErrorStatus.ValidationFaild);
                }

                User user = input.Adapt<User>();
                user.Password = MD5Encryption.Encrypt("123456");//默认密码为123456
                return _userRepository.InsertAsync(user);
            }
            else
            {
                var oldUser = _userRepository.Where(m => m.Account == input.Account&&m.Id!=input.Id).FirstOrDefault();
                if (oldUser != null)
                {
                    throw Oops.Oh(ErrorCode.WrongValidation, "该账号已存在").StatusCode(ErrorStatus.ValidationFaild);
                }
                User user = input.Adapt<User>();
                return _userRepository.UpdateExcludeAsync(user, new[] { nameof(user.IsDeleted), nameof(user.Password) });
            }
           

        }
        /// <summary>
        /// 删除员工
        /// </summary>
        [SecurityDefine("employee")]
        public Task DeleteEmployee(long id)
        {
            // 获取用户Id
            var userId = _authorizationManager.GetUserId();
            if (userId == id)
            {
                throw Oops.Oh(ErrorCode.WrongValidation, "不能删除当前账号").StatusCode(ErrorStatus.ValidationFaild);
            } 
            return _userRepository.FakeDeleteAsync(id);
        }
        /// <summary>
        /// 获取某个员工的角色
        /// </summary>
        [SecurityDefine("employee")]
        public List<RoleDto> GetRoleByUserId(long id)
        {
            var roles = _userRepository.Include(u => u.Roles)
              .Where(u => u.Id == id).SelectMany(u => u.Roles);
            return roles.Adapt<List<RoleDto>>();
        }
        /// <summary>
        /// 为用户分配角色
        /// </summary>
        [SecurityDefine("employee")]
        public Task GiveUserRole(UserRoleInput input)
        {
            // 获取用户Id
             
            input.RoleIds ??= Array.Empty<long>();
            _userRoleRepository.Delete(_userRoleRepository.Where(u => u.UserId == input.UserId).ToList());

            var list = new List<UserRole>();
            foreach (var roleid in input.RoleIds)
            {
                list.Add(new UserRole { UserId = input.UserId, RoleId = roleid });
            }

            return _userRoleRepository.InsertAsync(list);
        }



        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task UpdatePwd(UpdatePwdInput input)
        {
            // 获取用户Id
            var userId = long.Parse(App.User.FindFirst(ClaimConst.CLAINM_USERID).Value);
            var user = _userRepository.FindOrDefault(userId);
            if (!user.Password.Equals(MD5Encryption.Encrypt(input.OldPassword)))
            {
                throw Oops.Bah(ErrorCode.WrongOldPwd).StatusCode(ErrorStatus.ValidationFaild);
            }
            user.Password = MD5Encryption.Encrypt(input.Password);
            return _userRepository.UpdateIncludeAsync(user, new string[] { "Password" });
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [SecurityDefine("employee")]
        public Task ResetPwd(long userid)
        {
            var user = _userRepository.FindOrDefault(userid);
            user.Password = MD5Encryption.Encrypt(CommonConst.DEFAULT_PASSWORD);
            return _userRepository.UpdateIncludeAsync(user, new string[] { "Password" });
        }
        #endregion


    }
}