namespace EasyLeveling
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.sett_btn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Name_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CKO_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gsi8_chbox = new System.Windows.Forms.CheckBox();
            this.gsi16_chbox = new System.Windows.Forms.CheckBox();
            this.credo_chbox = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.sko_chbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // sett_btn
            // 
            this.sett_btn.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.sett_btn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sett_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.HotTrack;
            this.sett_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sett_btn.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sett_btn.Location = new System.Drawing.Point(229, 897);
            this.sett_btn.Margin = new System.Windows.Forms.Padding(4);
            this.sett_btn.Name = "sett_btn";
            this.sett_btn.Size = new System.Drawing.Size(150, 38);
            this.sett_btn.TabIndex = 11;
            this.sett_btn.Text = "Применить";
            this.sett_btn.UseVisualStyleBackColor = false;
            this.sett_btn.Click += new System.EventHandler(this.sett_btn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Name_Column,
            this.CKO_Column});
            this.dataGridView1.Location = new System.Drawing.Point(10, 139);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(382, 740);
            this.dataGridView1.TabIndex = 12;
            // 
            // Name_Column
            // 
            this.Name_Column.HeaderText = "Название Точки";
            this.Name_Column.MinimumWidth = 8;
            this.Name_Column.Name = "Name_Column";
            this.Name_Column.ReadOnly = true;
            this.Name_Column.Width = 150;
            // 
            // CKO_Column
            // 
            this.CKO_Column.HeaderText = "Дополнительные осадки в мм";
            this.CKO_Column.MinimumWidth = 8;
            this.CKO_Column.Name = "CKO_Column";
            this.CKO_Column.Width = 150;
            // 
            // gsi8_chbox
            // 
            this.gsi8_chbox.AutoSize = true;
            this.gsi8_chbox.Location = new System.Drawing.Point(10, 18);
            this.gsi8_chbox.Name = "gsi8_chbox";
            this.gsi8_chbox.Size = new System.Drawing.Size(147, 21);
            this.gsi8_chbox.TabIndex = 13;
            this.gsi8_chbox.Text = "Сохранить в GSI-8";
            this.gsi8_chbox.UseVisualStyleBackColor = true;
            this.gsi8_chbox.CheckStateChanged += new System.EventHandler(this.gsi8_chbox_CheckStateChanged);
            // 
            // gsi16_chbox
            // 
            this.gsi16_chbox.AutoSize = true;
            this.gsi16_chbox.Location = new System.Drawing.Point(10, 47);
            this.gsi16_chbox.Name = "gsi16_chbox";
            this.gsi16_chbox.Size = new System.Drawing.Size(155, 21);
            this.gsi16_chbox.TabIndex = 14;
            this.gsi16_chbox.Text = "Сохранить в GSI-16";
            this.gsi16_chbox.UseVisualStyleBackColor = true;
            this.gsi16_chbox.CheckStateChanged += new System.EventHandler(this.gsi16_chbox_CheckStateChanged);
            // 
            // credo_chbox
            // 
            this.credo_chbox.AutoSize = true;
            this.credo_chbox.Checked = true;
            this.credo_chbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.credo_chbox.Location = new System.Drawing.Point(195, 18);
            this.credo_chbox.Name = "credo_chbox";
            this.credo_chbox.Size = new System.Drawing.Size(190, 21);
            this.credo_chbox.TabIndex = 15;
            this.credo_chbox.Text = "Создать файл для Credo";
            this.credo_chbox.UseVisualStyleBackColor = true;
            this.credo_chbox.CheckedChanged += new System.EventHandler(this.credo_chbox_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox1.Location = new System.Drawing.Point(20, 88);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ShortcutsEnabled = false;
            this.textBox1.Size = new System.Drawing.Size(345, 45);
            this.textBox1.TabIndex = 16;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "Программа будет учитывать дополнительные осадки, введенные в ручную\r\n";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // sko_chbox
            // 
            this.sko_chbox.AutoSize = true;
            this.sko_chbox.Location = new System.Drawing.Point(195, 47);
            this.sko_chbox.Name = "sko_chbox";
            this.sko_chbox.Size = new System.Drawing.Size(106, 21);
            this.sko_chbox.TabIndex = 17;
            this.sko_chbox.Text = "Ввести СКО";
            this.sko_chbox.UseVisualStyleBackColor = true;
            this.sko_chbox.CheckedChanged += new System.EventHandler(this.sko_chbox_CheckedChanged);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(393, 954);
            this.Controls.Add(this.sko_chbox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.credo_chbox);
            this.Controls.Add(this.gsi16_chbox);
            this.Controls.Add(this.gsi8_chbox);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.sett_btn);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Дополнительные настройки";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button sett_btn;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn CKO_Column;
        private System.Windows.Forms.CheckBox gsi8_chbox;
        private System.Windows.Forms.CheckBox gsi16_chbox;
        private System.Windows.Forms.CheckBox credo_chbox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox sko_chbox;
    }
}