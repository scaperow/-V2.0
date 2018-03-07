using System;
using System.Windows.Forms;
using ReportCommon;

namespace ReportComponents
{
    public partial class SlashEditor : Form
    {
        Report report;
        GridElement Element;
        String Index;
        public SlashEditor(Report report, String Index, GridElement Element)
        {
            InitializeComponent();

            this.report = report;
            this.Element = Element;
            this.Index = Index;
        }

        public GridElement ReportElement
        {
            get
            {
                return this.Element;
            }
        }

        private void SlashEditor_Load(object sender, EventArgs e)
        {
            if (Element != null && Element.Value is Slash)
            {
                Slash Slash = Element.Value as Slash;
                tBoxSlashText.Text = Slash.Text;
                tBoxSlashText.SelectAll();
                tBoxSlashText.Focus();

                rButton_Clockwise.Checked = (Slash.RotationStyle == RotationStyle.Clockwise);
                rButton_Counterclockwise.Checked = (Slash.RotationStyle == RotationStyle.Counterclockwise);
            }
        }

        private void Button_Ok_Click(object sender, EventArgs e)
        {
            Slash Slash = null;
            if (Element == null || !(Element.Value is Slash))
            {
                Slash = new Slash();
            }
            else
            {
                Slash = Element.Value as Slash;
            }

            if (rButton_Clockwise.Checked)
            {
                Slash.RotationStyle = RotationStyle.Clockwise;
            }
            else
            {
                Slash.RotationStyle = RotationStyle.Counterclockwise;
            }

            Slash.Text = tBoxSlashText.Text;

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
