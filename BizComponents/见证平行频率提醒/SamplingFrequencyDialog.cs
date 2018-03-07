using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizCommon;
using Yqun.Permissions;
using FarPoint.Win.Spread;

namespace BizComponents
{
    public partial class SamplingFrequencyDialog : Form
    {
        String Index = "9d632d77-1dbf-4a30-9708-db3e7e539b10";
        Dictionary<string, ItemFrequency> ItemFrequencys = new Dictionary<string, ItemFrequency>();
        SamplingFrequencyInfo Info;

        public SamplingFrequencyDialog()
        {
            InitializeComponent();
        }

        private void SamplingFrequencyDialog_Load(object sender, EventArgs e)
        {
            List<IndexDescriptionPair> Models = DepositoryResourceCatlog.GetModels();
            ModelList.RowCount = Models.Count;
            for(int i = 0;i < Models.Count;i++)
            {
                ModelList.Cells[i,0].Value = Models[i].Description;
                ModelList.Cells[i, 1].Value = 0;
                ModelList.Cells[i, 2].Value = 0;
                ModelList.Rows[i].Tag = Models[i].Index;
            }

            ModelList.SetActiveCell(0, 1);

            Info = DepositorySamplingFrequencyInfo.InitSamplingFrequencyInfo(Index);
            if (Info == null)
            {
                Info = new SamplingFrequencyInfo();
                Info.Index = Index;
            }

            foreach (ItemFrequency Item in Info.ItemFrequencys)
                ItemFrequencys.Add(Item.ModelIndex, Item);

            foreach (Row Row in ModelList.Rows)
            {
                if (!ItemFrequencys.ContainsKey(Row.Tag.ToString()))
                    continue;

                ItemFrequency Item = ItemFrequencys[Row.Tag.ToString()];
                ModelList.Cells[Row.Index, 1].Value = Item.JianZhengFrequency;
                ModelList.Cells[Row.Index, 2].Value = Item.PingXingFrequency;
            }
        }

        private void Button_Save_Click(object sender, EventArgs e)
        {
            foreach (Row Row in ModelList.Rows)
            {
                if (!ItemFrequencys.ContainsKey(Row.Tag.ToString()))
                {
                    ItemFrequencys.Add(Row.Tag.ToString(), new ItemFrequency());
                }

                ItemFrequency Item = ItemFrequencys[Row.Tag.ToString()];
                Item.FrequencyInfoIndex = Info.Index;
                Item.ModelIndex = Row.Tag.ToString();
                Item.ModelName = ModelList.Cells[Row.Index, 0].Text;
                Item.JianZhengFrequency = Convert.ToSingle(ModelList.Cells[Row.Index, 1].Value);
                Item.PingXingFrequency = Convert.ToSingle(ModelList.Cells[Row.Index, 2].Value);
            }

            Info.ItemFrequencys.Clear();
            Info.ItemFrequencys.AddRange(ItemFrequencys.Values);

            Boolean Result = DepositorySamplingFrequencyInfo.UpdateSamplingFrequencyInfo(Info);
            String Message = (Result? "保存设置成功！":"保存设置失败！");
            MessageBox.Show(Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
