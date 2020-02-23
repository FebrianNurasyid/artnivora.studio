namespace Artnivora.Studio.Portal.Business.Services
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Business.Models.Enums;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Internal;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    /// <summary>
    /// Object representing service to manage emails
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class EmailService : IDisposable
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private string _host;
        private string _from;
        private string _alias;
        private string _username;
        private string _password;
        private int _port;
        private string _baseurl;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// </summary>
        public EmailService(IConfiguration iConfiguration)
        {
            _host = iConfiguration.GetSection("SMTP").GetSection("Host").Value;
            _from = iConfiguration.GetSection("SMTP").GetSection("From").Value;
            _alias = iConfiguration.GetSection("SMTP").GetSection("Alias").Value;
            _username = iConfiguration.GetSection("SMTP").GetSection("Username").Value;
            _password = iConfiguration.GetSection("SMTP").GetSection("Password").Value;
            _port = Convert.ToInt32(iConfiguration.GetSection("SMTP").GetSection("Port").Value);
            _baseurl = iConfiguration.GetSection("Site").GetSection("Url").Value;
        }

        public void SendEmail(User user, EmailType emailType)
        {
            try
            {
                using (SmtpClient client = new SmtpClient(_host))
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(_from, _alias);
                    mailMessage.BodyEncoding = Encoding.UTF8;
                    mailMessage.To.Add(user.Email);
                    mailMessage.IsBodyHtml = true;

                    client.Port = _port;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(_username, _password);

                    if (emailType == EmailType.ActivationAccount)
                    {
                        SendActivationEmail(user, mailMessage, client);
                    }
                    else
                    {
                        SendPasswordResetEmail(user, mailMessage, client);
                    }
                }

            } catch  (Exception exception)
            {
                Logger.Error("Error while creating smtp client");
                if (exception.InnerException != null)
                {
                    Logger.Error(exception.InnerException.ToString());
                } else
                {
                    Logger.Error(exception.ToString());
                }
            }
        }

        private void SendActivationEmail(User user, MailMessage mailMessage, SmtpClient smtpClient)
        {
            string headerLabel = "Activeer uw account door op de link te klikken!";
            string buttonLabel = "Activeer uw account";
            string footerLabel = "Dit is een automatische email. Reageer a.u.b. niet op deze email.";
            string urlAction = "/users/tokenconfirmation?activatetoken=";
            mailMessage.Body = GenerateHTMLBody(user.Activation_Token.ToString(), urlAction, headerLabel, footerLabel, buttonLabel);
            mailMessage.Subject = "Activation Account";

            Send(mailMessage, smtpClient);
        }

        private void SendPasswordResetEmail(User user, MailMessage mailMessage, SmtpClient smtpClient)
        {
            string headerLabel = "Herstel uw wachtwoord door op de link te klikken!";
            string buttonLabel = "Herstel uw wachtwoord";
            string footerLabel = "Dit is een automatische email. Reageer a.u.b. niet op deze email.";
            string urlAction = "/users/tokenconfirmation?recoverytoken=";
            mailMessage.Body = GenerateHTMLBody(user.PasswordRecoveryToken.ToString(), urlAction, headerLabel, footerLabel, buttonLabel);
            mailMessage.Subject = "Reset Password";

            Send(mailMessage, smtpClient);
        }

        private void Send(MailMessage mailMessage, SmtpClient smtpClient)
        {
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (SmtpException exception)
            {
                Logger.Error("Error while sending message through smtp client");
                if (exception.InnerException != null)
                {
                    Logger.Error(exception.InnerException.ToString());
                }
                else
                {
                    Logger.Error(exception.ToString());
                }
            }
        }

        private string GenerateHTMLBody(string activationToken, string urlAction, string headerLabel, string footerLabel, string buttonLabel)
        {
            string body;
            string webRootPath = AppContext.BaseDirectory;
            
            string filePath = $"{webRootPath}Templates\\EmailTemplate.html";

            StreamReader sr = new StreamReader(filePath);
            body = sr.ReadToEnd();
            string redirectUrl = _baseurl + urlAction + activationToken;
            body = body.Replace("{LINK}", redirectUrl);
            body = body.Replace("{HEADERLABEL}", headerLabel);
            body = body.Replace("{BUTTONLABEL}", buttonLabel);
            body = body.Replace("{FOOTERLABEL}", footerLabel);

            sr.Dispose();

            return body;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
