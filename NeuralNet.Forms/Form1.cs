using NeuralNet.Core;
using NeuralNet.Core.Global;
using NeuralNet.Core.Training;
using NeuralNet.Data;
using System.Windows.Forms.DataVisualization.Charting;

namespace NeuralNet.Forms
{
    public partial class Form1 : Form
    {
        public static string ModelStringKey = null;
        public static NetworkInstanceRunner Runner = null;

        public Form1()
        {
            //UI
            InitializeComponent();
            exitToolStripMenuItem.Click += (sender, args) => Application.Exit();

            //Add DataSets
            var dataSourceButtons = new List<ToolStripItem>()
            {
                toolStripDropDownButtonDataSet.DropDownItems.Add("XOR", null, (sender, args) => toolStripDropDownButtonDataSet.Text = "XOR"),
                toolStripDropDownButtonDataSet.DropDownItems.Add("MNIST", null, (sender, args) => toolStripDropDownButtonDataSet.Text = "MNIST")
            };

            //Time Control
            toolStripButtonStop.Click += (sender, args) =>
            {

            };

            //Event Timer
            timer1.Tick += (sender, args) =>
            {
                if (Runner != null)
                {                    
                    Runner.ProcessEvents();

                    if (Runner.IsFinished)
                    {
                        Runner = null;
                        toolStripButtonRefresh.Enabled = true;
                        toolStripButtonStop.Enabled = false;
                    }
                }
            };

            //Add Networks
            var nnButtons = new List<ToolStripItem>();
            foreach (var x in GlobalFactory.NetworkFactories.Values)
                nnButtons.Add(toolStripDropDownButtonNNModel.DropDownItems.Add(x.NetworkKey, null, (sender, args) => toolStripDropDownButtonNNModel.Text = x.NetworkKey));
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
            //Refresh Runs
            toolStripButtonRefresh.Click += async (sender, args) =>
            {
                toolStripButtonRefresh.Enabled = false;
                toolStripButtonStop.Enabled = true;
                //NetworkData data = NetworkData.InitXORData();
                NetworkData data = NetworkData.NetworkDataDictionary[toolStripDropDownButtonDataSet.Text ?? throw new Exception()];

                var instance = GlobalFactory.CreateNetworkInstance(ModelStringKey, "BackProp", new int[] { data.InputSetLength, 3, data.OutputSetLength });
                Runner = new NetworkInstanceRunner(instance, data, null,
                    null,//line => Console.WriteLine(line),
                    (index, value) => 
                        chartUserControl.AddSeriesDataPoint(instance.Guid.ToString(), index, value))
                {
                    Parameters = new TrainingParameters
                    {
                        Verbose = true,
                        VerboseModulus = 1
                    }
                };

                await Runner.Run();
            };
            toolStripButtonStop.Click += (sender, args) => Runner.Abort();

            //Perform Setup
            dataSourceButtons.Where(x => x.Text == "XOR").FirstOrDefault().PerformClick();
            //nnButtons.FirstOrDefault().PerformClick();
            nnButtons.Where(x => x.Text.Contains("Ron")).FirstOrDefault().PerformClick();
            //toolStripButtonRefresh.PerformClick();
        }
    }
}
