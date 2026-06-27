namespace TaskForge.Views
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            panelDashboard = new Panel();
            chartDashboard = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panel1 = new Panel();
            btnExport = new Button();
            btnImport = new Button();
            btnCheckGoals = new Button();
            lblTotalTime = new Label();
            button1 = new Button();
            panelHistory = new Panel();
            dataGridHistory = new DataGridView();
            panelFilters = new Panel();
            cmbCategory = new ComboBox();
            lblCategory = new Label();
            dtTo = new DateTimePicker();
            dtFrom = new DateTimePicker();
            lblTo = new Label();
            lblFrom = new Label();
            cmbApplication = new ComboBox();
            lblApplication = new Label();
            panelSettings = new Panel();
            tabSettings = new TabControl();
            tabPage1 = new TabPage();
            lblExistingCategories = new Label();
            lblAddCategory = new Label();
            lblCategoryTitle = new Label();
            btnDeleteCategory = new Button();
            btnAddCategory = new Button();
            txtCategory = new TextBox();
            lstCategories = new ListBox();
            tabPage2 = new TabPage();
            lblIgnoredList = new Label();
            lblAddIgnore = new Label();
            lblIgnoreTitle = new Label();
            lstIgnoredApps = new ListBox();
            btnDeleteIgnore = new Button();
            btnAddIgnore = new Button();
            txtIgnoreApp = new TextBox();
            tabPage3 = new TabPage();
            lblSavedGoals = new Label();
            label1 = new Label();
            label3 = new Label();
            lblSetGoal = new Label();
            lblGoalsTitle = new Label();
            lstGoals = new ListBox();
            btnSaveGoal = new Button();
            numGoalMinutes = new NumericUpDown();
            cmbGoalCategory = new ComboBox();
            menuStrip1 = new MenuStrip();
            dashboardToolStripMenuItem = new ToolStripMenuItem();
            historyToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            sqliteCommand1 = new Microsoft.Data.Sqlite.SqliteCommand();
            timerRefresh = new System.Windows.Forms.Timer(components);
            bindingSource1 = new BindingSource(components);
            toolTip1 = new ToolTip(components);
            reportsToolStripMenuItem = new ToolStripMenuItem();
            panelDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartDashboard).BeginInit();
            panel1.SuspendLayout();
            panelHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridHistory).BeginInit();
            panelFilters.SuspendLayout();
            panelSettings.SuspendLayout();
            tabSettings.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numGoalMinutes).BeginInit();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            // 
            // panelDashboard
            // 
            panelDashboard.Controls.Add(chartDashboard);
            panelDashboard.Controls.Add(panel1);
            panelDashboard.Dock = DockStyle.Fill;
            panelDashboard.Location = new Point(0, 0);
            panelDashboard.Margin = new Padding(4, 4, 4, 4);
            panelDashboard.Name = "panelDashboard";
            panelDashboard.Size = new Size(1500, 675);
            panelDashboard.TabIndex = 0;
            // 
            // chartDashboard
            // 
            chartArea2.Name = "ChartArea1";
            chartDashboard.ChartAreas.Add(chartArea2);
            chartDashboard.Dock = DockStyle.Fill;
            legend2.Name = "Legend1";
            chartDashboard.Legends.Add(legend2);
            chartDashboard.Location = new Point(0, 147);
            chartDashboard.Margin = new Padding(4, 4, 4, 4);
            chartDashboard.Name = "chartDashboard";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chartDashboard.Series.Add(series2);
            chartDashboard.Size = new Size(1500, 528);
            chartDashboard.TabIndex = 0;
            chartDashboard.Text = "chart1";
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(btnExport);
            panel1.Controls.Add(btnImport);
            panel1.Controls.Add(btnCheckGoals);
            panel1.Controls.Add(lblTotalTime);
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 4, 4, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1500, 147);
            panel1.TabIndex = 4;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(570, 76);
            btnExport.Margin = new Padding(4, 4, 4, 4);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(180, 44);
            btnExport.TabIndex = 4;
            btnExport.Text = "Export Database";
            btnExport.UseVisualStyleBackColor = true;
            // 
            // btnImport
            // 
            btnImport.Location = new Point(765, 76);
            btnImport.Margin = new Padding(4, 4, 4, 4);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(180, 44);
            btnImport.TabIndex = 5;
            btnImport.Text = "Import Database";
            btnImport.UseVisualStyleBackColor = true;
            // 
            // btnCheckGoals
            // 
            btnCheckGoals.AutoSize = true;
            btnCheckGoals.Location = new Point(974, 76);
            btnCheckGoals.Margin = new Padding(4, 4, 4, 4);
            btnCheckGoals.Name = "btnCheckGoals";
            btnCheckGoals.Size = new Size(204, 60);
            btnCheckGoals.TabIndex = 2;
            btnCheckGoals.Text = "Check Goals";
            toolTip1.SetToolTip(btnCheckGoals, "Check whether daily goals are exceeded or not.");
            btnCheckGoals.UseVisualStyleBackColor = true;
            btnCheckGoals.Click += btnCheckGoals_Click_1;
            // 
            // lblTotalTime
            // 
            lblTotalTime.AutoSize = true;
            lblTotalTime.BackColor = Color.White;
            lblTotalTime.Font = new Font("Yu Gothic UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalTime.ForeColor = Color.Crimson;
            lblTotalTime.Location = new Point(38, 80);
            lblTotalTime.Margin = new Padding(4, 0, 4, 0);
            lblTotalTime.Name = "lblTotalTime";
            lblTotalTime.Size = new Size(270, 32);
            lblTotalTime.TabIndex = 1;
            lblTotalTime.Text = "Total Time Today: 0 min";
            // 
            // button1
            // 
            button1.Location = new Point(1182, 76);
            button1.Margin = new Padding(4, 4, 4, 4);
            button1.Name = "button1";
            button1.Size = new Size(280, 44);
            button1.TabIndex = 3;
            button1.Text = "Edit App Category";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panelHistory
            // 
            panelHistory.Controls.Add(dataGridHistory);
            panelHistory.Controls.Add(panelFilters);
            panelHistory.Dock = DockStyle.Fill;
            panelHistory.Location = new Point(0, 0);
            panelHistory.Margin = new Padding(4, 4, 4, 4);
            panelHistory.Name = "panelHistory";
            panelHistory.Size = new Size(1500, 675);
            panelHistory.TabIndex = 1;
            panelHistory.Visible = false;
            // 
            // dataGridHistory
            // 
            dataGridHistory.AllowUserToAddRows = false;
            dataGridHistory.AllowUserToDeleteRows = false;
            dataGridHistory.AllowUserToResizeColumns = false;
            dataGridHistory.AllowUserToResizeRows = false;
            dataGridHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridHistory.BackgroundColor = Color.White;
            dataGridHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridHistory.Dock = DockStyle.Fill;
            dataGridHistory.Location = new Point(0, 147);
            dataGridHistory.Margin = new Padding(4, 4, 4, 4);
            dataGridHistory.Name = "dataGridHistory";
            dataGridHistory.ReadOnly = true;
            dataGridHistory.RowHeadersWidth = 51;
            dataGridHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridHistory.Size = new Size(1500, 528);
            dataGridHistory.TabIndex = 0;
            // 
            // panelFilters
            // 
            panelFilters.BackColor = Color.White;
            panelFilters.Controls.Add(cmbCategory);
            panelFilters.Controls.Add(lblCategory);
            panelFilters.Controls.Add(dtTo);
            panelFilters.Controls.Add(dtFrom);
            panelFilters.Controls.Add(lblTo);
            panelFilters.Controls.Add(lblFrom);
            panelFilters.Controls.Add(cmbApplication);
            panelFilters.Controls.Add(lblApplication);
            panelFilters.Dock = DockStyle.Top;
            panelFilters.Location = new Point(0, 0);
            panelFilters.Margin = new Padding(4, 4, 4, 4);
            panelFilters.Name = "panelFilters";
            panelFilters.Size = new Size(1500, 147);
            panelFilters.TabIndex = 0;
            // 
            // cmbCategory
            // 
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Items.AddRange(new object[] { "All", "Work", "Entertainment", "Other" });
            cmbCategory.Location = new Point(1197, 70);
            cmbCategory.Margin = new Padding(4, 4, 4, 4);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(224, 38);
            cmbCategory.TabIndex = 8;
            cmbCategory.SelectedIndexChanged += cmbCategory_SelectedIndexChanged;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(1080, 75);
            lblCategory.Margin = new Padding(4, 0, 4, 0);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(101, 30);
            lblCategory.TabIndex = 7;
            lblCategory.Text = "Category:";
            // 
            // dtTo
            // 
            dtTo.Format = DateTimePickerFormat.Short;
            dtTo.Location = new Point(830, 72);
            dtTo.Margin = new Padding(4, 4, 4, 4);
            dtTo.Name = "dtTo";
            dtTo.Size = new Size(148, 35);
            dtTo.TabIndex = 6;
            dtTo.ValueChanged += dtTo_ValueChanged;
            // 
            // dtFrom
            // 
            dtFrom.Format = DateTimePickerFormat.Short;
            dtFrom.Location = new Point(576, 72);
            dtFrom.Margin = new Padding(4, 4, 4, 4);
            dtFrom.Name = "dtFrom";
            dtFrom.Size = new Size(148, 35);
            dtFrom.TabIndex = 5;
            dtFrom.ValueChanged += dtFrom_ValueChanged;
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Location = new Point(778, 75);
            lblTo.Margin = new Padding(4, 0, 4, 0);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(39, 30);
            lblTo.TabIndex = 4;
            lblTo.Text = "To:";
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(498, 75);
            lblFrom.Margin = new Padding(4, 0, 4, 0);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(65, 30);
            lblFrom.TabIndex = 3;
            lblFrom.Text = "From:";
            // 
            // cmbApplication
            // 
            cmbApplication.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbApplication.FormattingEnabled = true;
            cmbApplication.Location = new Point(176, 70);
            cmbApplication.Margin = new Padding(4, 4, 4, 4);
            cmbApplication.Name = "cmbApplication";
            cmbApplication.Size = new Size(268, 38);
            cmbApplication.TabIndex = 1;
            cmbApplication.SelectedIndexChanged += cmbApplication_SelectedIndexChanged;
            // 
            // lblApplication
            // 
            lblApplication.AutoSize = true;
            lblApplication.Location = new Point(33, 75);
            lblApplication.Margin = new Padding(4, 0, 4, 0);
            lblApplication.Name = "lblApplication";
            lblApplication.Size = new Size(123, 30);
            lblApplication.TabIndex = 0;
            lblApplication.Text = "Application:";
            // 
            // panelSettings
            // 
            panelSettings.BackColor = Color.White;
            panelSettings.Controls.Add(tabSettings);
            panelSettings.Dock = DockStyle.Fill;
            panelSettings.Location = new Point(0, 0);
            panelSettings.Margin = new Padding(4, 4, 4, 4);
            panelSettings.Name = "panelSettings";
            panelSettings.Size = new Size(1500, 675);
            panelSettings.TabIndex = 2;
            panelSettings.Visible = false;
            // 
            // tabSettings
            // 
            tabSettings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabSettings.Appearance = TabAppearance.Buttons;
            tabSettings.Controls.Add(tabPage1);
            tabSettings.Controls.Add(tabPage2);
            tabSettings.Controls.Add(tabPage3);
            tabSettings.Location = new Point(0, 44);
            tabSettings.Margin = new Padding(4, 4, 4, 4);
            tabSettings.Name = "tabSettings";
            tabSettings.SelectedIndex = 0;
            tabSettings.Size = new Size(1500, 632);
            tabSettings.TabIndex = 3;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.White;
            tabPage1.Controls.Add(lblExistingCategories);
            tabPage1.Controls.Add(lblAddCategory);
            tabPage1.Controls.Add(lblCategoryTitle);
            tabPage1.Controls.Add(btnDeleteCategory);
            tabPage1.Controls.Add(btnAddCategory);
            tabPage1.Controls.Add(txtCategory);
            tabPage1.Controls.Add(lstCategories);
            tabPage1.ForeColor = Color.Black;
            tabPage1.Location = new Point(4, 42);
            tabPage1.Margin = new Padding(4, 4, 4, 4);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(4, 4, 4, 4);
            tabPage1.Size = new Size(1492, 586);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Categories";
            // 
            // lblExistingCategories
            // 
            lblExistingCategories.AutoSize = true;
            lblExistingCategories.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblExistingCategories.Location = new Point(32, 279);
            lblExistingCategories.Margin = new Padding(4, 0, 4, 0);
            lblExistingCategories.Name = "lblExistingCategories";
            lblExistingCategories.Size = new Size(200, 30);
            lblExistingCategories.TabIndex = 6;
            lblExistingCategories.Text = "Existing Categories";
            // 
            // lblAddCategory
            // 
            lblAddCategory.AutoSize = true;
            lblAddCategory.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAddCategory.Location = new Point(32, 118);
            lblAddCategory.Margin = new Padding(4, 0, 4, 0);
            lblAddCategory.Name = "lblAddCategory";
            lblAddCategory.Size = new Size(200, 30);
            lblAddCategory.TabIndex = 5;
            lblAddCategory.Text = "Add New Category";
            // 
            // lblCategoryTitle
            // 
            lblCategoryTitle.AutoSize = true;
            lblCategoryTitle.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCategoryTitle.Location = new Point(32, 22);
            lblCategoryTitle.Margin = new Padding(4, 0, 4, 0);
            lblCategoryTitle.Name = "lblCategoryTitle";
            lblCategoryTitle.Size = new Size(296, 36);
            lblCategoryTitle.TabIndex = 4;
            lblCategoryTitle.Text = "Category Management";
            // 
            // btnDeleteCategory
            // 
            btnDeleteCategory.Location = new Point(32, 526);
            btnDeleteCategory.Margin = new Padding(4, 4, 4, 4);
            btnDeleteCategory.Name = "btnDeleteCategory";
            btnDeleteCategory.Size = new Size(654, 44);
            btnDeleteCategory.TabIndex = 3;
            btnDeleteCategory.Text = "Delete Selected";
            btnDeleteCategory.UseVisualStyleBackColor = true;
            // 
            // btnAddCategory
            // 
            btnAddCategory.Location = new Point(32, 180);
            btnAddCategory.Margin = new Padding(4, 4, 4, 4);
            btnAddCategory.Name = "btnAddCategory";
            btnAddCategory.Size = new Size(654, 44);
            btnAddCategory.TabIndex = 2;
            btnAddCategory.Text = "Add";
            btnAddCategory.UseVisualStyleBackColor = true;
            // 
            // txtCategory
            // 
            txtCategory.Location = new Point(273, 114);
            txtCategory.Margin = new Padding(4, 4, 4, 4);
            txtCategory.Name = "txtCategory";
            txtCategory.Size = new Size(410, 35);
            txtCategory.TabIndex = 1;
            // 
            // lstCategories
            // 
            lstCategories.FormattingEnabled = true;
            lstCategories.Location = new Point(273, 279);
            lstCategories.Margin = new Padding(4, 4, 4, 4);
            lstCategories.Name = "lstCategories";
            lstCategories.Size = new Size(410, 214);
            lstCategories.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.White;
            tabPage2.Controls.Add(lblIgnoredList);
            tabPage2.Controls.Add(lblAddIgnore);
            tabPage2.Controls.Add(lblIgnoreTitle);
            tabPage2.Controls.Add(lstIgnoredApps);
            tabPage2.Controls.Add(btnDeleteIgnore);
            tabPage2.Controls.Add(btnAddIgnore);
            tabPage2.Controls.Add(txtIgnoreApp);
            tabPage2.Location = new Point(4, 42);
            tabPage2.Margin = new Padding(4, 4, 4, 4);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(4, 4, 4, 4);
            tabPage2.Size = new Size(1492, 586);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Ignore List";
            // 
            // lblIgnoredList
            // 
            lblIgnoredList.AutoSize = true;
            lblIgnoredList.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIgnoredList.Location = new Point(38, 276);
            lblIgnoredList.Margin = new Padding(4, 0, 4, 0);
            lblIgnoredList.Name = "lblIgnoredList";
            lblIgnoredList.Size = new Size(220, 30);
            lblIgnoredList.TabIndex = 7;
            lblIgnoredList.Text = "Ignored Applications";
            // 
            // lblAddIgnore
            // 
            lblAddIgnore.AutoSize = true;
            lblAddIgnore.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAddIgnore.Location = new Point(38, 116);
            lblAddIgnore.Margin = new Padding(4, 0, 4, 0);
            lblAddIgnore.Name = "lblAddIgnore";
            lblAddIgnore.Size = new Size(272, 30);
            lblAddIgnore.TabIndex = 5;
            lblAddIgnore.Text = "Add Application to Ignore";
            // 
            // lblIgnoreTitle
            // 
            lblIgnoreTitle.AutoSize = true;
            lblIgnoreTitle.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIgnoreTitle.Location = new Point(38, 24);
            lblIgnoreTitle.Margin = new Padding(4, 0, 4, 0);
            lblIgnoreTitle.Name = "lblIgnoreTitle";
            lblIgnoreTitle.Size = new Size(252, 36);
            lblIgnoreTitle.TabIndex = 4;
            lblIgnoreTitle.Text = "Ignore Applications";
            // 
            // lstIgnoredApps
            // 
            lstIgnoredApps.FormattingEnabled = true;
            lstIgnoredApps.Location = new Point(315, 276);
            lstIgnoredApps.Margin = new Padding(4, 4, 4, 4);
            lstIgnoredApps.Name = "lstIgnoredApps";
            lstIgnoredApps.Size = new Size(528, 214);
            lstIgnoredApps.TabIndex = 3;
            // 
            // btnDeleteIgnore
            // 
            btnDeleteIgnore.Location = new Point(38, 512);
            btnDeleteIgnore.Margin = new Padding(4, 4, 4, 4);
            btnDeleteIgnore.Name = "btnDeleteIgnore";
            btnDeleteIgnore.Size = new Size(807, 44);
            btnDeleteIgnore.TabIndex = 2;
            btnDeleteIgnore.Text = "Delete Selected";
            btnDeleteIgnore.UseVisualStyleBackColor = true;
            // 
            // btnAddIgnore
            // 
            btnAddIgnore.Location = new Point(38, 182);
            btnAddIgnore.Margin = new Padding(4, 4, 4, 4);
            btnAddIgnore.Name = "btnAddIgnore";
            btnAddIgnore.Size = new Size(807, 44);
            btnAddIgnore.TabIndex = 1;
            btnAddIgnore.Text = "Add";
            btnAddIgnore.UseVisualStyleBackColor = true;
            // 
            // txtIgnoreApp
            // 
            txtIgnoreApp.Location = new Point(382, 111);
            txtIgnoreApp.Margin = new Padding(4, 4, 4, 4);
            txtIgnoreApp.Name = "txtIgnoreApp";
            txtIgnoreApp.Size = new Size(460, 35);
            txtIgnoreApp.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.BackColor = Color.White;
            tabPage3.Controls.Add(lblSavedGoals);
            tabPage3.Controls.Add(label1);
            tabPage3.Controls.Add(label3);
            tabPage3.Controls.Add(lblSetGoal);
            tabPage3.Controls.Add(lblGoalsTitle);
            tabPage3.Controls.Add(lstGoals);
            tabPage3.Controls.Add(btnSaveGoal);
            tabPage3.Controls.Add(numGoalMinutes);
            tabPage3.Controls.Add(cmbGoalCategory);
            tabPage3.Location = new Point(4, 42);
            tabPage3.Margin = new Padding(4, 4, 4, 4);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(4, 4, 4, 4);
            tabPage3.Size = new Size(1492, 586);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Daily Goals";
            // 
            // lblSavedGoals
            // 
            lblSavedGoals.AutoSize = true;
            lblSavedGoals.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSavedGoals.Location = new Point(604, 108);
            lblSavedGoals.Margin = new Padding(4, 0, 4, 0);
            lblSavedGoals.Name = "lblSavedGoals";
            lblSavedGoals.Size = new Size(131, 30);
            lblSavedGoals.TabIndex = 8;
            lblSavedGoals.Text = "Saved Goals";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 272);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(151, 30);
            label1.TabIndex = 7;
            label1.Text = "Target Minutes";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(60, 171);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(96, 30);
            label3.TabIndex = 6;
            label3.Text = "Category";
            // 
            // lblSetGoal
            // 
            lblSetGoal.AutoSize = true;
            lblSetGoal.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSetGoal.Location = new Point(30, 94);
            lblSetGoal.Margin = new Padding(4, 0, 4, 0);
            lblSetGoal.Name = "lblSetGoal";
            lblSetGoal.Size = new Size(150, 30);
            lblSetGoal.TabIndex = 5;
            lblSetGoal.Text = "Set Daily Goal";
            // 
            // lblGoalsTitle
            // 
            lblGoalsTitle.AutoSize = true;
            lblGoalsTitle.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblGoalsTitle.Location = new Point(60, 20);
            lblGoalsTitle.Margin = new Padding(4, 0, 4, 0);
            lblGoalsTitle.Name = "lblGoalsTitle";
            lblGoalsTitle.Size = new Size(149, 36);
            lblGoalsTitle.TabIndex = 4;
            lblGoalsTitle.Text = "Daily Goals";
            // 
            // lstGoals
            // 
            lstGoals.FormattingEnabled = true;
            lstGoals.Location = new Point(604, 170);
            lstGoals.Margin = new Padding(4, 4, 4, 4);
            lstGoals.Name = "lstGoals";
            lstGoals.Size = new Size(520, 244);
            lstGoals.TabIndex = 3;
            // 
            // btnSaveGoal
            // 
            btnSaveGoal.Location = new Point(60, 372);
            btnSaveGoal.Margin = new Padding(4, 4, 4, 4);
            btnSaveGoal.Name = "btnSaveGoal";
            btnSaveGoal.Size = new Size(392, 44);
            btnSaveGoal.TabIndex = 2;
            btnSaveGoal.Text = "Save";
            btnSaveGoal.UseVisualStyleBackColor = true;
            // 
            // numGoalMinutes
            // 
            numGoalMinutes.Location = new Point(225, 268);
            numGoalMinutes.Margin = new Padding(4, 4, 4, 4);
            numGoalMinutes.Name = "numGoalMinutes";
            numGoalMinutes.Size = new Size(225, 35);
            numGoalMinutes.TabIndex = 1;
            // 
            // cmbGoalCategory
            // 
            cmbGoalCategory.FormattingEnabled = true;
            cmbGoalCategory.Location = new Point(225, 166);
            cmbGoalCategory.Margin = new Padding(4, 4, 4, 4);
            cmbGoalCategory.Name = "cmbGoalCategory";
            cmbGoalCategory.Size = new Size(224, 38);
            cmbGoalCategory.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.Transparent;
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { dashboardToolStripMenuItem, historyToolStripMenuItem, settingsToolStripMenuItem, reportsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(9, 3, 0, 3);
            menuStrip1.Size = new Size(1500, 42);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // dashboardToolStripMenuItem
            // 
            dashboardToolStripMenuItem.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dashboardToolStripMenuItem.ForeColor = Color.Black;
            dashboardToolStripMenuItem.Image = Properties.Resources.dashboard_48;
            dashboardToolStripMenuItem.Name = "dashboardToolStripMenuItem";
            dashboardToolStripMenuItem.Size = new Size(176, 36);
            dashboardToolStripMenuItem.Text = "Dashboard";
            dashboardToolStripMenuItem.Click += dashboardToolStripMenuItem_Click;
            // 
            // historyToolStripMenuItem
            // 
            historyToolStripMenuItem.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            historyToolStripMenuItem.Image = Properties.Resources.history_48;
            historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            historyToolStripMenuItem.Size = new Size(136, 36);
            historyToolStripMenuItem.Text = "History";
            historyToolStripMenuItem.Click += historyToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            settingsToolStripMenuItem.Image = Properties.Resources.setting_48;
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(144, 36);
            settingsToolStripMenuItem.Text = "Settings";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // sqliteCommand1
            // 
            sqliteCommand1.CommandTimeout = 30;
            sqliteCommand1.Connection = null;
            sqliteCommand1.Transaction = null;
            sqliteCommand1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // timerRefresh
            // 
            timerRefresh.Interval = 5000;
            timerRefresh.Tick += timerRefresh_Tick;
            // 
            // reportsToolStripMenuItem
            // 
            reportsToolStripMenuItem.Font = new Font("Segoe UI", 9.857143F, FontStyle.Bold, GraphicsUnit.Point, 0);
            reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            reportsToolStripMenuItem.Size = new Size(121, 36);
            reportsToolStripMenuItem.Text = "Reports";
            reportsToolStripMenuItem.Click += reportsToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1500, 675);
            Controls.Add(menuStrip1);
            Controls.Add(panelDashboard);
            Controls.Add(panelHistory);
            Controls.Add(panelSettings);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 4, 4, 4);
            Name = "MainForm";
            Text = "TaskForge";
            panelDashboard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartDashboard).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridHistory).EndInit();
            panelFilters.ResumeLayout(false);
            panelFilters.PerformLayout();
            panelSettings.ResumeLayout(false);
            tabSettings.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numGoalMinutes).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelDashboard;
        private Panel panelHistory;
        private Panel panelSettings;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem dashboardToolStripMenuItem;
        private ToolStripMenuItem historyToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private Microsoft.Data.Sqlite.SqliteCommand sqliteCommand1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDashboard;
        private System.Windows.Forms.Timer timerRefresh;
        private Label lblTotalTime;
        private DataGridView dataGridHistory;
        private Panel panelFilters;
        private ComboBox cmbApplication;
        private Label lblApplication;
        private DateTimePicker dtFrom;
        private Label lblTo;
        private Label lblFrom;
        private DateTimePicker dtTo;
        private ComboBox cmbCategory;
        private Label lblCategory;
        private TabControl tabSettings;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private ListBox lstCategories;
        private TextBox txtCategory;
        private Button btnDeleteCategory;
        private Button btnAddCategory;
        private Button btnDeleteIgnore;
        private Button btnAddIgnore;
        private TextBox txtIgnoreApp;
        private ListBox lstIgnoredApps;
        private ListBox lstGoals;
        private Button btnSaveGoal;
        private NumericUpDown numGoalMinutes;
        private ComboBox cmbGoalCategory;
        private Label lblExistingCategories;
        private Label lblAddCategory;
        private Label lblCategoryTitle;
        private Label lblAddIgnore;
        private Label lblIgnoreTitle;
        private Label lblIgnoredList;
        private Label label3;
        private Label lblSetGoal;
        private Label lblGoalsTitle;
        private Label lblSavedGoals;
        private Label label1;
        private BindingSource bindingSource1;
        private Button btnCheckGoals;
        private Button button1;
        private Panel panel1;
        private ToolTip toolTip1;
        private Button btnExport;
        private Button btnImport;
        private ToolStripMenuItem reportsToolStripMenuItem;
    }
}