using Furion.DatabaseAccessor;

namespace JoyAdmin.Core
{
    /// <summary>
    /// 用户和角色关系表
    /// </summary>
    public class UserRole : IEntity
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        public User User { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }

        public Role Role { get; set; }
    }
}