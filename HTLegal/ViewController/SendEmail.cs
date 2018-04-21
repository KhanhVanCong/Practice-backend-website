using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.ComponentModel;
using System.Configuration;

namespace HTLegal.ViewController
{
    public class SendEmail
    {

        #region send email function

        static string smtp = ConfigurationManager.AppSettings["SMTP"];
        static string _domain = ConfigurationManager.AppSettings["Domain"];
       
        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="bccEmail">chuoi cac email can bbc ngan cach boi dau ;</param>
        /// <param name="fileAttack">duong dan tuong doi file dinh kem tren server</param>
        /// <returns></returns>
        public static string Send(string to, string subject, string body, string bccTo = "" , string fileAttach = null)
        {
            try
            {
                string supportEmail = ConfigurationManager.AppSettings["SupportEmail"];
                string supportEmailPass = ConfigurationManager.AppSettings["SupportEmailPass"];
                
                SmtpClient client = new SmtpClient(smtp);
                NetworkCredential nc = new NetworkCredential(supportEmail, supportEmailPass);
                client.Credentials = nc;
                client.EnableSsl = false;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(supportEmail);
                
                //to
                string[] arrTo = to.Split(new char[] { ';' });
                for (int i = 0; i < arrTo.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(arrTo[i]) == false)
                    {
                        mail.To.Add(new MailAddress(arrTo[i]));
                    }

                }

                //bcc
                if (string.IsNullOrWhiteSpace(bccTo) == false)
                {
                    string[] ademail = bccTo.Split(new char[] { ';' });
                    foreach (var item in ademail)
                    {
                        if (string.IsNullOrWhiteSpace(item) == false)
                        {
                            mail.Bcc.Add(new MailAddress(item));
                        }
                    }

                }


                mail.Subject = subject;
                mail.Body = "<html><body>" + body + "</body></html>";
                mail.IsBodyHtml = true;
                mail.BodyEncoding = System.Text.Encoding.UTF8;


                if (string.IsNullOrWhiteSpace(fileAttach) == false)
                {
                    string[] attachFileArr = fileAttach.Split(new char[] { ';' });
                    foreach (var item in attachFileArr)
                    {
                        if (string.IsNullOrWhiteSpace(item) == false)
                        {
                            Attachment attack = new Attachment(item, MediaTypeNames.Application.Octet);
                            mail.Attachments.Add(attack);
                        }
                    }

                }

                client.Send(mail);

                client.Dispose();

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



        }

        #endregion

        /// <summary>
        /// gui email thong bao
        /// </summary>
        /// <param name="body"></param>
        /// <param name="to"></param>
        /// <param name="fullname"></param>
        /// <param name="subject"></param>
        /// <param name="bccTo"> bcc cho admin</param>
        /// <param name="footer">false: khong dung default footer.</param>
        /// <returns>tra ve thong tin loi neu khong gui duoc</returns>
        public static string SendEmailNotice(string body, string to, string firstname, string subject, string bccTo = "", bool footer = true)
        {
            string b = "Dear " + firstname + ",<br/>";
            b += body;
            if (footer)
            {
                b += "<br/><br/>Best Regards, <br/>" + _domain + "<br/><hr/>" +
                    "<p  style='color:darkgrey' >Please do not reply to this email.This mailbox is not monitored and you will not receive a response.</p>";
            }
           return Send(to, subject, b, bccTo);
        }


        #region changes pass or forgot password

        /// <summary>
        ///  Email notice when password has changed
        /// </summary>
        public static string SendEmailAfterChangedPass(string memberName, string email, string password, string to, string bccTo = "", string lang = "en")
        {
            if (lang == "en")
            {
                string body = "<p>Dear " + memberName + ", </p> " +
                        "<p>You are receiving this notification because you have changed password  in <a href='" + _domain + "'>" + _domain + "</a>.</p>" +
                        "<p>Your account information is as follows:</p>" +
                        "<p>Email: " + email + " </p>" +
                        "<p>Password : " + password + " </p> " +
                        "<br/>" +

                       "<p>Best Regards,<br/>" +
                       "<a href='" + _domain + "'>" + _domain + "</a> </p>";

               return Send(to, "Your password has changed", body);

            }
            else
            {
                string body = "<p>Xin chào " + memberName + ", </p> " +
                        "<p>Mật khẩu của bạn đã được thay đổi trên <a href='" + _domain + "'>" + _domain + "</a>.</p>" +
                        "<p>Đây là mật khẩu đăng nhập mới của bạn:</p>" +
                        "<p>Email: " + email + " </p>" +
                        "<p>Password : " + password + " </p> " +
                        "<br/>" +

                       "<p><br/>Trân Trong!<br/>" +
                       "<a href='" + _domain + "'>" + _domain + "</a> </p>";

               return Send(to, "Mật khẩu của bạn đã được thay đổi", body);
            }
            

        }

        public static string ForgotPassword(string name, string email, string password, string to, string bccTo = "", string lang = "en")
        {
            if (lang == "en")
            {
                string body = "<p>Dear " + name + ", </p> " +
                        "<p>Your account information is as follows:</p>" +
                        "<p>Email: " + email + " </p>" +
                        "<p>Password : " + password + " </p> " +
                        "<br/>" +

                       "<p>Best Regards,<br/>" +
                       "<a href='" + _domain + "'>" + _domain + "</a> </p> ";
                      

              return  Send(to, "Your password notification", body, bccTo);
            }
            else
            {
                string body = "<p>Xin chào " + name + ", </p> " +
                        "<p>Chúng tôi xin gửi bạn thông tin đăng nhập:</p>" +
                        "<p>Email: " + email + " </p>" +
                        "<p>Password : " + password + " </p> " +
                        "<br/>" +

                       "<p><br/>Trân Trọng!<br/>" +
                       "<a href='" + _domain + "'>" + _domain + "</a></p> ";
                 return   Send(to, "Thông tin đăng nhập", body, bccTo);
            }
            

        }

        #endregion


    }
}