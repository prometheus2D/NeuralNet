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
        private Task _RunTask;
        public Task RunTask 
        {
            get => _RunTask;
            set
            {
                _RunTask = value;
                toolStripButtonStop.Enabled = value != null;
            }
        }

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
            toolStripButtonRefresh.Click += (sender, args) =>
            {
                //NetworkData data = NetworkData.InitXORData();
                NetworkData data = NetworkData.NetworkDataDictionary[toolStripDropDownButtonDataSet.Text ?? throw new Exception()];

                var instance = GlobalFactory.CreateNetworkInstance(ModelStringKey, "BackProp", new int[] { data.InputSetLength, 3, data.OutputSetLength });
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

                //runner.Run();
                //RunTask =
                    //Task.Run(() =>
                    //{
                        runner.Run();
                    //}//, 
                    //_RunTokenSource.Token
                    //).ContinueWith((x) => RunTask = null
                    //);
            };

            //Perform Setup
            dataSourceButtons.Where(x => x.Text == "XOR").FirstOrDefault().PerformClick();
            nnButtons.FirstOrDefault().PerformClick();
            //toolStripButtonRefresh.PerformClick();
        }
    }
}
