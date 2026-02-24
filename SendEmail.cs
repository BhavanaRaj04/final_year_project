using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace CloudBasedEncryptedCrime
{
    public class SendEmail
    {
        public static void Send(string EmailID, string Message,string Subject)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("bhavanarajwork@gmail.com", "wklnvbdpiuydrzpd");
            smtp.EnableSsl = true;

            MailAddress _from = new MailAddress("bhavanarajwork@gmail.com");
            MailAddress _to = new MailAddress(EmailID);

            MailMessage mail = new MailMessage(_from, _to);
            mail.Subject = Subject;
            mail.Body = "<font size=15> Your <br> " + Message + "</font>";
            mail.IsBodyHtml = true;
            try
            {
                smtp.Send(mail);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        
    }
}