using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace BDD_1
{
    public partial class Form1 : Form
    {
        public struct DataCapsule
        {
            public string name, schoolid,curp,date;
            public int id;
        };
        int totalindatabase = 0;
        DataBase Fetch = new DataBase();
        public DataCapsule capsule = new DataCapsule();
        private List<DataCapsule> StoredData = new List<DataCapsule>();
        List<ListBox> ShowData = new List<ListBox>();
        bool isadding = false;
        public Form1()
        {
            InitializeComponent();
            InitializeDynamicComponent();
            DataFetch();
        }
        public void InitializeDynamicComponent()
        {
            
            ListBox
                nameBox = new ListBox(),
                dateBox = new ListBox(),
                curpBox = new ListBox(),
                scIdBox = new ListBox();

            Label
                ldbid = new Label(),
                lname = new Label(),
                ldate = new Label(),
                lscid = new Label(),
                lcurp = new Label();


            Button
                dynamicEdit = new Button(),
                dynamicAdd = new Button(),
                dynamicDelete = new Button();

            this.Width =800;
            this.Height = 250;
            this.MaximumSize = new Size(800, 1000);
            this.MinimumSize = new Size(800, 250);
            ldbid.Text = "ID";
            lname.Text = "Nombre";
            ldate.Text = "Fecha de Nacimiento";
            lscid.Text = "Matricula";
            lcurp.Text = "CURP";
            dynamicAdd.Text = "Añadir";
            dynamicEdit.Text = "Editar";
            dynamicDelete.Text = "Eliminar";

            ldbid.Width = TextRenderer.MeasureText(ldate.Text,ldbid.Font).Width;
            lname.Width = TextRenderer.MeasureText(ldate.Text,ldbid.Font).Width;
            ldate.Width = TextRenderer.MeasureText(ldate.Text,ldbid.Font).Width;
            lscid.Width = TextRenderer.MeasureText(ldate.Text,ldbid.Font).Width;
            lcurp.Width = TextRenderer.MeasureText(ldate.Text,ldbid.Font).Width;

            scIdBox.Location = new Point
                (((this.Width/5)-(scIdBox.Width/2)), ((this.Height / 3) - (scIdBox.Height / 2)));
            nameBox.Location = new Point
                (((scIdBox.Location.X) + (scIdBox.Width)), (scIdBox.Location.Y));
            dateBox.Location = new Point
                (((nameBox.Location.X) + (nameBox.Width)), (scIdBox.Location.Y));
            curpBox.Location = new Point
                (((dateBox.Location.X) + (dateBox.Width)), (scIdBox.Location.Y));



            lscid.Location = new Point
                (scIdBox.Location.X, (scIdBox.Location.Y-ldbid.Height-1 ));
            lname.Location = new Point
                (nameBox.Location.X, lscid.Location.Y);
            ldate.Location = new Point
                (dateBox.Location.X, lscid.Location.Y);
            lcurp.Location = new Point
                (curpBox.Location.X, lscid.Location.Y);
            
            dynamicAdd.Location = new Point
               (((curpBox.Location.X) + (curpBox.Width)), (scIdBox.Location.Y));
            dynamicEdit.Location = new Point
                (((curpBox.Location.X) + (curpBox.Width)), (dynamicAdd.Location.Y + dynamicEdit.Height));
            dynamicDelete.Location = new Point
                (((curpBox.Location.X) + (curpBox.Width)), (dynamicEdit.Location.Y + dynamicDelete.Height));

            dynamicAdd.Click += new EventHandler(dynamicAdd_Click);

            this.FormClosing += new FormClosingEventHandler(Closing);
            this.SizeChanged += new EventHandler(WindowResize);

            scIdBox.DoubleClick += new EventHandler(OnListEdit);
            nameBox.DoubleClick += OnListEdit;
            dateBox.DoubleClick += OnListEdit;
            curpBox.DoubleClick += OnListEdit;
            dynamicEdit.Click += OnListEdit;

            scIdBox.KeyPress += new KeyPressEventHandler(OnListDelete);
            nameBox.KeyPress += OnListDelete;
            dateBox.KeyPress += OnListDelete;
            curpBox.KeyPress += OnListDelete;
            dynamicDelete.Click += new EventHandler(dynamicDelete_Click);

            Controls.Add(dynamicDelete);
            Controls.Add(dynamicAdd);
            Controls.Add(dynamicEdit);
            Controls.Add(nameBox);
            Controls.Add(dateBox);
            Controls.Add(scIdBox);
            Controls.Add(curpBox);
            Controls.Add(lname);
            Controls.Add(ldate);
            Controls.Add(lscid);
            Controls.Add(lcurp);

            
            ShowData.Add(scIdBox);
            ShowData.Add(nameBox);
            ShowData.Add(dateBox);
            ShowData.Add(curpBox);

            UpdateData();
        }
        private void DataDump()
        {
            if(StoredData!=null)
                Fetch.OutputHandler(StoredData, this);
        }
        private void DataFetch()
        {
            Fetch.InputHandler();
            if(Fetch.Getdatabasenames()!=null)
            {
            StoredData = Fetch.Getdatabasenames();

            }
            isadding = true;
            UpdateData();
        }
        private void dynamicDelete_Click(object sender, EventArgs e)
        {
            DeleteStudent();
        }
        private void WindowResize(object sender, EventArgs e)
        {
           if(this.Width>MaximumSize.Width)
            {
                this.Width=800;
            }
           if(this.Height<(MaximumSize.Height/2)&&this.Height>this.MinimumSize.Height)
            {
                for (int index = 0; index < ShowData.Count; ++index)
                {
                    ShowData[index].Height = (3 * this.Height / 5);
                }
                
            }
            else
            {
                for (int index = 0; index < ShowData.Count; ++index)
                {
                    ShowData[index].Height = (3 * this.Height / 4);
                }
               
            }
           
        }

        new public void Closing(object sender,FormClosingEventArgs e)
        {
            if(StoredData!=null)
            {

            if(MessageBox.Show("Quieres Guardar todo?","Warning",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                DataDump();
                MessageBox.Show("Guardado con Exito!");

            }

            }
            
        }
        public void EditStudent()
        {
            DataCapsule getUser = new DataCapsule();
            int editat = -1;
            for (int index = 0; index < ShowData.Count; ++index)
            {
                if (ShowData[index].SelectedIndex != -1)
                {
                    editat = ShowData[index].SelectedIndex;
                    break;
                }
            }
            if(editat!=-1)
            {
                
                Form2 EditForm = new Form2(StoredData[editat]);
                if (EditForm.ShowDialog() == DialogResult.OK)
                {
                    getUser = EditForm.capsule;
                    getUser.id = editat + 1;
                    StoredData[editat] = getUser;
                    isadding = true;
                    UpdateData();
                }
            }
           else
                MessageBox.Show("Seleccione un alumno para editar");
        }
        private void OnListEdit(object sender, EventArgs e)
        {
            EditStudent();

        }
        private void DeleteStudent()
        {
            int deleteat = -1;
            for (int index = 0; index < ShowData.Count; ++index)
            {
                if (ShowData[index].SelectedIndex != -1)
                {
                    deleteat = ShowData[index].SelectedIndex;
                    break;
                }
            }
            if(deleteat!=-1)
            {
                if (MessageBox.Show("Estas seguro que quieres borrar el alumno " + StoredData[deleteat].name + "?",
                "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    StoredData.RemoveAt(deleteat);
                    isadding = true;
                    UpdateData();
                }
            }
            else
                MessageBox.Show("Seleccione un alumno para eliminar");
        }
        private void OnListDelete(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                DeleteStudent();

            }
        }
        public void AddStudent()
        {
            DataCapsule getUser = new DataCapsule();
            Form2 addEntry = new Form2();
            if (addEntry.ShowDialog() == DialogResult.OK)
            {
                getUser = addEntry.capsule;
                getUser.id = (totalindatabase + 1);
                ++totalindatabase;
                StoredData.Add(getUser);
                isadding = true;
                UpdateData();
            }

            
        }
        public void dynamicAdd_Click(object sender, EventArgs e)
        {
            AddStudent();
        }
        private void UpdateData()
        {
            for(int index=0;index<ShowData.Count;++index)
            {
                ShowData[index].Items.Clear();
            }
            if(StoredData!= null && isadding == true )
            {
                List<DataCapsule> temporaryLoad = StoredData;
                for (int index = 0; index < temporaryLoad.Count; ++index)
                {

                    ShowData[0].Items.Add(temporaryLoad[index].schoolid);
                    ShowData[1].Items.Add(temporaryLoad[index].name);
                    ShowData[2].Items.Add(temporaryLoad[index].date);
                    ShowData[3].Items.Add(temporaryLoad[index].curp);
                }
                isadding = false;
            }
            
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
