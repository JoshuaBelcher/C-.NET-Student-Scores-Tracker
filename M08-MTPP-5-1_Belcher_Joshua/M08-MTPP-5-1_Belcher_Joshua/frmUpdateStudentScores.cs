using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M08_MTPP_5_1_Belcher_Joshua {
    public partial class frmUpdateStudentScores : Form {
        public frmUpdateStudentScores(Student clonedStudent) {
            InitializeComponent();

            updatedStudent = clonedStudent;

            lblNameContent.Text = updatedStudent.Name;

            refreshListBox();
        }

        private Student updatedStudent;

        // opens the form and returns the Student object clone when closed
        public Student GetUpdatedStudent() {
            this.ShowDialog();

            return updatedStudent;
        }

        // clears listbox then adds all scores in the student's score list to the listbox
        public void refreshListBox() {
            lstScores.Items.Clear();

            for (int i = 0; i < updatedStudent.ScoreCount; i++) {
                lstScores.Items.Add(updatedStudent[i]);
            }

        }

        // opens Add Score dialog
        private void btnAdd_Click(object sender, EventArgs e) {
            frmAddScore addScoreForm = new frmAddScore(updatedStudent);
            addScoreForm.ShowDialog();

            refreshListBox();
        }

        // opens Update Score dialog
        private void btnUpdate_Click(object sender, EventArgs e) {
            if (lstScores.SelectedIndex != -1) {
                frmUpdateScore updateScoreForm = new frmUpdateScore(updatedStudent, lstScores.SelectedIndex);
                updateScoreForm.ShowDialog();

                refreshListBox();
            } else {
                MessageBox.Show("Please select a score to update.", "Missing Selection");
            }
        }

        // resets the student's scores and the list display
        private void btnClearScores_Click(object sender, EventArgs e) {
            updatedStudent = new Student(lblNameContent.Text);
            lstScores.Items.Clear();
        }

        // deletes the score at the index of the selected score
        private void btnRemove_Click(object sender, EventArgs e) {
            if (lstScores.SelectedIndex != -1) {
                updatedStudent.removeScore(lstScores.SelectedIndex);

                refreshListBox();

            } else {
                MessageBox.Show("Please select a score to delete.", "Missing Selection");
            }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            updatedStudent = null;
            Close();
        }
    }
}
