using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RobotsRUs
{
    /// <summary>
    /// Initial form to obtain upper coordinates of the plateau
    /// </summary>
    public partial class frmGridDetails : Form
    {
        public frmGridDetails()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Checks the entry for error then loads the Deployment form for robot depolyment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                int upperX=0, upperY=0;
                string coords = txtUpperRightCoords.Text;
                if (coords.Trim() == "")
                {
                    MessageBox.Show("Please enter the upper coordinates");
                    txtUpperRightCoords.Text = "";
                    return;
                }

                string[] values = coords.Split(' ');
                if (values.GetLength(0) == 2)
                {
                    upperX = Convert.ToInt32(values[0]);
                    upperY = Convert.ToInt32(values[1]);
                }
                else
                {
                    MessageBox.Show("Please enter the upper coordinates");
                    txtUpperRightCoords.Text = "";
                    return;
                }

                frmDeployment deploymentForm = new frmDeployment(upperX, upperY);

                this.Hide();

                DialogResult result = deploymentForm.ShowDialog();
                if (result == DialogResult.Cancel)
                {
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Invalid entry.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
