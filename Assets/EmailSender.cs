using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using UnityEngine;

public class EmailSender : MonoBehaviour
{
    public static void SendEmailWithScreenshot(string smtpServer, int smtpPort, string username, string password, string recipient, string subject, string body, string screenshotPath)
    {
        using (MailMessage mail = new MailMessage())
        {
            mail.From = new MailAddress(username);
            mail.To.Add(recipient);
            mail.Subject = subject;
            mail.Body = body;

            Attachment attachment = new Attachment(screenshotPath);
            mail.Attachments.Add(attachment);


            using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(username, password);
                smtpClient.EnableSsl = true; // Aktifkan jika server SMTP mendukung SSL
                smtpClient.Send(mail);
            }
        }
    }
}
