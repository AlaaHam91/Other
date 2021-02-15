using BookStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Entity;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;

namespace BookStore.Domain.Concerate
{
   public class EmailSettings
    {
        public string MailToAdress = "orders@xbooksstore.com,admin@xbooksstore.com";
        public string MailFromAdress = "infoxbooksstore@gmal.com";//system mail, need the passord
        public bool UseSsl = false;//as sent from file
        public string Username= "infoxbooksstore@gmal.com";
        public string Password = "MyPassword";
        public string ServerName = "smtp.gmail.com";
        public int  ServerPort = 587;//smtp port for ssl
        public bool WriteAsFile = false;//incase offline to save mail
        public string FileLocation = @"C:\orders_bookstore_email";
    }
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSetting;
        public  EmailOrderProcessor(EmailSettings setting)
        {
            emailSetting = setting;
        }
        void IOrderProcessor.ProcessOrder(Cart cart, ShippingDetails shippingdetails)
        {
            using (var smptmClient = new SmtpClient())
            {
                smptmClient.EnableSsl = emailSetting.UseSsl;
                smptmClient.Host = emailSetting.ServerName;
                smptmClient.Port = emailSetting.ServerPort;
                smptmClient.UseDefaultCredentials = false;
                smptmClient.Credentials = new NetworkCredential(emailSetting.Username,emailSetting.Password);
                if (emailSetting.WriteAsFile)//save the mail first then send them
                {
                    smptmClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smptmClient.PickupDirectoryLocation = emailSetting.FileLocation;
                    smptmClient.EnableSsl= false;
                }
                StringBuilder body = new StringBuilder()
                    .AppendLine("A new order has been submitted")
                    .AppendLine("......")
                    .AppendLine("Books:");
                foreach (var line in cart.Lines)
                {
                    var subtotal =line.Book.Price*line.Quantity;
                    body.AppendFormat("{0}x{1} (subtotal: {2:c})"
                        ,line.Quantity,line.Book.Title,subtotal);
                }
                body.AppendFormat("Total order value:{0:c}", cart.ComputeTotalValue())
                    .AppendLine("-----")
                    .AppendLine("Ship To:")
                    .AppendLine(shippingdetails.Name)
                    .AppendLine(shippingdetails.Line1)
                    .AppendLine(shippingdetails.Line2)
                    .AppendLine(shippingdetails.State)
                    .AppendLine(shippingdetails.City)
                    .AppendLine(shippingdetails.Country)
                    .AppendLine("-------")
                    .AppendFormat("GiftWarp {0}",shippingdetails.GiftWrap ?"Yes" : "No");

                MailMessage mailMessage = new MailMessage(
                    emailSetting.MailFromAdress, emailSetting.MailToAdress, "New Order Submitted", body.ToString()
                    
                    );

                if (emailSetting.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }
                try
                {
                    smptmClient.Send(mailMessage);
                }
                catch(Exception e)
                {
                    Debug.Print(e.ToString());
                }
            }
        }
    }
}
