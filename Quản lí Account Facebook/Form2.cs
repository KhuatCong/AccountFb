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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            QLTKFBEntities lg = new QLTKFBEntities();
            string taikhoan, matkhau;
            taikhoan = Txttaikhoan.Text;
            matkhau = Txtmatkhau.Text;
            if (taikhoan==""||matkhau=="")
            {
                MessageBox.Show("Bạn chưa nhập tài khoản hoặc mật khẩu");
            }
            else
            {
                if (lg.Table_TKFB.Where(x=>x.Tài_khoản==taikhoan).Count()>0 && lg.Table_TKFB.Where(x => x.Mật_khẩu == matkhau).Count() > 0)
                {
                    MessageBox.Show("Login thành công");
                    Form2 f2 = new Form2();
                    f2.Hide();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác");
                }
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
