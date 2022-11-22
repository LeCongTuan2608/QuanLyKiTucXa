using MySql.Data.MySqlClient;

namespace QuanLyKiTucXa
{
    public partial class Form1 : Form
    {
        connection con = new connection();
        string username, password;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text != "" && txtPassword.Text != "")
                {

                    con.Open();
                    string query = "select * from tai_khoan WHERE id_user ='" + txtUserName.Text + "' AND mat_khau ='" + txtPassword.Text + "'";
                    MySqlDataReader row;
                    row = con.ExecuteReader(query);
                    if (row.HasRows)
                    {
                        while (row.Read())
                        {
                            username = row["id_user"].ToString();
                            password = row["mat_khau"].ToString();
                        }
                        this.Hide();
                        Home home = new Home(txtUserName.Text);
                        home.ShowDialog();
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Sai tài khoản hoặc mật khẩu", "Information");
                    }
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không được để trống", "Information");
                }
            }
            catch
            {
                MessageBox.Show("Connection Error", "Information");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Bạn có muốn thoát khỏi chương trình không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}