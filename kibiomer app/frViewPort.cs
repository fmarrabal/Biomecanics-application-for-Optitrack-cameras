using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
namespace kibiomer_app
{
    public partial class frViewPort : Form//MaterialSkin.Controls.MaterialForm
    {
        public Form1 ofrInicio;
        public frViewPort()
        {
            InitializeComponent();
            //var materialSkinManager = MaterialSkinManager.Instance;
            //materialSkinManager.AddFormToManage(this);
            //materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            //materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void frViewPort_Load(object sender, EventArgs e)
        {
            //kibiomer_app.wpf.kibiomerviewport kvp = new kibiomer_app.wpf.kibiomerviewport();
            //elementHost1.Child = kvp;

        }
        public bool UpdateDataCSV(double[] FrameOfData)
        {
            try
            {
                kibiomerviewport1.addPointCSV(FrameOfData);
                return true;
            }
            catch
            { return false; }         
        }
    }
}
