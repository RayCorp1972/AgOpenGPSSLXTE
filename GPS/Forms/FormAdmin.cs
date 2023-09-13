using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgOpenGPS.Forms
{
    public partial class FormAdmin : Form
    {
        
        private readonly FormGPS mf = null;

        

        //constructor
        public FormAdmin(Form callingForm)
        {
            mf = callingForm as FormGPS;

           
            InitializeComponent();

        }
       
        
        //public FormAdmin(FormGPS formGPS)
        //{
        //    InitializeComponent();
            


        //}

        private void FormAdmin_Load(object sender, EventArgs e)
        {

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text.Contains("2806"))
            {
                {
                    var frm = (FormGPS)this.Owner;
                    if (frm != null)
                        frm.button1.PerformClick();
                }

            }

            Close();






        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
