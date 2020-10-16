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
    public partial class frmUpdateScore : Form {
        public frmUpdateScore(Student updatedStudent, int selectedIndex) {
            InitializeComponent();

            student = updatedStudent;

            index = selectedIndex;
        }

        private Student student;
        private int index;

        // validates user entry then closes form if successful
        private void btnUpdate_Click(object sender, EventArgs e) {
            if (student.updateScore(txtScore, index)) {
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
