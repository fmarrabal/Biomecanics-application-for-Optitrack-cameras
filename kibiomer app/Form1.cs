using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using MaterialSkin;
using System.IO;
using kibiomerlab;
using System.Globalization;
using System.Net;
namespace kibiomer_app
{
    public partial class Form1 : Form
    {
        //KeystrokMessageFilter keyStrokeMessageFilter = new KeystrokMessageFilter();
        frViewPort ofr = new frViewPort();
        kl okl;
        listamarcadores olm;
        double[,] matrixMov;
        private static object syncLock = new object();
        HiResTimer timer;
        Int64 lastTime = 0;
        Int64 currTime = 0;
        double timeNow = 0.0;
        public Form1()
        {
            InitializeComponent();
            //Application.AddMessageFilter(keyStrokeMessageFilter);
            timer = new HiResTimer();
            okl = new kl();
            olm = new listamarcadores();
            olm.m1 = "1 Cabeza derecha";
            olm.m2 = "2 Cabeza posterior";
            olm.m3 = "3 Cabeza izquierda";
            olm.m4 = "4 Cabeza frontal";
            olm.m5 = "5 Cervical a";
            olm.m6 = "6 Cervical b";
            olm.m7 = "7Toráfica 12 a";
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
            //var materialSkinManager = MaterialSkinManager.Instance;
            //materialSkinManager.AddFormToManage(this);
            //materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            //materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            okl.MachineName = Dns.GetHostName();
            okl.ipHost = Dns.GetHostEntry(okl.MachineName);
            ofr.MdiParent = this;
            ofr.ofrInicio = this;
            ofr.Show();
            splitContainer1.Panel2.Controls.Add(ofr);
            ofr.Width = splitContainer1.Panel2.Width;
            ofr.Height = splitContainer1.Panel2.Height;
            
            
        }

        private void splitContainer1_Panel2_Resize(object sender, EventArgs e)
        {
            ofr.Width = splitContainer1.Panel2.Width;
            ofr.Height = splitContainer1.Panel2.Height;
        }

        private void abrirToolStripButton_Click(object sender, EventArgs e)
        {
                 
            ofdAbrir.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            ofdAbrir.FilterIndex = 1;
            ofdAbrir.RestoreDirectory = true;

            if (ofdAbrir.ShowDialog() == DialogResult.OK)
            {
                //try
                //{
                    using (CsvFileReader reader = new CsvFileReader(ofdAbrir.FileName))
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
                            //if (!bl)
                            //{
                            //    break;
                            //}
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
                                tspb.Maximum = okl.TotalFrames+9;
                                //foreach (string s in row)
                                //{
                                //}
                                cont++;
                                tspb.Value++;
                            }
                            else
                            {
                                if (cont > 8)
                                {
                                    for (j = 0; j<=row.Count - 1; j++)
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
                                    tspb.Value++;
                                }
                                else
                                {
                                    cont++;
                                    tspb.Value++;
                                    
                                }
                            }
                        }
                        okl.readCSV = true;
                        okl.CurrentRow = 0;
                        tspb.Value = 0;
                        tsbStop.Enabled = true;
                        tsbPlay.Enabled = true;
                        tspb.Maximum = okl.TotalFrames;
                        tspb.Value = 0;
                        tsbBegin.Enabled = true;
                        tsbEnd.Enabled = true;
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
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                //    okl.readCSV = false;
                //    okl.CurrentRow = 0;
                //    tspb.Value = 0;
                //    tsbStop.Enabled = false;
                //    tsbPlay.Enabled = false;
                //    tsbBegin.Enabled = false;
                //    tsbEnd.Enabled = false;
                //}
            }
        }

        private void nuevoToolStripButton_Click(object sender, EventArgs e)
        {
            okl.readCSV = false;
            okl.CurrentRow = 0;
            tspb.Value = 0;
            tsbStop.Enabled = false;
            tsbPlay.Enabled = false;
            tsbBegin.Enabled = false;
            tsbEnd.Enabled = false;
        }
        private void UpdateDateCSV()
        { 
            
        }
        private void timerObject_Tick(object sender, EventArgs e)
        {
            currTime = timer.Value;
            if (lastTime != 0)
            {
                Int64 timeElapsedInTicks = currTime - lastTime;
                Int64 timeElapseInTenthsOfMilliseconds = (timeElapsedInTicks * 10000) / timer.Frequency;
                timeNow = timeNow + Convert.ToDouble(timeElapseInTenthsOfMilliseconds)/10000;
                //OutputMessage("Frame Delivered: (" + timeElapseInTenthsOfMilliseconds.ToString() + ")  FrameTimestamp: " + data.fLatency);
                TimestampValue.Text = "Timespan: " + timeNow.ToString("N3") + " seconds";
            }         
            lastTime = currTime;
            lock (syncLock)
            {
                if (okl.CurrentRow < okl.TotalFrames)
                {

                    if (UpdateDataCSV(okl.CurrentRow))
                    {
                        okl.CurrentRow++;
                        tspb.Value++;
                    }
                    else
                    {
                        timerObject.Enabled = false;
                        tspb.Value = 0;
                        timeNow = 0.0;
                        TimestampValue.Text = "Timespan: 0 " + " seconds"; lastTime = currTime;
                        // AQUÍ HAY QUE PONER UN MENSAJE DE ERROR PORQUE HAY ALGO QUE NO HA IDO BINEN
                    }
                    
                    
                }
                else
                {
                    timerObject.Enabled = false;
                    tspb.Value = 0;
                    timeNow = 0.0;
                    TimestampValue.Text = "Timespan: 0" + " seconds"; lastTime = currTime;
                    // AQUÍ HAY QUE PONER UN MENSAJE DE TERMINADO CORRECTAMENTE LA REPRODUCCIÓN
                }
                
            }
        }

        private void tsbPlay_Click(object sender, EventArgs e)
        {
            if (tspb.Value == okl.TotalFrames)
            {
                tspb.Value = 0;
                timeNow = 0.0;
                TimestampValue.Text = "Timespan: 0" + " seconds"; lastTime = currTime;
            }
            timerObject.Interval = Convert.ToInt16(okl.ExportFrameRate);
            timerObject.Enabled = true;
                   
        }
        private bool UpdateDataCSV(int i)
        {            
            int longitud;
            longitud = matrixMov.GetLength(1);
            double[] matrix = new double[longitud];
            
            for(int j = 0; j<longitud;j++)
            {
                 matrix[j] = matrixMov[i,j];
                
            }
            bool bl = ofr.UpdateDataCSV(matrix);
            return bl;                                  
        }

        private void tsbStop_Click(object sender, EventArgs e)
        {
            timerObject.Enabled = false;
            lastTime = currTime;
        }

        private void tsbEnd_Click(object sender, EventArgs e)
        {
            okl.CurrentRow = okl.TotalFrames;
            timerObject.Enabled = false;
            tspb.Value = okl.TotalFrames;
            if (UpdateDataCSV(okl.CurrentRow-1))
            {
                
            }
            else
            {
                timerObject.Enabled = false;
                tspb.Value = 0;
                timeNow = 0.0;
                TimestampValue.Text = "Timespan: 0" + " seconds"; lastTime = currTime;
                // AQUÍ HAY QUE PONER UN MENSAJE DE ERROR PORQUE HAY ALGO QUE NO HA IDO BINEN
            }
        }

        private void tsbBegin_Click(object sender, EventArgs e)
        {
            okl.CurrentRow = 0;
            tspb.Value = 0;
            TimestampValue.Text = "Timespan: 0" + " seconds"; lastTime = currTime;
            timeNow = 0.0;
        }

        private void ts_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addskeleton ofr = new addskeleton();            
            ofr.BringToFront();
            ofr.Focus();
            ofr.Show();
           
        }
    }
}
