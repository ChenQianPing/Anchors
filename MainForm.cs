
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using wfEnginer;

using MindFusion.Diagramming.WinForms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Collections.Specialized;

namespace Anchors
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
        private MindFusion.Diagramming.WinForms.FlowChart fcx;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.OpenFileDialog ofd;
		private System.Windows.Forms.SaveFileDialog sfd;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		internal System.Windows.Forms.PictureBox icon;
        private GroupBox groupBox1;
        private TextBox editID;
        private Label label4;
        private bool selfChange = false;
        private NumericUpDown editTimeout;
        private Label label7;
        private TextBox editDesc;
        private Label label6;
        private Label label8;
        private ListView editAssgn;
        private ColumnHeader ����;
        private ColumnHeader ����;
        private ColumnHeader ID;
        private Button btnDelUser;
        private Button btnAddUser;
        private Label label9;
        private wfProcessStor wfps= new wfProcessStor();
        private wfEnginer.FlowChart fw;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
        private System.Collections.Hashtable UsedActs = new Hashtable();
        private string ProcessID;
        private Button btnNew;
        private Button btnProp;
        private string UserCode = "test";
        private int tmpID = 0;
        private Box StartBox = null;
        private Button btnSaveAs;
        private Button btnPrint;
        private Box EndBox = null;
        private CheckBox checkBoxSimDept;
        private CheckBox checkBoxCurrDept;
        private GroupBox groupBoxPre;
        private TextBox editPreSQL;
        private Label label1;
        private ComboBox comboBoxPreType;
        private CheckedListBox checkedListBoxManual;
        private Label label3;
        private TextBox textBoxHint;
        private FormCondition frmCon = null;
        private ComboBox comboBoxMerge;
        private Label label5;
        private StringCollection SelectItemValue = new StringCollection();

		public MainForm()
		{

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.fcx = new MindFusion.Diagramming.WinForms.FlowChart();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxMerge = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxSimDept = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnDelUser = new System.Windows.Forms.Button();
            this.checkBoxCurrDept = new System.Windows.Forms.CheckBox();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.editAssgn = new System.Windows.Forms.ListView();
            this.���� = new System.Windows.Forms.ColumnHeader();
            this.���� = new System.Windows.Forms.ColumnHeader();
            this.ID = new System.Windows.Forms.ColumnHeader();
            this.label8 = new System.Windows.Forms.Label();
            this.editTimeout = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.editDesc = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.editID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnProp = new System.Windows.Forms.Button();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.icon = new System.Windows.Forms.PictureBox();
            this.groupBoxPre = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxPreType = new System.Windows.Forms.ComboBox();
            this.editPreSQL = new System.Windows.Forms.TextBox();
            this.checkedListBoxManual = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxHint = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icon)).BeginInit();
            this.groupBoxPre.SuspendLayout();
            this.SuspendLayout();
            // 
            // fcx
            // 
            this.fcx.ActiveMnpColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.fcx.AlignToGrid = false;
            this.fcx.AllowLinksRepeat = false;
            this.fcx.AllowRefLinks = false;
            this.fcx.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fcx.AntiAlias = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.fcx.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.fcx.ArrowSegments = ((short)(3));
            this.fcx.ArrowStyle = MindFusion.Diagramming.WinForms.ArrowStyle.Cascading;
            this.fcx.BoxFrameColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.fcx.BoxHandlesStyle = MindFusion.Diagramming.WinForms.HandlesStyle.DashFrame;
            this.fcx.DocExtents = ((System.Drawing.RectangleF)(resources.GetObject("fcx.DocExtents")));
            this.fcx.InplaceEditFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fcx.Location = new System.Drawing.Point(6, 44);
            this.fcx.Name = "fcx";
            this.fcx.SelectAfterCreate = false;
            this.fcx.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.fcx.Size = new System.Drawing.Size(556, 451);
            this.fcx.SnapToAnchor = MindFusion.Diagramming.WinForms.SnapToAnchor.OnCreateOrModify;
            this.fcx.TabIndex = 0;
            this.fcx.TableFrameColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.fcx.BoxCreated += new MindFusion.Diagramming.WinForms.BoxEvent(this.fcx_BoxCreated);
            this.fcx.BoxDeleting += new MindFusion.Diagramming.WinForms.BoxConfirmation(this.fcx_BoxDeleting);
            this.fcx.BoxDblClicked += new MindFusion.Diagramming.WinForms.BoxMouseEvent(this.fcx_BoxDblClicked);
            this.fcx.ArrowCreating += new MindFusion.Diagramming.WinForms.AttachConfirmation(this.fcx_ArrowCreating);
            this.fcx.MouseHover += new System.EventHandler(this.fcx_MouseHover);
            this.fcx.DrawMark += new MindFusion.Diagramming.WinForms.MarkCustomDraw(this.fcx_DrawMark);
            this.fcx.SelectionChanged += new MindFusion.Diagramming.WinForms.SelectionEvent(this.fcx_SelectionChanged);
            this.fcx.BoxClicked += new MindFusion.Diagramming.WinForms.BoxMouseEvent(this.fcx_BoxClicked);
            this.fcx.ArrowCreated += new MindFusion.Diagramming.WinForms.ArrowEvent(this.fcx_ArrowCreated);
            this.fcx.BoxDeleted += new MindFusion.Diagramming.WinForms.BoxEvent(this.fcx_BoxDeleted);
            // 
            // radioButton1
            // 
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(552, 8);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(53, 18);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "�ڵ�";
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.Location = new System.Drawing.Point(614, 8);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(65, 18);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.Text = "����";
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(477, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "�ڵ�����";
            // 
            // ofd
            // 
            this.ofd.Filter = "����ģ��(*.fwb)|*.fwb";
            // 
            // sfd
            // 
            this.sfd.DefaultExt = "fwb";
            this.sfd.FileName = "doc1";
            this.sfd.Filter = "����ģ��(*.fwb)|*.fwb";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(82, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "��(&O)";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(240, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "����(&S)";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.comboBoxMerge);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.checkBoxSimDept);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.btnDelUser);
            this.groupBox1.Controls.Add(this.checkBoxCurrDept);
            this.groupBox1.Controls.Add(this.btnAddUser);
            this.groupBox1.Controls.Add(this.editAssgn);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.editTimeout);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.editDesc);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.editID);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(568, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(172, 299);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "����";
            // 
            // comboBoxMerge
            // 
            this.comboBoxMerge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMerge.FormattingEnabled = true;
            this.comboBoxMerge.Items.AddRange(new object[] {
            "��",
            "��"});
            this.comboBoxMerge.Location = new System.Drawing.Point(66, 91);
            this.comboBoxMerge.Name = "comboBoxMerge";
            this.comboBoxMerge.Size = new System.Drawing.Size(98, 20);
            this.comboBoxMerge.TabIndex = 19;
            this.comboBoxMerge.SelectedIndexChanged += new System.EventHandler(this.comboBoxMerge_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "�ϲ���ʽ";
            // 
            // checkBoxSimDept
            // 
            this.checkBoxSimDept.AutoSize = true;
            this.checkBoxSimDept.Location = new System.Drawing.Point(21, 276);
            this.checkBoxSimDept.Name = "checkBoxSimDept";
            this.checkBoxSimDept.Size = new System.Drawing.Size(120, 16);
            this.checkBoxSimDept.TabIndex = 17;
            this.checkBoxSimDept.Text = "ͬ���ź��ϼ�����";
            this.checkBoxSimDept.UseVisualStyleBackColor = true;
            this.checkBoxSimDept.Visible = false;
            this.checkBoxSimDept.MouseHover += new System.EventHandler(this.checkBoxSimDept_MouseHover);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(142, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "��";
            // 
            // btnDelUser
            // 
            this.btnDelUser.Enabled = false;
            this.btnDelUser.Location = new System.Drawing.Point(121, 112);
            this.btnDelUser.Name = "btnDelUser";
            this.btnDelUser.Size = new System.Drawing.Size(42, 23);
            this.btnDelUser.TabIndex = 15;
            this.btnDelUser.Text = "ɾ��";
            this.btnDelUser.UseVisualStyleBackColor = true;
            this.btnDelUser.Click += new System.EventHandler(this.btnDelUser_Click);
            // 
            // checkBoxCurrDept
            // 
            this.checkBoxCurrDept.AutoSize = true;
            this.checkBoxCurrDept.Location = new System.Drawing.Point(22, 257);
            this.checkBoxCurrDept.Name = "checkBoxCurrDept";
            this.checkBoxCurrDept.Size = new System.Drawing.Size(72, 16);
            this.checkBoxCurrDept.TabIndex = 17;
            this.checkBoxCurrDept.Text = "ͬһ����";
            this.checkBoxCurrDept.UseVisualStyleBackColor = true;
            this.checkBoxCurrDept.Visible = false;
            this.checkBoxCurrDept.MouseHover += new System.EventHandler(this.checkBoxCurrDept_MouseHover);
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(73, 112);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(42, 23);
            this.btnAddUser.TabIndex = 14;
            this.btnAddUser.Text = "���";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // editAssgn
            // 
            this.editAssgn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.editAssgn.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.����,
            this.����,
            this.ID});
            this.editAssgn.Location = new System.Drawing.Point(8, 137);
            this.editAssgn.Name = "editAssgn";
            this.editAssgn.Size = new System.Drawing.Size(157, 116);
            this.editAssgn.TabIndex = 13;
            this.editAssgn.UseCompatibleStateImageBehavior = false;
            this.editAssgn.View = System.Windows.Forms.View.Details;
            this.editAssgn.SelectedIndexChanged += new System.EventHandler(this.editAssgn_SelectedIndexChanged);
            // 
            // ����
            // 
            this.����.Text = "����";
            this.����.Width = 76;
            // 
            // ����
            // 
            this.����.Text = "����";
            this.����.Width = 80;
            // 
            // ID
            // 
            this.ID.Text = "����";
            this.ID.Width = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 117);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "Ȩ��";
            // 
            // editTimeout
            // 
            this.editTimeout.Location = new System.Drawing.Point(66, 39);
            this.editTimeout.Name = "editTimeout";
            this.editTimeout.Size = new System.Drawing.Size(73, 21);
            this.editTimeout.TabIndex = 11;
            this.editTimeout.ValueChanged += new System.EventHandler(this.editTimeout_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "��ʱ����";
            // 
            // editDesc
            // 
            this.editDesc.Location = new System.Drawing.Point(43, 65);
            this.editDesc.MaxLength = 255;
            this.editDesc.Name = "editDesc";
            this.editDesc.Size = new System.Drawing.Size(122, 21);
            this.editDesc.TabIndex = 9;
            this.editDesc.TextChanged += new System.EventHandler(this.editDesc_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "˵��";
            // 
            // editID
            // 
            this.editID.Location = new System.Drawing.Point(44, 14);
            this.editID.Name = "editID";
            this.editID.ReadOnly = true;
            this.editID.Size = new System.Drawing.Size(122, 21);
            this.editID.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "ID";
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(4, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 17;
            this.btnNew.Text = "�½�(&N)";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnProp
            // 
            this.btnProp.Location = new System.Drawing.Point(161, 3);
            this.btnProp.Name = "btnProp";
            this.btnProp.Size = new System.Drawing.Size(75, 23);
            this.btnProp.TabIndex = 18;
            this.btnProp.Text = "����(&a)";
            this.btnProp.UseVisualStyleBackColor = true;
            this.btnProp.Click += new System.EventHandler(this.btnProp_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(319, 3);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(75, 23);
            this.btnSaveAs.TabIndex = 19;
            this.btnSaveAs.Text = "���Ϊ";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(398, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 20;
            this.btnPrint.Text = "��ӡ";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // icon
            // 
            this.icon.Image = ((System.Drawing.Image)(resources.GetObject("icon.Image")));
            this.icon.Location = new System.Drawing.Point(758, 439);
            this.icon.Name = "icon";
            this.icon.Size = new System.Drawing.Size(120, 54);
            this.icon.TabIndex = 15;
            this.icon.TabStop = false;
            this.icon.Visible = false;
            // 
            // groupBoxPre
            // 
            this.groupBoxPre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPre.Controls.Add(this.label1);
            this.groupBoxPre.Controls.Add(this.comboBoxPreType);
            this.groupBoxPre.Controls.Add(this.editPreSQL);
            this.groupBoxPre.Location = new System.Drawing.Point(567, 347);
            this.groupBoxPre.Name = "groupBoxPre";
            this.groupBoxPre.Size = new System.Drawing.Size(171, 146);
            this.groupBoxPre.TabIndex = 23;
            this.groupBoxPre.TabStop = false;
            this.groupBoxPre.Text = "ǰ������";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "SQL����";
            // 
            // comboBoxPreType
            // 
            this.comboBoxPreType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPreType.FormattingEnabled = true;
            this.comboBoxPreType.Items.AddRange(new object[] {
            "����ͨ��",
            "������ͨ��",
            "ʹ��SQL������"});
            this.comboBoxPreType.Location = new System.Drawing.Point(6, 15);
            this.comboBoxPreType.Name = "comboBoxPreType";
            this.comboBoxPreType.Size = new System.Drawing.Size(160, 20);
            this.comboBoxPreType.TabIndex = 24;
            this.comboBoxPreType.SelectedIndexChanged += new System.EventHandler(this.comboBoxPreType_SelectedIndexChanged);
            // 
            // editPreSQL
            // 
            this.editPreSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editPreSQL.Location = new System.Drawing.Point(6, 57);
            this.editPreSQL.Multiline = true;
            this.editPreSQL.Name = "editPreSQL";
            this.editPreSQL.ReadOnly = true;
            this.editPreSQL.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.editPreSQL.Size = new System.Drawing.Size(160, 86);
            this.editPreSQL.TabIndex = 23;
            this.editPreSQL.WordWrap = false;
            this.editPreSQL.MouseHover += new System.EventHandler(this.editPreSQL_MouseHover);
            this.editPreSQL.TextChanged += new System.EventHandler(this.editPreSQL_TextChanged_1);
            // 
            // checkedListBoxManual
            // 
            this.checkedListBoxManual.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxManual.FormattingEnabled = true;
            this.checkedListBoxManual.Location = new System.Drawing.Point(570, 512);
            this.checkedListBoxManual.Name = "checkedListBoxManual";
            this.checkedListBoxManual.Size = new System.Drawing.Size(166, 36);
            this.checkedListBoxManual.TabIndex = 24;
            this.checkedListBoxManual.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxManual_ItemCheck);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(572, 496);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "�ֶ�ѡ��ת��";
            // 
            // textBoxHint
            // 
            this.textBoxHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHint.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxHint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxHint.Location = new System.Drawing.Point(6, 498);
            this.textBoxHint.Multiline = true;
            this.textBoxHint.Name = "textBoxHint";
            this.textBoxHint.ReadOnly = true;
            this.textBoxHint.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxHint.Size = new System.Drawing.Size(556, 51);
            this.textBoxHint.TabIndex = 26;
            this.textBoxHint.Text = "�ڻ�ͼ�����϶�����ʼ���̶���";
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(742, 555);
            this.Controls.Add(this.textBoxHint);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkedListBoxManual);
            this.Controls.Add(this.groupBoxPre);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnProp);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.icon);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.fcx);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "���̶���";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icon)).EndInit();
            this.groupBoxPre.ResumeLayout(false);
            this.groupBoxPre.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
            bool rc = false;
            int Rep = 0;
            string userName="", password;
            DialogResult mr = DialogResult.OK;

            FormLogin frm = new FormLogin();
            while (Rep < 3 && mr == DialogResult.OK)
            {
                mr = frm.ShowDialog();
                if (mr == DialogResult.OK)
                {
                    userName = frm.UserCode;
                    password = frm.Password;
                    int st=CheckUser(userName, password);
                    if (st==0)
                    {
                        rc = true;
                        break;
                    }
                    else if (st == 1)
                    {
                        MessageBox.Show("��֤ʧ��\n��˶��û������������Ƿ���ȷ");
                    }
                    else if (st == 2)
                    {
                        MessageBox.Show("����������ʧ�ܻ��޷���֤���ݣ���˶Է���������!");
                    }
                }
                Rep++;
            }

            if (rc)
            {
                MainForm mfrm = new MainForm();
                mfrm.UserName = userName;
                Application.Run(mfrm);
            }
		}

        private static int CheckUser(string uname, string pwd)
        {
            try
            {
                string sConn = ConnStrMng.LoadFromReg(ConnStrMng.DefaultKeyName, ConnStrMng.DefaultParamName, ConnStrMng.DefaultKeyVal);
                if (sConn == "")
                    return 2;
                IDataReader dr = dbData.getDataReader(sConn, "select F_PASSWORD from T_ACCOUNTS_USER_INFO where F_USER_CODE='" + uname.Replace("'", "''") + "'");
                if (dr == null)
                    return 2;
                string spwd = "";
                if (dr.Read())
                {
                    spwd = dr.IsDBNull(0) ? "" : dr.GetString(0);
                    dbConn.DBConn = sConn;
                }
                return ((spwd == pwd) ? 0 : 1);
            }
            catch (Exception e)
            {
                return 2;
            }
        }


		AnchorPattern apat1, apat2;

		private void Form1_Load(object sender, System.EventArgs e)
		{
            //InitConn();
			fcx.BoxBrush = new MindFusion.Drawing.LinearGradientBrush(
				Color.LightGray, Color.DarkTurquoise, 30);
			//Box pb1, pb2, pb3, pb4, decb1, decb2;

			apat1 = new AnchorPattern(new AnchorPoint[] {
				new AnchorPoint(50, 0, true, true),
				new AnchorPoint(100, 50, true, true),
				new AnchorPoint(50, 100, true, true),
				new AnchorPoint(0, 50, true, true) });

			apat2 = new AnchorPattern(new AnchorPoint[] {
				new AnchorPoint(10, 0, true, false, MarkStyle.Circle, Color.RoyalBlue),
				new AnchorPoint(50, 0, true, false, MarkStyle.Circle, Color.Blue),
				new AnchorPoint(90, 0, true, false, MarkStyle.Circle, Color.Firebrick),
				new AnchorPoint(10, 100, false, true, MarkStyle.Rectangle),
				new AnchorPoint(50, 100, false, true, MarkStyle.Rectangle),
				new AnchorPoint(90, 100, false, true, MarkStyle.Rectangle),
				new AnchorPoint(0, 50, true, true, MarkStyle.Custom) });


            frmCon = new FormCondition();

            //string sConn = "Provider=SQLOLEDB;Data Source=.\\sqlexpress;Persist Security Info=True;User ID=sa;password=qqq;Initial Catalog=assetsdb";
            //string sConn = "Provider=SQLOLEDB;Data Source=192.168.0.1;Persist Security Info=True;User ID=sa;password=123456;Initial Catalog=Asset";

            ProcessID = "-1";
            //dbConn.DBConn = sConn;
            fw = new wfEnginer.FlowChart();
            fw.ID = ProcessID;
            fw.Name = "������";
            fw.Desc = "˵��:������";
            fw.UserCode = UserCode;

            //wfps.ConnectionStr = sConn;
            //fw = wfps.GetProcess(ProcessID);

            //UsedActs.Clear();
            //InitFlow(fw);
            /*            NodeProp nprop = new NodeProp();
            nprop.ID = "";
            nprop.NodeType = NodeProp.NODE_TYPE_START;
            nprop.Desc = "��ʼ����";

			pb1 = fcx.CreateBox(10, 7, 25, 18);
			pb1.Style = MindFusion.Diagramming.WinForms.BoxStyle.Ellipse;
			pb1.Text = "��ʼ";
            pb1.Tag = nprop;
            
			//pb1.IncmAnchor = EArrowAnchor.aaPattern;
			//pb1.OutgAnchor = EArrowAnchor.aaPattern;
			pb1.AnchorPattern = apat1;
            pb1.AllowIncomingArrows = false;

            nprop = new NodeProp();
            nprop.ID = "";
            nprop.NodeType = NodeProp.NODE_TYPE_NODE;
            nprop.Desc = "�������";

			pb2 = fcx.CreateBox(20, 75, 25, 18);
			pb2.Text = "node 1";
			// the line below is equivalent to the 3 property assignements for pb1
			pb2.AnchorPattern = apat2;
            pb2.Tag = nprop;

            nprop = new NodeProp();
            nprop.ID = "";
            nprop.NodeType = NodeProp.NODE_TYPE_NODE;
            nprop.Desc = "�豸�����";
            
            pb3 = fcx.CreateBox(70, 70, 25, 18);
			pb3.Text = "node 2";
			pb3.AnchorPattern=apat2;
            pb3.Tag = nprop;

            nprop = new NodeProp();
            nprop.ID = "";
            nprop.NodeType = NodeProp.NODE_TYPE_END;
            nprop.Desc = "��������";
            
            pb4 = fcx.CreateBox(80, 100, 25, 18);
			pb4.Style = MindFusion.Diagramming.WinForms.BoxStyle.Ellipse;
			pb4.Text = "����";
            pb4.Tag = nprop;
			pb4.AnchorPattern=apat1;
            pb4.AllowOutgoingArrows = false;
            pb4.FillColor = Color.Blue;


			decb1 = fcx.CreateBox(20, 35, 30, 20);
			decb1.Style = BoxStyle.Rhombus;
			decb1.Text = "check 1";
			decb1.AnchorPattern=AnchorPattern.Decision1In3Out;

			decb2 = fcx.CreateBox(70, 30, 30, 20);
			decb2.Style = BoxStyle.Rhombus;
			decb2.Text = "check 2";
			decb2.AnchorPattern=AnchorPattern.Decision2In2Out;

			Arrow arr=fcx.CreateArrow(decb1, decb2);

            */
		}

        private bool InitConn()
        {
            bool rc = false;
            /* Type dataLinksClass = Type.GetTypeFromProgID("DataLinks");
             object udl = Activator.CreateInstance(dataLinksClass);

             // ���ô�������ʹ����ģʽ����򿪡�
             dataLinksClass.InvokeMember("hWnd", BindingFlags.SetProperty, null, udl, new object[] { this.Handle.ToInt32() });

             // �����������ַ���
             object conn = dataLinksClass.InvokeMember("PromptNew", BindingFlags.InvokeMethod, null, udl, null);
            

             if (conn!=null)
             {
                 string ConnStr = (string)conn.GetType().InvokeMember("ConnectionString", BindingFlags.GetProperty, null, conn, null);
                 rc = true;
             }
             if (udl != null) Marshal.ReleaseComObject(udl);
             if (conn != null) Marshal.ReleaseComObject(conn);
            */
             return rc;
         }

         private void InitFlow(wfEnginer.FlowChart fw)
         {
             //��ʼ�ڵ�
             wfActivity act = fw.StartNode;
             int y = 10;
             int x = 10;
             Box pb1 = fcx.CreateBox(x, y, 30, 20);
             pb1.Style = MindFusion.Diagramming.WinForms.BoxStyle.Ellipse;
             pb1.Text = act.Name;
             pb1.Tag = act.ID;
             pb1.AllowIncomingArrows = false;
             StartBox = pb1;   

             UsedActs.Add(act.ID,pb1);

             for (int i = 0; i < act.ChildCount; i++)
             {
                 //if (UsedActs.IndexOf(act.ChildNode(i).Node.ID) < 0)
                 //{
                     addNextAct(act.ChildNode(i).Node, pb1, x, y+30);
                     x += 30;
                 //}
             }
         }

         private void addNextAct(wfActivity act, Box pBox, int x, int y)
         {
             if (UsedActs.ContainsKey(act.ID))
             {
                 Box pb0=(Box)UsedActs[act.ID];
                 fcx.CreateArrow(pBox, pb0);
                 return;
             }
             Box pb1 = fcx.CreateBox(x, y, 30, 20);
             //pb1.Style = MindFusion.Diagramming.WinForms.BoxStyle.Rectangle;
             if (act.ActType == wfConsts.NODE_END)
             {
                 pb1.Style = BoxStyle.Ellipse;
             }
             pb1.Text = act.Name;
             pb1.Tag = act.ID;
             UsedActs.Add(act.ID,pb1);
             if (act.ActType == wfConsts.NODE_END)
             {
                 pb1.AllowOutgoingArrows = false;
                 EndBox = pb1;
             }
             else
             {
                 pb1.AnchorPattern = apat2;
             }

             fcx.CreateArrow(pBox, pb1);



             for (int i = 0; i < act.ChildCount; i++)
             {
                     addNextAct(act.ChildNode(i).Node, pb1, x, y+30);
                     x += 50;
             }
         }

         private void radioButton1_CheckedChanged(object sender, System.EventArgs e)
         {
             fcx.BoxStyle = BoxStyle.RoundedRectangle;
         }

         private void radioButton2_CheckedChanged(object sender, System.EventArgs e)
         {
             fcx.BoxStyle = BoxStyle.Rhombus;
         }

         private void fcx_BoxCreated(object sender, MindFusion.Diagramming.WinForms.BoxEventArgs e)
         {
             if (e.Box.Style == BoxStyle.Rhombus)
                 e.Box.AnchorPattern=AnchorPattern.Decision1In3Out;
             else
                 e.Box.AnchorPattern=apat2;
             
             if (fcx.Boxes.Count < 2)
             {
                 tmpID--;                              //��ʱID
                 Box pb1 = fcx.CreateBox(10, 7, 25, 18);
                 pb1.Style = MindFusion.Diagramming.WinForms.BoxStyle.Ellipse;
                 pb1.Text = "��ʼ";
                 pb1.Tag = tmpID.ToString();
                 pb1.AllowIncomingArrows = false;
                 StartBox = pb1;
                 AddNode(pb1);

                 tmpID--;                              //��ʱID
                 pb1 = fcx.CreateBox(10, 170, 25, 18);
                 pb1.Style = MindFusion.Diagramming.WinForms.BoxStyle.Ellipse;
                 pb1.Text = "����";
                 pb1.Tag = tmpID.ToString();
                 pb1.AllowOutgoingArrows = false;
                 pb1.FillColor = Color.Blue;
                 EndBox = pb1;
                 AddNode(pb1);

                 ChangeCaption();

             }

             tmpID--;                              //��ʱID
            e.Box.Tag = tmpID.ToString();
            AddNode(e.Box);
            
         }

        private void AddNode(Box linkBox)
        {
            wfActivity act = new wfActivity();

            if (linkBox==StartBox)
                act.ActType = wfConsts.NODE_START;
            else if (linkBox == EndBox)
                act.ActType= wfConsts.NODE_END;
            else if (linkBox.Style == BoxStyle.Rhombus)
                act.ActType = wfConsts.NODE_IF_BRANCH;
            else
                act.ActType = wfConsts.NODE_NORMAL;
            act.ID = linkBox.Tag.ToString();

            act.Changed = 1;//new data


            fw.AddActivity(act);
        }

         private void button1_Click(object sender, System.EventArgs e)
         {
             /*if (ofd.ShowDialog() == DialogResult.OK)
             {
                 try
                 {
                     fcx.LoadFromFile(ofd.FileName);
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message);
                 }
             }
              * */
             FormProcessList frmlist = new FormProcessList();
             if (frmlist.ShowDialog(this) == DialogResult.OK)
             {
                 string iid = frmlist.SelectRowID;
                 if (iid!="")
                 {
                     ProcessID = iid;

                     LoadFlowDataFromDB(ProcessID);
                     ChangeCaption();
                     System.IO.MemoryStream sm = new System.IO.MemoryStream();
                     try
                     {
                         if (LoadFromDB(ProcessID, sm) > 0)
                         {
                             sm.Position = 0;
                             fcx.LoadFromStream(sm);
                             fcx.Tag = fw.ID;
                         }
                     }
                     finally
                     {
                         sm.Close();
                         sm.Dispose();
                     }

                     for (int i = 0; i < fcx.Boxes.Count; i++)
                     {
                         string sid = fcx.Boxes[i].Tag.ToString();
                         wfActivity act = fw.GetActivityByID(sid);
                         if (act != null)
                         {
                             if (act.ActType == wfConsts.NODE_START)
                                 StartBox = fcx.Boxes[i];
                             else if (act.ActType == wfConsts.NODE_END)
                                 EndBox = fcx.Boxes[i];
                         }
                         
                     }
                 }

             }
             frmlist.Close();
             frmlist.Dispose();
         }

         private void button2_Click(object sender, System.EventArgs e)
         {
             if (!CheckFlow()) return;
             //if (sfd.ShowDialog() == DialogResult.OK)
             {
                 //fcx.SaveToFile(sfd.FileName, true);
                 System.IO.MemoryStream sm = new System.IO.MemoryStream();
                 try
                 {
                     SynIDs();

                     fcx.SaveToStream(sm);

                     string sxml = GetChartXml();
                     //MessageBox.Show(sxml);
                     //return;
                     sm.Position = 0;
                     
                     SaveToDB(ProcessID, sm,sxml);
                     fw.ID = ProcessID;
                 }
                 finally
                 {
                     sm.Close();
                     sm.Dispose();
                 }

             }
         }

        private string GetChartXml()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.IO.TextWriter sw = new System.IO.StringWriter(sb);
            System.Xml.XmlTextWriter xw = new System.Xml.XmlTextWriter(sw);

            xw.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"gb2312\"");
            xw.WriteStartElement("chart");
         
            xw.WriteAttributeString("name", fw.Name);
            xw.WriteAttributeString("id", fw.ID);
            xw.WriteStartElement("Nodes");
            //д�ڵ��嵥
            writeNode(xw, StartBox);
            for (int i = 0; i < fcx.Boxes.Count; i++)
            {
                Box cb = fcx.Boxes[i];
                if (cb!= StartBox && cb !=EndBox)
                {
                    writeNode(xw, cb);
                }
            }
            writeNode(xw, EndBox);
            xw.WriteEndElement();

            //д������
            xw.WriteStartElement("Lines");
            for (int i = 0; i < fcx.Arrows.Count; i++)
            {
                writeLine(xw, fcx.Arrows[i]);
            }
            xw.WriteEndElement(); //"Lines"
            xw.WriteEndElement();
            
           
            return sb.ToString();
        }

        private void writeNode(System.Xml.XmlTextWriter xw, Box cb)
        {
            xw.WriteStartElement("Node");
            xw.WriteAttributeString("name", cb.Text);
            xw.WriteAttributeString("id", cb.Tag.ToString());
            wfActivity act = fw.GetActivityByID(cb.Tag.ToString());
            xw.WriteAttributeString("desc", act.Desc);
            xw.WriteAttributeString("ActType", act.ActType);
            xw.WriteAttributeString("left", cb.BoundingRect.Left.ToString());
            xw.WriteAttributeString("top", cb.BoundingRect.Top.ToString());
            xw.WriteAttributeString("width", cb.BoundingRect.Width.ToString());
            xw.WriteAttributeString("height", cb.BoundingRect.Height.ToString());
            xw.WriteEndElement();
        }

        private void writeLine(System.Xml.XmlTextWriter xw, Arrow cl)
        {
            xw.WriteStartElement("Line");

            Box cb = (Box)cl.Origin;
            Box cto = (Box)cl.Destination;

            xw.WriteAttributeString("fromid", cb.Tag.ToString());
            xw.WriteAttributeString("toid", cto.Tag.ToString());

            for (int i = 0; i < cl.ControlPoints.Count; i++)
            {
                xw.WriteStartElement("Point");
                xw.WriteAttributeString("x", cl.ControlPoints[i].X.ToString());
                xw.WriteAttributeString("y", cl.ControlPoints[i].Y.ToString());
                xw.WriteEndElement();
            }
            xw.WriteEndElement();

        }

        private void SynIDs()
        {
            dbData dd = new dbData();
            dd.ConnectionStr = dbConn.DBConn;
            if (int.Parse(fw.ID) < 1)
            {
                
                ProcessID = dd.GetSEQ("SEQ_WF_PROCESS");
                fcx.Tag = ProcessID;
        
                fw.ID = ProcessID;
            }

            //��ڵ�
            for (int i = 0; i < fcx.Boxes.Count; i++)
            {
                Box pb = fcx.Boxes[i];
                string cid = pb.Tag.ToString();
                if (int.Parse(cid) < 1)
                {
                    wfActivity act = fw.GetActivityByID(cid);
                    if (act != null)
                    {
                        string nid = dd.GetSEQ("SEQ_WF_ACTIVITY");
                        pb.Tag = nid;
                        act.ID = nid;
                        ReplaceRefID(cid, nid);
                    }
                }
            }
        }

        private void ReplaceRefID(string OldID, string NewID)
        {
            for (int i = 0; i < fw.Count; i++)
            {
                wfActivity act = fw.Items(i);
                for (int j = 0; j < act.SelectNodes.Count; j++)
                {
                    if (act.SelectNodes[j] == OldID)
                        act.SelectNodes[j] = NewID;
                }

            }

            for (int i=0;i<SelectItemValue.Count;i++)
                if (SelectItemValue[i] == OldID)
                    SelectItemValue[i] = NewID;

        }



        private void SaveToDB(string processID, System.IO.MemoryStream sm,string sxml)
         {
             IDbConnection conn = new System.Data.OleDb.OleDbConnection();
             conn.ConnectionString = dbConn.DBConn;
             conn.Open();
             IDbTransaction trans= conn.BeginTransaction();
            
             try
             {
                 bool rc = false;

                 //����ͼ
                 IDbCommand cmd = conn.CreateCommand();
                 cmd.CommandText = "Select F_ID from WF_PROCESS_CHART where F_ID=" + processID;
                 cmd.Transaction = trans;
                 IDataReader dr = cmd.ExecuteReader();
                 if (dr.Read())
                 {
                     rc = true;
                 }
                 dr.Close();
                 dr.Dispose();

                 dr = null;

                 IDbDataParameter pm;
                 if (rc)
                 {
                     cmd.CommandText = "Update WF_PROCESS_CHART set F_DATA = ?,F_CHANGE_DATE= ? ,F_USER_ID= ?,F_XML=? where F_ID=" + processID;
                 }
                 else
                 {
                     cmd.CommandText = "insert into WF_PROCESS_CHART (F_ID, F_DATA,F_CHANGE_DATE,F_USER_ID,F_XML) values ( ? , ? ,? ,?,?) ";
                     pm = cmd.CreateParameter();
                     pm.DbType = DbType.Int64;
                     //pm.Size = sm.Length;
                     pm.ParameterName = "@id";
                     Int64 iid = int.Parse(processID);
                     pm.Value = iid;

                     cmd.Parameters.Add(pm);
                 }

                 pm = cmd.CreateParameter();
                 pm.DbType = DbType.Binary;
                 pm.Size = (int)sm.Length;
                 pm.ParameterName = "@blobdata";
                 byte[] imageData = new Byte[sm.Length];
                 sm.Position = 0;
                 sm.Read(imageData, 0, (int)sm.Length);
                 pm.Value = imageData;

                 cmd.Parameters.Add(pm);

                 pm = cmd.CreateParameter();
                 pm.DbType = DbType.DateTime;
                 pm.ParameterName = "@changedate";
                 pm.Value = DateTime.Now;
                 cmd.Parameters.Add(pm);

                 pm = cmd.CreateParameter();
                 pm.DbType = DbType.String;
                 pm.Size = UserCode.Length;
                 pm.ParameterName = "@change";
                 pm.Value = UserCode;
                 cmd.Parameters.Add(pm);

                 pm = cmd.CreateParameter();
                 pm.DbType = DbType.String;
                 pm.Size = sxml.Length;
                 pm.ParameterName = "@xml";
                 pm.Value = sxml;
                 cmd.Parameters.Add(pm);

                 cmd.ExecuteNonQuery();

                 //����������Ϣ
                 rc = false;

                 cmd = conn.CreateCommand();
                 cmd.Transaction = trans;
                 cmd.CommandText = "Select F_ID from WF_PROCESS where F_ID=" + processID;
                 dr = cmd.ExecuteReader(CommandBehavior.KeyInfo);
                 if (dr.Read())
                 {
                     rc = true;
                 }
                 dr.Close();
                 dr.Dispose();

                 dr = null;


                 if (rc)
                 {
                     //F_NAME, F_DESC, F_CHANGETIME, F_CHANGEUSER
                     cmd.CommandText = "Update WF_PROCESS set F_NAME = ?,F_DESC= ? ,F_CHANGETIME= ?,F_CHANGEUSER=?" +
                         " where F_ID=" + processID;
                 }
                 else
                 {
                     cmd.CommandText = "insert into WF_PROCESS (F_ID,F_NAME, F_DESC, F_CHANGETIME, F_CHANGEUSER) values " +
                         "( ? , ? ,? ,?,?) ";
                     pm = cmd.CreateParameter();
                     pm.DbType = DbType.Int64;
                     //pm.Size = sm.Length;
                     pm.ParameterName = "@id";
                     Int64 iid = int.Parse(processID);
                     pm.Value = iid;

                     cmd.Parameters.Add(pm);
                 }

                 pm = cmd.CreateParameter();
                 pm.DbType = DbType.String;
                 pm.Size = fw.Name.Length;
                 pm.ParameterName = "@name";
                 pm.Value = fw.Name;
                 cmd.Parameters.Add(pm);

                 pm = cmd.CreateParameter();
                 pm.DbType = DbType.String;
                 pm.Size = fw.Desc.Length;
                 pm.ParameterName = "@desc";
                 pm.Value = fw.Name;
                 cmd.Parameters.Add(pm);

                 pm = cmd.CreateParameter();
                 pm.DbType = DbType.DateTime;
                 pm.ParameterName = "@changedate";
                 pm.Value = DateTime.Now;
                 cmd.Parameters.Add(pm);

                 pm = cmd.CreateParameter();
                 pm.DbType = DbType.String;
                 pm.Size = UserCode.Length;
                 pm.ParameterName = "@change";
                 pm.Value = UserCode;
                 cmd.Parameters.Add(pm);

                 cmd.ExecuteNonQuery();


                 cmd.Dispose();

                 SaveActs(conn,trans);
                 trans.Commit();
             }
             catch (Exception e)
             {
                 trans.Rollback();
                 throw (e);
             }
             conn.Dispose();
         }

        /// <summary>
        /// �����ڵ�
        /// </summary>
        private void SaveActs(IDbConnection conn,IDbTransaction trans)
        {
            ArrayList SavedNodes = new ArrayList();
            IDbCommand cmd = conn.CreateCommand();
            cmd.Transaction = trans;
            cmd.CommandText = "delete from WF_ACTIVITY where F_PROCESS_ID=" + fcx.Tag.ToString();
            cmd.ExecuteNonQuery();

            cmd.CommandText = "delete from WF_PRE_RULE where F_PROC_ID=" + fcx.Tag.ToString();
            cmd.ExecuteNonQuery();

            cmd.CommandText = "delete from WF_ROUTING_RULE where F_PROC_ID=" + fcx.Tag.ToString();
            cmd.ExecuteNonQuery();

            cmd.CommandText = "delete from WF_SELECT_ROUNTING where F_PROCESS_ID=" + fcx.Tag.ToString();
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            SaveBox(conn, trans,SavedNodes, StartBox);
        }

        private void SaveBox(IDbConnection conn,IDbTransaction trans, ArrayList SaveNodes, Box cbox)
        {
            
            IDbCommand cmd = conn.CreateCommand();
            cmd.Transaction = trans;
            cmd.CommandText = "insert into WF_ACTIVITY (F_ID ,F_PROCESS_ID ,F_NAME ,F_TIME_ALLOWED ,F_RULE_APPLIED ,"+
                "F_ACT_TYPE , F_OR_MERGE_FLAG , F_NUM_VOTE_NEEDED , F_ACT_DESC) values " +
                "( ? , ? , ? , ? , ? , ? , ? , ? , ? )";
            IDbDataParameter pm = cmd.CreateParameter();
            pm.ParameterName = "@F_ID";
            pm.DbType = DbType.Int64;
            pm.Value = cbox.Tag.ToString();
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.ParameterName = "@F_PROCESS_ID";
            pm.DbType = DbType.Int64;
            pm.Value = fcx.Tag.ToString();
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.ParameterName = "@F_NAME";
            pm.DbType = DbType.String;
            pm.Size = cbox.Text.Length;
            pm.Value = cbox.Text;
            cmd.Parameters.Add(pm);

            wfActivity act = fw.GetActivityByID(cbox.Tag.ToString());

            pm = cmd.CreateParameter();
            pm.ParameterName = "@F_TIME_ALLOWED";
            pm.DbType = DbType.Int32;
            pm.Value = act.TimeAllowed;
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.ParameterName = "@F_RULE_APPLIED";
            pm.DbType = DbType.Int16;
            pm.Value = 0;
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.ParameterName = "@F_ACT_TYPE";
            pm.DbType = DbType.String;
            pm.Value = act.ActType;
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.ParameterName = "@F_OR_MERGE_FLAG";
            pm.DbType = DbType.Int16;
            pm.Value = 0;
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.ParameterName = "@F_NUM_VOTE_NEEDED";
            pm.DbType = DbType.Int16;
            pm.Value = 0;
            cmd.Parameters.Add(pm);

            pm = cmd.CreateParameter();
            pm.ParameterName = "@F_ACT_DESC";
            pm.DbType = DbType.String;
            pm.Value = act.Desc;
            cmd.Parameters.Add(pm);
            

            cmd.ExecuteNonQuery();
            cmd.Dispose();

            SaveNodes.Add(cbox);
            SaveActAssgn(conn, trans, act);   //�洢��Ȩ
            SavePreRule(conn, trans, SaveNodes, cbox);
            SaveRountingRule(conn, trans, SaveNodes, cbox);
            SaveSelectRounting(conn, trans, act);

            for (int i = 0; i < cbox.OutgoingArrows.Count; i++)
            {
                Box nbox = (Box)cbox.OutgoingArrows[i].Destination;
                if (SaveNodes.IndexOf(nbox) == -1)
                {
                    SaveBox(conn, trans, SaveNodes, nbox);
                }
            }
        }

        /// <summary>
        /// ������ѡ��תѡ��
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="act"></param>
        private void SaveSelectRounting(IDbConnection conn, IDbTransaction trans, wfActivity act)
        {
            IDbCommand cmd = conn.CreateCommand();
            cmd.Transaction = trans;

            for (int i = 0; i < act.SelectNodes.Count; i++)
            {
                cmd.CommandText = "insert into WF_SELECT_ROUNTING (F_ACT_ID, F_PROCESS_ID, F_SELECT_ID) values " +
                    "(" + act.ID + " ," + fw.ID + "," + act.SelectNodes[i] + ")";
                cmd.ExecuteNonQuery();
            }

        }

        private void SaveActAssgn(IDbConnection conn, IDbTransaction trans, wfActivity act)
        {
            IDbCommand cmd = conn.CreateCommand();
            cmd.Transaction = trans;
            cmd.CommandText = "delete from WF_ASSGN_RULE where F_ACT_ID=" + act.ID;
            cmd.ExecuteNonQuery();

            for (int i = 0; i < act.UserList.Count; i++)
            {
                actAssgn aa =act.UserList[i];
                cmd.CommandText = "insert into WF_ASSGN_RULE (F_ACT_ID, F_BASED_ON, F_USER_ID) values " +
                    "("+act.ID+" ,'"+aa.UserType+"','"+aa.ID.Replace("'","''")+"')";
                cmd.ExecuteNonQuery();
            }

        }

        private void SavePreRule(IDbConnection conn, IDbTransaction trans, ArrayList SaveNodes, Box cbox)
        {

            IDbCommand cmd = conn.CreateCommand();
            cmd.Transaction = trans;
            for (int i = 0; i < cbox.IncomingArrows.Count; i++)
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "INSERT INTO WF_PRE_RULE (F_DEPNT_ID ,F_DEPNT_ACT_ID ,F_DEPNT_ACT_STATUS ,F_PROC_ID)" +
                    " VALUES ( ? , ? , ? , ? )";
                IDbDataParameter pm = cmd.CreateParameter();
                pm.ParameterName = "@F_DEPNT_ID";
                pm.DbType = DbType.Int64;
                pm.Value = cbox.Tag.ToString();
                cmd.Parameters.Add(pm);

                Box pbox = (Box)cbox.IncomingArrows[i].Origin;
                pm = cmd.CreateParameter();
                pm.ParameterName = "@F_DEPNT_ACT_ID";
                pm.DbType = DbType.Int64;
                pm.Value = pbox.Tag.ToString();
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.ParameterName = "@F_DEPNT_ACT_STATUS";
                pm.DbType = DbType.String;
                pm.Value = wfConsts.RUN_STATE_ACCEPT;
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.ParameterName = "@F_PROC_ID";
                pm.DbType = DbType.String;
                pm.Value = fcx.Tag.ToString();
                cmd.Parameters.Add(pm);

                cmd.ExecuteNonQuery();

            }

            cmd.Dispose();
        }

        private void SaveRountingRule(IDbConnection conn, IDbTransaction trans, ArrayList SaveNodes, Box cbox)
        {

            IDbCommand cmd = conn.CreateCommand();
            cmd.Transaction = trans;
            for (int i = 0; i < cbox.OutgoingArrows.Count; i++)
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "INSERT INTO WF_ROUTING_RULE (F_PRE_ACT_ID ,F_CURR_ACT_ID ,F_COMPLETION_FLAG ,F_PROC_ID)" +
                    " VALUES ( ? , ? , ? , ? )";
                IDbDataParameter pm = cmd.CreateParameter();
                pm.ParameterName = "@F_PRE_ACT_ID";
                pm.DbType = DbType.Int64;
                pm.Value = cbox.Tag.ToString();
                cmd.Parameters.Add(pm);

                Box pbox = (Box)cbox.OutgoingArrows[i].Destination;
                pm = cmd.CreateParameter();
                pm.ParameterName = "@F_CURR_ACT_ID";
                pm.DbType = DbType.Int64;
                pm.Value = pbox.Tag.ToString();
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.ParameterName = "@F_COMPLETION_FLAG";
                pm.DbType = DbType.String;
                if (cbox.OutgoingArrows[i].Tag != null)
                {
                    actRule ar = (actRule)cbox.OutgoingArrows[i].Tag;
                    if (ar.Node.ID == pbox.Tag.ToString())
                    {
                        pm.Value = ar.Rule;
                    }
                    else
                        pm.Value = wfConsts.RUN_STATE_ACCEPT;
                }
                else
                    pm.Value = wfConsts.RUN_STATE_ACCEPT;
                cmd.Parameters.Add(pm);

                pm = cmd.CreateParameter();
                pm.ParameterName = "@F_PROC_ID";
                pm.DbType = DbType.String;
                pm.Value = fcx.Tag.ToString();
                cmd.Parameters.Add(pm);

                cmd.ExecuteNonQuery();

            }

            cmd.Dispose();
        }

        private bool LoadFlowDataFromDB(string processID)
        {

            if (fw != null)
                fw.Clear();
            fw = null;
            wfps.ConnectionStr = dbConn.DBConn;
            fw = wfps.GetProcess(processID);


            return true;

        }

        private bool CheckFlow()
        {
            if (fcx.Boxes.Count < 3)
            {
                MessageBox.Show("�����̲��ܱ���");
                return false;
            }

            for (int i = 0; i < fcx.Boxes.Count; i++)
            {
                Box cb = fcx.Boxes[i];
                if ((cb.IncomingArrows.Count == 0 && cb != StartBox) || (cb.OutgoingArrows.Count == 0 &&
                    cb != EndBox))
                {
                   MessageBox.Show("�����нڵ�["+cb.Text+"]û�����������������\n��������������");
                   return false;
                }

                
            }
            return true;
        }

        private int LoadFromDB(string processID, System.IO.MemoryStream sm)
        {
            IDbConnection conn = new System.Data.OleDb.OleDbConnection();
            conn.ConnectionString = dbConn.DBConn;
            conn.Open();
            long offset = 0;

            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Select F_DATA from WF_PROCESS_CHART where F_ID=" + processID;
            IDataReader dr = cmd.ExecuteReader(CommandBehavior.KeyInfo);
            if (dr.Read())
            {
                byte[] bdata = new byte[4096];
                while (true)
                {
                    int rsize =(int)dr.GetBytes(0, offset, bdata, 0, 4096);
                    sm.Write(bdata, 0, rsize);
                    offset += rsize;
                    if (rsize < 4096) break;
                }
            }
            dr.Close();
            dr.Dispose();

            dr = null;
            cmd.Dispose();
            conn.Dispose();

            return (int)offset;
        }

        private void fcx_DrawMark(object sender, MindFusion.Diagramming.WinForms.MarkDrawArgs e)
         {
             PointF imgPos = e.Location;
             imgPos.Y -= 1.8f;
             e.Graphics.DrawImage(icon.Image, imgPos);
         }

         private void button3_Click(object sender, System.EventArgs e)
         {
             if (fcx.ActiveObject != null &&
                 fcx.ActiveObject is MindFusion.Diagramming.WinForms.Box)
             {
                 Box node = (Box)fcx.ActiveObject;
                 node.AnchorPattern=AnchorPattern.Decision2In2Out;
             }
         }


         private void fcx_BoxClicked(object sender, BoxMouseArgs e)
         {
             SelectChange();
             return;
            /* selfChange = true;
             editName.Text = e.Box.Text;
             selfChange = false;
                */
             /*if (e.Box.Tag != null)
             {
                 //NodeProp nprop = (NodeProp)e.Box.Tag;
                 string currid = e.Box.Tag.ToString();
                 wfActivity act = this.fw.GetActivityByID(currid);
                 if (act != null)
                 {
                     selfChange = true;
                     //editName.Text = e.Box.Text;
                     editID.Text = act.ID;
                     editDesc.Text = act.Desc;
                     selfChange = false;
                 }

             }

            
             /*
             label1.Text = e.Box.Text + " out Arrows:" + e.Box.OutgoingArrows.Count.ToString();
             if (e.Box.OutgoingArrows.Count > 0)
             {
                  Box b1=(Box)e.Box.OutgoingArrows[0].Destination;
                  label1.Text = label1.Text + "\n��һ�ڵ�:" + b1.Text;
             }
             */
        }


        private void fcx_BoxDeleting(object sender, BoxConfirmArgs e)
        {
            if (e.Box == StartBox || e.Box == EndBox)
            {
                e.Confirm = false;
            }
            
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FormProcessProp frm = new FormProcessProp();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (fw!=null)
                    fw.Clear();
                fw = null;
                fw = new wfEnginer.FlowChart();
                fw.ID = "-1";
                fw.Name = frm.NameValue;
                fw.Desc = frm.DescValue;
                
                fcx.ClearAll();
                fcx.Tag = fw.ID;
                ChangeCaption();
            }
            frm.Dispose();
        }

        private void btnProp_Click(object sender, EventArgs e)
        {
            FormProcessProp frm = new FormProcessProp();
            frm.NameValue = fw.Name;
            frm.DescValue = fw.Desc;
            if (fw.LastChange != DateTime.MinValue)
                frm.ChangeTimeValue = fw.LastChange.ToString();
            frm.ChangeUserValue = fw.UserCode;
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                fw.Name = frm.NameValue;
                fw.Desc = frm.DescValue;
                this.Text = "���̶���[" + fw.Name + "]";
            }
            frm.Dispose();

        }

        private void fcx_BoxDblClicked(object sender, BoxMouseArgs e)
        {
            e.Box.BeginInplaceEdit();
            
        }

        private void fcx_ArrowCreating(object sender, AttachConfirmArgs e)
        {
            
            for (int i=0;i<e.Node.OutgoingArrows.Count;i++)
            {
                Arrow ar = e.Node.OutgoingArrows[i];
                if (ar.Destination == e.Arrow.Origin)
                {
                    e.Confirm = false;
                    return;
                }
            }

        }

        private void fcx_BoxDeleted(object sender, BoxEventArgs e)
        {
            if (e.Box.Tag != null)
            {
                string sID = e.Box.Tag.ToString();
                wfActivity act = fw.GetActivityByID(sID);
                if (act != null)
                    act.Changed = 4;   //delete
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (fcx.ActiveObject == null) return;
            if (fcx.ActiveObject is Box)
            {

                FormUserList frm = new FormUserList();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    int uType = frm.UserType;
                    string uCode = "", uName = "";
                    string usType = wfConsts.ASSIGN_TYPE_USER;
                    if (uType != 0)
                        usType = wfConsts.ASSIGN_TYPE_ROLE;
                    if (frm.GetSelected(ref uCode, ref uName))
                    {
                        int idx = -1;
                        Box cbox = (Box)fcx.ActiveObject;
                        if (cbox.Tag != null)
                        {
                            string cid = cbox.Tag.ToString();
                            wfActivity act = fw.GetActivityByID(cid);
                            idx =act.UserList.Add(uCode, usType, uName);
                        }
                        if (idx > -1)
                        {
                            ListViewItem item = editAssgn.Items.Add(uName);
                            item.SubItems.Add(usType);
                            item.SubItems.Add(uCode);
                        }
                    }
                }

                frm.Dispose();
            }
        }

        private void editDesc_TextChanged(object sender, EventArgs e)
        {
            if (selfChange) return;
            if ( fcx.ActiveObject==null) return;
            if (fcx.ActiveObject is Box)
            {
                Box cbox = (Box)fcx.ActiveObject;
                if (cbox.Tag != null)
                {
                    string sid = cbox.Tag.ToString();
                    wfActivity act = fw.GetActivityByID(sid);
                    act.Desc = editDesc.Text;
                }
            }
        }

        /// <summary>
        /// ��ƽ���ѡ�ж��󱻸ı�ʱ
        /// </summary>
        private void SelectChange()
        {
            editAssgn.Items.Clear();

            selfChange = true;
            editID.Text = "";
            editDesc.Text = "";
            editTimeout.Value = 0;
            comboBoxPreType.SelectedIndex = 0;
            groupBoxPre.Enabled = false;
            editPreSQL.Text = "";
            editPreSQL.ReadOnly = true;
            checkedListBoxManual.Enabled = false;
            comboBoxMerge.Enabled = false;

            selfChange = false;

            frmCon.Visible = false;

            if (fcx.ActiveObject == null) return;
            if (fcx.ActiveObject is Arrow)                //ѡ�м�ͷ
            {
                groupBoxPre.Enabled = true;
                Arrow arr = (Arrow)fcx.ActiveObject;
                Box pb = (Box)arr.Origin;
                Box cb = (Box)arr.Destination;
                wfActivity act = fw.GetActivityByID(pb.Tag.ToString());
                wfActivity actChild = fw.GetActivityByID(cb.Tag.ToString());
                if (act !=null)
                {
                    actRule cr = null;
                    for (int i = 0; i < act.ChildCount; i++)
                    {
                        cr = act.ChildNode(i);
                        if (cr.Node.ID == cb.Tag.ToString())  //�ӽڵ��ID����һ�����ӵ�ID��ͬ
                        {
                            editPreSQL.Text = cr.Rule;
                            arr.Tag = cr;
                            SyncPreSQL(arr, cr);
                            break;
                        }
                    }

                    //������
                    if (cr == null)
                    {
                        cr = act.AddChild(actChild, wfConsts.RUN_STATE_ACCEPT);
                        arr.Tag = cr;
                        SyncPreSQL(arr, cr);
                    }
                }
            }
            else                                          //ѡ�к���
            {

                Box cb = (Box)fcx.ActiveObject;

                if (cb.Tag != null)
                {
                    string currid = cb.Tag.ToString();
                    wfActivity act = this.fw.GetActivityByID(currid);
                    if (act != null)
                    {
                        selfChange = true;
                        editID.Text = act.ID;
                        editDesc.Text = act.Desc;
                        editTimeout.Value = act.TimeAllowed;

                        if (act.ActType == wfConsts.NODE_MERGE_AND)
                            comboBoxMerge.SelectedIndex = 0;
                        else
                            comboBoxMerge.SelectedIndex = 1;

                        if (act.ActType != wfConsts.NODE_START && act.ActType != wfConsts.NODE_END &&
                            act.ActType != wfConsts.NODE_IF_BRANCH)
                            comboBoxMerge.Enabled = true;


                        for (int i=0;i<act.UserList.Count;i++)
                        {
                            actAssgn aa = act.UserList[i];
                            ListViewItem lv = editAssgn.Items.Add(aa.Desc);
                            lv.SubItems.Add(aa.UserType);
                            lv.SubItems.Add(aa.ID);
                        }

                        checkedListBoxManual.Items.Clear();
                        SelectItemValue.Clear();

                        for (int i = 0; i < fcx.Boxes.Count; i++)
                        {
                            if (fcx.Boxes[i] != cb)
                            {
                                SelectItemValue.Add(fcx.Boxes[i].Tag.ToString());
                                checkedListBoxManual.Items.Add(fcx.Boxes[i].Text + "[" + fcx.Boxes[i].Tag.ToString() + "]");
                            }
                        }
                        SyncSelectNodes(act);   //ͬ����ѡ��

                        selfChange = false;
                        checkedListBoxManual.Enabled = true;
                        //ShowCondtBox(cb, act); //��ʾת����������
                    }

                }
                
            }
        }

        private void SyncSelectNodes(wfActivity act)
        {
            for (int i = 0; i < act.SelectNodes.Count; i++)
            {
                string id = act.SelectNodes[i];
                for (int j = 0; j < checkedListBoxManual.Items.Count; j++)
                {
                    string ctxt = SelectItemValue[j];
                    if (id == ctxt)
                    {
                        checkedListBoxManual.SetItemChecked(j, true);
                        break;
                    }
                }
            }
        }

        private void SyncPreSQL(Arrow arr, actRule cr)
        {
            selfChange = true;
            if (cr.Rule == wfConsts.RUN_STATE_ACCEPT)
                comboBoxPreType.SelectedIndex = 0;
            else if (cr.Rule == wfConsts.RUN_STATE_REJECT)
                comboBoxPreType.SelectedIndex = 1;
            else
            {
                comboBoxPreType.SelectedIndex = 2;
                editPreSQL.Enabled = true;
            }
            selfChange = false;

        }
        /// <summary>
        /// ��ʾת����������
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="act"></param>
        private void ShowCondtBox(Box cb, wfActivity act)
        {
            return;
            //�Ƿ���ʾ��������
            bool ShDlg = false;
            selfChange = true;
            editPreSQL.Text = "";
            selfChange = false;
            editPreSQL.ReadOnly = true;

            for (int i = 0; i < cb.IncomingArrows.Count; i++)
            {
                Box pb = (Box)cb.IncomingArrows[i].Origin;
                if (pb.Style == BoxStyle.Rhombus)
                {
                    ShDlg = true;
                    break;
                }
            }

            if (ShDlg)
            {
                selfChange = true;
                editPreSQL.Text = act.PreRule;
                selfChange = false;
                editPreSQL.ReadOnly = false;
            }
            /*
            if (ShDlg)
            {
                Rectangle r = Screen.PrimaryScreen.WorkingArea;
                if (MousePosition.X < r.Right / 2)
                    frmCon.Left = MousePosition.X + 5;
                else
                    frmCon.Left = MousePosition.X - 5 - frmCon.Width;
                if (MousePosition.Y < r.Bottom / 2)
                    frmCon.Top = MousePosition.Y + 5;
                else
                    frmCon.Top = MousePosition.Y - 5 - frmCon.Height;
                frmCon.Show(this);
            }
             * */

        }

        private void editAssgn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (editAssgn.SelectedItems.Count > 0)
            {
                btnDelUser.Enabled = true;
            }
            else
                btnDelUser.Enabled = false;
        }

        private void btnDelUser_Click(object sender, EventArgs e)
        {
            if (fcx.ActiveObject == null) return;
            if (fcx.ActiveObject is Box)
            {
                Box cb = (Box)fcx.ActiveObject;
                wfActivity act = fw.GetActivityByID(cb.Tag.ToString());
                if (act != null)
                {
                    for (int i = editAssgn.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        ListViewItem li = editAssgn.SelectedItems[i];
                        act.UserList.Delete(li.SubItems[0].Text, li.SubItems[1].Text);
                        editAssgn.Items.Remove(li);
                    }
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            fcx.PrintOptions.DocumentName = fw.Name;
            fcx.PrintOptions.HeaderFormat = "%D";
          
            fcx.PrintPreview();
        }

        private void ChangeCaption()
        {
            this.Text = "���̶���[" + fw.Name + "]";
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                fcx.SaveToFile(sfd.FileName);
            }
        }

        private void fcx_SelectionChanged(object sender, EventArgs e)
        {
            this.SelectChange();
        }

        public string UserName
        {
            set { UserCode = value; }
        }

        private void editPreSQL_TextChanged(object sender, EventArgs e)
        {
            if (selfChange) return;
            if (fcx.ActiveObject == null) return;
            if (fcx.ActiveObject is Box)
            {
                Box cb = (Box)fcx.ActiveObject;
                if (cb.Tag != null)
                {
                    wfActivity act = fw.GetActivityByID(cb.Tag.ToString());
                    if (act != null)
                        act.PreRule = editPreSQL.Text;
                }
            }
        }

        private void editTimeout_ValueChanged(object sender, EventArgs e)
        {
            if (selfChange) return;
            if (fcx.ActiveObject == null) return;
            if (fcx.ActiveObject is Box)
            {
                Box cb = (Box)fcx.ActiveObject;
                if (cb.Tag != null)
                {
                    wfActivity act = fw.GetActivityByID(cb.Tag.ToString());
                    if (act != null)
                        act.TimeAllowed =(int) editTimeout.Value ;
                }
            }

        }

        private void checkBoxCurrDept_MouseHover(object sender, EventArgs e)
        {
            textBoxHint.Text = "������Ȩ�޿���ѡ���������Ĳ���Ȩ�ޣ��ڱ���Ȩ�������û��У�ֻ�����ύ�����û�����ͬ���ŵ��û�������";
        }

        private void fcx_MouseHover(object sender, EventArgs e)
        {
            textBoxHint.Text = "���̻�ͼ�壺�ڿհ״�������꣬�϶������½ڵ㣬�ڵ������롰�ڵ����͡���ء�";

        }

        private void checkBoxSimDept_MouseHover(object sender, EventArgs e)
        {
            textBoxHint.Text = "������Ȩ�޿���ѡ���������Ĳ���Ȩ�ޣ��ڱ���Ȩ�������û��У�ֻ�����ύ�����û�����ͬ�����Լ����ŵ��ϼ����ŵ��û�������";

        }

        private void fcx_ArrowCreated(object sender, ArrowEventArgs e)
        {
            Box pb=(Box) e.Arrow.Origin;
            Box cb = (Box)e.Arrow.Destination;
            if (pb.Tag == null || cb.Tag == null) return;

            wfActivity pact = fw.GetActivityByID(pb.Tag.ToString());
            wfActivity cact = fw.GetActivityByID(cb.Tag.ToString());
            if (pact == null || cact == null) return;

            actRule ar= pact.AddChild(cact, wfConsts.RUN_STATE_ACCEPT);
            e.Arrow.Tag = ar; 
        }

        private void comboBoxPreType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selfChange) return;
            editPreSQL.ReadOnly = (comboBoxPreType.SelectedIndex!=2);
            if (comboBoxPreType.SelectedIndex != 2)
            {
                if (fcx.ActiveObject == null) return;
                if (fcx.ActiveObject is Arrow)
                {
                    Arrow arr = (Arrow)fcx.ActiveObject;
                    if (arr.Tag != null)
                    {
                        if (comboBoxPreType.SelectedIndex==0)
                            ((actRule)arr.Tag).Rule = wfConsts.RUN_STATE_ACCEPT;
                        else if (comboBoxPreType.SelectedIndex == 1)
                            ((actRule)arr.Tag).Rule = wfConsts.RUN_STATE_REJECT;
                    }
                }
            }
        }

        private void editPreSQL_MouseHover(object sender, EventArgs e)
        {
            textBoxHint.Text = "������SQL�ж�ѡ���������Ĳ�������SQL����жϣ�\r\n�ж�����Ϊ��" +
                "ִ�п��е�SQL��䣬����������ݼ�¼��Ϊ0����Ϊ����ͨ�����������ͨ��\r\n" +
                "------  ������SQL�����ʹ�����±��� --\r\n" +
                "#EntityID\t��ǰ�������ݵļ�ֵ\r\n" +
                "#Status\t�����߿�ʼ�ڵ���������\r\n" +
                "#StartUserCode\t�������̵��û�\r\n" +
                "#UserCode\t�����߿�ʼ�ڵ�����û�\r\n" +
                "#Dept\t�����߿�ʼ�ڵ�����û���������\r\n" +
                "#StartDept\t���������û���������\r\n" +
                "#StartDeptParent\t���������û��������ŵ��ϼ�����\r\n" +
                "--------\r\n" +
                "ע�������ʹ�ú��ʵ���������,\r\n" +
                "������������λ�ã�Ҫ�ں���ӿո�\r\n" +
                "����:\r\n" +
                "select F_ID from T_ENTITY where F_ID=#EntityID and F_PRICE>1000\r\n" +
                "������ʾ����T_ENTITY��F_IDΪ��ǰ�����ļ�ֵ�����Ҽ۸�>1000ʱ���Դ�����·����������";

        }

        private void editPreSQL_TextChanged_1(object sender, EventArgs e)
        {
            if (selfChange) return;
            if (fcx.ActiveObject == null) return;
            if (fcx.ActiveObject is Arrow)
            {
                Arrow arr = (Arrow)fcx.ActiveObject;
                if (arr.Tag != null)
                {
                    actRule ar = (actRule)arr.Tag;
                    ar.Rule = editPreSQL.Text;
                }

            }
        }

        private void checkedListBoxManual_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (selfChange) return;
            if (fcx.ActiveObject == null) return;
            if (fcx.ActiveObject is Box)
            {
                Box cb = (Box)fcx.ActiveObject;
                if (cb.Tag != null)
                {
                    wfActivity act = fw.GetActivityByID(cb.Tag.ToString());
                    if (act != null)
                    {
                        int idx =e.Index;
                        if (e.NewValue == CheckState.Checked)
                        {
                            act.SelectNodes.Add(SelectItemValue[idx]);
                        }
                        else
                        {
                            act.SelectNodes.Remove(SelectItemValue[idx]);
                        }

                    }
                }
            }

        }

        private void comboBoxMerge_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selfChange) return;
            if (fcx.ActiveObject == null) return;
            if (fcx.ActiveObject is Box)
            {
                Box cb = (Box)fcx.ActiveObject;
                if (cb.Tag != null)
                {
                    wfActivity act = fw.GetActivityByID(cb.Tag.ToString());
                    if (comboBoxMerge.SelectedIndex == 0)
                        act.ActType = wfConsts.NODE_MERGE_AND;
                    else if (comboBoxMerge.SelectedIndex == 1)
                        act.ActType = wfConsts.NODE_MERGE_OR;
                }
            }
        }



    }
}
