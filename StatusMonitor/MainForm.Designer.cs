namespace StatusMonitor
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private AxCpfmsgacxa.AxCpfMsg axCpfMsg;
        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSet = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuSet = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuReFresh = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuOverTimeSet = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGetLog = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuWaitTime = new System.Windows.Forms.ToolStripMenuItem();
            this.sumMenuCol = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuOption = new System.Windows.Forms.ToolStripMenuItem();
            this.sumMenuQuickAnswer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMonitorTitle = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQueCall = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuIdle = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuKyokuGroupSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSkillShowSet = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuMonitorItemShow = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuLineCutItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuOtherSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.statusTabCtrl = new System.Windows.Forms.TabControl();
            this.agentStatusPage = new System.Windows.Forms.TabPage();
            this.agentPie = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox7 = new System.Windows.Forms.ComboBox();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.agentStatusListView = new System.Windows.Forms.ListView();
            this.largeImageList = new System.Windows.Forms.ImageList(this.components);
            this.smallImageList = new System.Windows.Forms.ImageList(this.components);
            this.agentIconListView = new System.Windows.Forms.ListView();
            this.lineStatusPage = new System.Windows.Forms.TabPage();
            this.lineIconListView = new System.Windows.Forms.ListView();
            this.lineStatusListView = new System.Windows.Forms.ListView();
            this.tabMonitor = new System.Windows.Forms.TabPage();
            this.dvMonitor = new System.Windows.Forms.DataGridView();
            this.tabWaitCall = new System.Windows.Forms.TabPage();
            this.totalListView = new System.Windows.Forms.ListView();
            this.axCpfMsg = new AxCpfmsgacxa.AxCpfMsg();
            this.mainNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.mainNotifyIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mainNotifyMenuShow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mainNotifyMenuEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.groupComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.selectViewButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lineUseageProgressLabel = new System.Windows.Forms.Label();
            this.menuLineGroupBox = new System.Windows.Forms.GroupBox();
            this.connectTimer = new System.Windows.Forms.Timer(this.components);
            this.useageLabel = new System.Windows.Forms.Label();
            this.RightMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeAgentMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuAgentState = new System.Windows.Forms.ToolStripMenuItem();
            this.keepAlivetimer = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.ListUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.folderDia = new System.Windows.Forms.FolderBrowserDialog();
            this.LineRightMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuDropLine = new System.Windows.Forms.ToolStripMenuItem();
            this.lblHelpON = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.wbGroupPersonal = new System.Windows.Forms.WebBrowser();
            this.retTimer = new System.Windows.Forms.Timer(this.components);
            this.webGetCall = new System.Windows.Forms.WebBrowser();
            this.AgentTimer = new System.Windows.Forms.Timer(this.components);
            this.CallTimer = new System.Windows.Forms.Timer(this.components);
            this.MonitorTimer = new System.Windows.Forms.Timer(this.components);
            this.dvBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.webGetGroup = new System.Windows.Forms.WebBrowser();
            this.mainMenu.SuspendLayout();
            this.statusTabCtrl.SuspendLayout();
            this.agentStatusPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.agentPie)).BeginInit();
            this.lineStatusPage.SuspendLayout();
            this.tabMonitor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvMonitor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCpfMsg)).BeginInit();
            this.mainNotifyIconMenu.SuspendLayout();
            this.RightMenu.SuspendLayout();
            this.LineRightMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.BackColor = System.Drawing.SystemColors.Menu;
            this.mainMenu.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuSet});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.mainMenu.Size = new System.Drawing.Size(1018, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "mainMenu";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileEnd});
            this.menuFile.Name = "menuFile";
            this.menuFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.menuFile.Size = new System.Drawing.Size(66, 20);
            this.menuFile.Text = "ファイル(&F)";
            // 
            // menuFileEnd
            // 
            this.menuFileEnd.Name = "menuFileEnd";
            this.menuFileEnd.Size = new System.Drawing.Size(109, 22);
            this.menuFileEnd.Text = "終了(&X)";
            this.menuFileEnd.Click += new System.EventHandler(this.menuFileEnd_Click);
            // 
            // menuSet
            // 
            this.menuSet.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuSet,
            this.subMenuReFresh,
            this.subMenuOverTimeSet,
            this.menuGetLog,
            this.subMenuWaitTime,
            this.sumMenuCol,
            this.subMenuOption,
            this.sumMenuQuickAnswer,
            this.menuMonitorTitle,
            this.menuQueCall,
            this.MenuIdle,
            this.subMenuKyokuGroupSetting,
            this.MenuSkillShowSet,
            this.MenuMonitorItemShow,
            this.MenuLineCutItem,
            this.subMenuOtherSetting});
            this.menuSet.Name = "menuSet";
            this.menuSet.Size = new System.Drawing.Size(41, 20);
            this.menuSet.Text = "設定";
            // 
            // subMenuSet
            // 
            this.subMenuSet.Name = "subMenuSet";
            this.subMenuSet.Size = new System.Drawing.Size(192, 22);
            this.subMenuSet.Text = "通話モニタ電話番号設定";
            this.subMenuSet.Click += new System.EventHandler(this.subMenuSet_Click);
            // 
            // subMenuReFresh
            // 
            this.subMenuReFresh.Name = "subMenuReFresh";
            this.subMenuReFresh.Size = new System.Drawing.Size(192, 22);
            this.subMenuReFresh.Text = "refresh";
            this.subMenuReFresh.Click += new System.EventHandler(this.subMenuReFresh_Click);
            // 
            // subMenuOverTimeSet
            // 
            this.subMenuOverTimeSet.Name = "subMenuOverTimeSet";
            this.subMenuOverTimeSet.Size = new System.Drawing.Size(192, 22);
            this.subMenuOverTimeSet.Text = "超過時間警告表示設定";
            this.subMenuOverTimeSet.Click += new System.EventHandler(this.subMenuOverTimeSet_Click);
            // 
            // menuGetLog
            // 
            this.menuGetLog.Name = "menuGetLog";
            this.menuGetLog.Size = new System.Drawing.Size(192, 22);
            this.menuGetLog.Text = "ログ取得";
            this.menuGetLog.Click += new System.EventHandler(this.menuGetLog_Click);
            // 
            // subMenuWaitTime
            // 
            this.subMenuWaitTime.Name = "subMenuWaitTime";
            this.subMenuWaitTime.Size = new System.Drawing.Size(192, 22);
            this.subMenuWaitTime.Text = "自動待機時間設定";
            this.subMenuWaitTime.Visible = false;
            this.subMenuWaitTime.Click += new System.EventHandler(this.subMenuWaitTime_Click);
            // 
            // sumMenuCol
            // 
            this.sumMenuCol.Name = "sumMenuCol";
            this.sumMenuCol.Size = new System.Drawing.Size(192, 22);
            this.sumMenuCol.Text = "列の選択";
            this.sumMenuCol.Click += new System.EventHandler(this.sumMenuCol_Click);
            // 
            // subMenuOption
            // 
            this.subMenuOption.Name = "subMenuOption";
            this.subMenuOption.Size = new System.Drawing.Size(192, 22);
            this.subMenuOption.Text = "オプション名設定";
            this.subMenuOption.Click += new System.EventHandler(this.subMenuOption_Click);
            // 
            // sumMenuQuickAnswer
            // 
            this.sumMenuQuickAnswer.Name = "sumMenuQuickAnswer";
            this.sumMenuQuickAnswer.Size = new System.Drawing.Size(192, 22);
            this.sumMenuQuickAnswer.Text = "即答秒数設定";
            this.sumMenuQuickAnswer.Click += new System.EventHandler(this.sumMenuQuickAnswer_Click);
            // 
            // menuMonitorTitle
            // 
            this.menuMonitorTitle.Name = "menuMonitorTitle";
            this.menuMonitorTitle.Size = new System.Drawing.Size(192, 22);
            this.menuMonitorTitle.Text = "モニタタイトル定義";
            this.menuMonitorTitle.Click += new System.EventHandler(this.menuMonitorTitle_Click);
            // 
            // menuQueCall
            // 
            this.menuQueCall.Name = "menuQueCall";
            this.menuQueCall.Size = new System.Drawing.Size(192, 22);
            this.menuQueCall.Text = "待ち呼警告設定";
            this.menuQueCall.Click += new System.EventHandler(this.menuQueCall_Click);
            // 
            // MenuIdle
            // 
            this.MenuIdle.Name = "MenuIdle";
            this.MenuIdle.Size = new System.Drawing.Size(192, 22);
            this.MenuIdle.Text = "受付可警告設定";
            this.MenuIdle.Click += new System.EventHandler(this.MenuIdle_Click);
            // 
            // subMenuKyokuGroupSetting
            // 
            this.subMenuKyokuGroupSetting.Name = "subMenuKyokuGroupSetting";
            this.subMenuKyokuGroupSetting.Size = new System.Drawing.Size(192, 22);
            this.subMenuKyokuGroupSetting.Text = "局番グループ表示設定";
            this.subMenuKyokuGroupSetting.Click += new System.EventHandler(this.subMenuKyokuGroupSetting_Click);
            // 
            // MenuSkillShowSet
            // 
            this.MenuSkillShowSet.Name = "MenuSkillShowSet";
            this.MenuSkillShowSet.Size = new System.Drawing.Size(192, 22);
            this.MenuSkillShowSet.Text = "スキル表示設定";
            this.MenuSkillShowSet.Click += new System.EventHandler(this.MenuSkillShowSet_Click);
            // 
            // MenuMonitorItemShow
            // 
            this.MenuMonitorItemShow.Name = "MenuMonitorItemShow";
            this.MenuMonitorItemShow.Size = new System.Drawing.Size(192, 22);
            this.MenuMonitorItemShow.Text = "モニタタイトル表示設定";
            this.MenuMonitorItemShow.Click += new System.EventHandler(this.MenuMonitorItemShow_Click);
            // 
            // MenuLineCutItem
            // 
            this.MenuLineCutItem.Name = "MenuLineCutItem";
            this.MenuLineCutItem.Size = new System.Drawing.Size(192, 22);
            this.MenuLineCutItem.Text = "回線切断表示設定";
            this.MenuLineCutItem.Click += new System.EventHandler(this.MenuLineCutItem_Click);
            // 
            // subMenuOtherSetting
            // 
            this.subMenuOtherSetting.Name = "subMenuOtherSetting";
            this.subMenuOtherSetting.Size = new System.Drawing.Size(192, 22);
            this.subMenuOtherSetting.Text = "その他";
            this.subMenuOtherSetting.Click += new System.EventHandler(this.subMenuOtherSetting_Click);
            // 
            // statusTabCtrl
            // 
            this.statusTabCtrl.Controls.Add(this.agentStatusPage);
            this.statusTabCtrl.Controls.Add(this.lineStatusPage);
            this.statusTabCtrl.Controls.Add(this.tabMonitor);
            this.statusTabCtrl.Controls.Add(this.tabWaitCall);
            this.statusTabCtrl.Location = new System.Drawing.Point(4, 72);
            this.statusTabCtrl.Name = "statusTabCtrl";
            this.statusTabCtrl.SelectedIndex = 0;
            this.statusTabCtrl.Size = new System.Drawing.Size(650, 621);
            this.statusTabCtrl.TabIndex = 4;
            this.statusTabCtrl.SelectedIndexChanged += new System.EventHandler(this.statusTabCtrl_SelectedIndexChanged);
            this.statusTabCtrl.TabIndexChanged += new System.EventHandler(this.statusTabCtrl_TabIndexChanged);
            // 
            // agentStatusPage
            // 
            this.agentStatusPage.BackColor = System.Drawing.Color.Transparent;
            this.agentStatusPage.Controls.Add(this.agentPie);
            this.agentStatusPage.Controls.Add(this.label7);
            this.agentStatusPage.Controls.Add(this.label6);
            this.agentStatusPage.Controls.Add(this.label5);
            this.agentStatusPage.Controls.Add(this.label10);
            this.agentStatusPage.Controls.Add(this.label4);
            this.agentStatusPage.Controls.Add(this.label8);
            this.agentStatusPage.Controls.Add(this.label3);
            this.agentStatusPage.Controls.Add(this.comboBox5);
            this.agentStatusPage.Controls.Add(this.comboBox4);
            this.agentStatusPage.Controls.Add(this.comboBox3);
            this.agentStatusPage.Controls.Add(this.comboBox2);
            this.agentStatusPage.Controls.Add(this.comboBox7);
            this.agentStatusPage.Controls.Add(this.comboBox6);
            this.agentStatusPage.Controls.Add(this.comboBox1);
            this.agentStatusPage.Controls.Add(this.agentStatusListView);
            this.agentStatusPage.Controls.Add(this.agentIconListView);
            this.agentStatusPage.Location = new System.Drawing.Point(4, 23);
            this.agentStatusPage.Name = "agentStatusPage";
            this.agentStatusPage.Padding = new System.Windows.Forms.Padding(3);
            this.agentStatusPage.Size = new System.Drawing.Size(642, 594);
            this.agentStatusPage.TabIndex = 0;
            this.agentStatusPage.Text = "エージェント 状態";
            this.agentStatusPage.UseVisualStyleBackColor = true;
            // 
            // agentPie
            // 
            this.agentPie.BackColor = System.Drawing.SystemColors.Control;
            this.agentPie.Location = new System.Drawing.Point(136, 493);
            this.agentPie.Name = "agentPie";
            this.agentPie.Size = new System.Drawing.Size(503, 98);
            this.agentPie.TabIndex = 14;
            this.agentPie.TabStop = false;
            this.agentPie.VisibleChanged += new System.EventHandler(this.agentPie_VisibleChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(516, 36);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Option5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(388, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Option4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(261, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Option3";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(133, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "ヘルプ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(133, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Option2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 10);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "状態";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 36);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Option1";
            // 
            // comboBox5
            // 
            this.comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Location = new System.Drawing.Point(566, 32);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(73, 21);
            this.comboBox5.TabIndex = 12;
            this.comboBox5.SelectedIndexChanged += new System.EventHandler(this.comboBox5_SelectedIndexChanged);
            // 
            // comboBox4
            // 
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(439, 32);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(73, 21);
            this.comboBox4.TabIndex = 12;
            this.comboBox4.SelectedIndexChanged += new System.EventHandler(this.comboBox4_SelectedIndexChanged);
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(312, 32);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(73, 21);
            this.comboBox3.TabIndex = 12;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(185, 32);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(73, 21);
            this.comboBox2.TabIndex = 12;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // comboBox7
            // 
            this.comboBox7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox7.FormattingEnabled = true;
            this.comboBox7.Location = new System.Drawing.Point(185, 7);
            this.comboBox7.Name = "comboBox7";
            this.comboBox7.Size = new System.Drawing.Size(73, 21);
            this.comboBox7.TabIndex = 12;
            this.comboBox7.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox6
            // 
            this.comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Location = new System.Drawing.Point(58, 7);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(73, 21);
            this.comboBox6.TabIndex = 12;
            this.comboBox6.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(58, 32);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(73, 21);
            this.comboBox1.TabIndex = 12;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // agentStatusListView
            // 
            this.agentStatusListView.GridLines = true;
            this.agentStatusListView.LargeImageList = this.largeImageList;
            this.agentStatusListView.Location = new System.Drawing.Point(3, 59);
            this.agentStatusListView.MultiSelect = false;
            this.agentStatusListView.Name = "agentStatusListView";
            this.agentStatusListView.ShowItemToolTips = true;
            this.agentStatusListView.Size = new System.Drawing.Size(636, 428);
            this.agentStatusListView.SmallImageList = this.smallImageList;
            this.agentStatusListView.TabIndex = 0;
            this.agentStatusListView.UseCompatibleStateImageBehavior = false;
            this.agentStatusListView.View = System.Windows.Forms.View.Details;
            this.agentStatusListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.agentStatusListView_ColumnClick);
            this.agentStatusListView.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.agentStatusListView_ColumnWidthChanged);
            this.agentStatusListView.SelectedIndexChanged += new System.EventHandler(this.NoSelectListView_SelectedIndexChanged);
            this.agentStatusListView.DoubleClick += new System.EventHandler(this.agentStatusListView_DoubleClick);
            this.agentStatusListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.agentStatusListView_MouseClick);
            // 
            // largeImageList
            // 
            this.largeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("largeImageList.ImageStream")));
            this.largeImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.largeImageList.Images.SetKeyName(0, "IconCallIdle");
            this.largeImageList.Images.SetKeyName(1, "IconCallCalling");
            this.largeImageList.Images.SetKeyName(2, "IconCallPreparing");
            this.largeImageList.Images.SetKeyName(3, "IconCallIvr");
            this.largeImageList.Images.SetKeyName(4, "IconCallOperator");
            this.largeImageList.Images.SetKeyName(5, "IconOpeIdle");
            this.largeImageList.Images.SetKeyName(6, "IconOpeWait");
            this.largeImageList.Images.SetKeyName(7, "IconOpePreparing");
            this.largeImageList.Images.SetKeyName(8, "IconOpeOffering");
            this.largeImageList.Images.SetKeyName(9, "IconOpeConnect");
            this.largeImageList.Images.SetKeyName(10, "IconOpeWorktime");
            this.largeImageList.Images.SetKeyName(11, "IconOpeTelephone");
            this.largeImageList.Images.SetKeyName(12, "IconTelIdle");
            this.largeImageList.Images.SetKeyName(13, "IconTelCalling");
            this.largeImageList.Images.SetKeyName(14, "IconTelConnect");
            this.largeImageList.Images.SetKeyName(15, "IconTelHold");
            this.largeImageList.Images.SetKeyName(16, "IconTelTransfer");
            this.largeImageList.Images.SetKeyName(17, "IconTelConf");
            this.largeImageList.Images.SetKeyName(18, "IconTelMonitor");
            this.largeImageList.Images.SetKeyName(19, "IconTelRecord");
            this.largeImageList.Images.SetKeyName(20, "IconOpeSeatoff");
            // 
            // smallImageList
            // 
            this.smallImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("smallImageList.ImageStream")));
            this.smallImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.smallImageList.Images.SetKeyName(0, "IconCallIdle");
            this.smallImageList.Images.SetKeyName(1, "IconCallCalling");
            this.smallImageList.Images.SetKeyName(2, "IconCallPreparing");
            this.smallImageList.Images.SetKeyName(3, "IconCallIvr");
            this.smallImageList.Images.SetKeyName(4, "IconCallOperator");
            this.smallImageList.Images.SetKeyName(5, "IconOpeIdle");
            this.smallImageList.Images.SetKeyName(6, "IconOpeWait");
            this.smallImageList.Images.SetKeyName(7, "IconOpePreparing");
            this.smallImageList.Images.SetKeyName(8, "IconOpeOffering");
            this.smallImageList.Images.SetKeyName(9, "IconOpeConnect");
            this.smallImageList.Images.SetKeyName(10, "IconOpeWorktime");
            this.smallImageList.Images.SetKeyName(11, "IconOpeTelephone");
            this.smallImageList.Images.SetKeyName(12, "IconTelIdle");
            this.smallImageList.Images.SetKeyName(13, "IconTelCalling");
            this.smallImageList.Images.SetKeyName(14, "IconTelConnect");
            this.smallImageList.Images.SetKeyName(15, "IconTelHold");
            this.smallImageList.Images.SetKeyName(16, "IconTelTransfer");
            this.smallImageList.Images.SetKeyName(17, "IconTelConf");
            this.smallImageList.Images.SetKeyName(18, "IconTelMonitor");
            this.smallImageList.Images.SetKeyName(19, "IconTelRecord");
            this.smallImageList.Images.SetKeyName(20, "IconOpeSeatoff");
            // 
            // agentIconListView
            // 
            this.agentIconListView.BackColor = System.Drawing.SystemColors.Control;
            this.agentIconListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.agentIconListView.Enabled = false;
            this.agentIconListView.Location = new System.Drawing.Point(3, 493);
            this.agentIconListView.Name = "agentIconListView";
            this.agentIconListView.Scrollable = false;
            this.agentIconListView.Size = new System.Drawing.Size(140, 98);
            this.agentIconListView.SmallImageList = this.smallImageList;
            this.agentIconListView.TabIndex = 11;
            this.agentIconListView.TabStop = false;
            this.agentIconListView.UseCompatibleStateImageBehavior = false;
            this.agentIconListView.View = System.Windows.Forms.View.List;
            // 
            // lineStatusPage
            // 
            this.lineStatusPage.Controls.Add(this.lineIconListView);
            this.lineStatusPage.Controls.Add(this.lineStatusListView);
            this.lineStatusPage.Location = new System.Drawing.Point(4, 23);
            this.lineStatusPage.Name = "lineStatusPage";
            this.lineStatusPage.Padding = new System.Windows.Forms.Padding(3);
            this.lineStatusPage.Size = new System.Drawing.Size(642, 594);
            this.lineStatusPage.TabIndex = 1;
            this.lineStatusPage.Text = "回線 状態";
            this.lineStatusPage.UseVisualStyleBackColor = true;
            // 
            // lineIconListView
            // 
            this.lineIconListView.BackColor = System.Drawing.SystemColors.Control;
            this.lineIconListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lineIconListView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lineIconListView.Enabled = false;
            this.lineIconListView.Location = new System.Drawing.Point(3, 531);
            this.lineIconListView.Name = "lineIconListView";
            this.lineIconListView.Scrollable = false;
            this.lineIconListView.Size = new System.Drawing.Size(636, 60);
            this.lineIconListView.SmallImageList = this.smallImageList;
            this.lineIconListView.TabIndex = 12;
            this.lineIconListView.TabStop = false;
            this.lineIconListView.UseCompatibleStateImageBehavior = false;
            this.lineIconListView.View = System.Windows.Forms.View.List;
            // 
            // lineStatusListView
            // 
            this.lineStatusListView.Dock = System.Windows.Forms.DockStyle.Top;
            this.lineStatusListView.FullRowSelect = true;
            this.lineStatusListView.GridLines = true;
            this.lineStatusListView.LargeImageList = this.largeImageList;
            this.lineStatusListView.Location = new System.Drawing.Point(3, 3);
            this.lineStatusListView.MultiSelect = false;
            this.lineStatusListView.Name = "lineStatusListView";
            this.lineStatusListView.Size = new System.Drawing.Size(636, 498);
            this.lineStatusListView.SmallImageList = this.smallImageList;
            this.lineStatusListView.TabIndex = 0;
            this.lineStatusListView.UseCompatibleStateImageBehavior = false;
            this.lineStatusListView.View = System.Windows.Forms.View.Details;
            this.lineStatusListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lineStatusListView_ColumnClick);
            this.lineStatusListView.SelectedIndexChanged += new System.EventHandler(this.NoSelectListView_SelectedIndexChanged);
            this.lineStatusListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lineStatusListView_MouseClick);
            // 
            // tabMonitor
            // 
            this.tabMonitor.Controls.Add(this.dvMonitor);
            this.tabMonitor.Location = new System.Drawing.Point(4, 23);
            this.tabMonitor.Name = "tabMonitor";
            this.tabMonitor.Padding = new System.Windows.Forms.Padding(3);
            this.tabMonitor.Size = new System.Drawing.Size(642, 594);
            this.tabMonitor.TabIndex = 3;
            this.tabMonitor.Text = "モニタ";
            this.tabMonitor.UseVisualStyleBackColor = true;
            // 
            // dvMonitor
            // 
            this.dvMonitor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvMonitor.Dock = System.Windows.Forms.DockStyle.Top;
            this.dvMonitor.Location = new System.Drawing.Point(3, 3);
            this.dvMonitor.MultiSelect = false;
            this.dvMonitor.Name = "dvMonitor";
            this.dvMonitor.RowTemplate.Height = 21;
            this.dvMonitor.Size = new System.Drawing.Size(636, 588);
            this.dvMonitor.TabIndex = 0;
            this.dvMonitor.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvMonitor_CellDoubleClick);
            this.dvMonitor.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dvMonitor_DataError);
            // 
            // tabWaitCall
            // 
            this.tabWaitCall.Location = new System.Drawing.Point(4, 23);
            this.tabWaitCall.Name = "tabWaitCall";
            this.tabWaitCall.Padding = new System.Windows.Forms.Padding(3);
            this.tabWaitCall.Size = new System.Drawing.Size(642, 594);
            this.tabWaitCall.TabIndex = 4;
            this.tabWaitCall.Text = "待ち呼";
            this.tabWaitCall.UseVisualStyleBackColor = true;
            // 
            // totalListView
            // 
            this.totalListView.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.totalListView.FullRowSelect = true;
            this.totalListView.GridLines = true;
            this.totalListView.Location = new System.Drawing.Point(660, 96);
            this.totalListView.MultiSelect = false;
            this.totalListView.Name = "totalListView";
            this.totalListView.Size = new System.Drawing.Size(358, 597);
            this.totalListView.TabIndex = 5;
            this.totalListView.UseCompatibleStateImageBehavior = false;
            this.totalListView.View = System.Windows.Forms.View.Details;
            this.totalListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.totalListView_ColumnClick);
            this.totalListView.SelectedIndexChanged += new System.EventHandler(this.NoSelectListView_SelectedIndexChanged);
            // 
            // axCpfMsg
            // 
            this.axCpfMsg.Enabled = true;
            this.axCpfMsg.Location = new System.Drawing.Point(648, 32);
            this.axCpfMsg.Name = "axCpfMsg";
            this.axCpfMsg.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCpfMsg.OcxState")));
            this.axCpfMsg.Size = new System.Drawing.Size(59, 25);
            this.axCpfMsg.TabIndex = 10;
            this.axCpfMsg.TabStop = false;
            this.axCpfMsg.OnCommand += new AxCpfmsgacxa._ICpfMsgEvents_OnCommandEventHandler(this.axCpfMsg_OnCommand);
            this.axCpfMsg.OnClose += new System.EventHandler(this.axCpfMsg_OnClose);
            // 
            // mainNotifyIcon
            // 
            this.mainNotifyIcon.ContextMenuStrip = this.mainNotifyIconMenu;
            this.mainNotifyIcon.Visible = true;
            this.mainNotifyIcon.DoubleClick += new System.EventHandler(this.mainNotifyIcon_DoubleClick);
            // 
            // mainNotifyIconMenu
            // 
            this.mainNotifyIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainNotifyMenuShow,
            this.toolStripMenuItem1,
            this.mainNotifyMenuEnd});
            this.mainNotifyIconMenu.Name = "mainNotifyIconMenu";
            this.mainNotifyIconMenu.Size = new System.Drawing.Size(159, 54);
            // 
            // mainNotifyMenuShow
            // 
            this.mainNotifyMenuShow.Name = "mainNotifyMenuShow";
            this.mainNotifyMenuShow.Size = new System.Drawing.Size(158, 22);
            this.mainNotifyMenuShow.Text = "表示・非表示(&S)";
            this.mainNotifyMenuShow.Click += new System.EventHandler(this.mainNotifyMenuShow_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(155, 6);
            // 
            // mainNotifyMenuEnd
            // 
            this.mainNotifyMenuEnd.Name = "mainNotifyMenuEnd";
            this.mainNotifyMenuEnd.Size = new System.Drawing.Size(158, 22);
            this.mainNotifyMenuEnd.Text = "終了(&X)";
            this.mainNotifyMenuEnd.Click += new System.EventHandler(this.mainNotifyMenuEnd_Click);
            // 
            // groupComboBox
            // 
            this.groupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.groupComboBox.FormattingEnabled = true;
            this.groupComboBox.Location = new System.Drawing.Point(102, 40);
            this.groupComboBox.Name = "groupComboBox";
            this.groupComboBox.Size = new System.Drawing.Size(168, 21);
            this.groupComboBox.Sorted = true;
            this.groupComboBox.TabIndex = 1;
            this.groupComboBox.SelectedValueChanged += new System.EventHandler(this.groupComboBox_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "スキルグループ";
            // 
            // selectViewButton
            // 
            this.selectViewButton.Location = new System.Drawing.Point(276, 40);
            this.selectViewButton.Name = "selectViewButton";
            this.selectViewButton.Size = new System.Drawing.Size(80, 24);
            this.selectViewButton.TabIndex = 2;
            this.selectViewButton.Text = "表示切替";
            this.selectViewButton.UseVisualStyleBackColor = true;
            this.selectViewButton.Click += new System.EventHandler(this.selectViewButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(690, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "回線使用率";
            // 
            // lineUseageProgressLabel
            // 
            this.lineUseageProgressLabel.BackColor = System.Drawing.SystemColors.Control;
            this.lineUseageProgressLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lineUseageProgressLabel.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lineUseageProgressLabel.ForeColor = System.Drawing.Color.White;
            this.lineUseageProgressLabel.Location = new System.Drawing.Point(777, 52);
            this.lineUseageProgressLabel.Name = "lineUseageProgressLabel";
            this.lineUseageProgressLabel.Size = new System.Drawing.Size(240, 20);
            this.lineUseageProgressLabel.TabIndex = 16;
            this.lineUseageProgressLabel.Text = "100 %";
            this.lineUseageProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lineUseageProgressLabel.UseMnemonic = false;
            this.lineUseageProgressLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.lineUseageProgressLabel_Paint);
            // 
            // menuLineGroupBox
            // 
            this.menuLineGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuLineGroupBox.Location = new System.Drawing.Point(0, 24);
            this.menuLineGroupBox.Name = "menuLineGroupBox";
            this.menuLineGroupBox.Size = new System.Drawing.Size(1018, 2);
            this.menuLineGroupBox.TabIndex = 0;
            this.menuLineGroupBox.TabStop = false;
            // 
            // connectTimer
            // 
            this.connectTimer.Interval = 1000;
            this.connectTimer.Tick += new System.EventHandler(this.connectTimer_Tick);
            // 
            // useageLabel
            // 
            this.useageLabel.Location = new System.Drawing.Point(694, 64);
            this.useageLabel.Name = "useageLabel";
            this.useageLabel.Size = new System.Drawing.Size(64, 16);
            this.useageLabel.TabIndex = 17;
            this.useageLabel.Text = "100 / 100";
            this.useageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RightMenu
            // 
            this.RightMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeAgentMenu,
            this.subMenuAgentState});
            this.RightMenu.Name = "RightMenu";
            this.RightMenu.Size = new System.Drawing.Size(144, 48);
            // 
            // closeAgentMenu
            // 
            this.closeAgentMenu.Name = "closeAgentMenu";
            this.closeAgentMenu.Size = new System.Drawing.Size(143, 22);
            this.closeAgentMenu.Click += new System.EventHandler(this.closeAgentMenu_Click);
            // 
            // subMenuAgentState
            // 
            this.subMenuAgentState.Name = "subMenuAgentState";
            this.subMenuAgentState.Size = new System.Drawing.Size(143, 22);
            this.subMenuAgentState.Text = "ステータス変更";
            this.subMenuAgentState.Click += new System.EventHandler(this.subMenuAgentState_Click);
            // 
            // keepAlivetimer
            // 
            this.keepAlivetimer.Tick += new System.EventHandler(this.keepAlivetimer_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(373, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 24);
            this.button1.TabIndex = 18;
            this.button1.Text = "再表示";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ListUpdateTimer
            // 
            this.ListUpdateTimer.Interval = 200;
            this.ListUpdateTimer.Tick += new System.EventHandler(this.ListUpdateTimer_Tick);
            // 
            // LineRightMenu
            // 
            this.LineRightMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDropLine});
            this.LineRightMenu.Name = "LineRightMenu";
            this.LineRightMenu.Size = new System.Drawing.Size(99, 26);
            // 
            // menuDropLine
            // 
            this.menuDropLine.Name = "menuDropLine";
            this.menuDropLine.Size = new System.Drawing.Size(98, 22);
            this.menuDropLine.Text = "切断";
            this.menuDropLine.Click += new System.EventHandler(this.menuDropLine_Click);
            // 
            // lblHelpON
            // 
            this.lblHelpON.AutoSize = true;
            this.lblHelpON.Location = new System.Drawing.Point(603, 74);
            this.lblHelpON.Name = "lblHelpON";
            this.lblHelpON.Size = new System.Drawing.Size(0, 13);
            this.lblHelpON.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(553, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "ヘルプ中";
            // 
            // wbGroupPersonal
            // 
            this.wbGroupPersonal.Location = new System.Drawing.Point(489, 35);
            this.wbGroupPersonal.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbGroupPersonal.Name = "wbGroupPersonal";
            this.wbGroupPersonal.Size = new System.Drawing.Size(32, 20);
            this.wbGroupPersonal.TabIndex = 21;
            this.wbGroupPersonal.Visible = false;
            this.wbGroupPersonal.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbGroupPersonal_DocumentCompleted);
            // 
            // retTimer
            // 
            this.retTimer.Tick += new System.EventHandler(this.retTimer_Tick);
            // 
            // webGetCall
            // 
            this.webGetCall.Location = new System.Drawing.Point(556, 37);
            this.webGetCall.MinimumSize = new System.Drawing.Size(20, 20);
            this.webGetCall.Name = "webGetCall";
            this.webGetCall.Size = new System.Drawing.Size(32, 20);
            this.webGetCall.TabIndex = 21;
            this.webGetCall.Visible = false;
            this.webGetCall.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webGetCall_DocumentCompleted);
            // 
            // AgentTimer
            // 
            this.AgentTimer.Tick += new System.EventHandler(this.AgentTimer_Tick);
            // 
            // CallTimer
            // 
            this.CallTimer.Tick += new System.EventHandler(this.CallTimer_Tick);
            // 
            // MonitorTimer
            // 
            this.MonitorTimer.Interval = 200;
            this.MonitorTimer.Tick += new System.EventHandler(this.MonitorTimer_Tick);
            // 
            // webGetGroup
            // 
            this.webGetGroup.Location = new System.Drawing.Point(475, 60);
            this.webGetGroup.MinimumSize = new System.Drawing.Size(20, 20);
            this.webGetGroup.Name = "webGetGroup";
            this.webGetGroup.Size = new System.Drawing.Size(32, 20);
            this.webGetGroup.TabIndex = 22;
            this.webGetGroup.Visible = false;
            this.webGetGroup.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webGetGroup_DocumentCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1018, 725);
            this.Controls.Add(this.webGetGroup);
            this.Controls.Add(this.wbGroupPersonal);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblHelpON);
            this.Controls.Add(this.webGetCall);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.useageLabel);
            this.Controls.Add(this.menuLineGroupBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.selectViewButton);
            this.Controls.Add(this.lineUseageProgressLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupComboBox);
            this.Controls.Add(this.totalListView);
            this.Controls.Add(this.statusTabCtrl);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.axCpfMsg);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "StatusMonitor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.statusTabCtrl.ResumeLayout(false);
            this.agentStatusPage.ResumeLayout(false);
            this.agentStatusPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.agentPie)).EndInit();
            this.lineStatusPage.ResumeLayout(false);
            this.tabMonitor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvMonitor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCpfMsg)).EndInit();
            this.mainNotifyIconMenu.ResumeLayout(false);
            this.RightMenu.ResumeLayout(false);
            this.LineRightMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       
       
        
       
            

        
        #endregion       
		
        
		private System.Windows.Forms.MenuStrip mainMenu;
		private System.Windows.Forms.ToolStripMenuItem menuFile;
		private System.Windows.Forms.ToolStripMenuItem menuFileEnd;
        private System.Windows.Forms.TabControl statusTabCtrl;
        private System.Windows.Forms.TabPage agentStatusPage;
		private System.Windows.Forms.TabPage lineStatusPage;
		private System.Windows.Forms.ListView totalListView;
		public System.Windows.Forms.ImageList smallImageList;
		public System.Windows.Forms.ImageList largeImageList;
		private System.Windows.Forms.ListView agentStatusListView;
		private System.Windows.Forms.ListView lineStatusListView;
		private System.Windows.Forms.NotifyIcon mainNotifyIcon;
		private System.Windows.Forms.ContextMenuStrip mainNotifyIconMenu;
		private System.Windows.Forms.ToolStripMenuItem mainNotifyMenuShow;
		private System.Windows.Forms.ToolStripMenuItem mainNotifyMenuEnd;
		private System.Windows.Forms.ListView agentIconListView;
		private System.Windows.Forms.ListView lineIconListView;
		private System.Windows.Forms.ComboBox groupComboBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button selectViewButton;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lineUseageProgressLabel;
		private System.Windows.Forms.GroupBox menuLineGroupBox;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.Timer connectTimer;
		private System.Windows.Forms.Label useageLabel;
        private System.Windows.Forms.ToolStripMenuItem menuSet;
        private System.Windows.Forms.ToolStripMenuItem subMenuSet;
        private System.Windows.Forms.ContextMenuStrip RightMenu;
        private System.Windows.Forms.ToolStripMenuItem closeAgentMenu;
        private System.Windows.Forms.Timer keepAlivetimer;
        private System.Windows.Forms.ToolStripMenuItem subMenuReFresh;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer ListUpdateTimer;
        private System.Windows.Forms.ToolStripMenuItem menuGetLog;
        private System.Windows.Forms.FolderBrowserDialog folderDia;
        private System.Windows.Forms.ToolStripMenuItem subMenuOverTimeSet;
        public System.Windows.Forms.ContextMenuStrip LineRightMenu;
        private System.Windows.Forms.ToolStripMenuItem menuDropLine;
        private System.Windows.Forms.ToolStripMenuItem subMenuWaitTime;
        private System.Windows.Forms.ToolStripMenuItem subMenuAgentState;
        private System.Windows.Forms.ToolStripMenuItem sumMenuCol;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ToolStripMenuItem subMenuOption;
        private System.Windows.Forms.Label lblHelpON;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox7;
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.TabPage tabMonitor;
        private System.Windows.Forms.DataGridView dvMonitor;
        private System.Windows.Forms.WebBrowser wbGroupPersonal;
        private System.Windows.Forms.ToolStripMenuItem sumMenuQuickAnswer;
        private System.Windows.Forms.Timer retTimer;
        private System.Windows.Forms.WebBrowser webGetCall;
        private System.Windows.Forms.ToolStripMenuItem menuMonitorTitle;
        private System.Windows.Forms.Timer AgentTimer;
        private System.Windows.Forms.Timer CallTimer;
        private System.Windows.Forms.ToolStripMenuItem menuQueCall;
        private System.Windows.Forms.ToolStripMenuItem MenuIdle;
        private System.Windows.Forms.ToolStripMenuItem MenuSkillShowSet;
        private System.Windows.Forms.ToolStripMenuItem MenuMonitorItemShow;
        private System.Windows.Forms.TabPage tabWaitCall;
        private System.Windows.Forms.Timer MonitorTimer;
        private System.Windows.Forms.BindingSource dvBindingSource;
        private System.Windows.Forms.ToolStripMenuItem MenuLineCutItem;
        private System.Windows.Forms.PictureBox agentPie;
        private System.Windows.Forms.ToolStripMenuItem subMenuOtherSetting;
        private System.Windows.Forms.WebBrowser webGetGroup;
        private System.Windows.Forms.ToolStripMenuItem subMenuKyokuGroupSetting;
    }
}