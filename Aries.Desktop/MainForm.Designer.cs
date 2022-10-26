namespace Aries.Desktop
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Processes");
            this.treeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.reportTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.reportButton = new System.Windows.Forms.ToolStripButton();
            this.runButton = new System.Windows.Forms.ToolStripButton();
            this.configTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.configButton = new System.Windows.Forms.ToolStripButton();
            this.outerPanel = new System.Windows.Forms.Panel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.processorTree = new System.Windows.Forms.TreeView();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.treeMenu.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.outerPanel.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeMenu
            // 
            this.treeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.moveUpToolStripMenuItem,
            this.moveDownToolStripMenuItem});
            this.treeMenu.Name = "treeMenu";
            this.treeMenu.Size = new System.Drawing.Size(139, 92);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.addToolStripMenuItem.Text = "&Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.removeToolStripMenuItem.Text = "&Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // moveUpToolStripMenuItem
            // 
            this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
            this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.moveUpToolStripMenuItem.Text = "Move &Up";
            this.moveUpToolStripMenuItem.Click += new System.EventHandler(this.moveUpToolStripMenuItem_Click);
            // 
            // moveDownToolStripMenuItem
            // 
            this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
            this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.moveDownToolStripMenuItem.Text = "Move &Down";
            this.moveDownToolStripMenuItem.Click += new System.EventHandler(this.moveDownToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveButton,
            this.reportTextBox,
            this.reportButton,
            this.runButton,
            this.configTextBox,
            this.configButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1228, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // saveButton
            // 
            this.saveButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveButton.Image = ((System.Drawing.Image)(resources.GetObject("saveButton.Image")));
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(35, 22);
            this.saveButton.Text = "&Save";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // reportTextBox
            // 
            this.reportTextBox.Enabled = false;
            this.reportTextBox.Name = "reportTextBox";
            this.reportTextBox.Size = new System.Drawing.Size(400, 25);
            this.reportTextBox.Text = "D:\\projects\\Aries\\sample.xml";
            // 
            // reportButton
            // 
            this.reportButton.Image = global::Aries.Desktop.Properties.Resources.xml_9_64;
            this.reportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reportButton.Name = "reportButton";
            this.reportButton.Size = new System.Drawing.Size(136, 22);
            this.reportButton.Text = "Accounting Report...";
            this.reportButton.Click += new System.EventHandler(this.reportButton_Click);
            // 
            // runButton
            // 
            this.runButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.runButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.runButton.Image = ((System.Drawing.Image)(resources.GetObject("runButton.Image")));
            this.runButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(32, 22);
            this.runButton.Text = "&Run";
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // configTextBox
            // 
            this.configTextBox.Enabled = false;
            this.configTextBox.Name = "configTextBox";
            this.configTextBox.Size = new System.Drawing.Size(400, 25);
            this.configTextBox.Text = "D:\\projects\\Aries\\config.json";
            // 
            // configButton
            // 
            this.configButton.Image = global::Aries.Desktop.Properties.Resources.json_1_64;
            this.configButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.configButton.Name = "configButton";
            this.configButton.Size = new System.Drawing.Size(93, 22);
            this.configButton.Text = "Config File...";
            this.configButton.Click += new System.EventHandler(this.configButton_Click);
            // 
            // outerPanel
            // 
            this.outerPanel.Controls.Add(this.mainPanel);
            this.outerPanel.Controls.Add(this.rightPanel);
            this.outerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outerPanel.Location = new System.Drawing.Point(0, 25);
            this.outerPanel.Name = "outerPanel";
            this.outerPanel.Size = new System.Drawing.Size(1228, 651);
            this.outerPanel.TabIndex = 11;
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(718, 651);
            this.mainPanel.TabIndex = 13;
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.processorTree);
            this.rightPanel.Controls.Add(this.propertyGrid1);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightPanel.Location = new System.Drawing.Point(718, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(510, 651);
            this.rightPanel.TabIndex = 14;
            // 
            // processorTree
            // 
            this.processorTree.ContextMenuStrip = this.treeMenu;
            this.processorTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processorTree.Location = new System.Drawing.Point(0, 0);
            this.processorTree.Name = "processorTree";
            treeNode1.Name = "RootNode";
            treeNode1.Text = "Processes";
            this.processorTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.processorTree.Size = new System.Drawing.Size(510, 351);
            this.processorTree.TabIndex = 6;
            this.processorTree.DoubleClick += new System.EventHandler(this.processorTree_DoubleClick);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 351);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(510, 300);
            this.propertyGrid1.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 676);
            this.Controls.Add(this.outerPanel);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Accounint Report Information Extraction System (ARIES)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.treeMenu.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.outerPanel.ResumeLayout(false);
            this.rightPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ContextMenuStrip treeMenu;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem removeToolStripMenuItem;
        private ToolStripMenuItem moveUpToolStripMenuItem;
        private ToolStripMenuItem moveDownToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripButton saveButton;
        private ToolStripTextBox reportTextBox;
        private ToolStripButton reportButton;
        private ToolStripButton runButton;
        private ToolStripTextBox configTextBox;
        private Panel outerPanel;
        private Panel mainPanel;
        private Panel rightPanel;
        private TreeView processorTree;
        private PropertyGrid propertyGrid1;
        private ToolStripButton configButton;
    }
}