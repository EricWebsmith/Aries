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
            //Filter processor = new Filter();
            //processor.XPath = "//page[@number='1']";
            //propertyGrid1.SelectedObject = processor;

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

            // load xml
            doc = XDocument.Load("D:/projects/Aries/sample.xml");
            // convert xml to html

            webBrowser = new WebBrowser();
            webBrowser.Show();
            webBrowser.Visible = true;
            webBrowser.Navigate(new Uri("file:///D:/projects/Aries/sample.html"));
            webBrowser.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(webBrowser);
            //this.Controls.Add(webBrowser);

            string binFile = @"D:\projects\Aries\MyFile.bin";
            if (File.Exists(binFile))
            {
                processorContainer = ProcessorSerializer.Deserialize(binFile);
                BuildTree(processorContainer);
            }
            else
            {
                processorContainer = new ProcessorContainer();
            }
            
            
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
                newNode.Text = p.GetType().Name;
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

            if(node == null)
            {
                treeRoot.Nodes.Insert(0, newNode);
            }
            else
            {
                node.Nodes.Insert(node.Index, newNode);
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
            ProcessorSerializer.Serialize(processorContainer, @"D:\projects\Aries\MyFile.bin");
        }
    }
}