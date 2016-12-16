using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using kibiomerlab;
namespace kibiomer_app
{
    public partial class addskeleton : Form
    {
        List<Data.Marcadores> mark;
        kl okl;
        listamarcadores olm;
        double[,] matrixMov;
        double[] _matrix;
        double _infinityReference;
        double[] _Module;
        public addskeleton()
        {
            InitializeComponent();
        }

        private void addskeleton_Load(object sender, EventArgs e)
        {
            mark = Data.marcadoresOrdenes.ObtenerMarcadores();
            okl = new kl();
            olm = new listamarcadores();
            olm.m1 = "1 Cabeza derecha";
            olm.m2 = "2 Cabeza posterior";
            olm.m3 = "3 Cabeza izquierda";
            olm.m4 = "4 Cabeza frontal";
            olm.m5 = "5 Cervical a";
            olm.m6 = "6 Cervical b";
            olm.m7 = "7 Toráfica 12 a";
            olm.m8 = "8 Toráfica 12 b";
            olm.m9 = "9 Sacro a";
            olm.m10 = "10 Sacro b";
            olm.m11 = "11 Hombro izquierdo";
            olm.m12 = "12 Hombro derecho";
            olm.m13 = "13 Codo izquierdo";
            olm.m14 = "14 Codo derecho";
            olm.m15 = "15 Muñeca izquieda";
            olm.m16 = "16 Muñeca derecha";
            olm.m17 = "17 Cadera izquierda";
            olm.m18 = "18 Cadera derecha";
            olm.m19 = "19 Iliaco izquierdo";
            olm.m20 = "20 Iliaco derecho";
            olm.m21 = "21 Rodilla izquierda";
            olm.m22 = "22 Rodilla derecha";
            olm.m23 = "23 Tobillo izquierdo";
            olm.m24 = "24 Tobillo derecho";
            olm.m25 = "25 Talón izquierdo";
            olm.m26 = "26 Talón derecho";
            olm.m27 = "27 Pie izquierdo";
            olm.m28 = "28 Pie derecho";
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private bool checkFilled()
        {
            foreach ( Control c in this.Controls ) {
                try
                {
                    ComboBox cb = c as ComboBox;
                    if (((ComboBox)cb).Text == String.Empty)
                    {

                        return false;
                    }
                }
                catch
                {

                }
            }
            return true;
        }
        private bool checkRepeat()
        {
           
            foreach (Control c1 in this.Controls)
            {
                ComboBox cb1 = c1 as ComboBox;
                foreach (Control c2 in this.Controls)
                {
                    ComboBox cb2 = c2 as ComboBox;
                    try
                    {
                        if (((ComboBox)c1).Text == ((ComboBox)c2).Text)
                        {
                            MessageBox.Show("Los marcadores deben tener números distintos, compruebe la selección de marcadores", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                    }
                    catch { }
                    
                }
            }
            return true;
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            openFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (CsvFileReader reader = new CsvFileReader(openFileDialog1.FileName))
                 {
                     int cont = 1;
                     int i = 0;
                     int j = 0;
                     CsvRow row = new CsvRow();
                     bool bl = true;
                     bool bl1 = false;
                     while (bl)
                     {

                         if (bl1)
                         {
                             if (!reader.ReadRow(row))
                             {
                                 bl = false;
                             }
                         }
                         
                         if (!bl1)
                         {
                             if (!reader.ReadRow(row))
                             {
                                 bl1 = true;
                             }
                         }


                         if (cont == 1)
                         {
                             okl.FormatVersion = row[1];
                             okl.TakeName = row[3];
                             okl.CaptureFrameRate = Convert.ToDouble(row[5]);
                             okl.ExportFrameRate = Convert.ToDouble(row[7]);
                             okl.CaptureStartTime = row[9];
                             okl.TotalFrames = Convert.ToInt16(row[11]);
                             okl.readCSV = false;
                             matrixMov = new double[okl.TotalFrames, 86];
                            
                            
                             cont++;

                         }
                         else
                         {
                             if (cont > 8)
                             {
                                 for (j = 0; j <= row.Count - 1; j++)
                                 {
                                     try
                                     {
                                         double db = Convert.ToDouble(row[j]);
                                         matrixMov[i, j] = db;
                                     }
                                     catch
                                     {
                                         if (i > 0)
                                         {
                                             matrixMov[i, j] = matrixMov[i - 1, j];
                                         }
                                         else
                                         {
                                             matrixMov[i, j] = 0.0;
                                         }
                                     }
                                 }
                                 cont++;
                                 i++;
                                 
                             }
                             else
                             {
                                 cont++;


                             }
                         }
                     }
                     okl.readCSV = true;
                     okl.CurrentRow = 0;                   
                     int longitud;
                     longitud = matrixMov.GetLength(1);
                     double[] matrix = new double[longitud];
                     for (int t = 0; t < longitud; t++)
                     {
                         matrix[t] = matrixMov[1, t];
                     }
                     okl.NameMarks = okl.PredictionNamesMarksCSV(matrix);
                     okl.MatrixMov = matrixMov;
                 }
                int _longitud;
                _longitud = matrixMov.GetLength(1);
                _matrix = new double[_longitud];

                for (int j = 0; j < _longitud; j++)
                {
                    _matrix[j] = matrixMov[1, j];

                }
                kibiomerviewport1.addPointCSV(_matrix);
                _infinityReference = 20;
                _Module = okl.CalculeModule(_matrix, _infinityReference);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (checkFilled() == true && checkRepeat() == true)
            {

            }
        }
    }
}
