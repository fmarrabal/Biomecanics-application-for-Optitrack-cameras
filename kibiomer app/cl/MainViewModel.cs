using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using NatNetML;
using System.Windows.Controls;
using System.Windows;

namespace kibiomer_app.cl  
{
    class MainViewModel : INotifyPropertyChanged
    {
        private IList<Model3D> selectedModels;
        public Point3D[] Data { get; set; }

        public double[] Values { get; set; }
        public string[] NamePoints { get; set; }
        public RectangleSelectionCommand RectangleSelectionCommand { get; private set; }

        public PointSelectionCommand PointSelectionCommand { get; private set; }
        public Model3DGroup Lights
        {
            get
            {
                var group = new Model3DGroup();
                group.Children.Add(new AmbientLight(Colors.White));
                return group;
            }
        }

        public Brush SurfaceBrush
        {
            get
            {
                // return BrushHelper.CreateGradientBrush(Colors.White, Colors.Blue);
                //return GradientBrushes.RainbowStripes;
                return GradientBrushes.BlueWhiteRed;
            }
        }

        public MainViewModel(FrameOfMocapData m_FrameOfData, Viewport3D viewport)
        {
            UpdateModel(m_FrameOfData);
            this.RectangleSelectionCommand = new RectangleSelectionCommand(viewport, this.HandleSelectionEvent);
            this.PointSelectionCommand = new PointSelectionCommand(viewport, this.HandleSelectionEvent);
        }             
        private void UpdateModel(FrameOfMocapData m_FrameOfData)
        {
            //Data = Enumerable.Range(0, 7 * 7 * 7).Select(i => new Point3D(i % 7, (i % 49) / 7, i / 49)).ToArray();

            //var rnd = new Random();
            //this.Values = Data.Select(d => rnd.NextDouble()).ToArray();
            Data = new Point3D[m_FrameOfData.nOtherMarkers];
            double[] V = new double[m_FrameOfData.nOtherMarkers];
            for (int i = 0; i < m_FrameOfData.nOtherMarkers; i++)
            {
                NatNetML.Marker ms = m_FrameOfData.OtherMarkers[i];
                float x, y, z;
                x = ms.x * 10;
                y = ms.y * 10;
                z = ms.z * 10;
                Data[i] = new Point3D(x, y, z);
                V[i] = y;
            }
            this.Values = V;
            //var rnd = new Random();
            //this.Values = Data.Select(d => rnd.NextDouble()).ToArray();
            RaisePropertyChanged("Data");
            RaisePropertyChanged("Values");
            RaisePropertyChanged("SurfaceBrush");
            RaisePropertyChanged("NamePoints");
        }
        private void HandleSelectionEvent(object sender, ModelsSelectedEventArgs args)
        {
            this.ChangeMaterial(this.selectedModels, Materials.Blue);
            this.selectedModels = args.SelectedModels;
            var rectangleSelectionArgs = args as ModelsSelectedByRectangleEventArgs;
            if (rectangleSelectionArgs != null)
            {
                this.ChangeMaterial(
                    this.selectedModels,
                    rectangleSelectionArgs.Rectangle.Size != default(Size) ? Materials.Red : Materials.Green);
            }
            else
            {
                this.ChangeMaterial(this.selectedModels, Materials.Orange);
            }
        }
        private void ChangeMaterial(IEnumerable<Model3D> models, Material material)
        {
            if (models == null)
            {
                return;
            }

            foreach (var model in models)
            {
                var geometryModel = model as GeometryModel3D;
                if (geometryModel != null)
                {
                    geometryModel.Material = geometryModel.BackMaterial = material;
                }
            }
        }
        public SelectionHitMode SelectionMode
        {
            get
            {
                return this.RectangleSelectionCommand.SelectionHitMode;
            }

            set
            {
                this.RectangleSelectionCommand.SelectionHitMode = value;
            }
        }

        public IEnumerable<SelectionHitMode> SelectionModes
        {
            get
            {
                return Enum.GetValues(typeof(SelectionHitMode)).Cast<SelectionHitMode>();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string property)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
