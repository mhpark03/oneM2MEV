namespace WindowsFormsApp2
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cBoxBaudRate = new System.Windows.Forms.ComboBox();
            this.cBoxCOMPORT = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbRTS = new System.Windows.Forms.CheckBox();
            this.cbDTR = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCOM = new System.Windows.Forms.TabPage();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.button128 = new System.Windows.Forms.Button();
            this.textBox81 = new System.Windows.Forms.TextBox();
            this.button121 = new System.Windows.Forms.Button();
            this.textBox80 = new System.Windows.Forms.TextBox();
            this.button120 = new System.Windows.Forms.Button();
            this.textBox79 = new System.Windows.Forms.TextBox();
            this.button63 = new System.Windows.Forms.Button();
            this.textBox64 = new System.Windows.Forms.TextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.button119 = new System.Windows.Forms.Button();
            this.textBox78 = new System.Windows.Forms.TextBox();
            this.button86 = new System.Windows.Forms.Button();
            this.textBox24 = new System.Windows.Forms.TextBox();
            this.button99 = new System.Windows.Forms.Button();
            this.textBox58 = new System.Windows.Forms.TextBox();
            this.button100 = new System.Windows.Forms.Button();
            this.textBox59 = new System.Windows.Forms.TextBox();
            this.button101 = new System.Windows.Forms.Button();
            this.textBox60 = new System.Windows.Forms.TextBox();
            this.textBox61 = new System.Windows.Forms.TextBox();
            this.button62 = new System.Windows.Forms.Button();
            this.gbDeviceLog = new System.Windows.Forms.GroupBox();
            this.listView3 = new System.Windows.Forms.ListView();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.label47 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.lbModemVer = new System.Windows.Forms.TextBox();
            this.textBox89 = new System.Windows.Forms.TextBox();
            this.lbIccid = new System.Windows.Forms.TextBox();
            this.textBox87 = new System.Windows.Forms.TextBox();
            this.textBox86 = new System.Windows.Forms.TextBox();
            this.button87 = new System.Windows.Forms.Button();
            this.textBox85 = new System.Windows.Forms.TextBox();
            this.textBox57 = new System.Windows.Forms.TextBox();
            this.textBox40 = new System.Windows.Forms.TextBox();
            this.textBox38 = new System.Windows.Forms.TextBox();
            this.textBox33 = new System.Windows.Forms.TextBox();
            this.button71 = new System.Windows.Forms.Button();
            this.button83 = new System.Windows.Forms.Button();
            this.textBox44 = new System.Windows.Forms.TextBox();
            this.button88 = new System.Windows.Forms.Button();
            this.textBox45 = new System.Windows.Forms.TextBox();
            this.textBox46 = new System.Windows.Forms.TextBox();
            this.textBox47 = new System.Windows.Forms.TextBox();
            this.textBox48 = new System.Windows.Forms.TextBox();
            this.textBox49 = new System.Windows.Forms.TextBox();
            this.button89 = new System.Windows.Forms.Button();
            this.button90 = new System.Windows.Forms.Button();
            this.button91 = new System.Windows.Forms.Button();
            this.tabModule = new System.Windows.Forms.TabPage();
            this.groupBox28 = new System.Windows.Forms.GroupBox();
            this.listView11 = new System.Windows.Forms.ListView();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.label55 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.button55 = new System.Windows.Forms.Button();
            this.button57 = new System.Windows.Forms.Button();
            this.button58 = new System.Windows.Forms.Button();
            this.button59 = new System.Windows.Forms.Button();
            this.groupBox27 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button44 = new System.Windows.Forms.Button();
            this.groupBox26 = new System.Windows.Forms.GroupBox();
            this.label53 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button37 = new System.Windows.Forms.Button();
            this.button46 = new System.Windows.Forms.Button();
            this.button47 = new System.Windows.Forms.Button();
            this.button48 = new System.Windows.Forms.Button();
            this.button49 = new System.Windows.Forms.Button();
            this.groupBox25 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button75 = new System.Windows.Forms.Button();
            this.tabServer = new System.Windows.Forms.TabPage();
            this.label23 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.listView7 = new System.Windows.Forms.ListView();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.button94 = new System.Windows.Forms.Button();
            this.lbmodemfwrver = new System.Windows.Forms.Label();
            this.btnDeviceCheck = new System.Windows.Forms.Button();
            this.lbdevicever = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.tbSeverPort = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tbSeverIP = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btnDelRemoteCSE = new System.Windows.Forms.Button();
            this.btnSetRemoteCSE = new System.Windows.Forms.Button();
            this.btnGetRemoteCSE = new System.Windows.Forms.Button();
            this.btnMEFAuth = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.tbSvcSvrCd = new System.Windows.Forms.TextBox();
            this.tbSvcSvrNum = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.tabLOG = new System.Windows.Forms.TabPage();
            this.listView10 = new System.Windows.Forms.ListView();
            this.listView9 = new System.Windows.Forms.ListView();
            this.listView8 = new System.Windows.Forms.ListView();
            this.label49 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.button127 = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.tBResultCode = new System.Windows.Forms.TextBox();
            this.textBox94 = new System.Windows.Forms.TextBox();
            this.tbDeviceCTN = new System.Windows.Forms.TextBox();
            this.textBox95 = new System.Windows.Forms.TextBox();
            this.button126 = new System.Windows.Forms.Button();
            this.button122 = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.button123 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnGetLogList = new System.Windows.Forms.Button();
            this.button124 = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.webpage = new System.Windows.Forms.TabPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.button81 = new System.Windows.Forms.Button();
            this.button72 = new System.Windows.Forms.Button();
            this.lbActionState = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.tBoxDeviceVer = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.tbSvcCd = new System.Windows.Forms.TextBox();
            this.tBoxDeviceSN = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.tBoxDeviceModel = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabCOM.SuspendLayout();
            this.groupBox23.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.gbDeviceLog.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.tabModule.SuspendLayout();
            this.groupBox28.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox27.SuspendLayout();
            this.groupBox26.SuspendLayout();
            this.groupBox25.SuspendLayout();
            this.tabServer.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.tabLOG.SuspendLayout();
            this.webpage.SuspendLayout();
            this.SuspendLayout();
            // 
            // cBoxBaudRate
            // 
            this.cBoxBaudRate.FormattingEnabled = true;
            this.cBoxBaudRate.Items.AddRange(new object[] {
            "2400",
            "4800",
            "9600",
            "38400",
            "76800",
            "115200"});
            this.cBoxBaudRate.Location = new System.Drawing.Point(120, 12);
            this.cBoxBaudRate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cBoxBaudRate.Name = "cBoxBaudRate";
            this.cBoxBaudRate.Size = new System.Drawing.Size(66, 20);
            this.cBoxBaudRate.TabIndex = 2;
            this.cBoxBaudRate.Text = "115200";
            // 
            // cBoxCOMPORT
            // 
            this.cBoxCOMPORT.FormattingEnabled = true;
            this.cBoxCOMPORT.Items.AddRange(new object[] {
            "COM103"});
            this.cBoxCOMPORT.Location = new System.Drawing.Point(35, 12);
            this.cBoxCOMPORT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cBoxCOMPORT.Name = "cBoxCOMPORT";
            this.cBoxCOMPORT.Size = new System.Drawing.Size(80, 20);
            this.cBoxCOMPORT.TabIndex = 1;
            this.cBoxCOMPORT.Text = "COM103";
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.progressBar1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.progressBar1.Location = new System.Drawing.Point(12, 12);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(19, 18);
            this.progressBar1.TabIndex = 2;
            this.progressBar1.Click += new System.EventHandler(this.ProgressBar1_Click);
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialPort1_DataReceived);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbRTS);
            this.panel1.Controls.Add(this.cbDTR);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.button81);
            this.panel1.Controls.Add(this.button72);
            this.panel1.Controls.Add(this.lbActionState);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.cBoxBaudRate);
            this.panel1.Controls.Add(this.label30);
            this.panel1.Controls.Add(this.tBoxDeviceVer);
            this.panel1.Controls.Add(this.label34);
            this.panel1.Controls.Add(this.tbSvcCd);
            this.panel1.Controls.Add(this.tBoxDeviceSN);
            this.panel1.Controls.Add(this.cBoxCOMPORT);
            this.panel1.Controls.Add(this.label31);
            this.panel1.Controls.Add(this.tBoxDeviceModel);
            this.panel1.Controls.Add(this.label32);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1195, 804);
            this.panel1.TabIndex = 10;
            // 
            // cbRTS
            // 
            this.cbRTS.AutoSize = true;
            this.cbRTS.Checked = true;
            this.cbRTS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRTS.Location = new System.Drawing.Point(246, 14);
            this.cbRTS.Name = "cbRTS";
            this.cbRTS.Size = new System.Drawing.Size(48, 16);
            this.cbRTS.TabIndex = 80;
            this.cbRTS.Text = "RTS";
            this.cbRTS.UseVisualStyleBackColor = true;
            this.cbRTS.CheckedChanged += new System.EventHandler(this.cbRTS_CheckedChanged);
            // 
            // cbDTR
            // 
            this.cbDTR.AutoSize = true;
            this.cbDTR.Checked = true;
            this.cbDTR.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDTR.Location = new System.Drawing.Point(192, 15);
            this.cbDTR.Name = "cbDTR";
            this.cbDTR.Size = new System.Drawing.Size(48, 16);
            this.cbDTR.TabIndex = 79;
            this.cbDTR.Text = "DTR";
            this.cbDTR.UseVisualStyleBackColor = true;
            this.cbDTR.CheckedChanged += new System.EventHandler(this.cbDTR_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabCOM);
            this.tabControl1.Controls.Add(this.tabModule);
            this.tabControl1.Controls.Add(this.tabServer);
            this.tabControl1.Controls.Add(this.tabLOG);
            this.tabControl1.Controls.Add(this.webpage);
            this.tabControl1.Location = new System.Drawing.Point(3, 40);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1180, 758);
            this.tabControl1.TabIndex = 44;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabCOM
            // 
            this.tabCOM.Controls.Add(this.textBox2);
            this.tabCOM.Controls.Add(this.textBox1);
            this.tabCOM.Controls.Add(this.groupBox23);
            this.tabCOM.Controls.Add(this.groupBox9);
            this.tabCOM.Controls.Add(this.gbDeviceLog);
            this.tabCOM.Controls.Add(this.groupBox10);
            this.tabCOM.Location = new System.Drawing.Point(4, 22);
            this.tabCOM.Name = "tabCOM";
            this.tabCOM.Size = new System.Drawing.Size(1172, 732);
            this.tabCOM.TabIndex = 3;
            this.tabCOM.Text = " 모뎀정보 ";
            this.tabCOM.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(535, 18);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(147, 21);
            this.textBox2.TabIndex = 72;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(688, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(479, 21);
            this.textBox1.TabIndex = 71;
            // 
            // groupBox23
            // 
            this.groupBox23.Controls.Add(this.button128);
            this.groupBox23.Controls.Add(this.textBox81);
            this.groupBox23.Controls.Add(this.button121);
            this.groupBox23.Controls.Add(this.textBox80);
            this.groupBox23.Controls.Add(this.button120);
            this.groupBox23.Controls.Add(this.textBox79);
            this.groupBox23.Controls.Add(this.button63);
            this.groupBox23.Controls.Add(this.textBox64);
            this.groupBox23.Location = new System.Drawing.Point(24, 588);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new System.Drawing.Size(490, 141);
            this.groupBox23.TabIndex = 70;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "기타 AT command";
            // 
            // button128
            // 
            this.button128.Location = new System.Drawing.Point(7, 101);
            this.button128.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button128.Name = "button128";
            this.button128.Size = new System.Drawing.Size(147, 24);
            this.button128.TabIndex = 75;
            this.button128.Text = "ATCMD 4";
            this.button128.UseVisualStyleBackColor = true;
            this.button128.Click += new System.EventHandler(this.button128_Click);
            // 
            // textBox81
            // 
            this.textBox81.Location = new System.Drawing.Point(157, 101);
            this.textBox81.Name = "textBox81";
            this.textBox81.Size = new System.Drawing.Size(310, 21);
            this.textBox81.TabIndex = 74;
            // 
            // button121
            // 
            this.button121.Location = new System.Drawing.Point(7, 75);
            this.button121.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button121.Name = "button121";
            this.button121.Size = new System.Drawing.Size(147, 24);
            this.button121.TabIndex = 73;
            this.button121.Text = "ATCMD 3";
            this.button121.UseVisualStyleBackColor = true;
            this.button121.Click += new System.EventHandler(this.button121_Click);
            // 
            // textBox80
            // 
            this.textBox80.Location = new System.Drawing.Point(157, 75);
            this.textBox80.Name = "textBox80";
            this.textBox80.Size = new System.Drawing.Size(310, 21);
            this.textBox80.TabIndex = 72;
            // 
            // button120
            // 
            this.button120.Location = new System.Drawing.Point(7, 47);
            this.button120.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button120.Name = "button120";
            this.button120.Size = new System.Drawing.Size(147, 24);
            this.button120.TabIndex = 71;
            this.button120.Text = "ATCMD 2";
            this.button120.UseVisualStyleBackColor = true;
            this.button120.Click += new System.EventHandler(this.button120_Click);
            // 
            // textBox79
            // 
            this.textBox79.Location = new System.Drawing.Point(157, 47);
            this.textBox79.Name = "textBox79";
            this.textBox79.Size = new System.Drawing.Size(310, 21);
            this.textBox79.TabIndex = 70;
            // 
            // button63
            // 
            this.button63.Location = new System.Drawing.Point(7, 19);
            this.button63.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button63.Name = "button63";
            this.button63.Size = new System.Drawing.Size(147, 24);
            this.button63.TabIndex = 69;
            this.button63.Text = "ATCMD 1";
            this.button63.UseVisualStyleBackColor = true;
            this.button63.Click += new System.EventHandler(this.button63_Click_1);
            // 
            // textBox64
            // 
            this.textBox64.Location = new System.Drawing.Point(157, 19);
            this.textBox64.Name = "textBox64";
            this.textBox64.Size = new System.Drawing.Size(310, 21);
            this.textBox64.TabIndex = 68;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.button119);
            this.groupBox9.Controls.Add(this.textBox78);
            this.groupBox9.Controls.Add(this.button86);
            this.groupBox9.Controls.Add(this.textBox24);
            this.groupBox9.Controls.Add(this.button99);
            this.groupBox9.Controls.Add(this.textBox58);
            this.groupBox9.Controls.Add(this.button100);
            this.groupBox9.Controls.Add(this.textBox59);
            this.groupBox9.Controls.Add(this.button101);
            this.groupBox9.Controls.Add(this.textBox60);
            this.groupBox9.Controls.Add(this.textBox61);
            this.groupBox9.Controls.Add(this.button62);
            this.groupBox9.Location = new System.Drawing.Point(24, 378);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(490, 202);
            this.groupBox9.TabIndex = 59;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "NW CONTROL";
            // 
            // button119
            // 
            this.button119.Location = new System.Drawing.Point(6, 103);
            this.button119.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button119.Name = "button119";
            this.button119.Size = new System.Drawing.Size(147, 24);
            this.button119.TabIndex = 62;
            this.button119.Text = "CONNECT STATE";
            this.button119.UseVisualStyleBackColor = true;
            this.button119.Click += new System.EventHandler(this.button119_Click);
            // 
            // textBox78
            // 
            this.textBox78.Location = new System.Drawing.Point(157, 103);
            this.textBox78.Name = "textBox78";
            this.textBox78.Size = new System.Drawing.Size(310, 21);
            this.textBox78.TabIndex = 63;
            this.textBox78.Text = "AT+CGATT?";
            // 
            // button86
            // 
            this.button86.Location = new System.Drawing.Point(6, 19);
            this.button86.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button86.Name = "button86";
            this.button86.Size = new System.Drawing.Size(147, 24);
            this.button86.TabIndex = 48;
            this.button86.Text = "DATA REBOOT";
            this.button86.UseVisualStyleBackColor = true;
            this.button86.Click += new System.EventHandler(this.button86_Click);
            // 
            // textBox24
            // 
            this.textBox24.Location = new System.Drawing.Point(157, 19);
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new System.Drawing.Size(310, 21);
            this.textBox24.TabIndex = 49;
            this.textBox24.Text = "AT+NRB";
            // 
            // button99
            // 
            this.button99.Location = new System.Drawing.Point(5, 138);
            this.button99.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button99.Name = "button99";
            this.button99.Size = new System.Drawing.Size(147, 24);
            this.button99.TabIndex = 54;
            this.button99.Text = "NW ATTACH";
            this.button99.UseVisualStyleBackColor = true;
            this.button99.Click += new System.EventHandler(this.button99_Click);
            // 
            // textBox58
            // 
            this.textBox58.Location = new System.Drawing.Point(156, 138);
            this.textBox58.Name = "textBox58";
            this.textBox58.Size = new System.Drawing.Size(310, 21);
            this.textBox58.TabIndex = 55;
            this.textBox58.Text = "AT+CEREG=3";
            // 
            // button100
            // 
            this.button100.Location = new System.Drawing.Point(6, 47);
            this.button100.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button100.Name = "button100";
            this.button100.Size = new System.Drawing.Size(147, 24);
            this.button100.TabIndex = 56;
            this.button100.Text = "DATA CONNECT";
            this.button100.UseVisualStyleBackColor = true;
            this.button100.Click += new System.EventHandler(this.button100_Click);
            // 
            // textBox59
            // 
            this.textBox59.Location = new System.Drawing.Point(157, 47);
            this.textBox59.Name = "textBox59";
            this.textBox59.Size = new System.Drawing.Size(310, 21);
            this.textBox59.TabIndex = 57;
            this.textBox59.Text = "AT+CGATT=1";
            // 
            // button101
            // 
            this.button101.Location = new System.Drawing.Point(6, 75);
            this.button101.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button101.Name = "button101";
            this.button101.Size = new System.Drawing.Size(147, 24);
            this.button101.TabIndex = 58;
            this.button101.Text = "DATA DISCONNECT";
            this.button101.UseVisualStyleBackColor = true;
            this.button101.Click += new System.EventHandler(this.button101_Click);
            // 
            // textBox60
            // 
            this.textBox60.Location = new System.Drawing.Point(157, 75);
            this.textBox60.Name = "textBox60";
            this.textBox60.Size = new System.Drawing.Size(310, 21);
            this.textBox60.TabIndex = 59;
            this.textBox60.Text = "AT+CGATT=0";
            // 
            // textBox61
            // 
            this.textBox61.Location = new System.Drawing.Point(157, 166);
            this.textBox61.Name = "textBox61";
            this.textBox61.Size = new System.Drawing.Size(310, 21);
            this.textBox61.TabIndex = 61;
            this.textBox61.Text = "AT+CEREG?";
            // 
            // button62
            // 
            this.button62.Location = new System.Drawing.Point(6, 166);
            this.button62.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button62.Name = "button62";
            this.button62.Size = new System.Drawing.Size(147, 24);
            this.button62.TabIndex = 60;
            this.button62.Text = "NW STATUS";
            this.button62.UseVisualStyleBackColor = true;
            this.button62.Click += new System.EventHandler(this.button62_Click_1);
            // 
            // gbDeviceLog
            // 
            this.gbDeviceLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDeviceLog.Controls.Add(this.listView3);
            this.gbDeviceLog.Location = new System.Drawing.Point(532, 45);
            this.gbDeviceLog.Name = "gbDeviceLog";
            this.gbDeviceLog.Size = new System.Drawing.Size(635, 668);
            this.gbDeviceLog.TabIndex = 44;
            this.gbDeviceLog.TabStop = false;
            this.gbDeviceLog.Text = "AT command Message";
            // 
            // listView3
            // 
            this.listView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView3.HideSelection = false;
            this.listView3.Location = new System.Drawing.Point(3, 17);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(629, 648);
            this.listView3.TabIndex = 23;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.SelectedIndexChanged += new System.EventHandler(this.listView3_SelectedIndexChanged);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.label47);
            this.groupBox10.Controls.Add(this.label46);
            this.groupBox10.Controls.Add(this.label45);
            this.groupBox10.Controls.Add(this.label44);
            this.groupBox10.Controls.Add(this.lbModemVer);
            this.groupBox10.Controls.Add(this.textBox89);
            this.groupBox10.Controls.Add(this.lbIccid);
            this.groupBox10.Controls.Add(this.textBox87);
            this.groupBox10.Controls.Add(this.textBox86);
            this.groupBox10.Controls.Add(this.button87);
            this.groupBox10.Controls.Add(this.textBox85);
            this.groupBox10.Controls.Add(this.textBox57);
            this.groupBox10.Controls.Add(this.textBox40);
            this.groupBox10.Controls.Add(this.textBox38);
            this.groupBox10.Controls.Add(this.textBox33);
            this.groupBox10.Controls.Add(this.button71);
            this.groupBox10.Controls.Add(this.button83);
            this.groupBox10.Controls.Add(this.textBox44);
            this.groupBox10.Controls.Add(this.button88);
            this.groupBox10.Controls.Add(this.textBox45);
            this.groupBox10.Controls.Add(this.textBox46);
            this.groupBox10.Controls.Add(this.textBox47);
            this.groupBox10.Controls.Add(this.textBox48);
            this.groupBox10.Controls.Add(this.textBox49);
            this.groupBox10.Controls.Add(this.button89);
            this.groupBox10.Controls.Add(this.button90);
            this.groupBox10.Controls.Add(this.button91);
            this.groupBox10.Location = new System.Drawing.Point(24, 18);
            this.groupBox10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox10.Size = new System.Drawing.Size(490, 351);
            this.groupBox10.TabIndex = 40;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "MODULE INFORMATION";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(54, 290);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(97, 12);
            this.label47.TabIndex = 71;
            this.label47.Text = "버전 응답 메시지";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(56, 236);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(98, 12);
            this.label46.TabIndex = 70;
            this.label46.Text = "IMEI 응답 메시지";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(47, 178);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(105, 12);
            this.label45.TabIndex = 69;
            this.label45.Text = "ICCID 응답 메시지";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(55, 122);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(98, 12);
            this.label44.TabIndex = 68;
            this.label44.Text = "IMSI 응답 메시지";
            // 
            // lbModemVer
            // 
            this.lbModemVer.Location = new System.Drawing.Point(277, 262);
            this.lbModemVer.Name = "lbModemVer";
            this.lbModemVer.Size = new System.Drawing.Size(190, 21);
            this.lbModemVer.TabIndex = 67;
            // 
            // textBox89
            // 
            this.textBox89.Location = new System.Drawing.Point(277, 206);
            this.textBox89.Name = "textBox89";
            this.textBox89.Size = new System.Drawing.Size(190, 21);
            this.textBox89.TabIndex = 66;
            // 
            // lbIccid
            // 
            this.lbIccid.Location = new System.Drawing.Point(277, 150);
            this.lbIccid.Name = "lbIccid";
            this.lbIccid.Size = new System.Drawing.Size(190, 21);
            this.lbIccid.TabIndex = 65;
            // 
            // textBox87
            // 
            this.textBox87.Location = new System.Drawing.Point(277, 93);
            this.textBox87.Name = "textBox87";
            this.textBox87.Size = new System.Drawing.Size(190, 21);
            this.textBox87.TabIndex = 64;
            // 
            // textBox86
            // 
            this.textBox86.Location = new System.Drawing.Point(277, 65);
            this.textBox86.Name = "textBox86";
            this.textBox86.Size = new System.Drawing.Size(190, 21);
            this.textBox86.TabIndex = 63;
            // 
            // button87
            // 
            this.button87.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button87.Location = new System.Drawing.Point(132, 316);
            this.button87.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button87.Name = "button87";
            this.button87.Size = new System.Drawing.Size(156, 24);
            this.button87.TabIndex = 43;
            this.button87.Text = "모듈 정보 조회";
            this.button87.UseVisualStyleBackColor = true;
            this.button87.Click += new System.EventHandler(this.button87_Click);
            // 
            // textBox85
            // 
            this.textBox85.Location = new System.Drawing.Point(277, 36);
            this.textBox85.Name = "textBox85";
            this.textBox85.Size = new System.Drawing.Size(190, 21);
            this.textBox85.TabIndex = 62;
            // 
            // textBox57
            // 
            this.textBox57.Location = new System.Drawing.Point(158, 287);
            this.textBox57.Name = "textBox57";
            this.textBox57.Size = new System.Drawing.Size(114, 21);
            this.textBox57.TabIndex = 53;
            // 
            // textBox40
            // 
            this.textBox40.Location = new System.Drawing.Point(158, 233);
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new System.Drawing.Size(114, 21);
            this.textBox40.TabIndex = 52;
            this.textBox40.Text = "+CGSN:";
            // 
            // textBox38
            // 
            this.textBox38.Location = new System.Drawing.Point(158, 175);
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new System.Drawing.Size(114, 21);
            this.textBox38.TabIndex = 51;
            this.textBox38.Text = "+MUICCID:";
            // 
            // textBox33
            // 
            this.textBox33.Location = new System.Drawing.Point(158, 119);
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new System.Drawing.Size(114, 21);
            this.textBox33.TabIndex = 50;
            // 
            // button71
            // 
            this.button71.Location = new System.Drawing.Point(7, 148);
            this.button71.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button71.Name = "button71";
            this.button71.Size = new System.Drawing.Size(147, 24);
            this.button71.TabIndex = 40;
            this.button71.Text = "ICCID";
            this.button71.UseVisualStyleBackColor = true;
            this.button71.Click += new System.EventHandler(this.button71_Click);
            // 
            // button83
            // 
            this.button83.Location = new System.Drawing.Point(7, 33);
            this.button83.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button83.Name = "button83";
            this.button83.Size = new System.Drawing.Size(147, 24);
            this.button83.TabIndex = 39;
            this.button83.Text = "제조사";
            this.button83.UseVisualStyleBackColor = true;
            this.button83.Click += new System.EventHandler(this.button83_Click);
            // 
            // textBox44
            // 
            this.textBox44.Location = new System.Drawing.Point(158, 260);
            this.textBox44.Name = "textBox44";
            this.textBox44.Size = new System.Drawing.Size(114, 21);
            this.textBox44.TabIndex = 22;
            this.textBox44.Text = "AT+CGMR";
            // 
            // button88
            // 
            this.button88.Location = new System.Drawing.Point(7, 260);
            this.button88.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button88.Name = "button88";
            this.button88.Size = new System.Drawing.Size(147, 24);
            this.button88.TabIndex = 21;
            this.button88.Text = "FW VERSION";
            this.button88.UseVisualStyleBackColor = true;
            this.button88.Click += new System.EventHandler(this.button88_Click);
            // 
            // textBox45
            // 
            this.textBox45.Location = new System.Drawing.Point(157, 148);
            this.textBox45.Name = "textBox45";
            this.textBox45.Size = new System.Drawing.Size(114, 21);
            this.textBox45.TabIndex = 20;
            this.textBox45.Text = "AT+MUICCID";
            // 
            // textBox46
            // 
            this.textBox46.Location = new System.Drawing.Point(158, 91);
            this.textBox46.Name = "textBox46";
            this.textBox46.Size = new System.Drawing.Size(114, 21);
            this.textBox46.TabIndex = 19;
            this.textBox46.Text = "AT+CIMI";
            // 
            // textBox47
            // 
            this.textBox47.Location = new System.Drawing.Point(157, 63);
            this.textBox47.Name = "textBox47";
            this.textBox47.Size = new System.Drawing.Size(114, 21);
            this.textBox47.TabIndex = 18;
            this.textBox47.Text = "AT+CGMM";
            // 
            // textBox48
            // 
            this.textBox48.Location = new System.Drawing.Point(157, 33);
            this.textBox48.Name = "textBox48";
            this.textBox48.Size = new System.Drawing.Size(114, 21);
            this.textBox48.TabIndex = 17;
            this.textBox48.Text = "AT+CGMI";
            // 
            // textBox49
            // 
            this.textBox49.Location = new System.Drawing.Point(158, 204);
            this.textBox49.Name = "textBox49";
            this.textBox49.Size = new System.Drawing.Size(114, 21);
            this.textBox49.TabIndex = 15;
            this.textBox49.Text = "AT+CGSN=1";
            // 
            // button89
            // 
            this.button89.Location = new System.Drawing.Point(7, 204);
            this.button89.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button89.Name = "button89";
            this.button89.Size = new System.Drawing.Size(147, 24);
            this.button89.TabIndex = 14;
            this.button89.Text = "IMEI";
            this.button89.UseVisualStyleBackColor = true;
            this.button89.Click += new System.EventHandler(this.button89_Click);
            // 
            // button90
            // 
            this.button90.Location = new System.Drawing.Point(7, 91);
            this.button90.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button90.Name = "button90";
            this.button90.Size = new System.Drawing.Size(147, 24);
            this.button90.TabIndex = 10;
            this.button90.Text = "IMSI";
            this.button90.UseVisualStyleBackColor = true;
            this.button90.Click += new System.EventHandler(this.button90_Click);
            // 
            // button91
            // 
            this.button91.Location = new System.Drawing.Point(6, 63);
            this.button91.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button91.Name = "button91";
            this.button91.Size = new System.Drawing.Size(147, 24);
            this.button91.TabIndex = 8;
            this.button91.Text = "모델";
            this.button91.UseVisualStyleBackColor = true;
            this.button91.Click += new System.EventHandler(this.button91_Click);
            // 
            // tabModule
            // 
            this.tabModule.Controls.Add(this.groupBox28);
            this.tabModule.Controls.Add(this.groupBox8);
            this.tabModule.Controls.Add(this.groupBox27);
            this.tabModule.Controls.Add(this.groupBox26);
            this.tabModule.Controls.Add(this.groupBox25);
            this.tabModule.Location = new System.Drawing.Point(4, 22);
            this.tabModule.Name = "tabModule";
            this.tabModule.Size = new System.Drawing.Size(1172, 732);
            this.tabModule.TabIndex = 10;
            this.tabModule.Text = " 디바이스 ";
            this.tabModule.UseVisualStyleBackColor = true;
            // 
            // groupBox28
            // 
            this.groupBox28.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox28.Controls.Add(this.listView11);
            this.groupBox28.Location = new System.Drawing.Point(539, 8);
            this.groupBox28.Name = "groupBox28";
            this.groupBox28.Size = new System.Drawing.Size(618, 720);
            this.groupBox28.TabIndex = 84;
            this.groupBox28.TabStop = false;
            this.groupBox28.Text = "Device Message";
            // 
            // listView11
            // 
            this.listView11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView11.HideSelection = false;
            this.listView11.Location = new System.Drawing.Point(3, 17);
            this.listView11.Name = "listView11";
            this.listView11.Size = new System.Drawing.Size(612, 700);
            this.listView11.TabIndex = 23;
            this.listView11.UseCompatibleStateImageBehavior = false;
            this.listView11.SelectedIndexChanged += new System.EventHandler(this.listView11_SelectedIndexChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.comboBox5);
            this.groupBox8.Controls.Add(this.label55);
            this.groupBox8.Controls.Add(this.label54);
            this.groupBox8.Controls.Add(this.textBox6);
            this.groupBox8.Controls.Add(this.button55);
            this.groupBox8.Controls.Add(this.button57);
            this.groupBox8.Controls.Add(this.button58);
            this.groupBox8.Controls.Add(this.button59);
            this.groupBox8.Location = new System.Drawing.Point(13, 421);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(472, 172);
            this.groupBox8.TabIndex = 88;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "oneM2M Firmware";
            // 
            // comboBox5
            // 
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Items.AddRange(new object[] {
            "LTE(1)",
            "WiFi(2)",
            "NB-IoT(3)",
            "Cat M1(4)"});
            this.comboBox5.Location = new System.Drawing.Point(162, 114);
            this.comboBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(190, 20);
            this.comboBox5.TabIndex = 78;
            this.comboBox5.Text = "LTE(1)";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(47, 114);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(96, 12);
            this.label55.TabIndex = 77;
            this.label55.Text = "Network Type : ";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(90, 87);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(54, 12);
            this.label54.TabIndex = 76;
            this.label54.Text = "Cell ID : ";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(162, 84);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(190, 21);
            this.textBox6.TabIndex = 75;
            this.textBox6.Text = "51713297";
            // 
            // button55
            // 
            this.button55.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button55.Location = new System.Drawing.Point(24, 21);
            this.button55.Name = "button55";
            this.button55.Size = new System.Drawing.Size(119, 22);
            this.button55.TabIndex = 49;
            this.button55.Text = "DeviceFW 보고";
            this.button55.UseVisualStyleBackColor = true;
            this.button55.Click += new System.EventHandler(this.button55_Click_1);
            // 
            // button57
            // 
            this.button57.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button57.Location = new System.Drawing.Point(234, 21);
            this.button57.Name = "button57";
            this.button57.Size = new System.Drawing.Size(118, 22);
            this.button57.TabIndex = 23;
            this.button57.Text = "DeviceFW 조회";
            this.button57.UseVisualStyleBackColor = true;
            this.button57.Click += new System.EventHandler(this.button57_Click_1);
            // 
            // button58
            // 
            this.button58.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button58.Location = new System.Drawing.Point(25, 49);
            this.button58.Name = "button58";
            this.button58.Size = new System.Drawing.Size(118, 22);
            this.button58.TabIndex = 40;
            this.button58.Text = "ModemFW 보고";
            this.button58.UseVisualStyleBackColor = true;
            this.button58.Click += new System.EventHandler(this.button58_Click_1);
            // 
            // button59
            // 
            this.button59.Location = new System.Drawing.Point(234, 49);
            this.button59.Name = "button59";
            this.button59.Size = new System.Drawing.Size(118, 23);
            this.button59.TabIndex = 24;
            this.button59.Text = "ModemFW 조회";
            this.button59.UseVisualStyleBackColor = true;
            this.button59.Click += new System.EventHandler(this.button59_Click_1);
            // 
            // groupBox27
            // 
            this.groupBox27.Controls.Add(this.label12);
            this.groupBox27.Controls.Add(this.label6);
            this.groupBox27.Controls.Add(this.button44);
            this.groupBox27.Location = new System.Drawing.Point(13, 25);
            this.groupBox27.Name = "groupBox27";
            this.groupBox27.Size = new System.Drawing.Size(472, 55);
            this.groupBox27.TabIndex = 84;
            this.groupBox27.TabStop = false;
            this.groupBox27.Text = "oneM2M 인증";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(226, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(9, 12);
            this.label12.TabIndex = 45;
            this.label12.Text = ".";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(140, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 12);
            this.label6.TabIndex = 44;
            this.label6.Text = "CSR Name : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button44
            // 
            this.button44.Location = new System.Drawing.Point(17, 20);
            this.button44.Name = "button44";
            this.button44.Size = new System.Drawing.Size(97, 29);
            this.button44.TabIndex = 33;
            this.button44.Text = "MEF 인증";
            this.button44.UseVisualStyleBackColor = true;
            this.button44.Click += new System.EventHandler(this.button44_Click_1);
            // 
            // groupBox26
            // 
            this.groupBox26.Controls.Add(this.label53);
            this.groupBox26.Controls.Add(this.textBox5);
            this.groupBox26.Controls.Add(this.textBox3);
            this.groupBox26.Controls.Add(this.textBox4);
            this.groupBox26.Controls.Add(this.button37);
            this.groupBox26.Controls.Add(this.button46);
            this.groupBox26.Controls.Add(this.button47);
            this.groupBox26.Controls.Add(this.button48);
            this.groupBox26.Controls.Add(this.button49);
            this.groupBox26.Location = new System.Drawing.Point(13, 86);
            this.groupBox26.Name = "groupBox26";
            this.groupBox26.Size = new System.Drawing.Size(472, 158);
            this.groupBox26.TabIndex = 85;
            this.groupBox26.TabStop = false;
            this.groupBox26.Text = "remoteCSE";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(46, 132);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(84, 12);
            this.label53.TabIndex = 72;
            this.label53.Text = "IP 응답 메시지";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(149, 129);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(114, 21);
            this.textBox5.TabIndex = 71;
            this.textBox5.Text = "+CGPADDR: 2,";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(270, 108);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(190, 21);
            this.textBox3.TabIndex = 70;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(149, 106);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(114, 21);
            this.textBox4.TabIndex = 69;
            this.textBox4.Text = "AT+CGPADDR=2";
            // 
            // button37
            // 
            this.button37.Location = new System.Drawing.Point(24, 106);
            this.button37.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button37.Name = "button37";
            this.button37.Size = new System.Drawing.Size(119, 24);
            this.button37.TabIndex = 68;
            this.button37.Text = "DEVICE IP";
            this.button37.UseVisualStyleBackColor = true;
            this.button37.Click += new System.EventHandler(this.button37_Click_1);
            // 
            // button46
            // 
            this.button46.Location = new System.Drawing.Point(24, 46);
            this.button46.Name = "button46";
            this.button46.Size = new System.Drawing.Size(118, 20);
            this.button46.TabIndex = 22;
            this.button46.Text = "CSR 조회";
            this.button46.UseVisualStyleBackColor = true;
            this.button46.Click += new System.EventHandler(this.button46_Click_1);
            // 
            // button47
            // 
            this.button47.Location = new System.Drawing.Point(225, 46);
            this.button47.Name = "button47";
            this.button47.Size = new System.Drawing.Size(118, 20);
            this.button47.TabIndex = 25;
            this.button47.Text = "CSR 생성";
            this.button47.UseVisualStyleBackColor = true;
            this.button47.Click += new System.EventHandler(this.button47_Click_1);
            // 
            // button48
            // 
            this.button48.Location = new System.Drawing.Point(225, 72);
            this.button48.Name = "button48";
            this.button48.Size = new System.Drawing.Size(119, 20);
            this.button48.TabIndex = 26;
            this.button48.Text = "CSR 삭제";
            this.button48.UseVisualStyleBackColor = true;
            this.button48.Click += new System.EventHandler(this.button48_Click_1);
            // 
            // button49
            // 
            this.button49.Location = new System.Drawing.Point(24, 72);
            this.button49.Name = "button49";
            this.button49.Size = new System.Drawing.Size(119, 20);
            this.button49.TabIndex = 32;
            this.button49.Text = "CSR 수정";
            this.button49.UseVisualStyleBackColor = true;
            this.button49.Click += new System.EventHandler(this.button49_Click_1);
            // 
            // groupBox25
            // 
            this.groupBox25.Controls.Add(this.label1);
            this.groupBox25.Controls.Add(this.button75);
            this.groupBox25.Location = new System.Drawing.Point(13, 250);
            this.groupBox25.Name = "groupBox25";
            this.groupBox25.Size = new System.Drawing.Size(472, 157);
            this.groupBox25.TabIndex = 86;
            this.groupBox25.TabStop = false;
            this.groupBox25.Text = "oneM2M DATA";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(160, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(9, 12);
            this.label1.TabIndex = 43;
            this.label1.Text = ".";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button75
            // 
            this.button75.Location = new System.Drawing.Point(23, 129);
            this.button75.Name = "button75";
            this.button75.Size = new System.Drawing.Size(121, 20);
            this.button75.TabIndex = 42;
            this.button75.Text = "데이터전송 (서버)";
            this.button75.UseVisualStyleBackColor = true;
            this.button75.Click += new System.EventHandler(this.button75_Click_1);
            // 
            // tabServer
            // 
            this.tabServer.Controls.Add(this.label23);
            this.tabServer.Controls.Add(this.label25);
            this.tabServer.Controls.Add(this.groupBox12);
            this.tabServer.Controls.Add(this.groupBox14);
            this.tabServer.Controls.Add(this.groupBox15);
            this.tabServer.Controls.Add(this.btnMEFAuth);
            this.tabServer.Controls.Add(this.label28);
            this.tabServer.Controls.Add(this.tbSvcSvrCd);
            this.tabServer.Controls.Add(this.tbSvcSvrNum);
            this.tabServer.Controls.Add(this.label33);
            this.tabServer.Location = new System.Drawing.Point(4, 22);
            this.tabServer.Name = "tabServer";
            this.tabServer.Size = new System.Drawing.Size(1172, 732);
            this.tabServer.TabIndex = 6;
            this.tabServer.Text = " 서비스서버 ";
            this.tabServer.UseVisualStyleBackColor = true;
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(160, 99);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(224, 22);
            this.label23.TabIndex = 77;
            this.label23.Text = ".";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(53, 104);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(99, 12);
            this.label25.TabIndex = 76;
            this.label25.Text = "Server EntityID : ";
            // 
            // groupBox12
            // 
            this.groupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox12.Controls.Add(this.listView7);
            this.groupBox12.Location = new System.Drawing.Point(489, 24);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(673, 696);
            this.groupBox12.TabIndex = 49;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "SERVER INTERFACE";
            // 
            // listView7
            // 
            this.listView7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView7.HideSelection = false;
            this.listView7.Location = new System.Drawing.Point(3, 17);
            this.listView7.Name = "listView7";
            this.listView7.Size = new System.Drawing.Size(667, 676);
            this.listView7.TabIndex = 27;
            this.listView7.UseCompatibleStateImageBehavior = false;
            this.listView7.SelectedIndexChanged += new System.EventHandler(this.listView7_SelectedIndexChanged);
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.label2);
            this.groupBox14.Controls.Add(this.comboBox2);
            this.groupBox14.Controls.Add(this.label13);
            this.groupBox14.Controls.Add(this.button94);
            this.groupBox14.Controls.Add(this.lbmodemfwrver);
            this.groupBox14.Controls.Add(this.btnDeviceCheck);
            this.groupBox14.Controls.Add(this.lbdevicever);
            this.groupBox14.Controls.Add(this.label7);
            this.groupBox14.Controls.Add(this.label14);
            this.groupBox14.Location = new System.Drawing.Point(8, 249);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(461, 251);
            this.groupBox14.TabIndex = 47;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "oneM2M Device DATA";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(147, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(242, 22);
            this.label2.TabIndex = 87;
            this.label2.Text = ".";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "1. fwr-m2m_",
            "2. fwr-m2m_M",
            "3. fwr-m2m_M_"});
            this.comboBox2.Location = new System.Drawing.Point(152, 213);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(244, 20);
            this.comboBox2.TabIndex = 84;
            this.comboBox2.Text = "1. fwr-m2m_";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(15, 211);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(126, 22);
            this.label13.TabIndex = 83;
            this.label13.Text = "node name type";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button94
            // 
            this.button94.Location = new System.Drawing.Point(15, 140);
            this.button94.Name = "button94";
            this.button94.Size = new System.Drawing.Size(126, 23);
            this.button94.TabIndex = 79;
            this.button94.Text = "데이터 전송 (단말)";
            this.button94.UseVisualStyleBackColor = true;
            this.button94.Click += new System.EventHandler(this.button94_Click);
            // 
            // lbmodemfwrver
            // 
            this.lbmodemfwrver.Location = new System.Drawing.Point(210, 192);
            this.lbmodemfwrver.Name = "lbmodemfwrver";
            this.lbmodemfwrver.Size = new System.Drawing.Size(164, 16);
            this.lbmodemfwrver.TabIndex = 43;
            this.lbmodemfwrver.Text = ".";
            this.lbmodemfwrver.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDeviceCheck
            // 
            this.btnDeviceCheck.Location = new System.Drawing.Point(15, 171);
            this.btnDeviceCheck.Name = "btnDeviceCheck";
            this.btnDeviceCheck.Size = new System.Drawing.Size(126, 37);
            this.btnDeviceCheck.TabIndex = 40;
            this.btnDeviceCheck.Text = "펌웨어 버전";
            this.btnDeviceCheck.UseVisualStyleBackColor = true;
            this.btnDeviceCheck.Click += new System.EventHandler(this.btnDeviceCheck_Click);
            // 
            // lbdevicever
            // 
            this.lbdevicever.Location = new System.Drawing.Point(210, 171);
            this.lbdevicever.Name = "lbdevicever";
            this.lbdevicever.Size = new System.Drawing.Size(164, 16);
            this.lbdevicever.TabIndex = 45;
            this.lbdevicever.Text = ".";
            this.lbdevicever.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(147, 171);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 16);
            this.label7.TabIndex = 46;
            this.label7.Text = "device =";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(150, 190);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(60, 17);
            this.label14.TabIndex = 44;
            this.label14.Text = "modem =";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.tbSeverPort);
            this.groupBox15.Controls.Add(this.label15);
            this.groupBox15.Controls.Add(this.tbSeverIP);
            this.groupBox15.Controls.Add(this.label17);
            this.groupBox15.Controls.Add(this.btnDelRemoteCSE);
            this.groupBox15.Controls.Add(this.btnSetRemoteCSE);
            this.groupBox15.Controls.Add(this.btnGetRemoteCSE);
            this.groupBox15.Location = new System.Drawing.Point(11, 138);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(458, 105);
            this.groupBox15.TabIndex = 46;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "CSE";
            // 
            // tbSeverPort
            // 
            this.tbSeverPort.Location = new System.Drawing.Point(99, 74);
            this.tbSeverPort.Name = "tbSeverPort";
            this.tbSeverPort.Size = new System.Drawing.Size(205, 21);
            this.tbSeverPort.TabIndex = 14;
            this.tbSeverPort.Text = "8180";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(6, 73);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(94, 22);
            this.label15.TabIndex = 13;
            this.label15.Text = "서비스서버 port";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbSeverIP
            // 
            this.tbSeverIP.Location = new System.Drawing.Point(98, 49);
            this.tbSeverIP.Name = "tbSeverIP";
            this.tbSeverIP.Size = new System.Drawing.Size(206, 21);
            this.tbSeverIP.TabIndex = 12;
            this.tbSeverIP.Text = "http://172.17.224.57";
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(5, 48);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(94, 22);
            this.label17.TabIndex = 11;
            this.label17.Text = "서비스서버 IP";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDelRemoteCSE
            // 
            this.btnDelRemoteCSE.Location = new System.Drawing.Point(176, 20);
            this.btnDelRemoteCSE.Name = "btnDelRemoteCSE";
            this.btnDelRemoteCSE.Size = new System.Drawing.Size(118, 23);
            this.btnDelRemoteCSE.TabIndex = 2;
            this.btnDelRemoteCSE.Text = "CSR 삭제";
            this.btnDelRemoteCSE.UseVisualStyleBackColor = true;
            this.btnDelRemoteCSE.Click += new System.EventHandler(this.btnDelRemoteCSE_Click);
            // 
            // btnSetRemoteCSE
            // 
            this.btnSetRemoteCSE.Location = new System.Drawing.Point(323, 48);
            this.btnSetRemoteCSE.Name = "btnSetRemoteCSE";
            this.btnSetRemoteCSE.Size = new System.Drawing.Size(127, 46);
            this.btnSetRemoteCSE.TabIndex = 0;
            this.btnSetRemoteCSE.Text = "CSR 생성";
            this.btnSetRemoteCSE.UseVisualStyleBackColor = true;
            this.btnSetRemoteCSE.Click += new System.EventHandler(this.btnSetRemoteCSE_Click);
            // 
            // btnGetRemoteCSE
            // 
            this.btnGetRemoteCSE.Location = new System.Drawing.Point(7, 20);
            this.btnGetRemoteCSE.Name = "btnGetRemoteCSE";
            this.btnGetRemoteCSE.Size = new System.Drawing.Size(119, 23);
            this.btnGetRemoteCSE.TabIndex = 0;
            this.btnGetRemoteCSE.Text = "CSR 조회";
            this.btnGetRemoteCSE.UseVisualStyleBackColor = true;
            this.btnGetRemoteCSE.Click += new System.EventHandler(this.btnGetRemoteCSE_Click);
            // 
            // btnMEFAuth
            // 
            this.btnMEFAuth.Location = new System.Drawing.Point(273, 41);
            this.btnMEFAuth.Name = "btnMEFAuth";
            this.btnMEFAuth.Size = new System.Drawing.Size(110, 57);
            this.btnMEFAuth.TabIndex = 0;
            this.btnMEFAuth.Text = "MEF 인증";
            this.btnMEFAuth.UseVisualStyleBackColor = true;
            this.btnMEFAuth.Click += new System.EventHandler(this.btnMEFAuth_Click);
            // 
            // label28
            // 
            this.label28.Location = new System.Drawing.Point(25, 47);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(86, 16);
            this.label28.TabIndex = 6;
            this.label28.Text = "서버 SEQ";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbSvcSvrCd
            // 
            this.tbSvcSvrCd.Location = new System.Drawing.Point(117, 41);
            this.tbSvcSvrCd.Name = "tbSvcSvrCd";
            this.tbSvcSvrCd.Size = new System.Drawing.Size(100, 21);
            this.tbSvcSvrCd.TabIndex = 7;
            this.tbSvcSvrCd.Text = "111";
            // 
            // tbSvcSvrNum
            // 
            this.tbSvcSvrNum.Location = new System.Drawing.Point(117, 71);
            this.tbSvcSvrNum.Name = "tbSvcSvrNum";
            this.tbSvcSvrNum.Size = new System.Drawing.Size(100, 21);
            this.tbSvcSvrNum.TabIndex = 7;
            this.tbSvcSvrNum.Text = "1";
            // 
            // label33
            // 
            this.label33.Location = new System.Drawing.Point(25, 72);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(86, 16);
            this.label33.TabIndex = 6;
            this.label33.Text = "서버 NUM";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabLOG
            // 
            this.tabLOG.Controls.Add(this.listView10);
            this.tabLOG.Controls.Add(this.listView9);
            this.tabLOG.Controls.Add(this.listView8);
            this.tabLOG.Controls.Add(this.label49);
            this.tabLOG.Controls.Add(this.label48);
            this.tabLOG.Controls.Add(this.dateTimePicker1);
            this.tabLOG.Controls.Add(this.label18);
            this.tabLOG.Controls.Add(this.label16);
            this.tabLOG.Controls.Add(this.button127);
            this.tabLOG.Controls.Add(this.label27);
            this.tabLOG.Controls.Add(this.tBResultCode);
            this.tabLOG.Controls.Add(this.textBox94);
            this.tabLOG.Controls.Add(this.tbDeviceCTN);
            this.tabLOG.Controls.Add(this.textBox95);
            this.tabLOG.Controls.Add(this.button126);
            this.tabLOG.Controls.Add(this.button122);
            this.tabLOG.Controls.Add(this.label19);
            this.tabLOG.Controls.Add(this.label26);
            this.tabLOG.Controls.Add(this.label22);
            this.tabLOG.Controls.Add(this.button123);
            this.tabLOG.Controls.Add(this.comboBox1);
            this.tabLOG.Controls.Add(this.btnGetLogList);
            this.tabLOG.Controls.Add(this.button124);
            this.tabLOG.Controls.Add(this.label21);
            this.tabLOG.Location = new System.Drawing.Point(4, 22);
            this.tabLOG.Name = "tabLOG";
            this.tabLOG.Size = new System.Drawing.Size(1172, 732);
            this.tabLOG.TabIndex = 7;
            this.tabLOG.Text = " 플랫폼로그 검색 ";
            this.tabLOG.UseVisualStyleBackColor = true;
            // 
            // listView10
            // 
            this.listView10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView10.HideSelection = false;
            this.listView10.Location = new System.Drawing.Point(591, 344);
            this.listView10.Name = "listView10";
            this.listView10.Size = new System.Drawing.Size(571, 364);
            this.listView10.TabIndex = 79;
            this.listView10.UseCompatibleStateImageBehavior = false;
            this.listView10.SelectedIndexChanged += new System.EventHandler(this.listView10_SelectedIndexChanged);
            // 
            // listView9
            // 
            this.listView9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView9.HideSelection = false;
            this.listView9.Location = new System.Drawing.Point(591, 162);
            this.listView9.Name = "listView9";
            this.listView9.Size = new System.Drawing.Size(571, 145);
            this.listView9.TabIndex = 78;
            this.listView9.UseCompatibleStateImageBehavior = false;
            this.listView9.SelectedIndexChanged += new System.EventHandler(this.listView9_SelectedIndexChanged);
            // 
            // listView8
            // 
            this.listView8.HideSelection = false;
            this.listView8.Location = new System.Drawing.Point(7, 68);
            this.listView8.Name = "listView8";
            this.listView8.Size = new System.Drawing.Size(542, 640);
            this.listView8.TabIndex = 77;
            this.listView8.UseCompatibleStateImageBehavior = false;
            this.listView8.SelectedIndexChanged += new System.EventHandler(this.listView8_SelectedIndexChanged);
            // 
            // label49
            // 
            this.label49.Location = new System.Drawing.Point(530, 43);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(94, 22);
            this.label49.TabIndex = 76;
            this.label49.Text = "/";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label48
            // 
            this.label48.Location = new System.Drawing.Point(430, 43);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(94, 22);
            this.label48.TabIndex = 75;
            this.label48.Text = "CellID 정보 : ";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker1.Location = new System.Drawing.Point(768, 37);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(116, 21);
            this.dateTimePicker1.TabIndex = 74;
            this.dateTimePicker1.Value = new System.DateTime(2021, 3, 22, 9, 17, 8, 0);
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(56, 12);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(94, 22);
            this.label18.TabIndex = 73;
            this.label18.Text = "Platform 종류";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(55, 40);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(94, 22);
            this.label16.TabIndex = 72;
            this.label16.Text = "CTN";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button127
            // 
            this.button127.Location = new System.Drawing.Point(935, 9);
            this.button127.Name = "button127";
            this.button127.Size = new System.Drawing.Size(126, 23);
            this.button127.TabIndex = 70;
            this.button127.Text = "Server 로그 조회";
            this.button127.UseVisualStyleBackColor = true;
            this.button127.Click += new System.EventHandler(this.button127_Click);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(842, 323);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(37, 12);
            this.label27.TabIndex = 69;
            this.label27.Text = "LogID";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tBResultCode
            // 
            this.tBResultCode.Location = new System.Drawing.Point(710, 89);
            this.tBResultCode.Name = "tBResultCode";
            this.tBResultCode.Size = new System.Drawing.Size(88, 21);
            this.tBResultCode.TabIndex = 53;
            this.tBResultCode.Text = "20000000";
            this.tBResultCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox94
            // 
            this.textBox94.Location = new System.Drawing.Point(898, 319);
            this.textBox94.Name = "textBox94";
            this.textBox94.Size = new System.Drawing.Size(104, 21);
            this.textBox94.TabIndex = 67;
            this.textBox94.Text = "12345678";
            // 
            // tbDeviceCTN
            // 
            this.tbDeviceCTN.Location = new System.Drawing.Point(155, 40);
            this.tbDeviceCTN.Name = "tbDeviceCTN";
            this.tbDeviceCTN.Size = new System.Drawing.Size(114, 21);
            this.tbDeviceCTN.TabIndex = 40;
            this.tbDeviceCTN.Text = "01222991234";
            // 
            // textBox95
            // 
            this.textBox95.Location = new System.Drawing.Point(898, 135);
            this.textBox95.Name = "textBox95";
            this.textBox95.Size = new System.Drawing.Size(104, 21);
            this.textBox95.TabIndex = 64;
            this.textBox95.Text = "12345678";
            // 
            // button126
            // 
            this.button126.Location = new System.Drawing.Point(807, 91);
            this.button126.Name = "button126";
            this.button126.Size = new System.Drawing.Size(82, 19);
            this.button126.TabIndex = 52;
            this.button126.Text = "코드조회";
            this.button126.UseVisualStyleBackColor = true;
            this.button126.Click += new System.EventHandler(this.button126_Click);
            // 
            // button122
            // 
            this.button122.Location = new System.Drawing.Point(1008, 318);
            this.button122.Name = "button122";
            this.button122.Size = new System.Drawing.Size(121, 20);
            this.button122.TabIndex = 68;
            this.button122.Text = "상세 로그 조회";
            this.button122.UseVisualStyleBackColor = true;
            this.button122.Click += new System.EventHandler(this.button122_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(627, 96);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(82, 12);
            this.label19.TabIndex = 51;
            this.label19.Text = "ResultCode : ";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(846, 139);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(37, 12);
            this.label26.TabIndex = 66;
            this.label26.Text = "LogID";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label22.Location = new System.Drawing.Point(597, 322);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(28, 12);
            this.label22.TabIndex = 60;
            this.label22.Text = "ID : ";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button123
            // 
            this.button123.Location = new System.Drawing.Point(1008, 134);
            this.button123.Name = "button123";
            this.button123.Size = new System.Drawing.Size(121, 20);
            this.button123.TabIndex = 65;
            this.button123.Text = "LOGID 로그 조회";
            this.button123.UseVisualStyleBackColor = true;
            this.button123.Click += new System.EventHandler(this.button123_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "oneM2M",
            "LwM2M",
            "미지원"});
            this.comboBox1.Location = new System.Drawing.Point(156, 12);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(114, 20);
            this.comboBox1.TabIndex = 30;
            this.comboBox1.Text = "oneM2M";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnGetLogList
            // 
            this.btnGetLogList.Location = new System.Drawing.Point(935, 38);
            this.btnGetLogList.Name = "btnGetLogList";
            this.btnGetLogList.Size = new System.Drawing.Size(132, 23);
            this.btnGetLogList.TabIndex = 43;
            this.btnGetLogList.Text = "Device 로그 조회";
            this.btnGetLogList.UseVisualStyleBackColor = true;
            this.btnGetLogList.Click += new System.EventHandler(this.btnGetLogList_Click);
            // 
            // button124
            // 
            this.button124.Location = new System.Drawing.Point(281, 43);
            this.button124.Name = "button124";
            this.button124.Size = new System.Drawing.Size(117, 19);
            this.button124.TabIndex = 55;
            this.button124.Text = "Device 정보 조회";
            this.button124.UseVisualStyleBackColor = true;
            this.button124.Click += new System.EventHandler(this.button124_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label21.Location = new System.Drawing.Point(593, 139);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(84, 12);
            this.label21.TabIndex = 59;
            this.label21.Text = "서버로그  ID : ";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // webpage
            // 
            this.webpage.Controls.Add(this.webBrowser1);
            this.webpage.Location = new System.Drawing.Point(4, 22);
            this.webpage.Name = "webpage";
            this.webpage.Size = new System.Drawing.Size(1172, 732);
            this.webpage.TabIndex = 9;
            this.webpage.Text = " 관리자 웹페이지 ";
            this.webpage.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1172, 732);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("", System.UriKind.Relative);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // button81
            // 
            this.button81.Location = new System.Drawing.Point(1098, 11);
            this.button81.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button81.Name = "button81";
            this.button81.Size = new System.Drawing.Size(71, 24);
            this.button81.TabIndex = 41;
            this.button81.Text = "엑셀 쓰기";
            this.button81.UseVisualStyleBackColor = true;
            this.button81.Click += new System.EventHandler(this.button63_Click);
            // 
            // button72
            // 
            this.button72.Location = new System.Drawing.Point(1020, 11);
            this.button72.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button72.Name = "button72";
            this.button72.Size = new System.Drawing.Size(70, 24);
            this.button72.TabIndex = 42;
            this.button72.Text = "엑셀 읽기";
            this.button72.UseVisualStyleBackColor = true;
            this.button72.Click += new System.EventHandler(this.button62_Click);
            // 
            // lbActionState
            // 
            this.lbActionState.AutoSize = true;
            this.lbActionState.Location = new System.Drawing.Point(294, 16);
            this.lbActionState.Name = "lbActionState";
            this.lbActionState.Size = new System.Drawing.Size(43, 12);
            this.lbActionState.TabIndex = 58;
            this.lbActionState.Text = "closed";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(732, 15);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(37, 12);
            this.label30.TabIndex = 78;
            this.label30.Text = "버전 :";
            // 
            // tBoxDeviceVer
            // 
            this.tBoxDeviceVer.Location = new System.Drawing.Point(775, 9);
            this.tBoxDeviceVer.Name = "tBoxDeviceVer";
            this.tBoxDeviceVer.Size = new System.Drawing.Size(81, 21);
            this.tBoxDeviceVer.TabIndex = 77;
            this.tBoxDeviceVer.Text = "1.0.0";
            this.tBoxDeviceVer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(389, 15);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(49, 12);
            this.label34.TabIndex = 74;
            this.label34.Text = "모델명 :";
            // 
            // tbSvcCd
            // 
            this.tbSvcCd.Location = new System.Drawing.Point(936, 12);
            this.tbSvcCd.Name = "tbSvcCd";
            this.tbSvcCd.Size = new System.Drawing.Size(67, 21);
            this.tbSvcCd.TabIndex = 34;
            this.tbSvcCd.Text = "CATO";
            this.tbSvcCd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbSvcCd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSvcCd_KeyDown);
            // 
            // tBoxDeviceSN
            // 
            this.tBoxDeviceSN.Location = new System.Drawing.Point(622, 11);
            this.tBoxDeviceSN.Name = "tBoxDeviceSN";
            this.tBoxDeviceSN.Size = new System.Drawing.Size(100, 21);
            this.tBoxDeviceSN.TabIndex = 59;
            this.tBoxDeviceSN.Text = "123456";
            this.tBoxDeviceSN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label31
            // 
            this.label31.Location = new System.Drawing.Point(844, 13);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(86, 16);
            this.label31.TabIndex = 6;
            this.label31.Text = "서비스코드";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tBoxDeviceModel
            // 
            this.tBoxDeviceModel.Location = new System.Drawing.Point(447, 12);
            this.tBoxDeviceModel.Name = "tBoxDeviceModel";
            this.tBoxDeviceModel.Size = new System.Drawing.Size(98, 21);
            this.tBoxDeviceModel.TabIndex = 36;
            this.tBoxDeviceModel.Text = "LWEMG";
            this.tBoxDeviceModel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(556, 15);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(61, 12);
            this.label32.TabIndex = 76;
            this.label32.Text = "일련번호 :";
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1195, 804);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(1920, 1066);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1200, 843);
            this.Name = "Form1";
            this.Text = "LGU+ oneM2M";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabCOM.ResumeLayout(false);
            this.tabCOM.PerformLayout();
            this.groupBox23.ResumeLayout(false);
            this.groupBox23.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.gbDeviceLog.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.tabModule.ResumeLayout(false);
            this.groupBox28.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox27.ResumeLayout(false);
            this.groupBox27.PerformLayout();
            this.groupBox26.ResumeLayout(false);
            this.groupBox26.PerformLayout();
            this.groupBox25.ResumeLayout(false);
            this.groupBox25.PerformLayout();
            this.tabServer.ResumeLayout(false);
            this.tabServer.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.tabLOG.ResumeLayout(false);
            this.tabLOG.PerformLayout();
            this.webpage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ComboBox cBoxBaudRate;
        private System.Windows.Forms.ComboBox cBoxCOMPORT;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button72;
        private System.Windows.Forms.Button button81;
        private System.Windows.Forms.TextBox tBoxDeviceModel;
        private System.Windows.Forms.TextBox tbSvcCd;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label lbActionState;
        private System.Windows.Forms.TextBox tBoxDeviceSN;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox tBoxDeviceVer;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.CheckBox cbDTR;
        private System.Windows.Forms.CheckBox cbRTS;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCOM;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox23;
        private System.Windows.Forms.Button button128;
        private System.Windows.Forms.TextBox textBox81;
        private System.Windows.Forms.Button button121;
        private System.Windows.Forms.TextBox textBox80;
        private System.Windows.Forms.Button button120;
        private System.Windows.Forms.TextBox textBox79;
        private System.Windows.Forms.Button button63;
        private System.Windows.Forms.TextBox textBox64;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button button119;
        private System.Windows.Forms.TextBox textBox78;
        private System.Windows.Forms.Button button86;
        private System.Windows.Forms.TextBox textBox24;
        private System.Windows.Forms.Button button99;
        private System.Windows.Forms.TextBox textBox58;
        private System.Windows.Forms.Button button100;
        private System.Windows.Forms.TextBox textBox59;
        private System.Windows.Forms.Button button101;
        private System.Windows.Forms.TextBox textBox60;
        private System.Windows.Forms.TextBox textBox61;
        private System.Windows.Forms.Button button62;
        private System.Windows.Forms.GroupBox gbDeviceLog;
        private System.Windows.Forms.ListView listView3;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox lbModemVer;
        private System.Windows.Forms.TextBox textBox89;
        private System.Windows.Forms.TextBox lbIccid;
        private System.Windows.Forms.TextBox textBox87;
        private System.Windows.Forms.TextBox textBox86;
        private System.Windows.Forms.Button button87;
        private System.Windows.Forms.TextBox textBox85;
        private System.Windows.Forms.TextBox textBox57;
        private System.Windows.Forms.TextBox textBox40;
        private System.Windows.Forms.TextBox textBox38;
        private System.Windows.Forms.TextBox textBox33;
        private System.Windows.Forms.Button button71;
        private System.Windows.Forms.Button button83;
        private System.Windows.Forms.TextBox textBox44;
        private System.Windows.Forms.Button button88;
        private System.Windows.Forms.TextBox textBox45;
        private System.Windows.Forms.TextBox textBox46;
        private System.Windows.Forms.TextBox textBox47;
        private System.Windows.Forms.TextBox textBox48;
        private System.Windows.Forms.TextBox textBox49;
        private System.Windows.Forms.Button button89;
        private System.Windows.Forms.Button button90;
        private System.Windows.Forms.Button button91;
        private System.Windows.Forms.TabPage tabModule;
        private System.Windows.Forms.GroupBox groupBox28;
        private System.Windows.Forms.ListView listView11;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Button button55;
        private System.Windows.Forms.Button button57;
        private System.Windows.Forms.Button button58;
        private System.Windows.Forms.Button button59;
        private System.Windows.Forms.GroupBox groupBox27;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button44;
        private System.Windows.Forms.GroupBox groupBox26;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button button37;
        private System.Windows.Forms.Button button46;
        private System.Windows.Forms.Button button47;
        private System.Windows.Forms.Button button48;
        private System.Windows.Forms.Button button49;
        private System.Windows.Forms.GroupBox groupBox25;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button75;
        private System.Windows.Forms.TabPage tabServer;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.ListView listView7;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button94;
        private System.Windows.Forms.Label lbmodemfwrver;
        private System.Windows.Forms.Button btnDeviceCheck;
        private System.Windows.Forms.Label lbdevicever;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.TextBox tbSeverPort;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbSeverIP;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnDelRemoteCSE;
        private System.Windows.Forms.Button btnSetRemoteCSE;
        private System.Windows.Forms.Button btnGetRemoteCSE;
        private System.Windows.Forms.Button btnMEFAuth;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox tbSvcSvrCd;
        private System.Windows.Forms.TextBox tbSvcSvrNum;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TabPage tabLOG;
        private System.Windows.Forms.ListView listView10;
        private System.Windows.Forms.ListView listView9;
        private System.Windows.Forms.ListView listView8;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button127;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox tBResultCode;
        private System.Windows.Forms.TextBox textBox94;
        private System.Windows.Forms.TextBox tbDeviceCTN;
        private System.Windows.Forms.TextBox textBox95;
        private System.Windows.Forms.Button button126;
        private System.Windows.Forms.Button button122;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button button123;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnGetLogList;
        private System.Windows.Forms.Button button124;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TabPage webpage;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}

