namespace cms_api.Dto
{
    public class LoginDto
    {
        public string Username;
        public string Password;
    }

    public class RootUserDto
    {
        public string Username;
        public string Password;
        public string Email;
        public string Role;
    }

    public class MetaDto
    {
        public string BrandName;
        public string BrandDescription;
    }

    public class ChangeThemeDto
    {
        public string Id;
    }
}
