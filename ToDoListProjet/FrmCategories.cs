using ToDoListProjet.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToDoListProjet.DataAccess.Models;
using ServiceStack.OrmLite;
using Kimtoo.BindingProvider;


namespace ToDoListProjet
{
    public partial class FrmCategories : Form
    {
        public FrmCategories()
        {
            InitializeComponent();

            var db = DbContext.GetInstance();

            var data = db.Select<Category>();

            dgv.OnEdit<Category>((r, c) => db.Save(r) || true);
            dgv.OnDelete<Category>((r, c) => db.Delete(r) >= 0);
            dgv.OnError<Category>((r, c) => bunifuSnackbar1.Show(this, DbContext.Exception.Message, Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error));
            dgv.Bind(data);
        }

        private void FrmCategories_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            var db = DbContext.GetInstance();

            var newCat = new Category();

            db.Save(newCat);

            dgv.Bind(newCat, 0);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow.Tag == null) return;

            dgv.DeleteRow<Category>();
        }
    }
}
