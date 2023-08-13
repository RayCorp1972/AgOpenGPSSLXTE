using System;
using System.Windows.Forms;

namespace AgOpenGPS
{
    public partial class FormSaveOrNot : Form
    {
        private readonly FormGPS mf = null;
        //class variables
        public FormSaveOrNot(Form callingForm)
        {
            mf = callingForm as FormGPS;
            
        }
            public FormSaveOrNot(bool closing)
        {
          InitializeComponent();

            if (closing)
            {
                btnOk.Image = Properties.Resources.ExitAOG;
               
            }
            else
            {
                btnOk.Image = Properties.Resources.FieldClose;
                label3.Text = "Sluiten";
                
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //back to FormGPS
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Ignore;
            Close();
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
