﻿using System;
using System.Windows.Forms;

namespace AgIO.Forms
{
    public partial class AdminPass : Form
    {
        private readonly FormLoop mf;

        public AdminPass(Form callingForm)
        {
            mf = callingForm as FormLoop;
            InitializeComponent();
        }

        private void tboxUserPassword_TextChanged(object sender, EventArgs e)
        {
            if (mf.isKeyboardOn)
            {
                mf.KeyboardToText((TextBox)sender, this);
                btnSerialCancel.Focus();
            }
        }

        private void AdminPass_Load(object sender, EventArgs e)
        {

        }
    }
}
