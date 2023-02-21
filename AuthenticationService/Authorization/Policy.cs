namespace AuthenticationService.Authorization
{
    public class Policy
    {
        public static string[] Policies =
        {
            CanGetUser, CanCreateUser, CanUpdateUser, CanDeleteUser,
            CanGetRole, CanCreateRole, CanUpdateRole, CanDeleteRole
        };

        public const string CanGetUser = "CanGetUser_*_*";
        public const string CanCreateUser = "CanCreateUser_*_*";
        public const string CanUpdateUser = "CanUpdateUser_*_*";
        public const string CanDeleteUser = "CanDeleteUser_*_*";

        public const string CanGetRole = "CanGetRole_*_*";
        public const string CanCreateRole = "CanCreateRole_*_*";
        public const string CanUpdateRole = "CanUpdateRole_*_*";
        public const string CanDeleteRole = "CanDeleteRole_*_*";

    }
}
