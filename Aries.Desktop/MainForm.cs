using System.Reflection;


namespace Aries.Desktop
{
    public partial class MainForm : Form
    {
        private XDocument doc;
        private ProcessorContainer processorContainer;
        private WebBrowser webBrowser;
        private TreeNode treeRoot;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            ToolStripMenuItem[] items = new ToolStripMenuItem[UIHelper.ProcessorTypes.Count];
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new ToolStripMenuItem();
                items[i].Name = $"addMenu{i}";
                items[i].Tag = UIHelper.ProcessorTypes[i];
                items[i].Text = UIHelper.ProcessorTypes[i].Name;
                items[i].Click += AddMenu_Click;
            }

            treeRoot = processorTree.Nodes[0];

            addToolStripMenuItem.DropDownItems.AddRange(items);

            if (File.Exists(reportTextBox.Text))
            {
                doc = XDocument.Load(reportTextBox.Text);
            }

            webBrowser = new WebBrowser();
            webBrowser.Show();
            webBrowser.Visible = true;
            webBrowser.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(webBrowser);

            if (File.Exists(configTextBox.Text))
            {
                processorContainer = ProcessorSerializer.Deserialize(configTextBox.Text);
                BuildTree(processorContainer);
            }
            else
            {
                processorContainer = new ProcessorContainer();
            }

            this.InitLayout();
        }

        private void BuildTree(ProcessorContainer processorContainer)
        {
            treeRoot.Nodes.Clear();
            int id = 0;
            ProcessorNode rawNode = new ProcessorNode();
            rawNode.ProcessId = id;
            id++;

            foreach (Processor p in processorContainer)
            {
                ProcessorNode newNode = new ProcessorNode();
                newNode.Text = p.GetType().Name + "_" + p.Description;
                newNode.Processor = p;
                newNode.ProcessId = id;
                treeRoot.Nodes.Add(newNode);
                id++;
            }
        }

        private void AddMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem subAddMenu = (ToolStripMenuItem)sender;
            Type processorType = (Type)subAddMenu.Tag;
            Processor processor = (Processor)Activator.CreateInstance(processorType);

            ProcessorNode newNode = new ProcessorNode();
            newNode.Text = processorType.Name;
            newNode.Processor = processor;
            newNode.ProcessId = -1;

            TreeNode node = processorTree.SelectedNode;

            if (node == null || node.Parent == null)
            {
                treeRoot.Nodes.Add(newNode);
            }
            else
            {
                node.Parent.Nodes.Insert(node.Index + 1, newNode);
            }


            // Container
            foreach (PropertyInfo pi in processorType.GetProperties())
            {
                if (pi.PropertyType == typeof(ProcessorContainer))
                {
                    pi.SetValue(processor, new ProcessorContainer(), null);
                    ProcessorContainer pc = (ProcessorContainer)pi.GetValue(processor, null);

                    TreeNode containerNode = new TreeNode();
                    containerNode.Text = pi.Name;
                    containerNode.Tag = pc;
                    newNode.Nodes.Add(containerNode);
                }
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = processorTree.SelectedNode;
            node.Remove();
        }

        private void processorTree_DoubleClick(object sender, EventArgs e)
        {
            if(!(processorTree.SelectedNode is ProcessorNode))
            {
                return;
            }

            ProcessorNode node = (ProcessorNode)processorTree.SelectedNode;

            propertyGrid1.SelectedObject = node.Processor;

            if (node.ProcessId > 0)
            {
                string curDir = Directory.GetCurrentDirectory();
                webBrowser.Navigate($"file:///{curDir}/data/{node.ProcessId}.html");
            }

        }

        private void processorTree_Click(object sender, EventArgs e)
        {
            //if (processorTree.SelectedNode == null)
            //{
            //    return;
            //}

        }

        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = processorTree.SelectedNode;
            TreeNode parent = node.Parent;
            if (parent == null) { return; }
            int index = node.Index;
            if (index > 0)
            {
                parent.Nodes.RemoveAt(index);
                parent.Nodes.Insert(index - 1, node);
            }
        }

        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = processorTree.SelectedNode;
            TreeNode parent = node.Parent;
            if (parent == null) { return; }
            int index = node.Index;
            if (index < parent.Nodes.Count - 1)
            {
                parent.Nodes.RemoveAt(index);
                parent.Nodes.Insert(index + 1, node);
            }

        }

        private void runButton_Click(object sender, EventArgs e)
        {
            processorContainer.Clear();
            foreach (var node in treeRoot.Nodes)
            {
                if (node is ProcessorNode)
                {
                    ProcessorNode processorNode = (ProcessorNode)node;
                    processorContainer.Add(processorNode.Processor);
                }
            }
            UIHelper.Run(doc, processorContainer);
            BuildTree(processorContainer);
        }

        private void loadButton_Click(object sender, EventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            processorContainer.Clear();
            foreach (var node in treeRoot.Nodes)
            {
                if (node is ProcessorNode)
                {
                    ProcessorNode processorNode = (ProcessorNode)node;
                    processorContainer.Add(processorNode.Processor);
                }
            }
            ProcessorSerializer.Serialize(processorContainer, configTextBox.Text);
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            var result = ofd.ShowDialog();
            if(result == DialogResult.OK)
            {
                reportTextBox.Text = ofd.FileName;
            }

            doc = XDocument.Load(ofd.FileName);
        }

        private void configButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            var result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                configTextBox.Text = ofd.FileName;
            }

            if (File.Exists(configTextBox.Text))
            {
                processorContainer = ProcessorSerializer.Deserialize(configTextBox.Text);
                BuildTree(processorContainer);
            }
            else
            {
                processorContainer = new ProcessorContainer();
            }
        }
    }
}