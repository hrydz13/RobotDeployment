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
    /// Deployment form
    /// </summary>
    public partial class frmDeployment : Form
    {
        private int cntRobots;
        private int upperX;
        private int upperY;

        /// <summary>
        /// Form to deploy robots
        /// </summary>
        /// <param name="upperX"></param>
        /// <param name="upperY"></param>
        public frmDeployment(int upperX, int upperY)
        {
            cntRobots = 0;
            this.upperX = upperX;
            this.upperY = upperY;


            InitializeComponent();
        }

        /// <summary>
        /// Resets the navigation fields for entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNavigation_Click(object sender, EventArgs e)
        {
            txtPosition.Enabled = true;
            txtPosition.Text = "";

            txtInstructions.Enabled = true;
            txtInstructions.Text = "";

            txtFinal.Text = "";

            btnFinal.Enabled = true;
        }

        /// <summary>
        /// Navigates the robot to the final position and heading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFinal_Click(object sender, EventArgs e)
        {
            if (txtPosition.Text.Trim() == "" || txtInstructions.Text.Trim() == "")
            {
                MessageBox.Show("PLease enter all details.");
                return;
            }

            Navigate();
        }

        /// <summary>
        /// Moves the robot to the final position and heading based on the initial position and instructions
        /// </summary>
        public void Navigate()
        {
            try
            {
                Robot robot = new Robot(upperX, upperY);
                robot.PositionHeading = txtPosition.Text;
                robot.Instructions = txtInstructions.Text;
                robot.Navigate();
                string positionHeadingResult = robot.PositionHeading;
                if (positionHeadingResult.Contains("Error:"))
                {
                    MessageBox.Show("Invalid entry");
                    return;
                }

                txtFinal.Text = robot.PositionHeading;
                cntRobots++;

                string deployment = string.Format("Robot {0}: Initial - {1}, Final - {2}", cntRobots, txtPosition.Text, txtFinal.Text);
                lstDeployments.Items.Add(deployment);
            }
            catch
            {
                MessageBox.Show("Invalid entry");
            }

        }

        /// <summary>
        /// Closes the dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
