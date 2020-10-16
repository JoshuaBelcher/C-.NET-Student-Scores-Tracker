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
    public partial class frmAddScore : Form {
        public frmAddScore(Student updatedStudent) {
            InitializeComponent();

            student = updatedStudent;
        }

        private Student student;

        // validates user entry then closes form if successful
        private void btnAdd_Click(object sender, EventArgs e) {
            if (student.addScore(txtScore)) {
                Close();
            }        
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
