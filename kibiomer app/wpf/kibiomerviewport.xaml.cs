using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HelixToolkit.Wpf;
using HelixToolkit;
using System.Windows.Media.Media3D;
using System.ComponentModel;
using NatNetML;
namespace kibiomer_app.wpf
{
    /// <summary>
    /// Lógica de interacción para kibiomerviewport.xaml
    /// </summary>
    public partial class kibiomerviewport : UserControl
    {
        cl.MainViewModel mvm;
        cl.MainViewModelCSV mvmCSV;
        public kibiomerviewport()
        {
            InitializeComponent();
        }
        public void addPoint(FrameOfMocapData m_FrameOfData)
        {
            mvm = new cl.MainViewModel(m_FrameOfData, viewport.Viewport);
            this.DataContext = mvm;
            this.viewport.InputBindings.Add(new MouseBinding(mvm.RectangleSelectionCommand, new MouseGesture(MouseAction.LeftClick)));
            this.viewport.InputBindings.Add(new MouseBinding(mvm.PointSelectionCommand, new MouseGesture(MouseAction.LeftClick, ModifierKeys.Control)));
        }
        public void addPointCSV(double[] FrameOfData)
        {
            mvmCSV = new cl.MainViewModelCSV(FrameOfData, viewport.Viewport);
            this.DataContext = mvmCSV;
            this.viewport.InputBindings.Add(new MouseBinding(mvmCSV.RectangleSelectionCommand, new MouseGesture(MouseAction.LeftClick)));
            this.viewport.InputBindings.Add(new MouseBinding(mvmCSV.PointSelectionCommand, new MouseGesture(MouseAction.LeftClick, ModifierKeys.Control)));
        }
    }
}
