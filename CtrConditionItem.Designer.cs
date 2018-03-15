namespace Anchors
{
    partial class CtrConditionItem
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.editValue = new System.Windows.Forms.TextBox();
            this.comboBoxComp = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxField = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // editValue
            // 
            this.editValue.Location = new System.Drawing.Point(238, 6);
            this.editValue.MaxLength = 512;
            this.editValue.Name = "editValue";
            this.editValue.Size = new System.Drawing.Size(91, 21);
            this.editValue.TabIndex = 8;
            // 
            // comboBoxComp
            // 
            this.comboBoxComp.FormattingEnabled = true;
            this.comboBoxComp.Items.AddRange(new object[] {
            "=",
            ">",
            ">=",
            "<",
            "<=",
            "<>",
            "包含",
            "为空",
            "非空"});
            this.comboBoxComp.Location = new System.Drawing.Point(160, 6);
            this.comboBoxComp.Name = "comboBoxComp";
            this.comboBoxComp.Size = new System.Drawing.Size(75, 20);
            this.comboBoxComp.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(339, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(41, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "删除";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxField
            // 
            this.comboBoxField.DropDownWidth = 280;
            this.comboBoxField.FormattingEnabled = true;
            this.comboBoxField.ItemHeight = 12;
            this.comboBoxField.Location = new System.Drawing.Point(7, 7);
            this.comboBoxField.MaxDropDownItems = 15;
            this.comboBoxField.Name = "comboBoxField";
            this.comboBoxField.Size = new System.Drawing.Size(147, 20);
            this.comboBoxField.TabIndex = 12;
            // 
            // CtrConditionItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxField);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.editValue);
            this.Controls.Add(this.comboBoxComp);
            this.Name = "CtrConditionItem";
            this.Size = new System.Drawing.Size(388, 33);
            this.Load += new System.EventHandler(this.CtrConditionItem_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CtrConditionItem_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox editValue;
        private System.Windows.Forms.ComboBox comboBoxComp;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBoxField;
    }
}
