using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;

namespace dbuniver
{
    public partial class Form1 : Form
    {

        
        public static IMongoCollection<Card> collection;
        public static IMongoCollection<Product> collection_product;
        public static IMongoCollection<Users> collection_users;
        public static IMongoCollection<Raspisanie> collection_raspisanie;
        public static IMongoCollection<Product> collection_product_h;
        public static IMongoCollection<Check> collection_checks;
        List<Product> list_product;
        List<Product> list_product_h;
        List<Card> list;
        List<Users> list_user;
        List<Users> list_users;
        List<Raspisanie> list_raspisanie;
        List<Raspisanie> list_raspisanie_single;
        List<Check> list_check;
        static int level = 0;
        public static ObjectId UserId;
        private List<string> sortingStrings = new List<string>(); // products
        int column_sort;
        public Form1()
        {
            InitializeComponent();
            connect();


        }

        public void connect()
        {
            try
            {
                //"mongodb://manager_login:manager_pass@localhost"
                //var connectionString = "mongodb://"+login+":"+password+"@localhost";
                //Program.client = new MongoClient(connectionString);
                level = 1;

                collection = Program.db.GetCollection<Card>("card");
                collection_product = Program.db.GetCollection<Product>("product");
                collection_users = Program.db.GetCollection<Users>("users");
                collection_raspisanie = Program.db.GetCollection<Raspisanie>("raspisanie");
                collection_product_h = Program.db.GetCollection<Product>("product");
                collection_checks = Program.db.GetCollection<Check>("check");
                ReadAllDocuments();
                ReadProducts();
                readUser();
                ReadUsers();
                ReadRaspisanie();
                ReadProductHelp();
                ReadChecks();
            }
            catch (Exception er)
            {
                CurrentSotrudnik.Text = er.ToString();
                Program.login_flag = false;
                Program.errorLogin = "Bad login";
                //Program.Run();


            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Program.login_flag)
                Application.Restart();



        }
        private void readUser()
        {
            try
            {

                list_user = Users.findUsers(Program.login_str);
                Users user = list_user.First();
                CurrentSotrudnik.Text = user.Name;
                UserId = user.Id;
            }
            catch (Exception e)
            {
                String s = e.ToString();
            }
        }
        //Сотрудник


        ///CARDS///

        public void ReadAllDocuments()
        {

                list = collection.AsQueryable().ToList<Card>();
                addToGrid(list);

        }
        private void addToGrid(List<Card> list)
        {

            if (list.Count > 0)
            {
                dataGridView1.DataSource = list;
                textBox1.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
                ClearCard();
            }
            else
            {
                CardNotFound.Text = "Данные не найдены";
            }

        }
        //Скидочные карты/////////////////////////////////////////////////////////
        //find
        private void Button5_Click(object sender, EventArgs e)
        {
            var filter = new BsonDocument("$or", new BsonArray{
                        new BsonDocument("name",textBox2.Text),
                        new BsonDocument("phone", textBox3.Text)
            });
            list = collection.Find(filter).ToList<Card>();
            addToGrid(list);
        }
        private void ClearCard()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void Button4_Click(object sender, EventArgs e)//обновить содержимое
        {
            ReadAllDocuments();
        }

        private void AddCard_Click(object sender, EventArgs e)
        {

            Card s = new Card(textBox2.Text.ToString(), textBox3.Text.ToString());
            collection.InsertOne(s);
            ReadAllDocuments();
            ClearCard();

        }

        private void UpdateCard_Click(object sender, EventArgs e)
        {

            var updateDef = Builders<Card>.Update.Set("name", textBox2.Text).Set("phone", textBox3.Text);
            collection.UpdateOne(s => s.Id == ObjectId.Parse(textBox1.Text), updateDef);
            ReadAllDocuments();
            ClearCard();

        }

        private void DataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }

        }

        private void DeleteCard_Click(object sender, EventArgs e)
        {
            collection.DeleteOne(s => s.Id == ObjectId.Parse(textBox1.Text));
            ReadAllDocuments();
            ClearCard();
        }

        /// ////////////////////////////////


        public void ReadProducts()
        {

            try
            {
                list_product = collection_product.AsQueryable().ToList<Product>();
            }
            catch(Exception e)
            {
                String s = e.ToString();
                //show
            }
                
            addToGridProduct(list_product);
        }
        private void addToGridProduct(List<Product> list)
        {
            if (list_product.Count > 0)
            {
                dataProducts.DataSource = list;
                ClearCard();
            }
            else
            {
                //CardNotFound.Text = "Данные не найдены";
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///Экран склада
        /////////////////////////////////////////////////////////////////////////////////

        private void InsertProduct_Click(object sender, EventArgs e)
        {
            int costPr = 0;
            if (int.TryParse(CostProduct.Text, out costPr)) ;
            Product.insertProducr(NameProduct.Text, codeProduct.Text, int.Parse(quantityPtoduct.Text), costPr);
            ReadProducts();
        }

        private void FindProduct_Click(object sender, EventArgs e)
        {
            if (sortingStrings.Count > 0)
            {
                list_product = Product.findProduct(NameProduct.Text, Product.sorting(sortingStrings, typeSort.Checked));
            }
            else list_product = Product.findProduct(NameProduct.Text, "");

            addToGridProduct(list_product);
        }

        private void RefreshProduct_Click(object sender, EventArgs e)
        {
            ReadProducts();
        }

        private void DeleteProduct_Click(object sender, EventArgs e)
        {
            Product.deleteProduct(idProduct.Text);
            ReadProducts();
        }

        private void UpdateProduct_Click(object sender, EventArgs e)
        {
            int costPr = 0;
            if (int.TryParse(CostProduct.Text, out costPr)) ;
            Product.updateProduct(idProduct.Text, NameProduct.Text, codeProduct.Text, int.Parse(quantityPtoduct.Text), costPr);
            ReadProducts();
        }

        private void DataProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            column_sort = e.ColumnIndex;
            if (e.RowIndex >= 0)
            {
                idProduct.Text = dataProducts.Rows[e.RowIndex].Cells[0].Value.ToString();
                codeProduct.Text = dataProducts.Rows[e.RowIndex].Cells[2].Value.ToString();
                NameProduct.Text = dataProducts.Rows[e.RowIndex].Cells[1].Value.ToString();
                quantityPtoduct.Text = dataProducts.Rows[e.RowIndex].Cells[3].Value.ToString();
            }

        }

        private void ClearProduct_Click(object sender, EventArgs e)
        {
            codeProduct.Clear();
            NameProduct.Clear();
            quantityPtoduct.Clear();
            idProduct.Clear();
        }
        //-----------------------------------------------------------------------------


        private void Open_Click(object sender, EventArgs e)
        {
            if (Raspisanie.getStatus(DateTime.Today).Equals("-"))
                Raspisanie.updateStatus(DateTime.Today, "O");
            else Raspisanie.updateStatus(DateTime.Today, "C");
            ReadRaspisanie();
        }


        /////////////////////////////////////////////////////////////////////////////////
        ///Экран Управление сотрудниками
        /////////////////////////////////////////////////////////////////////////////////
        public void ReadUsers()
        {
            try
            {
                list_users = collection_users.AsQueryable().ToList<Users>();
            }
            catch (Exception e)
            {
                String s = e.ToString();
                //show
            }

            addToGridUsers(list_users);
        }
        private void addToGridUsers(List<Users> list)
        {
            if (list_users.Count > 0)
            {
                dataUsers.DataSource = list;
                //nameSotr.Text = dataUsers.Rows[0].Cells[0].Value.ToString();
                //phoneSotr.Text = dataUsers.Rows[0].Cells[1].Value.ToString();
                //loginSotr.Text = dataUsers.Rows[0].Cells[2].Value.ToString();           
            }
            else
            {
                //CardNotFound.Text = "Данные не найдены";
            }
        }
        private void refreshUsers()
        {
            ReadUsers();
            nameSotr.Clear();
            phoneSotr.Clear();
            loginSotr.Clear();
            passSotr.Clear();
            commentRasp.Clear();
        }
        private void RefreshSotr_Click(object sender, EventArgs e)
        {
            refreshUsers();
            refreshRaspisanie();
        }
        String idUser= "";
        private void DeleteSotr_Click(object sender, EventArgs e)
        {
            if(idUser.Length>0)
            Users.deleteUsers(idUser);
            refreshUsers();
        }

        private void UpdateSotr_Click(object sender, EventArgs e)
        {
            if (idUser.Length > 0 && nameSotr.Text.Length>0)
                Users.updateUsers(idUser, nameSotr.Text, phoneSotr.Text, loginSotr.Text);

            if (loginSotr.Text.Length > 0 && passSotr.Text.Length > 0)
                Users.updatePassword(loginSotr.Text, passSotr.Text);

            refreshUsers();
        }

        private void AddSotr_Click(object sender, EventArgs e)
        {
            Users.insertUsers(nameSotr.Text,phoneSotr.Text,loginSotr.Text);
            if (loginSotr.Text.Length > 0 && passSotr.Text.Length > 0)
                Users.addUserDatabase(loginSotr.Text, passSotr.Text);
            refreshUsers();
        }

        private void DataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idUser = dataUsers.Rows[e.RowIndex].Cells[0].Value.ToString();
            nameSotr.Text = dataUsers.Rows[e.RowIndex].Cells[1].Value.ToString();
            phoneSotr.Text = dataUsers.Rows[e.RowIndex].Cells[3].Value.ToString();
            loginSotr.Text = dataUsers.Rows[e.RowIndex].Cells[4].Value.ToString();
        }
        /////Расписание

        public void ReadRaspisanie()
        {
            try
            {
                list_raspisanie = collection_raspisanie.AsQueryable()
                    .OrderByDescending(c => c.Date)
                    .ToList<Raspisanie>();
                 list_raspisanie_single = collection_raspisanie.AsQueryable()
                .OrderByDescending(c => c.Date)
                .Where(c => c.Date > DateTime.Today)
                .Where(C => C.Sotrudnik == CurrentSotrudnik.Text)
                .ToList<Raspisanie>();

                if (Raspisanie.getStatus(DateTime.Today).Equals("O"))
                    Open.Text = "Закрыть смену";
                else if (Raspisanie.getStatus(DateTime.Today).Equals("C"))
                    Open.Text = "Смена закрыта";
            }
            catch (Exception e)
            {
                String s = e.ToString();
                //show
            }
            DateTime today = DateTime.Today.Date;
            currentDate.Text = today.ToString().Substring(0,10);
            SummaProdaj.Text = Check.getSum();


            addToGridRaspisanie(list_raspisanie);
            addToGridRaspisanieSingle(list_raspisanie_single);
        }
        private void addToGridRaspisanie(List<Raspisanie> list)
        {
            if (list_raspisanie.Count > 0)
            {
                dataRaspisanie.DataSource = list;
                //nameSotr.Text = dataUsers.Rows[0].Cells[0].Value.ToString();
                //phoneSotr.Text = dataUsers.Rows[0].Cells[1].Value.ToString();
                //loginSotr.Text = dataUsers.Rows[0].Cells[2].Value.ToString();           
            }
            else
            {
                //CardNotFound.Text = "Данные не найдены";
            }
        }
        private void addToGridRaspisanieSingle(List<Raspisanie> list)
        {
            if (list_raspisanie.Count > 0)
            {
                dataRasp.DataSource = list;
                //nameSotr.Text = dataUsers.Rows[0].Cells[0].Value.ToString();
                //phoneSotr.Text = dataUsers.Rows[0].Cells[1].Value.ToString();
                //loginSotr.Text = dataUsers.Rows[0].Cells[2].Value.ToString();           
            }
            else
            {
                //CardNotFound.Text = "Данные не найдены";
            }
        }
        private void refreshRaspisanie()
        {
            ReadRaspisanie();
        }
        private void AddRasp_Click(object sender, EventArgs e)
        {
            DateTime date = dateRasp.Value;
            if (commentRasp.Text.Length == 0)
                commentRasp.Text = "-";
            if(nameSotr.Text.Length>0 & date.ToString().Length>0)
            {
                if (DateTime.Now < dateRasp.Value)
                    Raspisanie.insertRasp(date.Date, nameSotr.Text, commentRasp.Text);
                refreshRaspisanie();
            }
        }
        string idRasp = "";
        private void UpdatePasp_Click(object sender, EventArgs e)
        {
            if (commentRasp.Text.Length == 0)
                commentRasp.Text = "-";
            if (nameSotr.Text.Length > 0 && idRasp.Length>0)
            {
                if (DateTime.Now < dateRasp.Value)
                    Raspisanie.updateRasp(idRasp, nameSotr.Text, commentRasp.Text);
                refreshRaspisanie();

            }
        }

        private void DeleteRasp_Click(object sender, EventArgs e)
        {
            Raspisanie.deleteRasp(idRasp);
            ReadRaspisanie();
        }

        private void DataRaspisanie_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idRasp = dataRaspisanie.Rows[e.RowIndex].Cells[0].Value.ToString();
            commentRasp.Text = dataRaspisanie.Rows[e.RowIndex].Cells[3].Value.ToString();
            dateRasp.Value = DateTime.Parse(dataRaspisanie.Rows[e.RowIndex].Cells[1].Value.ToString());
            nameSotr.Text = dataRaspisanie.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
        //Главный экран
        private string idProduct_help = "";
        private string nameProduct_help = "";
        private string codeProduct_help = "";
        private int countProduct_help = 0;
        private int countProduct_need = 0;
        private int cost = 0;
        private int sum = 0;
        private int currentPos = 0;
        //CurrentSotrudnik.Text
        private List<Product> check_list_products = new List<Product>();
        
        private void addToGridProductHelp(List<Product> list)
        {
            if (list_product.Count > 0)
            {
                dataProductHelp.DataSource = list;
                ClearCard();
            }
            else
            {
                //CardNotFound.Text = "Данные не найдены";
            }
        }
        public void ReadProductHelp()
        {
            try
            {
                list_product_h = collection_product_h.AsQueryable().ToList<Product>();
            }
            catch (Exception e)
            {
                String s = e.ToString();
                //show
            }

            addToGridProductHelp(list_product);
        }

        private void DataProductHelp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idProduct_help = dataProductHelp.Rows[e.RowIndex].Cells[0].Value.ToString();
            nameProduct_help = dataProductHelp.Rows[e.RowIndex].Cells[1].Value.ToString();
            codeProduct_help = dataProductHelp.Rows[e.RowIndex].Cells[2].Value.ToString();
            int.TryParse(dataProductHelp.Rows[e.RowIndex].Cells[3].Value.ToString(), out countProduct_help);
            int.TryParse(dataProductHelp.Rows[e.RowIndex].Cells[4].Value.ToString(), out cost);
        }

        private void AddToCheck_Click(object sender, EventArgs e)
        {
            Product product;
            if (int.TryParse(countProductHelp.Text, out countProduct_need))
            {
                if(countProduct_help >= countProduct_need)
                {
                    product = new Product(ObjectId.Parse(idProduct_help),nameProduct_help, codeProduct_help, countProduct_need, cost);
                    check_list_products.Add(product);
                }
            }
            addToGridCheck(check_list_products);
        }
        private void addToGridCheck(List<Product> list)
        {
            if (list.Count > 0)
            {
                dataProductsCheck.DataSource = null;
                dataProductsCheck.DataSource = list;
                dataProductsCheck.Refresh();
                sum += summ();
                sumCheck.Text = sum.ToString();
                //nameSotr.Text = dataUsers.Rows[0].Cells[0].Value.ToString();
                //phoneSotr.Text = dataUsers.Rows[0].Cells[1].Value.ToString();
                //loginSotr.Text = dataUsers.Rows[0].Cells[2].Value.ToString();           
            }
            else
            {
                dataProductsCheck.DataSource = null;
                sum = 0;
                sumCheck.Text = sum.ToString();
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            ReadProductHelp();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
          
            addToGridProductHelp(Product.findProduct(nameToFindPr.Text,""));
        }

        private void DeletePos_Click(object sender, EventArgs e)
        {
            //         idRasp = dataRaspisanie.Rows[e.RowIndex].Cells[0].Value.ToString();
            check_list_products.RemoveAt(currentPos);
            addToGridCheck(check_list_products);
        }

        private void DataProductsCheck_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentPos = e.RowIndex;

        }

        private void Button4_Click_1(object sender, EventArgs e)
        {
            check_list_products.Clear();
            addToGridCheck(check_list_products);
        }
        private int summ()
        {
            Decimal s = 0;
            foreach(Product p in check_list_products)
            {
                s += p.Cost * p.Quantity;
            }
            
            return int.Parse(s.ToString());
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Check.insertCheck(sum,check_list_products);
            foreach(Product p in check_list_products)
            {
                Product.updateQuantity(p);
            }
            ReadProducts();
            ReadProductHelp();
            check_list_products.Clear();
            addToGridCheck(check_list_products);
            SummaProdaj.Text = Check.getSum();


        }

        private void FindCheck_Click(object sender, EventArgs e)
        {
            list_check = Check.findCheck(checkNum.Text,dateCheck.Value.Date);
            dataChecksDel.DataSource = null;
            dataChecksDel.DataSource = list_check;
        }
        private List<Product>  list_one_pos = new List<Product>();
        private void DataChecksDel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            nCheckDel.Text = dataChecksDel.Rows[e.RowIndex].Cells[0].Value.ToString();
            dCheckDel.Text = dataChecksDel.Rows[e.RowIndex].Cells[3].Value.ToString();
            pCheckDel.Text = dataChecksDel.Rows[e.RowIndex].Cells[1].Value.ToString();

            
            list_one_pos = list_check.ElementAt(e.RowIndex).Products;
            dataCheckOne.DataSource = null;
            dataCheckOne.DataSource = list_one_pos;

        }

        public void ReadChecks()
        {
            try
            {
                list_check = collection_checks.AsQueryable()
                    .OrderByDescending(c => c.Date)
                    .ToList<Check>();
            }
            catch (Exception e)
            {
                String s = e.ToString();
                //show
            }

            addToGridChecks(list_check);
        }

        private void addToGridChecks(List<Check> list)
        {
            if (list.Count > 0)
            {
                dataChecks.DataSource = null;
                dataChecks.DataSource = list;
                dataChecks.Refresh();         
            }
            else
            {
                dataChecks.DataSource = null;

            }
        }

        private void DataProductCheck_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataChecks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            List<Product> list = new List<Product>();
            list = list_check.ElementAt(e.RowIndex).Products;
            dataProductCheck.DataSource = null;
            dataProductCheck.DataSource = list;
        }
        int index_del_pos = 0;
        private void DataCheckOne_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index_del_pos = e.RowIndex;
        }

        private void DeleteOne_Click(object sender, EventArgs e)
        {
            if (list_one_pos.Count > 1)
            {
                returnProd(list_one_pos.ElementAt(index_del_pos));
                list_one_pos.RemoveAt(index_del_pos);
                Check.updateCheck(nCheckDel.Text, list_one_pos);
                dataCheckOne.DataSource = null;
                dataCheckOne.DataSource = list_one_pos;
            }
            else
            {
                foreach(Product p in list_one_pos){
                    returnProd(p);
                }
                Check.deleteCheck(nCheckDel.Text);
                dataChecksDel.DataSource = null;
                dataCheckOne.DataSource = null;
            }

        }

        private void returnProd(Product p)
        { 
            p.Quantity = p.Quantity * -1;
            Product.updateQuantity(p);
        }

        private void DeleteAll_Click(object sender, EventArgs e)
        {
            foreach (Product p in list_one_pos)
            {
                returnProd(p);
            }
            Check.deleteCheck(nCheckDel.Text);
            dataChecksDel.DataSource = null;
            dataCheckOne.DataSource = null;
        }

        private void CostSort_CheckedChanged(object sender, EventArgs e)
        {
            if (NameSort.Checked && !IsAdded("name",sortingStrings)) sortingStrings.Add("name");

            else if(CountSort.Checked && !IsAdded("quantity", sortingStrings)) sortingStrings.Add("quantity");

            else if (CostSort.Checked && !IsAdded("cost", sortingStrings)) sortingStrings.Add("cost");

            else if (!NameSort.Checked) sortingStrings.Remove("name");

            else if (!CostSort.Checked) sortingStrings.Remove("cost");

            else if(!CountSort.Checked) sortingStrings.Remove("quantity");
        }
        private bool IsAdded(string us, List<String> list)
        {
                foreach (string s in list)
                {
                    if (s.Equals(us)) return true;
                }
                return false;
        }
    }
}
