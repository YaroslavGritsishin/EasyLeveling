using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyLeveling
{
    public partial class Form1 : Form
    {

        public SettingForm settingForm { get; set; }
        public OpenFileDialog OpenFile;
        public bool IsGSI_16;
        public bool IsCredoCreated;
        public bool IsImplementSKO;
        List<string> data;
        List<string> result;
        public Form1()
        {
            InitializeComponent();
            start_btn.Enabled = false;
            IsGSI_16 = true;
            IsImplementSKO = false;
            IsCredoCreated = true;
            SettingForm Form2 = new SettingForm();
            settingForm = Form2;
            settingForm.form1 = this;
            
        }

        private void импортToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile = new OpenFileDialog();
            OpenFile.Filter = "GSI | *.GSI; *.gsi";
            if(OpenFile.ShowDialog() == DialogResult.OK)
            {
               data = new List<string>();
               result = new List<string>();
               data.AddRange(File.ReadAllLines(OpenFile.FileName));
               foreach(var item in data)
                {
                    if (item.Contains("?")) continue;
                    result.Add(GET.PointName(item));
                     
                }
               settingForm.dataGridView1.Rows.Clear();
               foreach(var item in result.GroupBy(x => x).Select(y => y.Key).ToList())
               {
                 settingForm.dataGridView1.Rows.Add(item, "0.0");
               }
               start_btn.Enabled = true;
            } 
        }

        /// <summary>
        /// Запрет на ввод букв
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IsDigree(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) | e.KeyChar == '.' | e.KeyChar == Convert.ToChar(char.ConvertFromUtf32(8)))
            {
                e.Handled = false;
            }
            else e.Handled = true;
           
        }

        private async void start_btn_Click(object sender, EventArgs e)
        {
            if (data.Count == 0) 
                if(MessageBox.Show("Необходимо импортировать файл","Info", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK) return;

            start_btn.Visible = false;
            label1.Visible = true;
            start_btn.Enabled = false;
            settingForm.dataGridView1.Enabled = false;
            List<string[]> dataGrid = new List<string[]>();
            foreach(var item in settingForm.dataGridView1.Rows)
            {
                if ((item as DataGridViewRow).Cells[0].Value == null) continue;
                dataGrid.Add( new string[2]
                {
                    (item as DataGridViewRow).Cells[0].Value.ToString(),
                    (item as DataGridViewRow).Cells[1].Value.ToString() 
                });
               
            }

            Implementations implementations = new Implementations();
            if (!CONVERT.IsBFFB(data))
            {
                data = CONVERT.ToBFFB(data);
            }
            if (IsImplementSKO)
            {
                var ResultData = await Task.Run(() => implementations.MainAction(data, dataGrid, 0.2, IsGSI_16));
                implementations.SaveFile(ResultData, OpenFile, Implementations.SaveFileType.CHANGED);
                if (IsCredoCreated)
                {
                    var res = await Task.Factory.StartNew(() => ForCredo.CreateFile(ResultData, IsGSI_16));
                    implementations.SaveFile(res, OpenFile, Implementations.SaveFileType.CD31);
                }
            }
            else
            {
                data =  implementations.MainAction(data, dataGrid, IsGSI_16);
                implementations.SaveFile(data, OpenFile, Implementations.SaveFileType.CHANGED);
            }
            if(IsCredoCreated)
            {
                var res = await Task.Factory.StartNew(() => ForCredo.CreateFile(data, IsGSI_16));
                implementations.SaveFile(res, OpenFile, Implementations.SaveFileType.CD31);
            }
            if (!IsCredoCreated && !IsImplementSKO) MessageBox.Show("Функция изменения формата работает только при создании файла для CREDO или Внесения СКО", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            settingForm.dataGridView1.Enabled = true;
            settingForm.dataGridView1.Rows.Clear();
            data.Clear();
            start_btn.Enabled = true;
            start_btn.Visible = true;
            label1.Visible = false;

        }

        private void настройкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingForm.ShowDialog();
        }
    }
}
