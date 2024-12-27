namespace Renting_Application
{
    partial class RezervationManagement
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
            dgvReservations = new DataGridView();
            btnFinish = new Button();
            cbFilter = new ComboBox();
            btnRefresh = new Button();
            label1 = new Label();
            btnMenu = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvReservations).BeginInit();
            SuspendLayout();
            // 
            // dgvReservations
            // 
            dgvReservations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReservations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReservations.GridColor = SystemColors.Info;
            dgvReservations.Location = new Point(12, 61);
            dgvReservations.Name = "dgvReservations";
            dgvReservations.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvReservations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReservations.Size = new Size(733, 341);
            dgvReservations.TabIndex = 1;
            // 
            // btnFinish
            // 
            btnFinish.Location = new Point(775, 221);
            btnFinish.Name = "btnFinish";
            btnFinish.Size = new Size(127, 39);
            btnFinish.TabIndex = 2;
            btnFinish.Text = "Finish reservation";
            btnFinish.UseVisualStyleBackColor = true;
            btnFinish.Click += btnFinish_Click;
            // 
            // cbFilter
            // 
            cbFilter.FormattingEnabled = true;
            cbFilter.Location = new Point(763, 61);
            cbFilter.Name = "cbFilter";
            cbFilter.Size = new Size(152, 23);
            cbFilter.TabIndex = 3;
            cbFilter.SelectedIndexChanged += cbFilter_SelectedIndexChanged;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(763, 113);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(152, 32);
            btnRefresh.TabIndex = 4;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 22);
            label1.Name = "label1";
            label1.Size = new Size(156, 25);
            label1.TabIndex = 7;
            label1.Text = "Reservations List";
            // 
            // btnMenu
            // 
            btnMenu.Location = new Point(775, 363);
            btnMenu.Name = "btnMenu";
            btnMenu.Size = new Size(127, 39);
            btnMenu.TabIndex = 8;
            btnMenu.Text = "Go to menu";
            btnMenu.UseVisualStyleBackColor = true;
            btnMenu.Click += btnMenu_Click;
            // 
            // RezervationManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(945, 452);
            Controls.Add(btnMenu);
            Controls.Add(label1);
            Controls.Add(btnRefresh);
            Controls.Add(cbFilter);
            Controls.Add(btnFinish);
            Controls.Add(dgvReservations);
            Name = "RezervationManagement";
            Text = "RezervationManagement";
            ((System.ComponentModel.ISupportInitialize)dgvReservations).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvReservations;
        private Button btnFinish;
        private ComboBox cbFilter;
        private Button btnRefresh;
        private Label label1;
        private Button btnMenu;
    }
}