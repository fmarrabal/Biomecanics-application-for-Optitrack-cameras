using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace kibiomerlab
{
    public class kl 
    {
        string _MachineName;
        string _Version;
        string _LocalIP;
        string _ServerIP;
        IPHostEntry _ipHost;
        int _iConnetionType;
        double _m_ServerFramerate = 1.0f;
        float _m_fCurrentMocapFrameTimestamp = 0.0f;
        float _m_fFirstMocapFrameTimestamp = 0.0f;
        double _m_fLastFrameTimestamp = 0.0f;
        long _m_FrameCounter = 0;
        int _TotalFrames;
        string _FormatVersion;
        string _TakeName;
        double _CaptureFrameRate;
        double _ExportFrameRate;
        string _CaptureStartTime;
        bool _readCSV;
        int _CurrentRow;
        double[,] _MatrixMov;
        string[] _NameMarks;

        public long m_FrameCounter
        {
            get { return _m_FrameCounter; }
            set { _m_FrameCounter = value; }
        }
        public double m_fLastFrameTimestamp
        {
            get { return _m_fLastFrameTimestamp; }
            set { _m_fLastFrameTimestamp = value; }
        }
        public float m_fFirstMocapFrameTimestamp
        {
            get { return _m_fFirstMocapFrameTimestamp; }
            set { _m_fFirstMocapFrameTimestamp = value; }
        }
        public float m_fCurrentMocapFrameTimestamp
        {
            get { return _m_fCurrentMocapFrameTimestamp; }
            set { _m_fCurrentMocapFrameTimestamp = value; }
        }
        public double m_ServerFramerate
        {
            get { return _m_ServerFramerate; }
            set { _m_ServerFramerate = value; }
        }
        public string MachineName
        {
            get { return _MachineName; }
            set { _MachineName = value; }
        }
        public IPHostEntry ipHost
        {
            get { return _ipHost; }
            set { _ipHost = value; }
        }
        public string Version
        {
            get { return _Version; }
            set { _Version = value; }
        }
        public int iConnetionType
        {
            get { return _iConnetionType; }
            set { _iConnetionType = value; }
        }
        public string LocalIP
        {
            get { return _LocalIP; }
            set { _LocalIP = value; }
        }
        public string ServerIP
        {
            get { return _ServerIP; }
            set { _ServerIP = value; }
        }
        public int TotalFrames
        {
            get { return _TotalFrames; }
            set { _TotalFrames = value; }
        }       
        public string FormatVersion
        {
            get { return _FormatVersion; }
            set { _FormatVersion = value; }
        }
        public string TakeName
        {
            get { return _TakeName; }
            set { _TakeName = value; }
        }
        public double CaptureFrameRate
        {
            get { return _CaptureFrameRate; }
            set { _CaptureFrameRate = value; }
        }
        public double ExportFrameRate
        {
            get { return _ExportFrameRate; }
            set { _ExportFrameRate = value; }
        }
        public string CaptureStartTime
        {
            get { return _CaptureStartTime; }
            set { _CaptureStartTime = value; }
        }     

        public bool readCSV
        {
            get { return _readCSV; }
            set { _readCSV = value; }
        }
        public int CurrentRow
        {
            get { return _CurrentRow; }
            set { _CurrentRow = value; }
        }
        public double[,] MatrixMov
        {
            get { return _MatrixMov; }
            set { _MatrixMov = value; }
        }
        public string[] NameMarks
        {
            get { return _NameMarks; }
            set { _NameMarks = value; }
        }
        public string[] PredictionNamesMarksCSV(double[] Marks)
        {
            string[] NameMarks = new string[Convert.ToInt16((Marks.Length - 2) / 3)];
            double[] X = new double[Convert.ToInt16((Marks.Length - 2) / 3)];
            double[] Y = new double[Convert.ToInt16((Marks.Length - 2) / 3)];
            double[] Z = new double[Convert.ToInt16((Marks.Length - 2) / 3)];
            int j = 2;
            for (int i =0;i<Convert.ToInt16((Marks.Length - 2) / 3);i++)
            {
                X[i] = Marks[j];
                Y[i] = Marks[j+1];
                Z[i] = Marks[j+2];
                j++;
            }
            double maxX, maxY, maxZ;
            maxX = X.Max();
            maxY = Y.Max();
            maxZ = Z.Max();
            return NameMarks;
        }
        public double[] CalculeModule(double[] Matrix, double infinityReference)
        {
            int longitud = Matrix.Count();
            int cont = 1;
            int j = 1;
            double value = 0;
            double[] Module = new double[longitud/3];
            for (int i = 1; i <= longitud; i++)
            {
                if (cont <= 3)
                {
                    value = Math.Pow(Matrix[i] - infinityReference, 2) + value;
                    if (cont == 3)
                    {
                        Module[j] = Math.Sqrt(value);
                        value = 0;
                        cont = 0;
                        j++;
                    }
                    
                    cont++;
                }
            }
            return Module;
        }
    }
}
