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
        //String SMTPUserId = "documentos.super@planvital.cl";
        //String SMTPPassword = "documentossuper15";
        String SMTPUserId = "autorizaciones.administrativas@planvital.cl";
        //El login es autorizaciones.admin
        String SMTPPassword = "Planvital2016";
        int SMTPPort = 25;

        public void Mail(String sendFrom,
                                  String sendTo,
                                  String subject,
                                  String body)
        {
            MailAddress fromAddress = new MailAddress(sendFrom);
            MailAddress toAddress = new MailAddress(sendTo);
            MailMessage mail = new MailMessage();

            mail.From = fromAddress;
            mail.To.Add(toAddress);
            mail.Subject = subject;

            //If body.ToLower.Contains("<html>") Then
            //  .IsBodyHtml = True
            //End If

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