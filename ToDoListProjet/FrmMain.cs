
using ToDoListProjet.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListProjet.DataAccess.Models;
using Bunifu.Utils;
using Kimtoo.BindingProvider;
using ServiceStack.OrmLite;
using System.Windows.Forms;

namespace ToDoListProjet
{
    public partial class FrmMain : Form
    {
        private DateTime _date = DateTime.Today;
        private string _category = "Tous";
        private string _status = "Tous";
        public FrmMain()
        {
            InitializeComponent();


          
            var db = DbContext.GetInstance(); // Creation de la bdd & retourne la connexion etablie
            dgv.OnEdit<TodoItem>((r, c) => db.Save(r) || true);
            dgv.OnDelete<TodoItem>((r, c) => db.Delete(r) >= 0);
            dgv.OnError<TodoItem>((r, c) => bunifuSnackbar1.Show(this, DbContext.Exception.Message, Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error));

            // Récupère les catégories & les affiche dans le menu des catégories
            List<string> catItems = new List<string>();
            catItems.Add("Tous");
            foreach (var category in db.Select<Category>())
            {
                catItems.Add(category.CategoryName);

                barCategory.Items = catItems.ToArray();
            }
        }

        void ReloadData()
        {
            var db = DbContext.GetInstance();

            var data = db.Select<TodoItem>();
            data = data.Where(r => this._date >= r.StartDate.Date && this._date <= r.EndStae.Date).ToList();

            if (this._category != "Tous")
                data = data.Where(r => r.CategoryId == Category.GetCategoryByName(this._category).id).ToList();

            if (this._status != "Tous")
                data = data.Where(r => r.Done == (this._status == "Complete")).ToList();

            dgv.Bind(data);
             

        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            navMenu.Width = 360;
            ReloadData();
        }

        private void calendar_Load(object sender, EventArgs e)
        {

        }

        private void SepartorH_Click(object sender, EventArgs e)
        {

        }

        private void calendar_ValueChanged(object sender, EventArgs e)
        {
            this._date = calendar.Value;
            ReloadData();
        }

        private void barCategory_OnSelectionChange(object sender, EventArgs e)
        {
            this._category = sender.ToString();
            btnAdd.Visible = this._category != "All";
            ReloadData();
        }

        private void txtSearch_TextChange(object sender, EventArgs e)
        {
            ReloadData();
            dgv.SearchRows(txtSearch.Text.Trim());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var db = DbContext.GetInstance();

            var newTask = new TodoItem()
            {
                CategoryId = Category.GetCategoryByName(this._category).id,
            };

            db.Save(newTask);

            dgv.Bind(newTask, 1);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow.Tag == null) return;

            dgv.DeleteRow<TodoItem>(dgv.CurrentRow);
        }

        private void navMenu_OnItemSelected(object sender, string path, EventArgs e)
        {
            
        }
    }
}
