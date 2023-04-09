namespace IdentityService.Authorization
{
    public class Policy
    {
        public static string[] Policies =
        {
            CanGetUser, CanCreateUser, CanUpdateUser, CanDeleteUser,
            CanGetRole, CanCreateRole, CanUpdateRole, CanDeleteRole
        };

        public const string CanGetUser = "CanGetUser";
        public const string CanCreateUser = "CanCreateUser";
        public const string CanUpdateUser = "CanUpdateUser";
        public const string CanDeleteUser = "CanDeleteUser";

        public const string CanGetRole = "CanGetRole";
        public const string CanCreateRole = "CanCreateRole";
        public const string CanUpdateRole = "CanUpdateRole";
        public const string CanDeleteRole = "CanDeleteRole";

    }
}
