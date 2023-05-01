namespace cms_api.Dto
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class VerifyDto
    {
        public string Id { get; set;}
        public string Code { get; set;}
    }

    public class RootUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }

    public class MetaDto
    {
        public string BrandName { get; set; }
        public string BrandDescription { get; set; }
    }

    public class ChangeThemeDto
    {
        public string Name { get; set; }
    }
}
