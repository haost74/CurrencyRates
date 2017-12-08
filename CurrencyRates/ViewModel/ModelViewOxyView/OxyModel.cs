using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRates.ViewModel.ModelViewOxyView
{
    using OxyPlot;
    using OxyPlot.Axes;

    public class OxyModel : INotifyPropertyChanged
    {
        #region - INotifyPropertyChanged -
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        #endregion

        public string Title { get; private set; }
        public IList<DataPoint> Points { get; set; }
        public static object Constants { get; private set; }
        public DateTimeAxis dateAxis = null;

        public DateTime axismin = new DateTime(2017, 01, 01);
        public DateTime axismax = new DateTime(2017, 12, 01);

        private PlotModel plotModel;
        public PlotModel PlotModel
        {
            get { return plotModel; }
            set
            {
                plotModel = value;
                OnPropertyChanged();
            }
        }

        public OxyModel() { PlotModel = new PlotModel(); SetUpModel(); }   

        public void SetParameterDate(DateTime axismin, DateTime axismax)
        {
            this.axismin = axismin;
            this.axismax = axismax;
        }
        
        public void SetUpModel()
        {
            //PlotModel.LegendTitle = "Legend";
            //PlotModel.LegendOrientation = LegendOrientation.Horizontal;
            //PlotModel.LegendPlacement = LegendPlacement.Outside;
            //PlotModel.LegendPosition = LegendPosition.TopRight;
            //PlotModel.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            //PlotModel.LegendBorder = OxyColors.Black;                      

            dateAxis = new DateTimeAxis
            {
                StringFormat = "HH:mm dd/MM/yyyy",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Minimum = DateTimeAxis.ToDouble(axismin),
                Maximum = DateTimeAxis.ToDouble(axismax),
                IntervalLength = 80
            };

           
        }    

    }
}
