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
using System.Windows.Shapes;

namespace CurrencyRates.View
{
    /// <summary>
    /// Логика взаимодействия для OxyView.xaml
    /// </summary>
    public partial class OxyView : Window
    {
        public OxyView()
        {
            InitializeComponent();
            Loaded += OxyView_Loaded;
        }

        static readonly int kNumPoint = 3600;

        private void OxyView_Loaded(object sender, RoutedEventArgs e)
        {
            var series = new LineSeries
            {
                StrokeThickness = 0.5
            };
            Data.PlotModel.Axes.Add(Data.dateAxis);
            addSeries(series);
            Data.PlotModel.Series.Add(series);
        }

        private void addSeries(LineSeries series)
        {
            var gen = new Random();
            var wrkdt = DateTime.Now;
            for (int idx = 0; idx < kNumPoint; idx++)
            {
                var yval = gen.NextDouble() * 10.0;
                series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(wrkdt), yval));
                wrkdt += new TimeSpan(0, 10, 1); // h, m, s
            }
        }
    }
}
