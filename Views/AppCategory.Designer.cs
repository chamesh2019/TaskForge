namespace TaskForge.Views
{
    partial class AppCategory
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
            panel1 = new Panel();
            saveButton = new Button();
            backButton = new Button();
            panel2 = new Panel();
            appCategoryData = new DataGridView();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)appCategoryData).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(saveButton);
            panel1.Controls.Add(backButton);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 42);
            panel1.TabIndex = 0;
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            saveButton.Location = new Point(694, 7);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(94, 29);
            saveButton.TabIndex = 0;
            saveButton.Text = "Save All";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // backButton
            // 
            backButton.Location = new Point(12, 7);
            backButton.Name = "backButton";
            backButton.Size = new Size(94, 29);
            backButton.TabIndex = 1;
            backButton.Text = "Back";
            backButton.UseVisualStyleBackColor = true;
            backButton.Click += backButton_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(appCategoryData);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 42);
            panel2.Name = "panel2";
            panel2.Size = new Size(800, 408);
            panel2.TabIndex = 1;
            // 
            // appCategoryData
            // 
            appCategoryData.AllowUserToAddRows = false;
            appCategoryData.AllowUserToDeleteRows = false;
            appCategoryData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            appCategoryData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            appCategoryData.BackgroundColor = Color.White;
            appCategoryData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            appCategoryData.Dock = DockStyle.Fill;
            appCategoryData.GridColor = Color.Black;
            appCategoryData.Location = new Point(0, 0);
            appCategoryData.Name = "appCategoryData";
            appCategoryData.RowHeadersWidth = 51;
            appCategoryData.Size = new Size(800, 408);
            appCategoryData.TabIndex = 0;
            // 
            // AppCategory
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "AppCategory";
            Text = "AddCategory";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)appCategoryData).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private DataGridView appCategoryData;
        private Button saveButton;
        private Button backButton;
    }
}