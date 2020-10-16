using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M08_MTPP_5_1_Belcher_Joshua {
    public class Student : ICloneable {
        // student's list of test scores
        private List<int> scores;

        public int this[int i] {
            get { return scores[i]; }
            set { scores[i] = value; }
        }

        // auto-implemented property to store student's name
        public string Name { get; set; }

        // expression-bodied property to store the number of scores in the list
        public int ScoreCount => scores.Count;

        // constructors: one for instantiating a new Student with no scores and one for with scores
        public Student(string name) {
            this.Name = name;
            scores = new List<int>();
        }

        public Student(string name, List<int> scores) {
            this.Name = name;
            this.scores = scores;
        }

        // implementation of ICloneable interface 
        //(shallow copy creates new List, but simply copies the List elements from the existing one since they are value type variables)
        public object Clone() {
            Student s = new Student(this.Name);
            s.scores = new List<int>(this.scores);
            return s;
        }

        // remove a score at the passed index
        public void removeScore (int i) {
            scores.RemoveAt(i);
        }

        // overrides ToString method of the Object class in order to create a properly formatted
        //string representation of each student object for display use in form
        public override string ToString() {
            string scoreString = "";

            foreach (int score in scores) {
                scoreString += "|" + score;
            }

            string studentString = this.Name + scoreString;

            return studentString;
        }

        /******************************* METHODS TO CHANGE SCORE ENTRIES *********************************************/

        // if entry is valid, adds the score to the student's score list
        public bool addScore(TextBox textBox) {
            if (IsValidData(textBox)) {
                int score = Convert.ToInt32(textBox.Text);
                scores.Add(score);
                return true;
            }
            return false;
        }

        // effectively deletes the score by removing from the score from the student's score list
        public void deleteScore(int i) {
            scores.RemoveAt(i);
        }

        // if entry is valid, it replaces the score at the appropriate index in the student's score list
        public bool updateScore(TextBox textBox, int i) {
            if (IsValidData(textBox)) {
                int score = Convert.ToInt32(textBox.Text);
                scores[i] = score;
                return true;
            }
            return false;
        }

        /******************************* CODE FOR USER ENTRY VALIDATION **********************************************/

        // all individual validation check functions are combined to test for overall entry validity
        public bool IsValidData(TextBox textBox) {
            return
                IsPresent(textBox) && IsInteger(textBox) && IsWithinRange(textBox, 0, 100);
        }

        // validation check to see if user has entered any data
        public bool IsPresent(TextBox textBox) {
            if (textBox.Text == "") {
                MessageBox.Show("No score was entered.", "Entry Error");
                textBox.Focus();
                return false;
            }

            return true;
        }

        // validation check to see if the user entry is a positive integer
        public bool IsInteger(TextBox textBox) {
            int number = 0;
            if(Int32.TryParse(textBox.Text, out number)) {
                return true;
            } else {
                MessageBox.Show("Entry must be a whole number.", "Entry error.");
                textBox.Focus();
                return false;
            }
        }


        // validation check to see if the user entry is within the range of 0 to 100
        public bool IsWithinRange(TextBox textBox, int min, int max) {
            int number = Convert.ToInt32(textBox.Text);

            if (number < min || number > max) {
                MessageBox.Show("Entry must be between " + min.ToString() + " and " + max.ToString());
                textBox.Focus();
                return false;
            }
            return true;
        }

    }
}
