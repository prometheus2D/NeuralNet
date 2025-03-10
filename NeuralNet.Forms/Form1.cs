using NeuralNet.Network;
using System.Windows.Forms.DataVisualization.Charting;

namespace NeuralNet.Forms
{
    public partial class Form1 : Form
    {
        public static string ModelStringKey = null;

        public Form1()
        {
            //UI
            InitializeComponent();
            exitToolStripMenuItem.Click += (sender, args) => Application.Exit();
            toolStripDropDownButtonDataSet.DropDownItems.Add("XOR", null, (sender, args) => toolStripDropDownButtonDataSet.Text = "XOR");

            //Model UI
            foreach (var item in toolStripDropDownButtonNNModel.DropDownItems)
            {
                (item as ToolStripMenuItem).Click += (sender, args) =>
                {
                    ModelStringKey = toolStripDropDownButtonNNModel.Text = (item as ToolStripMenuItem).Text.Split('_').FirstOrDefault();
                    (item as ToolStripMenuItem).Checked = item == sender;

                    foreach (var item in toolStripDropDownButtonNNModel.DropDownItems)
                        (item as ToolStripMenuItem).Checked = item == sender;
                };
            }

            //Create Graph
            var series = new Series("Test")
            {
                ChartType = SeriesChartType.Line
            };

            //Refresh Runs
            toolStripButtonRefresh.Click += (sender, args) =>
            {
                series.Points.Clear();
                NetworkData data = NetworkData.XORData;

                var instance = GlobalFactory.CreateNetworkInstance(ModelStringKey, "BackProp", new int[] { 2, 3, 1 });
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
            };

            //Perform Setup
            ronBPToolStripMenuItem.PerformClick();
            toolStripButtonRefresh.PerformClick();
        }
    }
}
