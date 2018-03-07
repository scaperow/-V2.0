using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Lassalle.Flow;
using System.Xml;
using System.IO;
using System.Drawing.Drawing2D;

namespace Yqun.Client.BizUI
{
    public partial class TableFlow : UserControl
    {
        public TableFlow()
        {
            InitializeComponent();

            addFlow_Tables.CanSizeNode = false;
            addFlow_Tables.CanDrawNode = false;
            addFlow_Tables.BackColor = Color.White;
            addFlow_Tables.AutoScroll = true;
            //addFlow_Tables.DisplayHandles = false;
            //addFlow_Tables.MultiSel = false;
            addFlow_Tables.PageUnit = GraphicsUnit.Pixel;
            addFlow_Tables.DefNodeProp.Shape.Style = ShapeStyle.RoundRect;
            addFlow_Tables.DefNodeProp.FillColor = Color.LightYellow;
            addFlow_Tables.DefNodeProp.Dock = DockStyle.Top;
            addFlow_Tables.DefNodeProp.InLinkable = false;
            addFlow_Tables.DefNodeProp.DrawWidth = 2;
            addFlow_Tables.DefNodeProp.Alignment = Alignment.CenterTOP;
            addFlow_Tables.DefNodeProp.LabelEdit = false;
            addFlow_Tables.DefNodeProp.OutLinkable = false;
            addFlow_Tables.DefNodeProp.HighlightChildren = false;
            addFlow_Tables.DefLinkProp.Line.Style = LineStyle.Database;
            addFlow_Tables.DefLinkProp.DashStyle = DashStyle.Dash;
        }

        /// <summary>
        /// 字段的高度
        /// </summary>
        int fieldHeight = 24;
        public int FieldHeight
        {
            get
            {
                return fieldHeight;
            }
            set
            {
                fieldHeight = value;
            }
        }

        public virtual void AddLink(Node ToAddNode, Node FromAddNode, ArrowStyle Style, bool InLinks)
        {
            if (InLinks)
            {
                Link link = ToAddNode.InLinks.Add(FromAddNode);
                link.ArrowDst.Style = Style;
            }
            else
            {
                Link link = ToAddNode.OutLinks.Add(FromAddNode);
                link.ArrowDst.Style = Style;
            }
        }

        public virtual Node AddTable(string header, float x, float y, float cx, float cy)
        {
            Node tableNode = new Node(x, y, cx, cy, addFlow_Tables.DefNodeProp);
            tableNode.Properties["Type"].Value = "Table";
            addFlow_Tables.Nodes.Add(tableNode);
            AddHeader(tableNode, header);
            return tableNode;
        }

        public virtual Node AddHeader(Node tableNode, string headerName)
        {
            Node headerNode = new Node(0, 0, 20, fieldHeight, headerName, addFlow_Tables.DefNodeProp);
            headerNode.FillColor = Color.LightGreen;
            headerNode.DrawColor = Color.Transparent;
            headerNode.Selectable = false;
            headerNode.Logical = false;
            headerNode.LabelEdit = false;
            headerNode.Properties["Type"].Value = "Header";
            addFlow_Tables.Nodes.Add(headerNode);
            headerNode.Parent = tableNode;
            return headerNode;
        }

        public virtual Node AddField(Node tableNode, string fieldName, bool InLinkable, bool OutLinkable, Color DrawColor)
        {
            Node fieldNode = new Node(0, 0, 20, 20, fieldName, addFlow_Tables.DefNodeProp);
            fieldNode.DrawColor = DrawColor;
            fieldNode.Alignment = Alignment.LeftJustifyMIDDLE; // The field text is left justified
            fieldNode.Logical = false;
            fieldNode.InLinkable = InLinkable;
            fieldNode.OutLinkable = OutLinkable;
            fieldNode.LabelEdit = false;
            fieldNode.AutoSize = Lassalle.Flow.AutoSize.NodeToTextVert;
            fieldNode.Properties["Type"].Value = "Field";
            addFlow_Tables.Nodes.Add(fieldNode);
            fieldNode.Parent = tableNode;
            return fieldNode;
        }

        public virtual string SaveFlow()
        {
            string xml = "";

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(ms, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            this.addFlow_Tables.WriteXml(writer);

            byte[] bs = ms.ToArray();

            xml = System.Text.Encoding.UTF8.GetString(bs);

            writer.Close();
            ms.Close();
            this.addFlow_Tables.SetChangedFlag(false);

            return xml;
        }

        public virtual Node GetINlinksNode(Node ToAddNode) 
        {
            if (ToAddNode.InLinks.Count == 0) 
            {
                return null;
            }

            return ToAddNode.InLinks[0].Org;
        }

        public virtual void Clear() 
        {
            addFlow_Tables.Nodes.Clear();
        }

        public void Undo()
        {
            addFlow_Tables.Undo();
        }

        public void Redo()
        {
            addFlow_Tables.Redo();
        }

        private void addFlow_Tables_AfterAddLink(object sender, AfterAddLinkEventArgs e)
        {
            e.Link.DrawWidth = 2;
            e.Link.ArrowDst.Style = ArrowStyle.OpenedArrow;
            e.Link.ArrowOrg.Style = ArrowStyle.None;
        }

        public bool IsTableNode(Node node)
        {
            return ((string)(node.Properties["Type"].Value) == "Table");
        }

        public bool IsField(Node node)
        {
            return ((string)(node.Properties["Type"].Value) == "Field");
        }

        public bool IsHeader(Node node)
        {
            return ((string)(node.Properties["Type"].Value) == "Header");
        }

        public void LoadFromXml(string xml)
        {
            addFlow_Tables.ResetUndoRedo();
            addFlow_Tables.Nodes.Clear();
            byte[] bytes = Encoding.UTF8.GetBytes(xml);
            MemoryStream Stream = new MemoryStream(bytes);
            XmlTextReader reader = new XmlTextReader(Stream);
            reader.WhitespaceHandling = System.Xml.WhitespaceHandling.None;
            addFlow_Tables.ReadXml(reader);
            reader.Close();
            addFlow_Tables.SetChangedFlag(false);
        }

        public string SaveAsXml()
        {
            MemoryStream Stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(Stream,null);
            writer.Formatting = Formatting.Indented;
            addFlow_Tables.WriteXml(writer);
            writer.Close();
            addFlow_Tables.SetChangedFlag(false);

            return Encoding.UTF8.GetString(Stream.ToArray());
        }

        private void addFlow_Tables_WriteXMLNodeExtraData(object sender, WriteXMLNodeExtraDataEventArgs e)
        {
            string type = (string)e.Node.Properties["Type"].Value;
            string name = (string)e.Node.Properties["Name"].Value;
            string text = (string)e.Node.Properties["Text"].Value;
            string disptype = (string)e.Node.Properties["DispType"].Value;
            if (type != null && type.Length > 0)
                e.Writer.WriteElementString("Type", type);
            if (name != null && name.Length > 0)
                e.Writer.WriteElementString("Name", name);
            if (text != null && text.Length > 0)
                e.Writer.WriteElementString("Text", text);
            if (disptype != null && disptype.Length > 0)
                e.Writer.WriteElementString("DispType", disptype);
        }

        private void addFlow_Tables_ReadXMLNodeExtraData(object sender, ReadXMLNodeExtraDataEventArgs e)
        {
            if (e.Reader.Name == "Type")
                e.Node.Properties["Type"].Value = e.Reader.ReadElementContentAsString();
            else if (e.Reader.Name == "Name")
                e.Node.Properties["Name"].Value = e.Reader.ReadElementContentAsString();
            else if (e.Reader.Name == "Text")
                e.Node.Properties["Text"].Value = e.Reader.ReadElementContentAsString();
            else if (e.Reader.Name == "DispType")
                e.Node.Properties["DispType"].Value = e.Reader.ReadElementContentAsString();
        }

        private void addFlow_Tables_AfterResize(object sender, EventArgs e)
        {
            Item item = addFlow_Tables.SelectedItem;
            if (item is Node)
            {
                Node node = (Node)item;
                if (IsTableNode(node))
                {
                    addFlow_Tables.AddToLastAction();
                    if (node.Rect.Height < node.Children.Count * fieldHeight)
                    {
                        RectangleF rect = new RectangleF(node.Rect.Location, node.Rect.Size);
                        rect.Height = node.Children.Count * fieldHeight + fieldHeight / 2;
                        node.Rect = rect;
                    }

                    addFlow_Tables.EndAction();
                }
            }
        }

        private void addFlow_Tables_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (addFlow_Tables.ContextMenu != null)
                    addFlow_Tables.ContextMenu.Dispose();
                if (addFlow_Tables.PointedItem != null)
                {
                    if (addFlow_Tables.PointedItem is Lassalle.Flow.Node)
                    {
                        Node node = (Node)addFlow_Tables.PointedItem;
                        if (IsTableNode(node))
                        {
                            addFlow_Tables.ContextMenu = new ContextMenu();

                            EventHandler eh1 = new EventHandler(this.OnClickDeleteTable);
                            MenuItem menuItemDeleteTable = new MenuItem("删除数据表", eh1);
                            addFlow_Tables.ContextMenu.MenuItems.Add(menuItemDeleteTable);

                            addFlow_Tables.ContextMenu.Show(this, new System.Drawing.Point(e.X, e.Y));
                        }
                    }
                    else if (addFlow_Tables.PointedItem is Lassalle.Flow.Link)
                    {
                        addFlow_Tables.ContextMenu = new ContextMenu();

                        EventHandler eh2 = new EventHandler(this.OnClickDeleteRelation);
                        MenuItem menuItemDeleteRelation = new MenuItem("删除关系", eh2);
                        addFlow_Tables.ContextMenu.MenuItems.Add(menuItemDeleteRelation);

                        addFlow_Tables.ContextMenu.Show(this, new System.Drawing.Point(e.X, e.Y));
                    }
                }
            }
        }

        private void OnClickDeleteTable(object sender, System.EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("确定要删除这个数据表吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                Lassalle.Flow.Node node = addFlow_Tables.PointedItem as Lassalle.Flow.Node;
                node.Remove();
            }
        }

        private void OnClickDeleteRelation(object sender, System.EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("确定要删除这个关系吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                Lassalle.Flow.Link link = addFlow_Tables.PointedItem as Lassalle.Flow.Link;
                link.Remove();
            }
        }
    }
}
