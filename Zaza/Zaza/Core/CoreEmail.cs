using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Zaza.Core
{
	public static class CoreEmail
	{

		#region " Classes "

		public class ServerConnection
		{
			public int SmtpPort;
			public string SmtpServer;
			public string SmtpUser;
			public string SmtpPass;
			public string SmtpFrom;
			public bool SmptUseSSL;
			public string SendTo;
		}

		#endregion

		#region " Private Properties "

		//get smtp connection details
		private static ServerConnection SmtpConnection
		{
			get
			{
				ServerConnection oSmtp = new ServerConnection { SmtpPort = int.Parse(System.Configuration.ConfigurationManager.AppSettings["SmtpPort"]) };
				oSmtp.SmtpServer = System.Configuration.ConfigurationManager.AppSettings["SmtpServer"];
				oSmtp.SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
				oSmtp.SmtpPass = System.Configuration.ConfigurationManager.AppSettings["SmtpPass"];
				oSmtp.SmtpFrom = System.Configuration.ConfigurationManager.AppSettings["SmtpFrom"];
				oSmtp.SendTo = System.Configuration.ConfigurationManager.AppSettings["SendTo"];
				oSmtp.SmptUseSSL = System.Configuration.ConfigurationManager.AppSettings["SmtpUseSSL"] == "1";
				return oSmtp;
			}

		}


		#endregion

		#region " SMTP operations "

		public static void SendMail(string subject, string body)
		{
			SendMail(ConfigurationManager.AppSettings["SendTo"], subject, body, "");
		}

		public static void SendMail(string subject, string body, string strAttachPath)
		{
			SendMail(ConfigurationManager.AppSettings["SendTo"], subject, body, strAttachPath);
		}

		public static void SendMail(string emailsTo, string subject, string body, string strAttachPath)
		{
			if (!string.IsNullOrEmpty(emailsTo))
			{
				System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage();
				System.Net.Mail.Attachment attach = null;

				// compose the email message
				email.IsBodyHtml = true;
				email.From = new System.Net.Mail.MailAddress("\"Autoccasion Last Cars\" <" + SmtpConnection.SmtpFrom + ">");
				int i = 0;
				foreach (var emailAddress in emailsTo.Split(Convert.ToChar(";")).ToList())
				{
					if (emailAddress.IndexOf("@") > 0)
						email.To.Add(emailAddress.Trim());
				}
				email.Subject = subject;
				email.Body = body;

				if (strAttachPath.Length > 0)
				{
					attach = new System.Net.Mail.Attachment(strAttachPath);
					email.Attachments.Add(attach);
				}

				var client = new System.Net.Mail.SmtpClient(SmtpConnection.SmtpServer, SmtpConnection.SmtpPort) { EnableSsl = SmtpConnection.SmptUseSSL };
				if ((SmtpConnection.SmtpUser.Length > 0) & (SmtpConnection.SmtpPass.Length > 0))
					client.Credentials = new System.Net.NetworkCredential(SmtpConnection.SmtpUser, SmtpConnection.SmtpPass);

				// send the email
				client.Send(email);
			}
		}

		#endregion

	}
}