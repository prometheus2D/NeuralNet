namespace NeuralNet.Forms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            toolStripContainer1 = new ToolStripContainer();
            chartUserControl = new ChartUserControl();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            toolStrip1 = new ToolStrip();
            toolStripDropDownButtonDataSet = new ToolStripDropDownButton();
            toolStripDropDownButtonNNModel = new ToolStripDropDownButton();
            toolStripTextBoxNNPattern = new ToolStripTextBox();
            toolStripButtonStart = new ToolStripButton();
            toolStripButtonStop = new ToolStripButton();
            timer1 = new System.Windows.Forms.Timer(components);
            statusStrip1 = new StatusStrip();
            toolStripStatusLabelIterationStatic = new ToolStripStatusLabel();
            toolStripStatusLabelIterationValue = new ToolStripStatusLabel();
            toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            toolStripContainer1.ContentPanel.SuspendLayout();
            toolStripContainer1.TopToolStripPanel.SuspendLayout();
            toolStripContainer1.SuspendLayout();
            menuStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            toolStripContainer1.BottomToolStripPanel.Controls.Add(statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            toolStripContainer1.ContentPanel.Controls.Add(chartUserControl);
            toolStripContainer1.ContentPanel.Size = new Size(800, 379);
            toolStripContainer1.Dock = DockStyle.Fill;
            toolStripContainer1.Location = new Point(0, 0);
            toolStripContainer1.Name = "toolStripContainer1";
            toolStripContainer1.Size = new Size(800, 450);
            toolStripContainer1.TabIndex = 0;
            toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            toolStripContainer1.TopToolStripPanel.Controls.Add(menuStrip1);
            toolStripContainer1.TopToolStripPanel.Controls.Add(toolStrip1);
            // 
            // chartUserControl
            // 
            chartUserControl.Dock = DockStyle.Fill;
            chartUserControl.Location = new Point(0, 0);
            chartUserControl.Name = "chartUserControl";
            chartUserControl.Size = new Size(800, 379);
            chartUserControl.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Dock = DockStyle.None;
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(93, 22);
            exitToolStripMenuItem.Text = "Exit";
            // 
            // toolStrip1
            // 
            toolStrip1.Dock = DockStyle.None;
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripDropDownButtonDataSet, toolStripDropDownButtonNNModel, toolStripTextBoxNNPattern, toolStripButtonStart, toolStripButtonStop });
            toolStrip1.Location = new Point(3, 24);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(282, 25);
            toolStrip1.TabIndex = 1;
            // 
            // toolStripDropDownButtonDataSet
            // 
            toolStripDropDownButtonDataSet.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButtonDataSet.Image = (Image)resources.GetObject("toolStripDropDownButtonDataSet.Image");
            toolStripDropDownButtonDataSet.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButtonDataSet.Name = "toolStripDropDownButtonDataSet";
            toolStripDropDownButtonDataSet.Size = new Size(43, 22);
            toolStripDropDownButtonDataSet.Text = "XOR";
            // 
            // toolStripDropDownButtonNNModel
            // 
            toolStripDropDownButtonNNModel.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButtonNNModel.Image = (Image)resources.GetObject("toolStripDropDownButtonNNModel.Image");
            toolStripDropDownButtonNNModel.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButtonNNModel.Name = "toolStripDropDownButtonNNModel";
            toolStripDropDownButtonNNModel.Size = new Size(55, 22);
            toolStripDropDownButtonNNModel.Text = "RonBP";
            // 
            // toolStripTextBoxNNPattern
            // 
            toolStripTextBoxNNPattern.BorderStyle = BorderStyle.FixedSingle;
            toolStripTextBoxNNPattern.Name = "toolStripTextBoxNNPattern";
            toolStripTextBoxNNPattern.Size = new Size(100, 25);
            toolStripTextBoxNNPattern.Text = "[IN]x3x[OUT]";
            // 
            // toolStripButtonStart
            // 
            toolStripButtonStart.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButtonStart.Image = (Image)resources.GetObject("toolStripButtonStart.Image");
            toolStripButtonStart.ImageTransparentColor = Color.Magenta;
            toolStripButtonStart.Name = "toolStripButtonStart";
            toolStripButtonStart.Size = new Size(35, 22);
            toolStripButtonStart.Text = "Start";
            // 
            // toolStripButtonStop
            // 
            toolStripButtonStop.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButtonStop.Enabled = false;
            toolStripButtonStop.Image = (Image)resources.GetObject("toolStripButtonStop.Image");
            toolStripButtonStop.ImageTransparentColor = Color.Magenta;
            toolStripButtonStop.Name = "toolStripButtonStop";
            toolStripButtonStop.Size = new Size(35, 22);
            toolStripButtonStop.Text = "Stop";
            // 
            // timer1
            // 
            timer1.Enabled = true;
            // 
            // statusStrip1
            // 
            statusStrip1.Dock = DockStyle.None;
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabelIterationStatic, toolStripStatusLabelIterationValue });
            statusStrip1.Location = new Point(0, 0);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(800, 22);
            statusStrip1.TabIndex = 0;
            // 
            // toolStripStatusLabelIterationStatic
            // 
            toolStripStatusLabelIterationStatic.Name = "toolStripStatusLabelIterationStatic";
            toolStripStatusLabelIterationStatic.Size = new Size(54, 17);
            toolStripStatusLabelIterationStatic.Text = "Iteration:";
            // 
            // toolStripStatusLabelIterationValue
            // 
            toolStripStatusLabelIterationValue.Name = "toolStripStatusLabelIterationValue";
            toolStripStatusLabelIterationValue.Size = new Size(12, 17);
            toolStripStatusLabelIterationValue.Text = "-";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(toolStripContainer1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            toolStripContainer1.BottomToolStripPanel.PerformLayout();
            toolStripContainer1.ContentPanel.ResumeLayout(false);
            toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer1.TopToolStripPanel.PerformLayout();
            toolStripContainer1.ResumeLayout(false);
            toolStripContainer1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ToolStripContainer toolStripContainer1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripDropDownButton toolStripDropDownButtonDataSet;
        private ToolStripTextBox toolStripTextBoxNNPattern;
        private ChartUserControl chartUserControl;
        private ToolStripDropDownButton toolStripDropDownButtonNNModel;
        private ToolStripButton toolStripButtonStop;
        private System.Windows.Forms.Timer timer1;
        private ToolStripButton toolStripButtonStart;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabelIterationStatic;
        private ToolStripStatusLabel toolStripStatusLabelIterationValue;
    }
}
