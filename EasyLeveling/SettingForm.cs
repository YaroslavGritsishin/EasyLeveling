using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyLeveling
{
    public partial class SettingForm : Form
    {
        public Form1 form1 { get; set; }
        public SettingForm()
        {
            InitializeComponent();
            gsi16_chbox.CheckState = CheckState.Checked;
            dataGridView1.TopLeftHeaderCell.Value = "№";
            dataGridView1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach(DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            
            dataGridView1.RowsAdded += dgv_RowsAdded;
            dataGridView1.RowsRemoved += dgv_RowsDelete;
        }

        private void sett_btn_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void gsi8_chbox_CheckStateChanged(object sender, EventArgs e)
        {
            if (gsi8_chbox.CheckState == CheckState.Checked)
            {
                gsi16_chbox.CheckState = CheckState.Unchecked;
                if (form1 != null) form1.IsGSI_16 = false;
            }

            else
            {
                gsi16_chbox.CheckState = CheckState.Checked;
                if (form1 != null) form1.IsGSI_16 = true;
            }
                
        }

        private void gsi16_chbox_CheckStateChanged(object sender, EventArgs e)
        {
            if (gsi16_chbox.CheckState == CheckState.Checked)
            {
                gsi8_chbox.CheckState = CheckState.Unchecked;
                 if(form1 != null) form1.IsGSI_16 = true;
            }

            else
            {
                gsi8_chbox.CheckState = CheckState.Checked;
                form1.IsGSI_16 = false;
            }
        }
        private void dgv_RowsAdded(object sender,
    DataGridViewRowsAddedEventArgs e)
        {
            (sender as DataGridView).Rows[e.RowIndex].HeaderCell.Value =
                (e.RowIndex + 1).ToString();
        }
        private void dgv_RowsDelete(object sender,
   DataGridViewRowsRemovedEventArgs e)
        {
            if((sender as DataGridView).Rows.Count > 0)
            {
                (sender as DataGridView).Rows[e.RowIndex].HeaderCell.Value =
                (e.RowIndex + 1).ToString();
            }
            
        }

        private void credo_chbox_CheckedChanged(object sender, EventArgs e)
        {
            if (credo_chbox.CheckState == CheckState.Checked)
                form1.IsCredoCreated = true;
            else form1.IsCredoCreated = false;

        }

        private void sko_chbox_CheckedChanged(object sender, EventArgs e)
        {
            if(form1 != null)
                form1.IsImplementSKO = sko_chbox.Checked;
        }
    }
}
