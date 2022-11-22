using Google.Protobuf.WellKnownTypes;
using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanLyKiTucXa
{

    public partial class Home : Form
    {
        connection con = new connection();
        string toa;
        string ma_toa;
        string id_user;
        string ma_hoa_don;
        string value_PT_thanh_toan;
        public Home()
        {
            InitializeComponent();
            toa = "Tòa A";
            comboBox1.Text = toa;
            ma_toa = toa.Substring(toa.Length - 1, 1);


        }
        public Home(string id) : this()
        {
            id_user = id;
        }
        private void MenuItemLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.ShowDialog();
            this.Close();

        }

        private void Home_Load(object sender, EventArgs e)
        {


            Load_SoLuong();
            Load_Tong_SV();

            Load_DS_Toa();
            Load_DS_Phong(ma_toa);
            string query = "";
            if (id_user != "admin")
            {
                Load_Hoa_Don();
                Load_DS_Thong_Ke(query);
            }
            else
            {
                query = "select *, so_nuoc_cuoi - so_nuoc_dau as so_nuoc, "
                    + "so_dien_cuoi - so_dien_dau as so_dien from hoa_don_dien_nuoc";
                Load_DS_Thong_Ke(query);

            }


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            toa = comboBox1.Text;
            ma_toa = toa.Substring(toa.Length - 1, 1);

            Load_DS_Phong(ma_toa);
        }
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                bool isChecked = radioButton1.Checked;
                if (isChecked)
                    value_PT_thanh_toan = "MoMo";
                else
                    value_PT_thanh_toan = radioButton2.Text;

                con.Open();
                string queryString = "UPDATE hoa_don_dien_nuoc SET "
                    + "trang_thai = 'Đã thanh toán', phuong_thuc_thanh_toan = '"+value_PT_thanh_toan+"' "
                    + "WHERE ma_hoa_don = '"+ma_hoa_don+"';";

                MySqlDataReader row;
                row = con.ExecuteReader(queryString);
                btnThanhToan.Text = "Đã thanh toán";
                btnThanhToan.Enabled = false;
                con.Close();
            }
            catch
            {
                MessageBox.Show("Connection Error", "Information");
            }
        }
        // ---------------------------Load MySQl-------------------------------------------

        // thêm tổng số lượng phòng trống
        public void Load_SoLuong()
        {
            try {
                con.Open();
                string queryString = "select count(ma_phong) as tong_phong_trong, sum(suc_chua - sl_dang_o) as tong_cho_trong "
                    + "from phong where sl_dang_o < suc_chua;";

                MySqlDataReader row;
                row = con.ExecuteReader(queryString);

                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        tongPhongTrong.Text = row["tong_phong_trong"].ToString();
                        tongChoTrong.Text = row["tong_cho_trong"].ToString();
                    }
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Connection Error", "Information");
            }
        }


        // thêm tổng số lượng phòng trống
        public void Load_Tong_SV()
        {
            try {
                con.Open();
                string queryString = "select count(id_user) as tong_SV  from `user` where id_user != 'admin';";

                MySqlDataReader row;
                row = con.ExecuteReader(queryString);

                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        tongSV.Text = row["tong_SV"].ToString();
                    }
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Connection Error", "Information");
            }
        }

        // thêm tổng số lượng phòng trống
        public void Load_DS_Toa()
        {
            try {
                con.Open();
                string queryString = "select ma_toa from toa";

                MySqlDataReader row;
                row = con.ExecuteReader(queryString);

                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        comboBox1.Items.Add("Tòa "+ row["ma_toa"].ToString());
                    }
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Connection Error", "Information");
            }
        }


        // thêm tổng số lượng phòng trống
        public void Load_DS_Phong(string ma_toa)
        {
            try { 
                con.Open();
                string queryString = "select ma_phong, suc_chua, sl_dang_o, ma_loai from phong where ma_toa='" + ma_toa + "'";

                MySqlDataReader row;
                row = con.ExecuteReader(queryString);

                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = row["ma_phong"].ToString();
                        dataGridView1.Rows[index].Cells[1].Value = row["sl_dang_o"].ToString() + "/" + row["suc_chua"].ToString();
                        dataGridView1.Rows[index].Cells[2].Value = row["ma_loai"].ToString();


                    }
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Connection Error", "Information");
            }
        }

        public void Load_Hoa_Don()
        {
            try {
                btnThanhToan.Text = "Thanh toán";
                btnThanhToan.Enabled = true;

                con.Open();
                string queryString = "select hoa_don_dien_nuoc.ma_hoa_don, hoa_don_dien_nuoc.ma_phong, "
                    + "(hoa_don_dien_nuoc.so_nuoc_cuoi - hoa_don_dien_nuoc.so_nuoc_dau)*hoa_don_dien_nuoc.don_gia_nuoc as tien_nuoc, "
                    +"(hoa_don_dien_nuoc.so_dien_cuoi - hoa_don_dien_nuoc.so_dien_dau)*hoa_don_dien_nuoc.don_gia_dien as tien_dien, "
                    +"hoa_don_dien_nuoc.thanh_tien from hoa_don_dien_nuoc,`user` where`user`.id_user = '"+id_user+"' "
                    +"and so_dien_cuoi = (SELECT MAX(so_dien_cuoi) "
                    +"FROM hoa_don_dien_nuoc where hoa_don_dien_nuoc.ma_phong = `user`.ma_phong "
                    +"and hoa_don_dien_nuoc.trang_thai = 'Chưa thanh toán');"
                    + "select ho_ten, sdt from `user` where id_user = 'admin';";


                MySqlDataReader row;
                row = con.ExecuteReader(queryString);

                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        if (!row.IsDBNull("ma_hoa_don") && row["ma_hoa_don"].ToString() != "")
                        {
                            ma_hoa_don = row["ma_hoa_don"].ToString();
                            textPhong.Text = row["ma_phong"].ToString();
                            textTienNuoc.Text = String.Format("{0:n0}", row.GetValue("tien_nuoc")) + "đ";
                            textTienDien.Text = String.Format("{0:n0}", row.GetValue("tien_dien")) + "đ";
                            textTong.Text = String.Format("{0:n0}", row.GetValue("thanh_tien")) + "đ";
                        }
                    }
                    row.NextResult();
                    row.Read();
                    textName.Text = row["ho_ten"].ToString();
                    textPhone.Text = row["sdt"].ToString();
                }
                else
                {
                    btnThanhToan.Text = "Không có hóa đơn nào!!";
                    btnThanhToan.Enabled = false;

                    textTienNuoc.Text = "0đ";
                    textTienDien.Text = "0đ";
                    textTong.Text = "0đ";
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Connection Error", "Information");
            }
        }
        public void Load_DS_Thong_Ke(string? query_admin)
        {
            try {
                con.Open();
                string queryString;
                if (query_admin != "")
                {
                    queryString = query_admin;
                }
                else
                {
                    queryString = "select *, so_nuoc_cuoi - so_nuoc_dau as so_nuoc, "
                        +"so_dien_cuoi - so_dien_dau as so_dien from hoa_don_dien_nuoc, phong, `user`"
                        +" where  hoa_don_dien_nuoc.ma_phong = phong.ma_phong and "
                        +"`user`.ma_phong = phong.ma_phong and `user`.id_user = '"+ id_user + "'";
                }

                MySqlDataReader row;
                row = con.ExecuteReader(queryString);

                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        var index = dataGridView2.Rows.Add();
                        dataGridView2.Rows[index].Cells[0].Value = row["ma_hoa_don"].ToString();
                        dataGridView2.Rows[index].Cells[1].Value = row["ma_phong"].ToString();
                        dataGridView2.Rows[index].Cells[2].Value = row["ngay_lap"].ToString();
                        dataGridView2.Rows[index].Cells[3].Value = row["so_nuoc_cuoi"].ToString() +" - "+ row["so_nuoc_dau"].ToString() +" = "+ row["so_nuoc"].ToString();
                        dataGridView2.Rows[index].Cells[4].Value = row["so_dien_cuoi"].ToString() + " - " + row["so_dien_dau"].ToString() + " = " + row["so_dien"].ToString();
                        dataGridView2.Rows[index].Cells[5].Value = row["thanh_tien"].ToString();
                        dataGridView2.Rows[index].Cells[6].Value = row["trang_thai"].ToString();
                    }
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Connection Error", "Information");
            }
        }
        // mở form mới
        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Infomation info = new Infomation(id_user);
            info.ShowDialog();
            this.Close();

        }

        private void label12_Click(object sender, EventArgs e)
        {
            Load_Hoa_Don();
        }
    }
}
