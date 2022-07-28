using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quản_lí_Account_Facebook
{
    public partial class Form1 : Form
    {
        QLTKFBEntities db = new QLTKFBEntities();
        
        String taikhoan;
        string tk, mk, tnd, email;
        int sdt;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Table_TKFB.ToList();
            Lock();
        }
        private void Lock()
        {
            TxtEmail.Enabled = false;
            TxtMk.Enabled = false;
            TxtSdt.Enabled = false;
            TxtTk.Enabled = false;
            TxtTnd.Enabled = false;
            BtnGhi.Enabled = false;
            BtnXoa.Enabled = false;
            BtnSua.Enabled = false;
        }
        private void UnLock()
        {
            TxtEmail.Enabled = true;
            TxtMk.Enabled = true;
            TxtSdt.Enabled = true;
            TxtTk.Enabled = true;
            TxtTnd.Enabled = true;
            BtnGhi.Enabled = true;
        }
        private void Delete()
        {
            TxtEmail.Text = "";
            TxtMk.Text = "";
            TxtSdt.Text = "";
            TxtTk.Text = "";
            TxtTnd.Text = "";
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            UnLock();
            Delete();
            BtnSua.Enabled = false;
        }

        private void BtnGhi_Click(object sender, EventArgs e)
        {
            tk = TxtTk.Text;
            mk = TxtMk.Text;
            tnd = TxtTnd.Text;
            //sdt = int.Parse(TxtSdt.Text);
            email = TxtEmail.Text;
            if (tk==""||mk==""||tnd==""||TxtSdt.Text==null||email=="")    
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin");
            }
            else
            {
                if (db.Table_TKFB.Where(x=>x.Tài_khoản==tk).Count()>0)
                {
                    MessageBox.Show("Tài khoản đã tồn tại");
                }
                else
                {
                    Table_TKFB tb = new Table_TKFB();
                    sdt = int.Parse(TxtSdt.Text);
                    tb.Tài_khoản = tk;
                    tb.Mật_khẩu = mk;
                    tb.Tên_người_dùng = tnd;
                    tb.Số_điện_thoại = sdt;
                    tb.Email = email;
                    db.Table_TKFB.Add(tb);
                    db.SaveChanges();
                    dataGridView1.DataSource = db.Table_TKFB.ToList();
                    MessageBox.Show("Tạo tài khoản thành công");
                }
            }
        }

        private void BtnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (taikhoan==null)
            {
                MessageBox.Show("Chư chọn dữ liệu..chưa thể xóa");
            }
            else
            {
                DialogResult cauhoi = MessageBox.Show("Bạn có thực sự muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo);
                if (cauhoi!=DialogResult.No)
                {
                    if (taikhoan=="admin")
                    {
                        MessageBox.Show("Đây là tài khoản của admin...Không thể xóa");
                    }
                    else
                    {
                        var dlxoa = db.Table_TKFB.Where(i => i.Tài_khoản == taikhoan).FirstOrDefault();
                        if (dlxoa!=null)
                        {
                            db.Table_TKFB.Remove(dlxoa);
                            db.SaveChanges();
                            MessageBox.Show("Xóa thành công");
                            dataGridView1.DataSource = db.Table_TKFB.ToList();
                        }
                    }
                }
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            taikhoan = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtEmail.Enabled = true;
            TxtMk.Enabled = true;
            TxtSdt.Enabled = true;
            TxtTk.Enabled = false;
            TxtTnd.Enabled = true;
            BtnXoa.Enabled = true;
            BtnSua.Enabled = true;

            TxtTk.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtMk.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            TxtTnd.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtSdt.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            TxtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            //DialogResult delete =MessageBox.Show("Thực hiện điều này sẽ xóa bản cũ đi", "", MessageBoxButtons.YesNo);
            //if (delete!=DialogResult.No)
            //{
            //    var dlsua = db.Table_TKFB.Where(i => i.Tài_khoản == taikhoan).FirstOrDefault();
            //    if (dlsua != null)
            //    {
            //        db.Table_TKFB.Remove(dlsua);
            //        db.SaveChanges();
            //        dataGridView1.DataSource = db.Table_TKFB.ToList();
            //    }
            //}

            tk = TxtTk.Text;
            mk = TxtMk.Text;
            tnd = TxtTnd.Text;
            email = TxtEmail.Text;
            if (tk == "" || mk == "" || tnd == "" || TxtSdt.Text == null || email == "")
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin");
            }
            else
            {
                sdt = int.Parse(TxtSdt.Text);
                DialogResult update = MessageBox.Show("Xác nhận cập nhật", "", MessageBoxButtons.YesNo);
                if (update != DialogResult.No)
                {
                    Table_TKFB tb=db.Table_TKFB.Where(x=>x.Tài_khoản==taikhoan).FirstOrDefault();
                    tb.Mật_khẩu = mk;
                    tb.Tên_người_dùng = tnd;
                    tb.Số_điện_thoại = sdt;
                    tb.Email = email;
                    db.SaveChanges();
                    dataGridView1.DataSource = db.Table_TKFB.ToList();
                }
            }
        }
    }
}
