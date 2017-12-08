using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
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

namespace CurrencyRates.View
{
    /// <summary>
    /// Логика взаимодействия для OxyTest.xaml
    /// </summary>
    public partial class OxyTest : UserControl
    {
        static readonly int kNumPoint = 3600;
        public static OxyTest Customer = null;

        public OxyTest()
        {
            InitializeComponent();
            Customer = this;
            Loaded += OxyView_Loaded;
        }

        private void OxyView_Loaded(object sender, RoutedEventArgs e)
        {
            //Start(0.5);
        }

        public void Start(double d, double[] mass, string[] dates)
        {
            var series = new LineSeries {  StrokeThickness = d  };

            if (Data.PlotModel.Axes.Count > 0) { Data.PlotModel.Axes.Clear(); Data.PlotModel.Series.Clear(); }
            Data.PlotModel.Axes.Add(Data.dateAxis);
            addSeries(series, mass, dates);
            Data.PlotModel.Series.Add(series);

            //Plot1.InvalidatePlot(true);
            Data.PlotModel.InvalidatePlot(true);
        }

        private void addSeries(LineSeries series, double[] mass, string[] dates)
        {
            var gen = new Random();
            var wrkdt = Data.axismin;//new DateTime(2017, 01, 01);
            //for (int idx = 0; idx < kNumPoint; idx++)
            //{
            //    var yval = gen.NextDouble() * 10.0;
            //    series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(wrkdt), yval));
            //    wrkdt += new TimeSpan(1, 10, 1); // h, m, s
            //}

            for(int idx = 0; idx < mass.Length; idx++)
            {
                var yval = gen.NextDouble() * 10.0;
                var date = DateTime.Parse(dates[idx]);
                series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(date), mass[idx]));
                wrkdt += new TimeSpan(1, 10, 1);
            }
        }
    }
}
