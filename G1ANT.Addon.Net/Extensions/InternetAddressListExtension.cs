using G1ANT.Language;
using MimeKit;
using System;

namespace G1ANT.Addon.Net.Extensions
{
    public static class InternetAddressListExtension
    {
        public static void SetMailboxesFromStructure(this InternetAddressList mails, Structure newMails, string nameOfEntity)
        {
            mails.Clear();
            if (newMails is TextStructure text)
            {
                mails.Add(new MailboxAddress(text.Value));
            }
            else if (newMails is ListStructure list)
            {
                foreach (var mail in list.Value)
                    mails.Add(new MailboxAddress(mail.ToString()));
            }
            else
                throw new ArgumentException($"'{nameOfEntity}' should be text or list structure");
        }
    }
}
