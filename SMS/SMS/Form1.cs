using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace SMS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string to, from, mess, pass;
            MailMessage message = new MailMessage();

            to = (txtNumber.Text + "@txtlocal.co.uk").ToString();
            //from = (txtAccount.Text).ToString();
            mess = txtMessage.Text;
            //pass = (txtPass).ToString();

            message.To.Add(to);
            message.From = new MailAddress("xxx@gmail.com");
            message.Body = mess;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("xxx@gmail.com", "passxxx");

            try
            {
                smtp.Send(message);
                MessageBox.Show("Send Successfully", "message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
