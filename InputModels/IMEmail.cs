using databasepmapilearn6.models;
using MimeKit;

namespace databasepmapilearn6.InputModels;

public class IMEmail
{
    // email address information
    public class Address
    {
        public string name { get; set; }
        public string address { get; set; }

        // constructor
        private Address(string name, string address) { this.name = name; this.address = address.Trim(); }


        #region Hardcoded

        public static Address FromHardCode(string name, string address) => new Address(name, address);
        public static List<Address> ListSingleElementFromHardCode(string name, string address) => new List<Address>() { FromHardCode(name, address) };

        #endregion
        // end hardcoded

        #region From Database

        // Single object
        public static Address FromDb(MUser user) => new Address(user.Name, user.Email);
        public static List<Address> ListSingleElementFromDb(MUser user) => new List<Address>() { FromDb(user) };

        // List
        public static List<Address> FromDb(List<MUser> user) => user.Select(m => FromDb(m)).ToList();

        #endregion
        // end from database
    }

    // the email message
    public class Message
    {
        public List<Address> addressTo { get; set; }
        public List<Address> addressFrom { get; set; } = new List<Address>();
        public List<Address> addressCc { get; set; } = new List<Address>();
        public string subject;
        public BodyBuilder content;

        // constructor
        private Message() { }

        public static Message Create(List<Address> addressTo, string subject, BodyBuilder content)
        {
            return new Message
            {
                addressTo = addressTo,
                subject = subject,
                content = content
            };
        }

        public Message WithCc(List<Address> addressCc) { this.addressCc.AddRange(addressCc); return this; }


        // distinct email address
        // cara tau bacanya
        public List<MailboxAddress> ConvertAddressToToMailboxAdress() => addressTo.GroupBy(m => m.address).Select(m => m.First()).Select(m => new MailboxAddress(m.name, m.address)).ToList();
        public List<MailboxAddress> ConvertAddressFromToMailboxAddress() => addressFrom.GroupBy(m => m.address).Select(m => m.First()).Select(m => new MailboxAddress(m.name, m.address)).ToList();
        public List<MailboxAddress> ConvertAddressCcToMailboxAddress() => addressCc.GroupBy(m => m.address).Select(m => m.First()).Select(m => new MailboxAddress(m.name, m.address)).ToList();
    }
}