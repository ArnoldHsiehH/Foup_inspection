using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using Dapper;
using System.Security.Cryptography;
using System.IO;

namespace new_inspection
{
    public partial class frmAccount : Form
    {

        AES aes = new AES();
        public frmAccount()
        {
            InitializeComponent();
        }
        public IDbConnection GetConnection()
        {
            return new SQLiteConnection(@"Data source= C:\EFEMdb\userDB.db;Version=3;New=true;");
        }

        private void btn_Adduser_Click(object sender, EventArgs e)
        {
            using (IDbConnection db = GetConnection())
            {
                string account_number = textBox1.Text;
                string password = aes.aesEncryptBase64(textBox2.Text);

                if (null != db.QuerySingleOrDefault<user>("select * from users where account_number = '" + account_number + "'"))
                {
                    MessageBox.Show("user already exist");
                    return;
                }


                int n = db.Execute("insert into users (account_number,password,manul,opration,log,result,setting,setuser)" +
                    "values(@account_number,@password,@manul,@opration,@log,@result,@setting,@setuser)",
                    new
                    {
                        account_number = account_number,
                        password = password,
                        manul = 0,
                        opration = 0,
                        log = 0,
                        result = 0,
                        setting = 0,
                        setuser = 66
                    });

                if (n > 0)
                {
                    //    MessageBox.Show("inserted");
                    LoadData();
                }

            }
        }


        private void btn_set_pw_Click(object sender, EventArgs e)
        {
            string password = aes.aesEncryptBase64(textBox3.Text);

            string account_number = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            using (IDbConnection db = GetConnection())
            {
                db.Execute("update users set password=@password where account_number=@account_number",
                    new { password = password, account_number = account_number });
                MessageBox.Show("update");
                LoadData();

            }
        }

        private void btn_set_Click(object sender, EventArgs e)
        {
            string account_number = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            int manul = (int)dataGridView1.CurrentRow.Cells[2].Value;
            int opration = (int)dataGridView1.CurrentRow.Cells[3].Value;
            int log = (int)dataGridView1.CurrentRow.Cells[4].Value;
            int result = (int)dataGridView1.CurrentRow.Cells[5].Value;
            int setting = (int)dataGridView1.CurrentRow.Cells[6].Value;
            int setuser = (int)dataGridView1.CurrentRow.Cells[7].Value;
            using (IDbConnection db = GetConnection())
            {

                db.Execute("update users set manul=@manul,opration=@opration ,log=@log,result=@result,setting=@setting,setuser=@setuser where account_number=@account_number",
                    new { manul = manul, opration = opration, log = log, result = result, setting = setting, setuser = setuser, account_number = account_number });
                MessageBox.Show("update");
                LoadData();

            }
        }


        private void btn_delete_Click(object sender, EventArgs e)
        {
            string account_number = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            using (IDbConnection db = GetConnection())
            {
                int n = db.Execute("delete from users where account_number=@account_number", new { account_number = account_number });
                if (n > 0)
                {
                    //              MessageBox.Show("deleted");
                    LoadData();
                }

            }
        }

        private void frmAccount_Load(object sender, EventArgs e)
        {
            LoadData();

        }

        private void LoadData()
        {
            if (System.IO.File.Exists(@"C:\EFEMdb\userDB.db"))
            {
                using (IDbConnection db = GetConnection())
                {
                    var list = db.Query<user>("select * from users").ToList();
                    List<user> sectionList = list;

                    if (list.Count() > 0)
                    {
                        foreach (user User in sectionList)
                        {

                            User.password = "**";
                        }
                        list.RemoveAt(0);
                        dataGridView1.DataSource = list;
                    }


                }
            }
        }

    }

    public class user
    {
        public string account_number { get; set; }
        public string password { get; set; }
        public int manul { get; set; }
        public int opration { get; set; }
        public int log { get; set; }
        public int result { get; set; }
        public int setting { get; set; }
        public int setuser { get; set; }



    }

}
