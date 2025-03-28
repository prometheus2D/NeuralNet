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
            toolStripStatusLabelRunTime.Alignment = ToolStripItemAlignment.Right;
            exitToolStripMenuItem.Click += (sender, args) => Application.Exit();

            //Add DataSets
            var dataSourceButtons = NetworkData.NetworkDataFactoryDictionary
                .Select(x => toolStripDropDownButtonDataSet.DropDownItems.Add(x.Key, null, (sender, args) => toolStripDropDownButtonDataSet.Text = x.Key))
                .ToList();

            //Time Control
            toolStripButtonStop.Click += (sender, args) =>
            {
                Runner?.Abort();
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
                        toolStripButtonStart.Enabled = true;
                        toolStripButtonStop.Enabled = false;
                    }
                }

                if (Runner != null)
                {
                    toolStripStatusLabelIterationValue.Text = Runner != null ? Runner.CurrentIteration.ToString() : "-";
                    toolStripStatusLabelRunTime.Text = Runner != null ? Runner.RunTime.ToString() : "---";
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
            toolStripButtonStart.Click += async (sender, args) =>
            {
                toolStripButtonStart.Enabled = false;
                toolStripButtonStop.Enabled = true;
                //NetworkData data = NetworkData.InitXORData();
                var data = NetworkData.GetNetworkData(toolStripDropDownButtonDataSet.Text ?? throw new Exception());
                var instance = GlobalFactory.CreateNetworkInstance(ModelStringKey, "BackProp", new int[] { data.InputSetLength, 18, 12, data.OutputSetLength });
                Runner = new NetworkInstanceRunner(instance, data, null,
                    null,//line => Console.WriteLine(line),
                    (index, value) =>
                        chartUserControl.AddSeriesDataPoint(instance.Guid.ToString(), index, value))
                {
                    Parameters = TrainingParameters.Default,
                    MultiThreaded = toolStripButtonMultiThreaded.Checked
                };

                if (Runner.MultiThreaded)
                    await Runner.Run();
                else 
                    Runner.Run();
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
