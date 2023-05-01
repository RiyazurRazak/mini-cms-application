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

    public class AddBlogDto
    {
        public string Title { get; set; }

        public string Description { get; set; }
        public string Body { get; set; }
    }

    public class AddCommentDto
    {
        public string EmailAddress { get; set; }

        public string Name { get; set; }

        public string Message { get; set; }

    }
}
