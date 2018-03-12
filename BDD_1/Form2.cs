using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BDD_1
{
    public partial class Form2 : Form
    {
        
        public TextBox
            tId = new TextBox(),
            tName = new TextBox(),
            tCurp = new TextBox();
        DateTimePicker
                dDate = new DateTimePicker();
        public string sName;
        public Form1.DataCapsule capsule = new Form1.DataCapsule();
        public Form2()
        {
            InitializeComponent();
            InitializeDataEntry();
        }
        public Form2(Form1.DataCapsule edit)
        {
            InitializeComponent();
            InitializeDataEntry();
            EditData(edit);
        }
        public void EditData(Form1.DataCapsule edit)
        {
            tName.Text = edit.name;
            dDate.Value = new DateTime(
            int.Parse(edit.date.Substring(0, 4)),
            int.Parse(edit.date.Substring(5, 2)),
            int.Parse(edit.date.Substring(8, 2)));
            
            tId.Text = edit.schoolid;
            tCurp.Text = edit.curp;
            this.Text = "Editar entrada";
        }
        public void InitializeDataEntry()
        {
            this.Text = "Añadir un nuevo alumno";
            this.ClientSize = new System.Drawing.Size(300, 300);
            Label
                lId = new Label(),
                lName = new Label(),
                lCurp = new Label(),
                lDate = new Label(),
                lTitle = new Label();


            Button
                eButton = new Button();
            dDate.Format = DateTimePickerFormat.Short;
            lName.Text = "Nombre";
            lId.Text = "Matricula";
            lTitle.Text = "Ingresar un nuevo estudiante";
            lCurp.Text = "CURP";
            lDate.Text = "Fecha de nacimiento";
            lId.Width = TextRenderer.MeasureText(lDate.Text, lDate.Font).Width;
            lName.Width = TextRenderer.MeasureText(lDate.Text, lDate.Font).Width;
            lDate.Width = TextRenderer.MeasureText(lDate.Text, lDate.Font).Width;
            lCurp.Width = TextRenderer.MeasureText(lDate.Text, lDate.Font).Width;
            dDate.Width = tName.Width;
            eButton.Text = DialogResult.OK.ToString();
            
            
            
            
            lName.Location = new Point
                (((this.Width / 4) - (tName.Width / 2)), 40);
            lDate.Location = new Point
                (lName.Location.X, (lName.Location.Y + lDate.Height + 3));
            lId.Location = new Point
                (lName.Location.X, (lDate.Location.Y + lId.Height + 3));
            lCurp.Location = new Point
                (lName.Location.X, (lId.Location.Y + lCurp.Height + 3));
            tName.Location = new Point
                ((lName.Location.X + lName.Width), lName.Location.Y);
            dDate.Location = new Point
                (tName.Location.X, (tName.Location.Y + tName.Height + 3));
            tId.Location = new Point
                (tName.Location.X, (dDate.Location.Y + dDate.Height + 3));
            tCurp.Location = new Point
                (tName.Location.X, (tId.Location.Y + tId.Height + 3));
            eButton.Location = new Point
                (((this.Width / 2) - (eButton.Width / 2)), (2*(this.Height / 3) - (eButton.Height)));
            

            eButton.Click += new EventHandler(OptionsButton_Click);
            this.FormClosing += new FormClosingEventHandler(Form2_OnClosing);

            Controls.Add(tId);
            Controls.Add(tName);
            Controls.Add(dDate);
            Controls.Add(tCurp);
            Controls.Add(lId);
            Controls.Add(lName);
            Controls.Add(lDate);
            Controls.Add(lCurp);
            Controls.Add(eButton);

        }
        private void Form2_OnClosing(object sender, FormClosingEventArgs e)
        {
        }
        private void CheckData()
        {

            string smonth = "", sday = "";

            if (
                tName.TextLength == 0 ||
                tId.TextLength == 0 ||
                tCurp.TextLength == 0 ||
                dDate.Value.Year < 1950)
            {
                MessageBox.Show("Warning! \n All values must be complete!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                smonth = dDate.Value.Month.ToString();
                sday = dDate.Value.Day.ToString();
                if (dDate.Value.Month < 10)
                {
                    smonth = ("0" + dDate.Value.Month);
                }
                if (dDate.Value.Day < 10)
                {
                    sday = ("0" + dDate.Value.Day);
                }

                capsule.name = tName.Text;
                capsule.schoolid = tId.Text;
                capsule.date = (dDate.Value.Year + "-" + smonth + "-" + sday);
                capsule.curp = tCurp.Text;
                this.DialogResult = DialogResult.OK;

            }
        }
        private void OptionsButton_Click(object sender, EventArgs e)
        {
            CheckData();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
