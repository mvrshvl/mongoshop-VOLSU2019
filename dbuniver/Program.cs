using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;
namespace dbuniver
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 
        public static bool login_flag = false;
        public static MongoClient client;
        public static IMongoDatabase db;
        public static String login_str;
        public static String errorLogin = "";
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form2 f = new Form2();
            f.ShowDialog();
            if(login_flag)
            {
                //f.Close();
                Application.Run(new Form1());
            }



        }
    }
}
