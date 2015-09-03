namespace Component.Tools.Configurations.EMAIL_TEMPLATE
{
    public interface IEmailConfiguration
    {
        Email GetMail(string mailType);
    }
}