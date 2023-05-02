using SendGrid.Helpers.Mail;
using SendGrid;

namespace cms_api.Services
{
    public class EmailService
    {
        public static async Task SendMail(string email, string username, string password)
        {
            try
            {
                var apiKey = Environment.GetEnvironmentVariable("CMS_SENDGRID_KEY");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("riyazurrazakn.19ece@kongu.edu", "noreply@cmsadmin");
                var subject = "You are added to the cms";
                var to = new EmailAddress(email, username);
                var plainTextContent = $"You are successfully added to the cms website by the admins. You can access your credentials here. Don't share it with others. Thanks.\r\nTeam CMS Application\r\n Username : {username}\r\n Password : {password}\r\n You can access the account by http://localhost:5173/auth";
                var htmlContent = "<strong>You are successfully added to the cms website by the admins. You can access your credentials here. Don't share it with others.</strong><p>Thanks</p><p>Team CMS Application</p><h5> Username : {username}</h5><h5> Password : {password}</h5><a href='http://localhost:5173/auth'>You can access the account by click here</a>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }
    }
}
