using System.Text;
using databasepmapilearn6.Configurations;
using databasepmapilearn6.Domains.Utilities;
using databasepmapilearn6.InputModels;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace databasepmapilearn6.Utilities;

public class UtlEmail : IUtlEmail
{
    private readonly ConfEmail _confEmail;

    private readonly IHostEnvironment _hostingEnv;

    // constructor
    public UtlEmail(IOptions<ConfEmail> optEmail, IHostEnvironment hostingEnv)
    {
        _confEmail = optEmail.Value;
        _hostingEnv = hostingEnv;
    }

    // send message
    public void Send(UtlLogger logger, IMEmail.Message message)
    {
        // check environment is production
        if (!_hostingEnv.IsProduction()) return;

        // email massage can't null
        if (message == null) return;

        // create log
        UtlLogger logger1 = UtlLogger.FromExisting(logger, $"{nameof(UtlEmail)}/{nameof(Send)}", UtlConverter.ObjectToJson(new
        {
            subject = message.subject,
            to = message.addressTo,
            cc = message.addressCc
        }));

        // check addressTo null or not
        if (message.addressTo.Count <= 0) throw new ArgumentNullException(nameof(message.addressTo), "please provide 'to' addresses");

        // set from address
        message.addressFrom.Add(IMEmail.Address.FromHardCode("Database P&M WEB", _confEmail.SmtpUsername));

        // initialize mime message
        MimeMessage mimeMessage = new MimeMessage();

        // assign sender and receiver
        mimeMessage.From.AddRange(message.ConvertAddressFromToMailboxAddress());
        mimeMessage.To.AddRange(message.ConvertAddressToToMailboxAdress());
        mimeMessage.Cc.AddRange(message.ConvertAddressCcToMailboxAddress());

        // assign email subject
        mimeMessage.Subject = message.subject;

        // assign html body content
        mimeMessage.Body = message.content.ToMessageBody();

        try
        {
            // cari tau fungsi dan cara bacanya
            using (var smtpClient = new SmtpClient())
            {
                // connect to target smtp server
                // not using ssl because target server not supported
                smtpClient.Connect(_confEmail.SmtpServer, _confEmail.SmtpPort, MailKit.Security.SecureSocketOptions.None);

                // not using oauth functionality
                smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");

                // authenticate using username and password
                smtpClient.Authenticate(_confEmail.SmtpUsername, _confEmail.SmtpPassword);

                // send the message
                smtpClient.Send(mimeMessage);

                // disconnect from the smtp server cleanly
                smtpClient.Disconnect(true);
            }

            // log
            logger1.Success();
        }
        catch (System.Exception e)
        {
            // log
            logger1.Failed(e);

            throw e;
        }
    }

    // send many message
    public void Send(UtlLogger logger, List<IMEmail.Message> message)
    {
        try
        {
            // send all the message
            foreach (IMEmail.Message message1 in message)
            {
                Send(logger, message1);
            }
        }
        catch (System.Exception e)
        {
            throw e;
        }
    }


    #region HELPER, Cari tau bacanya
    // Helper class to build HTML email content.
    // The methods to build email content is implemented by <see cref="ExtUtlEmailContentBuilder"/> extension method.
    public class UtlEmailContentBuilder
    {
        public StringBuilder stringBuilder = new StringBuilder();

        public List<string> attachments = new List<string>();

        public UtlEmailContentBuilder() { }

        // ~ untuk apa ?
        ~UtlEmailContentBuilder()
        {
            stringBuilder = null;
        }
    }

    #endregion

}