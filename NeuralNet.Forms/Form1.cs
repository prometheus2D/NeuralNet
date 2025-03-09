using NeuralNet.Network;
using System.Windows.Forms.DataVisualization.Charting;

namespace NeuralNet.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //UI
            InitializeComponent();
            exitToolStripMenuItem.Click += (sender, args) => Application.Exit();
            toolStripDropDownButtonDataSet.DropDownItems.Add("XOR", null, (sender, args) => toolStripDropDownButtonDataSet.Text = "XOR");

            var series = new Series("Test")
            {
                ChartType = SeriesChartType.Line
            };
            //chart1.Series.Add(series);
            //for (int i = 0; i < 10; i++)
            //    series.Points.AddXY(i, i);


            toolStripButtonRefresh.Click += (sender, args) =>
            {
                series.Points.Clear();
                NetworkData data = NetworkData.XORData;

                var instance = GlobalFactory.CreateNetworkInstance("Accord", "BackProp", new int[] { 2, 3, 1 });
                var runner = new NetworkInstanceRunner(instance, data, null,
                    null,//line => Console.WriteLine(line),
                    (index, value) => chartUserControl.AddSeriesDataPoint(instance.Guid.ToString(), index, value))
                {
                    Parameters = new TrainingParameters
                    {
                        Verbose = true,
                        VerboseModulus = 100
                    }
                };

                runner.Run();

                //// ----- Create an Accord-based network runner -----
                //var factory = new AccordNetworkFactory();
                //var accordInstance = factory.CreateNetworkFF3(2, 3, 1);
                //var accordRunner = new NetworkInstanceRunner(accordInstance, data, null,
                //    null,//line => Console.WriteLine(line),
                //    (index, value) => chartUserControl.AddSeriesDataPoint(accordInstance.Guid.ToString(), index, value))
                //{
                //    Parameters = new TrainingParameters
                //    {
                //        Verbose = true,
                //        VerboseModulus = 100
                //    }
                //};

                //series.Points.AddXY(index, value));
                //accordRunner.Parameters.Verbose = true;
                //accordRunner.Parameters.VerboseModulus = 100;
                //accordRunner.Run();
            };

            toolStripButtonRefresh.PerformClick();
        }
    }
}
