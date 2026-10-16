namespace DeviceManager
{
    partial class frmDeviceManager
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox grpInput;
        private System.Windows.Forms.Label lblDeviceId;
        private System.Windows.Forms.Label lblDeviceName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtDeviceId;
        private System.Windows.Forms.TextBox txtDeviceName;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridView dgvDevices;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Label lblTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpInput = new System.Windows.Forms.GroupBox();
            this.lblDeviceId = new System.Windows.Forms.Label();
            this.lblDeviceName = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtDeviceId = new System.Windows.Forms.TextBox();
            this.txtDeviceName = new System.Windows.Forms.TextBox();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.dgvDevices = new System.Windows.Forms.DataGridView();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevices)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 78, 121);
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1000, 55);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ DANH MỤC THIẾT BỊ ĐIỆN TỬ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpInput
            // 
            this.grpInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpInput.Controls.Add(this.lblDeviceId);
            this.grpInput.Controls.Add(this.txtDeviceId);
            this.grpInput.Controls.Add(this.lblDeviceName);
            this.grpInput.Controls.Add(this.txtDeviceName);
            this.grpInput.Controls.Add(this.lblCategory);
            this.grpInput.Controls.Add(this.cboCategory);
            this.grpInput.Controls.Add(this.lblPrice);
            this.grpInput.Controls.Add(this.txtPrice);
            this.grpInput.Controls.Add(this.lblQuantity);
            this.grpInput.Controls.Add(this.numQuantity);
            this.grpInput.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpInput.Location = new System.Drawing.Point(20, 65);
            this.grpInput.Name = "grpInput";
            this.grpInput.Size = new System.Drawing.Size(960, 170);
            this.grpInput.TabIndex = 1;
            this.grpInput.TabStop = false;
            this.grpInput.Text = "Thông tin thiết bị";
            // 
            // Labels and fields
            // 
            this.lblDeviceId.AutoSize = true;
            this.lblDeviceId.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDeviceId.Location = new System.Drawing.Point(22, 38);
            this.lblDeviceId.Name = "lblDeviceId";
            this.lblDeviceId.Size = new System.Drawing.Size(77, 15);
            this.lblDeviceId.TabIndex = 0;
            this.lblDeviceId.Text = "Mã thiết bị";
            this.txtDeviceId.Location = new System.Drawing.Point(150, 34);
            this.txtDeviceId.Name = "txtDeviceId";
            this.txtDeviceId.ReadOnly = true;
            this.txtDeviceId.Size = new System.Drawing.Size(280, 23);
            this.txtDeviceId.TabIndex = 1;
            this.lblDeviceName.AutoSize = true;
            this.lblDeviceName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDeviceName.Location = new System.Drawing.Point(22, 78);
            this.lblDeviceName.Name = "lblDeviceName";
            this.lblDeviceName.Size = new System.Drawing.Size(80, 15);
            this.lblDeviceName.TabIndex = 2;
            this.lblDeviceName.Text = "Tên thiết bị";
            this.txtDeviceName.Location = new System.Drawing.Point(150, 74);
            this.txtDeviceName.Name = "txtDeviceName";
            this.txtDeviceName.Size = new System.Drawing.Size(280, 23);
            this.txtDeviceName.TabIndex = 3;
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCategory.Location = new System.Drawing.Point(22, 118);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(81, 15);
            this.lblCategory.TabIndex = 4;
            this.lblCategory.Text = "Loại thiết bị";
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(150, 114);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(280, 23);
            this.cboCategory.TabIndex = 5;
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPrice.Location = new System.Drawing.Point(490, 38);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(49, 15);
            this.lblPrice.TabIndex = 6;
            this.lblPrice.Text = "Đơn giá";
            this.txtPrice.Location = new System.Drawing.Point(610, 34);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(300, 23);
            this.txtPrice.TabIndex = 7;
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblQuantity.Location = new System.Drawing.Point(490, 78);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(82, 15);
            this.lblQuantity.TabIndex = 8;
            this.lblQuantity.Text = "Số lượng tồn";
            this.numQuantity.Location = new System.Drawing.Point(610, 74);
            this.numQuantity.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(300, 23);
            this.numQuantity.TabIndex = 9;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlButtons.Controls.Add(this.btnAdd);
            this.pnlButtons.Controls.Add(this.btnUpdate);
            this.pnlButtons.Controls.Add(this.btnDelete);
            this.pnlButtons.Controls.Add(this.btnClear);
            this.pnlButtons.Location = new System.Drawing.Point(20, 245);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(960, 50);
            this.pnlButtons.TabIndex = 2;
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(31, 119, 180);
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(150, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(145, 36);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Thêm mới";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(320, 6);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(145, 36);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(490, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(145, 36);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(90, 98, 104);
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(660, 6);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(145, 36);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Làm mới / Hủy";
            this.btnClear.UseVisualStyleBackColor = false;
            // 
            // dgvDevices
            // 
            this.dgvDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDevices.Location = new System.Drawing.Point(20, 310);
            this.dgvDevices.Name = "dgvDevices";
            this.dgvDevices.Size = new System.Drawing.Size(960, 310);
            this.dgvDevices.TabIndex = 3;
            // 
            // frmDeviceManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.ClientSize = new System.Drawing.Size(1000, 640);
            this.Controls.Add(this.dgvDevices);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.grpInput);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frmDeviceManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý thiết bị điện tử";
            this.grpInput.ResumeLayout(false);
            this.grpInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevices)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
