
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

namespace ToDoListProjet
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            var db = DbContext.GetInstance();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
