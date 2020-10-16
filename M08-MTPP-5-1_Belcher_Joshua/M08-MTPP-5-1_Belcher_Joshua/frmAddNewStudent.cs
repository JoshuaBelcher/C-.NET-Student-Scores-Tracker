using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M08_MTPP_5_1_Belcher_Joshua {
    public partial class frmAddNewStudent : Form {
        public frmAddNewStudent() {
            InitializeComponent();
            newStudent = new Student(txtName.Text);
        }

        // variables to hold data that will be used to construct a new Student object
        private Student newStudent;

        // opens the form and returns the new Student object when closed
        public Student GetNewStudent() {
            this.ShowDialog();

            return newStudent;
        }

        /************************************ CODE FOR EVENTS ***********************************************************/

        // closes form if the new student has a name
        private void btnOK_Click(object sender, EventArgs e) {
            if (newStudent.Name == "") {
                MessageBox.Show("The new student must have a name.", "Entry Error");
                txtName.Focus();
            } else {
                this.Close();
            }
        }

        // adds the entered score to both the List object and the label display
        private void btnAddScore_Click(object sender, EventArgs e) {
            if (newStudent.addScore(txtScore)) {
                lblScoresContent.Text += txtScore.Text + " ";
            }
            
            txtScore.Focus();
        }

        // resets the List object and the label display
        private void btnClearScores_Click(object sender, EventArgs e) {
            newStudent = new Student(txtName.Text);
            lblScoresContent.Text = "";
        }

        // updates name of new Student
        private void txtName_TextChanged(object sender, EventArgs e) {
            newStudent.Name = txtName.Text;
        }

        // nulls the new Student and close form
        private void btnCancel_Click(object sender, EventArgs e) {
            newStudent = null;
            this.Close();
        }
    }
}