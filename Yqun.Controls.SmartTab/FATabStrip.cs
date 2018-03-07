using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using Yqun.Client.Controls.Tab.Design;
using System.Collections;
using Yqun.Client.Controls.Tab.BaseClasses;

namespace Yqun.Client.Controls.Tab
{
    [Designer(typeof(FATabStripDesigner))]
    [DefaultEvent("TabStripItemSelectionChanged")]
    [DefaultProperty("Items")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(SmartTabControl))]
    public class SmartTabControl : BaseStyledPanel, ISupportInitialize
    {
        #region Static Fields

        internal static int PreferredWidth = 350;
        internal static int PreferredHeight = 200;
        
        #endregion

        #region Constants

        private const int DEF_HEADER_HEIGHT = 19;
        private int DEF_GLYPH_INDENT = 10;
        private int DEF_START_POS = 10;
        private const int DEF_GLYPH_WIDTH = 40;
        
        #endregion

        #region Fields

        private Rectangle stripButtonRect = Rectangle.Empty;
        private FATabStripItem selectedItem = null;
        private ContextMenuStrip menu = null;
        private FATabStripMenuGlyph menuGlyph = null;
        private FATabStripCloseButton closeButton = null;
        private FATabStripItemCollection items;
        private StringFormat sf = null;

        private bool alwaysShowClose = true;
        private bool isIniting = false;
        private bool alwaysShowMenuGlyph = true;
        private bool menuOpen = false;

        public event TabStripItemClosingHandler TabStripItemClosing;
        public event TabStripItemChangedHandler TabStripItemSelectionChanged;
        public event HandledEventHandler MenuItemsLoading;
        public event EventHandler MenuItemsLoaded;
        public event EventHandler TabStripItemClosed;

        #endregion

        #region Methods

        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);
            UpdateLayout();
            Invalidate();
        }

        private bool AllowDraw(FATabStripItem item)
        {
            bool result = true;

            if (RightToLeft == RightToLeft.No)
            {
                if (item.StripRect.Right >= stripButtonRect.Width)
                    result = false;
            }
            else
            {
                if (item.StripRect.Left <= stripButtonRect.Left)
                    return false;
            }

            return result;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            SetDefaultSelected();
            Rectangle borderRc = base.ClientRectangle;
            borderRc.Width--;
            borderRc.Height--;

            if (RightToLeft == RightToLeft.No)
            {
                DEF_START_POS = 10;
            }
            else
            {
                DEF_START_POS = stripButtonRect.Right;
            }

            e.Graphics.DrawRectangle(SystemPens.ControlDark, borderRc);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            RectangleF selectedButton = Rectangle.Empty;

            #region Draw Pages

            for (int i = 0; i < this.Items.Count; i++)
            {
                FATabStripItem currentItem = Items[i];
                if (!currentItem.Visible && !DesignMode)
                    continue;

                OnCalcTabPage(e.Graphics, currentItem);
                currentItem.IsDrawn = false;

                if(!AllowDraw(currentItem))
                    continue;

                OnDrawTabPage(e.Graphics, currentItem);
            }

            #endregion

            #region Draw UnderPage Line

            if (RightToLeft == RightToLeft.No)
            {
                if (Items.DrawnCount == 0 || Items.VisibleCount == 0)
                {
                    e.Graphics.DrawLine(SystemPens.ControlDark, new Point(0, DEF_HEADER_HEIGHT), new Point(ClientRectangle.Width, DEF_HEADER_HEIGHT));
                }
                else if (SelectedItem != null && SelectedItem.IsDrawn)
                {
                    Point end = new Point((int)SelectedItem.StripRect.Left - 9, DEF_HEADER_HEIGHT);
                    e.Graphics.DrawLine(SystemPens.ControlDark, new Point(0, DEF_HEADER_HEIGHT), end);
                    end.X += (int)SelectedItem.StripRect.Width + 10;
                    e.Graphics.DrawLine(SystemPens.ControlDark, end, new Point(ClientRectangle.Width, DEF_HEADER_HEIGHT));
                }
            }
            else
            {
                if (Items.DrawnCount == 0 || Items.VisibleCount == 0)
                {
                    e.Graphics.DrawLine(SystemPens.ControlDark, new Point(0, DEF_HEADER_HEIGHT), new Point(ClientRectangle.Width, DEF_HEADER_HEIGHT));
                }
                else if (SelectedItem != null && SelectedItem.IsDrawn)
                {
                    Point end = new Point((int)SelectedItem.StripRect.Left, DEF_HEADER_HEIGHT);
                    e.Graphics.DrawLine(SystemPens.ControlDark, new Point(0, DEF_HEADER_HEIGHT), end);
                    end.X += (int)SelectedItem.StripRect.Width + 20;
                    e.Graphics.DrawLine(SystemPens.ControlDark, end, new Point(ClientRectangle.Width, DEF_HEADER_HEIGHT));
                }
            }

            #endregion

            #region Draw Menu and Close Glyphs

            if (AlwaysShowMenuGlyph || Items.DrawnCount > Items.VisibleCount)
                menuGlyph.DrawGlyph(e.Graphics);

            if (AlwaysShowClose || (SelectedItem != null && SelectedItem.CanClose))
                closeButton.DrawCross(e.Graphics);

            #endregion
        }

        public void AddTab(FATabStripItem tabItem)
        {
            Items.Add(tabItem);
            tabItem.Dock = DockStyle.Fill;
        }

        public void RemoveTab(FATabStripItem tabItem)
        {
            int tabIndex = Items.IndexOf(tabItem);

            if (tabIndex >= 0)
            {
                UnSelectItem(tabItem);
                Items.Remove(tabItem);
            }

            if (Items.Count > 0)
            {
                if (RightToLeft == RightToLeft.No)
                {
                    if (Items[tabIndex - 1] != null)
                    {
                        SelectedItem = Items[tabIndex - 1];
                    }
                    else
                    {
                        SelectedItem = Items.FirstVisible;
                    }
                }
                else
                {
                    if (Items[tabIndex + 1] != null)
                    {
                        SelectedItem = Items[tabIndex + 1];
                    }
                    else
                    {
                        SelectedItem = Items.LastVisible;
                    }
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button != MouseButtons.Left)
                return;

            if (menuGlyph.Rect.Contains(e.Location))
            {
                HandledEventArgs args = new HandledEventArgs(false);
                OnMenuItemsLoading(args);
                if (!args.Handled)
                    OnMenuItemsLoad(EventArgs.Empty);

                OnMenuShow();
            }
            else if (closeButton.Rect.Contains(e.Location))
            {
                if (SelectedItem != null)
                {
                    TabStripItemClosingEventArgs args = new TabStripItemClosingEventArgs(SelectedItem);
                    OnTabStripItemClosing(args);
                    if (!args.Cancel && SelectedItem.CanClose)
                    {
                        RemoveTab(SelectedItem);
                        OnTabStripItemClosed(EventArgs.Empty);
                    }
                }
            }
            else
            {
                FATabStripItem item = GetTabItemByPoint(e.Location);
                if (item != null)
                    SelectedItem = item;
            }

            Invalidate();
        }

        public FATabStripItem GetTabItemByPoint(Point pt)
        {
            FATabStripItem item = null;

            for (int i = 0; i < Items.Count; i++)
            {
                FATabStripItem current = Items[i];
                if (current.StripRect.Contains(pt))
                {
                    item = current;
                }
            }

            return item;
        }

        protected internal virtual void OnTabStripItemClosing(TabStripItemClosingEventArgs e)
        {
            if (TabStripItemClosing != null)
                TabStripItemClosing(e);
        }

        protected internal virtual void OnTabStripItemClosed(EventArgs e)
        {
            if (TabStripItemClosed != null)
                TabStripItemClosed(this, e);
        }

        private void SetDefaultSelected()
        {
            if (selectedItem == null && Items.Count > 0)
                SelectedItem = Items[0];

            for (int i = 0; i < this.Items.Count; i++)
            {
                FATabStripItem itm = Items[i];
                itm.Dock = DockStyle.Fill;
            }
        }

        private void UnSelectAll()
        {
            for (int i = 0; i < this.Items.Count; i++)
            {
                FATabStripItem item = this.Items[i];
                UnSelectItem(item);
            }
        }

        internal void UnDrawAll()
        {
            for (int i = 0; i < this.Items.Count; i++)
            {
                this.Items[i].IsDrawn = false;
            }
        }

        internal void SelectItem(FATabStripItem tabItem)
        {
            tabItem.Dock = DockStyle.Fill;
            tabItem.Selected = true;
        }

        internal void UnSelectItem(FATabStripItem tabItem)
        {
            tabItem.Selected = false;
        }

        protected virtual void OnMenuItemsLoading(HandledEventArgs e)
        {
            if (MenuItemsLoading != null)
                MenuItemsLoading(this, e);
        }

        protected virtual void OnMenuShow()
        {
            if (menu.Visible == false && menu.Items.Count > 0)
            {
                if (RightToLeft == RightToLeft.No)
                {
                    menu.Show(this, new Point(menuGlyph.Rect.Left, menuGlyph.Rect.Bottom));
                }
                else
                {
                    menu.Show(this, new Point(menuGlyph.Rect.Right, menuGlyph.Rect.Bottom));
                }

                menuOpen = true;
            }
        }

        protected virtual void OnMenuItemsLoaded(EventArgs e)
        {
            if (MenuItemsLoaded != null)
                MenuItemsLoaded(this, e);
        }

        protected virtual void OnMenuItemsLoad(EventArgs e)
        {
            menu.RightToLeft = this.RightToLeft;
            menu.Items.Clear();
            
            for (int i = 0; i < Items.Count; i++)
            {
                FATabStripItem item = this.Items[i];
                if (!item.Visible)
                    continue;

                ToolStripMenuItem tItem = new ToolStripMenuItem(item.Title);
                tItem.Tag = item;
                menu.Items.Add(tItem);
            }

            OnMenuItemsLoaded(EventArgs.Empty);
        }

        private void OnMenuItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            FATabStripItem clickedItem = (FATabStripItem)e.ClickedItem.Tag;
            SelectedItem = clickedItem;
        }

        private void OnMenuVisibleChanged(object sender, EventArgs e)
        {
            if (menu.Visible == false)
            {
                menuOpen = false;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (menuGlyph.Rect.Contains(e.Location))
            {
                menuGlyph.IsMouseOver = true;
                this.Invalidate(menuGlyph.Rect);
            }
            else
            {
                if (menuGlyph.IsMouseOver != false && !menuOpen)
                {
                    menuGlyph.IsMouseOver = false;
                    this.Invalidate(menuGlyph.Rect);
                }
            }

            if (closeButton.Rect.Contains(e.Location))
            {
                closeButton.IsMouseOver = true;
                this.Invalidate(closeButton.Rect);
            }
            else
            {
                if (closeButton.IsMouseOver != false)
                {
                    closeButton.IsMouseOver = false;
                    this.Invalidate(closeButton.Rect);
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            menuGlyph.IsMouseOver = false;
            this.Invalidate(menuGlyph.Rect);

            closeButton.IsMouseOver = false;
            this.Invalidate(closeButton.Rect);
        }

        private void OnCalcTabPage(Graphics g, FATabStripItem currentItem)
        {
            Font currentFont = this.Font;
            if (currentItem == SelectedItem)
                currentFont = new Font(this.Font, FontStyle.Bold);

            SizeF textSize = g.MeasureString(currentItem.Title, currentFont, new SizeF(200, 10), sf);
            textSize.Width += 20;

            if (RightToLeft == RightToLeft.No)
            {
                RectangleF buttonRect = new RectangleF(DEF_START_POS, 3, textSize.Width, 17);
                currentItem.StripRect = buttonRect;
                DEF_START_POS += (int)textSize.Width;
            }
            else
            {
                RectangleF buttonRect = new RectangleF(DEF_START_POS - textSize.Width + 1, 3, textSize.Width - 1, 17);
                currentItem.StripRect = buttonRect;
                DEF_START_POS -= (int)textSize.Width;
            }
        }

        private void OnDrawTabPage(Graphics g, FATabStripItem currentItem)
        {
            bool isFirstTab = Items.IndexOf(currentItem) == 0;
            Font currentFont = this.Font;

            if (currentItem == SelectedItem)
                currentFont = new Font(this.Font, FontStyle.Bold);

            SizeF textSize = g.MeasureString(currentItem.Title, currentFont, new SizeF(200, 10), sf);
            textSize.Width += 20;
            RectangleF buttonRect = currentItem.StripRect;

            GraphicsPath path = new GraphicsPath();

            int mtop = 3;

            #region Draw Not Right-To-Left Tab

            if (RightToLeft == RightToLeft.No)
            {
                if (currentItem == SelectedItem || isFirstTab)
                {
                    path.AddLine(buttonRect.Left - 10, buttonRect.Bottom - 1, buttonRect.Left + (buttonRect.Height / 2) - 4, mtop + 4);
                }
                else
                {
                    path.AddLine(buttonRect.Left, buttonRect.Bottom - 1, buttonRect.Left, buttonRect.Bottom - (buttonRect.Height / 2) - 2);
                    path.AddLine(buttonRect.Left, buttonRect.Bottom - (buttonRect.Height / 2) - 3, buttonRect.Left + (buttonRect.Height / 2) - 4, mtop + 3);
                }

                path.AddLine(buttonRect.Left + (buttonRect.Height / 2) + 2, mtop, buttonRect.Right - 3, mtop);
                path.AddLine(buttonRect.Right, mtop + 2, buttonRect.Right, buttonRect.Bottom - 1);
                path.AddLine(buttonRect.Right - 4, buttonRect.Bottom - 1, buttonRect.Left, buttonRect.Bottom - 1);
                path.CloseFigure();

                LinearGradientBrush brush = null;
                if (currentItem == SelectedItem)
                {
                    brush = new LinearGradientBrush(buttonRect, SystemColors.ControlLightLight, SystemColors.Window, LinearGradientMode.Vertical);
                }
                else
                {
                    brush = new LinearGradientBrush(buttonRect, SystemColors.ControlLightLight, SystemColors.Control, LinearGradientMode.Vertical);
                }
                g.FillPath(brush, path);
                g.DrawPath(SystemPens.ControlDark, path);

                if (currentItem == SelectedItem)
                {
                    g.DrawLine(new Pen(brush), buttonRect.Left - 9, buttonRect.Height + 2, buttonRect.Left + buttonRect.Width - 1, buttonRect.Height + 2);
                }

                PointF textLoc = new PointF(buttonRect.Left + buttonRect.Height - 4, buttonRect.Top + (buttonRect.Height / 2) - (textSize.Height / 2) - 3);
                RectangleF textRect = buttonRect;
                textRect.Location = textLoc;
                textRect.Width = (float)buttonRect.Width - (textRect.Left - buttonRect.Left) - 4;
                textRect.Height = textSize.Height + currentFont.Size / 2;

                if (currentItem == SelectedItem)
                {
                    //textRect.Y -= 2;
                    g.DrawString(currentItem.Title, currentFont, new SolidBrush(this.ForeColor), textRect, sf);
                }
                else
                {
                    g.DrawString(currentItem.Title, currentFont, new SolidBrush(this.ForeColor), textRect, sf);
                }
            }

            #endregion

            #region Draw Right-To-Left Tab

            if (RightToLeft == RightToLeft.Yes)
            {
                if (currentItem == SelectedItem || isFirstTab)
                {
                    path.AddLine(buttonRect.Right + 10, buttonRect.Bottom - 1, buttonRect.Right - (buttonRect.Height / 2) + 4, mtop + 4);
                }
                else
                {
                    path.AddLine(buttonRect.Right, buttonRect.Bottom - 1, buttonRect.Right, buttonRect.Bottom - (buttonRect.Height / 2) - 2);
                    path.AddLine(buttonRect.Right, buttonRect.Bottom - (buttonRect.Height / 2) - 3, buttonRect.Right - (buttonRect.Height / 2) + 4, mtop + 3);
                }

                path.AddLine(buttonRect.Right - (buttonRect.Height / 2) - 2, mtop, buttonRect.Left + 3, mtop);
                path.AddLine(buttonRect.Left, mtop + 2, buttonRect.Left, buttonRect.Bottom - 1);
                path.AddLine(buttonRect.Left + 4, buttonRect.Bottom - 1, buttonRect.Right, buttonRect.Bottom - 1);
                path.CloseFigure();

                LinearGradientBrush brush = null;
                if (currentItem == SelectedItem)
                {
                    brush = new LinearGradientBrush(buttonRect, SystemColors.ControlLightLight, SystemColors.Window, LinearGradientMode.Vertical);
                }
                else
                {
                    brush = new LinearGradientBrush(buttonRect, SystemColors.ControlLightLight, SystemColors.Control, LinearGradientMode.Vertical);
                }

                g.FillPath(brush, path);
                g.DrawPath(SystemPens.ControlDark, path);

                if (currentItem == SelectedItem)
                {
                    g.DrawLine(new Pen(brush), buttonRect.Right + 9, buttonRect.Height + 2, buttonRect.Right - buttonRect.Width + 1, buttonRect.Height + 2);
                }

                PointF textLoc = new PointF(buttonRect.Left + 2, buttonRect.Top + (buttonRect.Height / 2) - (textSize.Height / 2) - 2);
                RectangleF textRect = buttonRect;
                textRect.Location = textLoc;
                textRect.Width = (float)buttonRect.Width - (textRect.Left - buttonRect.Left) - 10;
                textRect.Height = textSize.Height + currentFont.Size / 2;

                if (currentItem == SelectedItem)
                {
                    textRect.Y -= 1;
                    g.DrawString(currentItem.Title, currentFont, new SolidBrush(this.ForeColor), textRect, sf);
                }
                else
                {
                    g.DrawString(currentItem.Title, currentFont, new SolidBrush(this.ForeColor), textRect, sf);
                }

                g.FillRectangle(Brushes.Red, textRect);
            }

            #endregion

            currentItem.IsDrawn = true;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (isIniting)
                return;

            UpdateLayout();
        }

        private void UpdateLayout()
        {
            if (RightToLeft == RightToLeft.No)
            {
                sf.Trimming = StringTrimming.EllipsisCharacter;
                sf.FormatFlags |= StringFormatFlags.NoWrap;
                sf.FormatFlags &= StringFormatFlags.DirectionRightToLeft;

                stripButtonRect = new Rectangle(0, 0, this.ClientSize.Width - DEF_GLYPH_WIDTH - 2, 10);
                menuGlyph.Rect = new Rectangle(this.ClientSize.Width - DEF_GLYPH_WIDTH, 2, 16, 16);
                closeButton.Rect = new Rectangle(this.ClientSize.Width - 20, 2, 16, 16);
            }
            else
            {
                sf.Trimming = StringTrimming.EllipsisCharacter;
                sf.FormatFlags |= StringFormatFlags.NoWrap;
                sf.FormatFlags |= StringFormatFlags.DirectionRightToLeft;

                stripButtonRect = new Rectangle(DEF_GLYPH_WIDTH + 2, 0, this.ClientSize.Width - DEF_GLYPH_WIDTH - 15, 10);
                closeButton.Rect = new Rectangle(ClientSize.Width - DEF_GLYPH_WIDTH, 2, 16, 16);
                menuGlyph.Rect = new Rectangle(ClientSize.Width - 20, 2, 16, 16);
            }

            DockPadding.Top = DEF_HEADER_HEIGHT + 1;
            DockPadding.Bottom = 1;
            DockPadding.Right = 1;
            DockPadding.Left = 1;
        }

        protected virtual void OnTabStripItemChanged(TabStripItemChangedEventArgs e)
        {
            if (TabStripItemSelectionChanged != null)
                TabStripItemSelectionChanged(e);
        }

        private void OnCollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            FATabStripItem itm = (FATabStripItem)e.Element;

            if (e.Action == CollectionChangeAction.Add)
            {
                Controls.Add(itm);
                OnTabStripItemChanged(new TabStripItemChangedEventArgs(itm, FATabStripItemChangeTypes.Added));
            }
            else if (e.Action == CollectionChangeAction.Remove)
            {
                Controls.Remove(itm);
                OnTabStripItemChanged(new TabStripItemChangedEventArgs(itm, FATabStripItemChangeTypes.Removed));
            }
            else
            {
                OnTabStripItemChanged(new TabStripItemChangedEventArgs(itm, FATabStripItemChangeTypes.Changed));
            }

            UpdateLayout();
            Invalidate();
        }

        #endregion

        #region Ctor

        public SmartTabControl()
        {
            BeginInit();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.Selectable, true);

            items = new FATabStripItemCollection(this);
            items.CollectionChanged += new CollectionChangeEventHandler(OnCollectionChanged);
            base.Size = new Size(350, 200);

            menu = new ContextMenuStrip();
            menu.Renderer = ToolStripRenderer;
            menu.ItemClicked += new ToolStripItemClickedEventHandler(OnMenuItemClicked);
            menu.VisibleChanged += new EventHandler(OnMenuVisibleChanged);

            menuGlyph = new FATabStripMenuGlyph(ToolStripRenderer);
            closeButton = new FATabStripCloseButton(ToolStripRenderer);
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
            sf = new StringFormat();

            EndInit();

            UpdateLayout();
        }

        #endregion

        #region Props

        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public FATabStripItem SelectedItem
        {
            get 
            {
                return selectedItem;
            }
            set
            {
                if (selectedItem == value)
                    return;

                if (value == null && Items.Count > 0)
                {
                    FATabStripItem itm = Items[0];
                    if (itm.Visible)
                    {
                        selectedItem = itm;
                        selectedItem.Selected = true;
                        selectedItem.Dock = DockStyle.Fill;
                    }
                }
                else
                {
                    selectedItem = value;
                }

                foreach (FATabStripItem itm in Items)
                {
                    if (itm == selectedItem)
                    {
                        SelectItem(itm);
                        itm.Dock = DockStyle.Fill;
                        itm.Show();
                    }
                    else
                    {
                        UnSelectItem(itm);
                        itm.Hide();
                    }
                }

                SelectItem(selectedItem);
                Invalidate();

                if (!selectedItem.IsDrawn)
                {
                    Items.MoveTo(0, selectedItem);
                    Invalidate();
                }

                OnTabStripItemChanged(new TabStripItemChangedEventArgs(selectedItem, FATabStripItemChangeTypes.SelectionChanged));
            }
        }

        [DefaultValue(typeof(Size), "350,200")]
        public new Size Size
        {
            get { return base.Size; }
            set
            {
                if (base.Size == value)
                    return;

                base.Size = value;
                UpdateLayout();
            }
        }

        [DefaultValue(true)]
        public bool AlwaysShowMenuGlyph
        {
            get { return alwaysShowMenuGlyph; }
            set
            {
                if (alwaysShowMenuGlyph == value)
                    return;

                alwaysShowMenuGlyph = value;
                Invalidate();
            }
        }

        [DefaultValue(true)]
        public bool AlwaysShowClose
        {
            get { return alwaysShowClose; }
            set
            {
                if (alwaysShowClose == value)
                    return;  

                alwaysShowClose = value;
                Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public FATabStripItemCollection Items
        {
            get { return items; }
        }

        /// <summary>
        /// DesignerSerializationVisibility
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Control.ControlCollection Controls
        {
            get { return base.Controls; }
        }

        #endregion

        #region ShouldSerialize

        public bool ShouldSerializeSelectedItem()
        {
            return true;
        }

        public bool ShouldSerializeItems()
        {
            return items.Count > 0;
        }

        public bool ShouldSerializeFont()
        {
            return Font.Name != "Tahoma" && Font.Size != 8.25f && Font.Style != FontStyle.Regular;
        }

        public new void ResetFont()
        {
            Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
        }

        #endregion

        #region ISupportInitialize Members

        public void BeginInit()
        {
            isIniting = true;
        }

        public void EndInit()
        {
            isIniting = false;
        }

        #endregion

    }
}
