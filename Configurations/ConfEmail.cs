namespace databasepmapilearn6.Configurations;

public class ConfEmail
{
    // Target SMTP server
    public string SmtpServer { get; set; } = null!;

    // target port SMTP server
    public int SmtpPort { get; set; }

    // username to authenticate to email service
    public string SmtpUsername { get; set; } = null!;

    // password
    public string SmtpPassword { get; set; } = null!;
}