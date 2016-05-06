using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace LightSwitchApplication.UserCode
{
    public class EnviaMail
    {
        String SMTPServer = "mail.planvital.cl";
        String SMTPUserId = "autorizaciones.administrativas@planvital.cl";
        //El login es autorizaciones.admin
        String SMTPPassword = "Planvital2016";
        int SMTPPort = 25;

        //public void Mail(String sendFrom, String sendTo, String subject, String body)
        
        public void Mail(String sendTo, String subject, String body)
        {
            //MailAddress fromAddress = new MailAddress(sendFrom);
            MailAddress fromAddress = new MailAddress("autorizaciones.administrativas@planvital.cl");
            
            MailAddress toAddress = new MailAddress("cesar.riutor@planvital.cl");
            MailMessage mail = new MailMessage();

            mail.From = fromAddress;
            mail.To.Add(toAddress);
            mail.Subject = subject;
            mail.Body = body;

            SmtpClient smtp = new SmtpClient(SMTPServer, SMTPPort);
            // smtp.EnableSsl = false;
            // smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            // smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(SMTPUserId, SMTPPassword);
            smtp.Send(mail);
        }
    }
}