using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using QLHV_API.Entities;

namespace QLHV_API.Support
{
    public class support
    {
        QLHVContext _context = new QLHVContext();
        public string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public string GetFileExtension(string s)
        {
            string[] Name_extension = s.Split('.');
            int countarray = Name_extension.Count();
            s = Name_extension[countarray - 1];
            return s;
        }

        public string GetIPv4InPC()
        {
            string hostName = Dns.GetHostName(); // Lấy tên máy tính
            IPHostEntry hostEntry = Dns.GetHostEntry(hostName); // Lấy thông tin về máy tính
            string ip = "127.0.0.1";
            foreach (IPAddress ipAddress in hostEntry.AddressList)
            {
                if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ip = ipAddress.ToString();
                }
            }
            return ip;
        }

        public bool CheckIsEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            bool isValidEmail = Regex.IsMatch(email, pattern);
            return isValidEmail;
        }

        public DateTime ConvertToDateTime(string input)
        {
            string format = "yyyy-MM-dd h:mm:ss:tt";

            if (DateTime.TryParseExact(input, format, null, System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            else
            {
                return DateTime.Now;
            }
        }
        public string MaHV()
        {
            int num = 0;
            var nckh = _context.Hocviens.Where(m => m.MaHv.Substring(0, 3) == DateTime.Now.Year.ToString()).OrderByDescending(m => m.MaHv.Substring(5, 9)).FirstOrDefault();
            if (nckh == null || nckh.MaHv == null)
            {
                num++;
            }
            else
            {
                num++;
            }

            return DateTime.Now.Year.ToString() + "." + num.ToString().PadLeft(5, '0');

        }
        public void sendEmail(string name, string receiver, string linkUrl)
        {
            string sender = "baodepbaobao@gmail.com";
            string pass = "xgdenrcsaftrocjo";
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(sender);
            mail.To.Add(new MailAddress(receiver));
            mail.Subject = "Đặt lại mật khẩu hệ thống LMS";

            // Creating the link using an anchor tag
            string linkText = " tại đây";
            string linkHtml = $"<br><a>Đổi mật khẩu</a><a href='{linkUrl}'>{linkText}</a>";

            mail.Body = $"Tài khoản {name} của hệ thống QLHV đã được yêu cầu thay đổi mật khẩu. Nếu đây là bạn, vui lòng sử dụng liên kết sau để đặt lại mật khẩu của bạn. {linkHtml}";
            mail.IsBodyHtml = true; // Set to true since the body contains HTML
            mail.Priority = MailPriority.Normal;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(sender, pass);
            smtp.Send(mail);
        }
    }
}
