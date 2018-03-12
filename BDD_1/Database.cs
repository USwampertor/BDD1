using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace BDD_1
{
    class DataBase
    {
        

        private string DataBaseFile = "DataBase.txt";
        private StreamWriter OutputStream;
        private StreamReader InputStream;
        private int databaseSize;
        List<Form1.DataCapsule> DataList = new List<Form1.DataCapsule>();

        public int InputHandler()
        {
           
            string FullPath = DataBaseFile;
            if (File.Exists(FullPath))
            {
                InputStream = new StreamReader(FullPath);
                if(new FileInfo(FullPath).Length > 0)
                {
                    databaseSize = int.Parse(InputStream.ReadLine());
                    
                    for (int DataIndex = 0; DataIndex < databaseSize; ++DataIndex)
                    {
                        Form1.DataCapsule data = new Form1.DataCapsule();
                        data.id = DataIndex + 1;
                        data.name = InputStream.ReadLine();
                        data.date = InputStream.ReadLine();
                        data.schoolid = InputStream.ReadLine();
                        data.curp = InputStream.ReadLine();
                        DataList.Add(data);
                    }
                }
                
                InputStream.Close();
                return 1;
            }
            return 0;
        }

        public void OutputHandler(List<Form1.DataCapsule> outputnames,Form1 form)
        {

            string FullPath = DataBaseFile;
            OutputStream = new StreamWriter(FullPath);
           
                OutputStream.WriteLine(outputnames.Count);
                for (int DataIndex = 0; DataIndex < outputnames.Count; ++DataIndex)
                {

                    OutputStream.WriteLine(outputnames[DataIndex].name);
                    OutputStream.WriteLine(outputnames[DataIndex].date);
                    OutputStream.WriteLine(outputnames[DataIndex].schoolid);
                    OutputStream.WriteLine(outputnames[DataIndex].curp);
                }
            
            
            OutputStream.Close();
            //form.Close();
        }

        public List<Form1.DataCapsule> Getdatabasenames()
        {
            return DataList;
        }

    }
}
