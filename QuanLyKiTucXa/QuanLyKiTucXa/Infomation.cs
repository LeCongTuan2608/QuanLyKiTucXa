using Google.Protobuf;
using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanLyKiTucXa
{
    public partial class Infomation : Form
    {
        connection con = new connection();
        string id_user;
        string file_image;
        public Infomation()
        {
            InitializeComponent();
        }
        public Infomation(string id) : this()
        {
            id_user = id;
            txtID.Text = id_user;
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = new Home(id_user);
            home.ShowDialog();
            this.Close();
        }

        private void Infomation_Load(object sender, EventArgs e)
        {
            getInfoUser();
            //pictureBox1.Image = Image.FromFile(@"D:/images-Photoshop/banana1.png");
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            setInfoUser();
        }
        private void btnRevert_Click(object sender, EventArgs e)
        {
            getInfoUser();
        }
        public void setInfoUser()
        {
            try
            {
                con.Open();
                MySqlDataReader row;
                string dateTimeString = dateTimePicker1.Value.ToString("yyyy-MM-dd");

                string query = "UPDATE `user` SET ho_ten = '"+ txtName.Text +"', "
                    +"dia_chi = '"+txtAddR.Text+"', sdt = '"+ txtPhone.Text +"', ngay_sinh = '"
                    + dateTimeString + "', image = N'"+ file_image.Replace(@"\", "/") +"' "
                    +"WHERE id_user = '"+ id_user +"';";

                row = con.ExecuteReader(query);
                con.Close();
            }
            catch
            {
                MessageBox.Show("Connection Error", "Information");
            }
        }
        public void getInfoUser()
        {
            try
            {
                con.Open();
                MySqlDataReader row;
                string query;
                if (id_user == "admin")
                {
                    query = "select *, year(`user`.ngay_sinh) as nam, month(`user`.ngay_sinh) as thang,"
                        + " day(`user`.ngay_sinh) as ngay from `user` where id_user = 'admin'";
                }
                else
                {
                    query = "select `user`.cmnd, `user`.ho_ten, `user`.gioi_tinh, `user`.dia_chi, "
                    + "`user`.sdt,`user`.ma_phong,`user`.image, `phong`.ma_toa, year(`user`.ngay_sinh) as nam, "
                    + "month(`user`.ngay_sinh) as thang, day(`user`.ngay_sinh) as ngay "
                    + "from `user` , phong "
                    + "where id_user = '" + id_user + "' and `user`.ma_phong = `phong`.ma_phong;";
                }
                row = con.ExecuteReader(query);
                if (row.HasRows)
                {
                    row.Read();
                    txtName.Text = row["ho_ten"].ToString();
                    txtCMND.Text = row["cmnd"].ToString();
                    dateTimePicker1.Value = new DateTime(row.GetInt32("nam"), row.GetInt32("thang"), row.GetInt32("ngay"));
                    txtAddR.Text = row["dia_chi"].ToString();
                    txtPhone.Text = row["sdt"].ToString();

                    if (!row.IsDBNull("image") && row["image"].ToString() != "")
                    {
                        file_image = row["image"].ToString();
                        pictureBox1.Image = Image.FromFile(row["image"].ToString());
                    }


                    if (id_user != "admin")
                    {
                        textToa.Text = "Tòa "+row["ma_toa"].ToString();
                        textMaPhong.Text = row["ma_phong"].ToString();
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Error", "Information");
                }
            }
            catch
            {
                MessageBox.Show("Connection Error", "Information");
            }
        }

        private void btnUpdateImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                file_image = file.FileName;
                pictureBox1.Image = Image.FromFile(file_image);
            }
        }
    }
}
