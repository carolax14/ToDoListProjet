
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

using ServiceStack.OrmLite;
using System.Windows.Forms;

namespace ToDoListProjet
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            var db = DbContext.GetInstance(); // Creation de la bdd & retourne la connexion etablie

            foreach (var category in db.Select<Category>())
            {

            }
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            navMenu.Width = 360;
        }

        private void SepartorH_Click(object sender, EventArgs e)
        {

        }
    }
}
