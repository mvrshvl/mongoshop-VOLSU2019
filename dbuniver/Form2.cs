using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;
namespace dbuniver
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void CheckLogin_Click(object sender, EventArgs e)
        {
            List<Card> list;
            IMongoCollection<Card> collection;
            
            try
            {
                var connectionString = "mongodb://" + login.Text + ":" + password.Text + "@localhost";
                Program.client = new MongoClient(connectionString);
                Program.db = Program.client.GetDatabase("shop");
                Program.login_flag = true;
                Program.login_str = login.Text;
                Close();
            }
            catch(Exception er)
            {

                error.Text = "Не удалось войти";
            }
                
            
        }

        private void CloseDialog_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
