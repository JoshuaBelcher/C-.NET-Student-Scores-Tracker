using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M08_MTPP_5_1_Belcher_Joshua {
    public partial class frmStudentScores : Form {
        public frmStudentScores() {
            InitializeComponent();
        }

        private const string dir = @"..\..\";
        private const string path = dir + "StudentScores.txt";

        private List<Student> students = new List<Student>();

        // indexer to access students list
        public Student this[int i] {
            get {
                return students[i];
            }
            set {
                students[i] = value;
            }
        }

        // updates the calculation displays on this form
        public void refreshCalculations() {
            if (lstStudents.SelectedIndex != -1) {
                // displays number of scores in the selected student's score list
                int count = students[lstStudents.SelectedIndex].ScoreCount;

                lblScoreCountContent.Text = Convert.ToString(count);


                // calculate and display the total of all the selected student's score
                int total = 0;

                for (int i = 0; i < count; i++) {
                    total += students[lstStudents.SelectedIndex][i];
                }

                lblScoreTotalContent.Text = Convert.ToString(total);

                // calculate and display the average of the selected student's score
                int average = 0;

                if (count != 0) {
                    average = total / count;
                }

                lblAverageContent.Text = Convert.ToString(average);
            } else {
                lblScoreCountContent.Text = "";
                lblScoreTotalContent.Text = "";
                lblAverageContent.Text = "";
            }

        }

        // clears listbox then adds all students in the list to the listbox
        public void refreshListBox() {
            lstStudents.Items.Clear();

            foreach (Student student in students) {
                lstStudents.Items.Add(student.ToString());
            }

        }

        /************************************************************* CODE FOR READING/WRITING TO EXTERNAL FILE *******************************************************/
        private void saveScores() {

            StreamWriter dataOut = null;

            try {
                dataOut = new StreamWriter(new FileStream(path, FileMode.Truncate, FileAccess.Write));

                foreach (Student student in students) {
                    for (int i = 0; i < student.ScoreCount; i++) {
                        dataOut.Write(student[i] + "|");
                    }

                    dataOut.WriteLine(student.Name);
                }
            } catch (FileNotFoundException) {
                MessageBox.Show(path + " not found.", "File Not Found");
            } catch (DirectoryNotFoundException) {
                MessageBox.Show(dir + " not found.", "Directory Not Found");
            } catch (IOException ex) {
                MessageBox.Show(ex.Message, "IOException");
            } finally {
                if (dataOut != null) {
                    dataOut.Close();
                }
            }
        }

        private void loadScores() {

            StreamReader dataIn = null;

            try {

                dataIn = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));

                while (dataIn.Peek() != -1) {
                    string row = dataIn.ReadLine();
                    string[] columns = row.Split('|');

                    string name = columns[columns.Length - 1];

                    Array.Resize(ref columns, columns.Length - 1);

                    List<int> loadedScores = new List<int>();

                    for (int i = 0; i < columns.Length; i++) {
                        int score = Convert.ToInt32(columns[i]);
                        loadedScores.Add(score);
                    }

                    Student student = new Student(name, loadedScores);

                    students.Add(student);
                }

            } catch (FileNotFoundException) {
                MessageBox.Show(path + " not found.", "File Not Found");
            } catch (DirectoryNotFoundException) {
                MessageBox.Show(dir + " not found.", "Directory Not Found");
            } catch (IOException ex) {
                MessageBox.Show(ex.Message, "IOException");
            } finally {
                if (dataIn != null) {
                    dataIn.Close();
                }
            }

        }

        /*********************************************************************** CODE FOR EVENTS *******************************************************************************/

        // once forms loads, three test students are created, added to the list of students, then displayed in the form's listbox
        private void frmStudentScores_Load(object sender, EventArgs e) {
            loadScores();

            refreshListBox();
        }

        // if user changes their student selection, the calculations are updated
        private void lstStudents_SelectedIndexChanged(object sender, EventArgs e) {
            refreshCalculations();
        }

        // retrieves a new Student from the opened form, adds it to the students list, then refreshes the display
        private void btnAddNew_Click(object sender, EventArgs e) {
            frmAddNewStudent addNewStudentForm = new frmAddNewStudent();
            Student newStudent = addNewStudentForm.GetNewStudent();

            if (newStudent != null) {
                students.Add(newStudent);

                saveScores();

                refreshListBox();
            }
        }

        // retrieves a new Student from the opened form and replaces the existing Student at the appropriate index, then refreshes the display
        private void btnUpdate_Click(object sender, EventArgs e) {
            if (lstStudents.SelectedIndex != -1) {
                Student clonedStudent = (Student)students[lstStudents.SelectedIndex].Clone();
                frmUpdateStudentScores updateStudentScoresForm = new frmUpdateStudentScores(clonedStudent);
                Student updatedStudent = updateStudentScoresForm.GetUpdatedStudent();

                if (updatedStudent != null) {
                    students[lstStudents.SelectedIndex] = updatedStudent;

                    saveScores();

                    refreshListBox();
                }

            } else {
                MessageBox.Show("Please select a student to update.", "Missing Selection");
            }

        }

        // removes selected Student from the students List and from the list box display
        private void btnDelete_Click(object sender, EventArgs e) {
            if (lstStudents.SelectedIndex != -1) {
                students.RemoveAt(lstStudents.SelectedIndex);

                lstStudents.Items.RemoveAt(lstStudents.SelectedIndex);

                saveScores();

            } else {
                MessageBox.Show("Please select a student to delete.", "Missing Selection");
            }

            lstStudents.SelectedIndex = -1;
        }

        private void btnExit_Click(object sender, EventArgs e) {
            Close();
        }
    }
}