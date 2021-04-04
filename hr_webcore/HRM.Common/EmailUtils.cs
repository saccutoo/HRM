using System;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace HRM.Common
{
    public static class EmailUtils
    {

        static string[] listMail = {
                    "novaerp07@novaon.vn",
                    "novaerp03@novaon.vn",
                    "novaerp04@novaon.vn",
                    "novaerp05@novaon.vn",
                    "novaerp06@novaon.vn",
                };
        static string SMTPUser = listMail[randInt()];
        static string SMTPPassword = "moingaymotniemvui@556";//Utils.GetSetting<string>(MAIL_PASSWORD, MAIL_PASS);
        public static int randInt()
        {

            // Usually this can be a field rather than a method variable
            Random rand = new Random();

            // nextInt is normally exclusive of the top value,
            // so add 1 to make it inclusive
            int dice = rand.Next(0, 4);

            return dice;
        }
        public static int Send(string to, string subject, string body)
        {
            return Send(new[] { to }, null, null, subject, body);
        }
        public static int Send(string sender, string[] to, string subject, string content)
        {
            return Send(to, null, null, subject, content);
        }
        public static int Send(string[] to, string[] cc, string subject, string content)
        {
            return Send(to, cc, null, subject, content);
        }
        public static int Send(string[] tolist, string[] ccList, string[] bccList, string subject, string body)
        {
            try
            {

                //string SMTPUser = Utils.GetSetting<string>(MAIL_SENDER, MAIL_ADDRESS);
                //string SMTPPassword = Utils.GetSetting<string>(MAIL_PASSWORD, MAIL_PASS);
                SMTPUser = listMail[randInt()];
                //Now instantiate a new instance of MailMessage
                using (var mail = new MailMessage())
                {
                    //set the sender address of the mail message
                    mail.From = new MailAddress(SMTPUser, "NOVAON HRM");

                    //set the recepient addresses of the mail message
                    if (tolist != null && tolist.Any())
                        foreach (var to in tolist)
                        {

                            if (to != null)
                            {
                                string to1 = to.Trim();
                                if (to.Trim().EndsWith(","))
                                {
                                    to1 = to.Substring(0, to.Length - 1);
                                }
                                if (to1.Trim().Length > 0)
                                {
                                    mail.To.Add(to1.Trim());
                                }

                            }
                        }

                    //set the recepient addresses of the mail message
                    if (ccList != null && ccList.Any())
                        foreach (var cc in ccList)
                        {
                            if (cc != null)
                            {
                                string cc1 = cc.Trim();
                                if (cc.Trim().EndsWith(","))
                                {
                                    cc1 = cc.Substring(0, cc.Length - 1);
                                }
                                if (cc1.Trim().Length > 0)
                                {
                                    mail.CC.Add(cc1.Trim());
                                }
                            }
                        }

                    if (bccList != null && bccList.Any())
                        foreach (var bcc in bccList)
                        {
                            if (bcc != null)
                            {
                                mail.Bcc.Add(bcc);
                            }
                        }


                    //set the subject of the mail message
                    mail.Subject = subject;

                    //set the body of the mail message
                    mail.Body = body;

                    //leave as it is even if you are not sending HTML message
                    mail.IsBodyHtml = true;

                    //set the priority of the mail message to normal
                    mail.Priority = MailPriority.Normal;

                    mail.SubjectEncoding = Encoding.UTF8;
                    mail.BodyEncoding = Encoding.UTF8;

                    //instantiate a new instance of SmtpClient
                    using (var smtp = new SmtpClient())
                    {
                        //if you are using your smtp server, then change your host like "smtp.yourdomain.com"


                        smtp.Host ="smtp.gmail.com";

                        //chnage your port for your host
                        smtp.Port = 25; //or you can also use port# 587

                        //provide smtp credentials to authenticate to your account
                        smtp.Credentials = new System.Net.NetworkCredential(SMTPUser, SMTPPassword);

                        //if you are using secure authentication using SSL/TLS then "true" else "false"
                        smtp.EnableSsl = true;

                        smtp.Send(mail);
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.Instance.Info(string.Format("Send email exception: {0}\t\n {1}", ex.Message, subject));
                return 0;
            }
        }
        public static int Send(string[] tolist, string[] ccList, string subject, string body, string[] Attachfile)
        {
            try
            {

                //string SMTPUser = Utils.GetSetting<string>(MAIL_SENDER, MAIL_ADDRESS);
                //string SMTPPassword = Utils.GetSetting<string>(MAIL_PASSWORD, MAIL_PASS);
                SMTPUser = listMail[randInt()];
                //Now instantiate a new instance of MailMessage
                using (var mail = new MailMessage())
                {
                    //set the sender address of the mail message
                    mail.From = new MailAddress(SMTPUser,"NOVAON HRM");

                    //set the recepient addresses of the mail message

                    if (tolist != null && tolist.Any())
                        foreach (var to in tolist)
                        {

                            if (to != null)
                            {
                                string to1 = to;
                                if (to.Trim().EndsWith(","))
                                {
                                    to1 = to.Substring(0, to.Length - 1);
                                }
                                if (to1.Trim().Length > 0)
                                {
                                    mail.To.Add(to1);
                                }

                            }
                        }


                    //set the recepient addresses of the mail message
                    if (ccList != null && ccList.Any())
                        foreach (var cc in ccList)
                        {
                            if (cc != null)
                            {
                                string cc1 = cc;
                                if (cc.Trim().EndsWith(","))
                                {
                                    cc1 = cc.Substring(0, cc.Length - 1);
                                }
                                if (cc1.Trim().Length > 0)
                                {
                                    mail.CC.Add(cc1);
                                }

                            }
                        }


                    //set the subject of the mail message
                    mail.Subject = subject;

                    //set the body of the mail message
                    mail.Body = body;

                    //leave as it is even if you are not sending HTML message
                    mail.IsBodyHtml = true;

                    //set the priority of the mail message to normal
                    mail.Priority = MailPriority.Normal;

                    mail.SubjectEncoding = Encoding.UTF8;
                    mail.BodyEncoding = Encoding.UTF8;

                    //mail.Attachments.Add(new Attachment(_ms, "ABC-Certificate.Pdf", "application/pdf"));
                    System.Net.Mail.Attachment attachment;
                    foreach (var file in Attachfile)
                    {
                        if (file != null && file.Length > 1)
                        {
                            attachment = new System.Net.Mail.Attachment(file);
                            mail.Attachments.Add(attachment);
                        }
                    }




                    //instantiate a new instance of SmtpClient
                    using (var smtp = new SmtpClient())
                    {
                        //if you are using your smtp server, then change your host like "smtp.yourdomain.com"
                        smtp.Host = "smtp.gmail.com";

                        //chnage your port for your host
                        smtp.Port = 25; //or you can also use port# 587

                        //provide smtp credentials to authenticate to your account
                        smtp.Credentials = new System.Net.NetworkCredential(SMTPUser, SMTPPassword);

                        //if you are using secure authentication using SSL/TLS then "true" else "false"
                        smtp.EnableSsl = true;

                        smtp.Send(mail);

                        return 1;
                    }

                }
            }
            catch (Exception ex)
            {
                //Logger.Instance.Info(string.Format("Send email exception: {0}\t\n {1}", ex.Message, subject));
                return 0;
            }
            finally
            {
                foreach (var file in Attachfile)
                {
                    if (file != null && file.Length > 1)
                    {
                        if (System.IO.File.Exists(file))
                        {
                            System.IO.File.Delete(file);
                        }
                    }
                }
            }
        }
    }
}
