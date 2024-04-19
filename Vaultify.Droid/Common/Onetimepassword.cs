using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using MailKit.Net.Smtp;
using MimeKit;
using System;

using System.Threading.Tasks;

namespace Vaultify.Droid.Common
{
    public class Onetimepassword
    {
        public static string OTP { get; set; }


        //method for generating OTP
        public static async Task SendOTP(string email)
        {
            string otp_char = "0123456789";
            OTP = "";
            Random rnd = new Random();

            for (int i = 0; i < 6; i++)
            {

                var random_char = otp_char[rnd.Next(1, otp_char.Length)];
                OTP += random_char;

            }

            string msg = OTP;
            string senderEmail, senderPass, receiverEmail;
            receiverEmail = email;
            senderEmail = "timyvillarmia@gmail.com"; //Change this to your Sender's Gmail Email Address
            senderPass = "kwlo yjjj yahw maqn";  //Gmail's App Password Change this to your Sender's Gmail App Password

            MimeMessage message = new MimeMessage(); // Creating object for Message
            message.From.Add(new MailboxAddress("Vaultify - OTP", senderEmail)); //Sender's information
            message.To.Add(MailboxAddress.Parse(receiverEmail)); //Receiver's Information

            message.Subject = "One-Time-Password"; //Email's Subject

            //Email's Body
            message.Body = new TextPart("plain") //Plain text
            {
                Text = msg  //MSG = OTP
            };

            // allows sending of e-mail notifications using a SMTP server
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 465, true); //Gmail's smtp server, PORT: 465
                    client.Authenticate(senderEmail, senderPass); //Login sender's email and password
                    await client.SendAsync(message); //
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    client.Disconnect(true); // always Disconnect the service.
                    client.Dispose(); //Releases all resource used by the MailService object.
                }
            }




        }
    }
}