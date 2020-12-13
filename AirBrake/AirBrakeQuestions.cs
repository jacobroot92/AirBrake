using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirBrake.Models;
using AirBrake.Utils;

namespace AirBrake
{
    public partial class AirBrakeQuestions : Form
    {
        public AirBrakeQuestions()
        {
            InitializeComponent();

            DataTable questions = ParseFile.parseFile("airbrake.sum");

            dataGridView1.DataSource = questions;
        }

    }
}
