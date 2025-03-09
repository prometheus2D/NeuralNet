using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace NeuralNet.Forms
{
    public partial class ChatUserControl: UserControl
    {
        public ChatUserControl()
        {
            InitializeComponent();
        }

        public void AddSeriesDataPoint(string seriesName, double x, double y)
        {
            //var series = new Series("Test")
            //{
            //    ChartType = SeriesChartType.Line
            //};
            //chart1.Series.Add(series);
            //for (int i = 0; i < 10; i++)
            //    series.Points.AddXY(i, i);

            if (!chart1.Series.Where(x => x.Name == seriesName).Any())
                chart1.Series.Add(new Series(seriesName)
                {
                    ChartType = SeriesChartType.Line
                });

            var series = chart1.Series.Where(x => x.Name == seriesName).FirstOrDefault();

            series?.Points.AddXY(x, y);
        }
    }
}
