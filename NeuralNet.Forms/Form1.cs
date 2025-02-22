namespace NeuralNet.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            exitToolStripMenuItem.Click += (sender, args) => Application.Exit();
        }
    }
}
