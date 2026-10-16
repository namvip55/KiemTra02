.namespace InventoryManager
{
    partial class frmInventoryManager
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importCsvToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportCsvToolStripMenuItem;

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripButton tsbUpdate;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ToolStripButton tsbImport;
        private System.Windows.Forms.ToolStripButton tsbExport;

        private System.Windows.Forms.GroupBox grpProduct;
        private System.Windows.Forms.Label lblProductId;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblUnitPrice;
        private System.Windows.Forms.Label lblProductImage;

        private System.Windows.Forms.TextBox txtProductId;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.NumericUpDown nudUnitPrice;

        private System.Windows.Forms.PictureBox picProductImage;
        private System.Windows.Forms.Button btnChooseImage;

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;

        private System.Windows.Forms.DataGridView dgvProducts;

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblTotalQuantity;
        private System.Windows.Forms.ToolStripStatusLabel lblTotalValue;
        private System.Windows.Forms.ToolStripStatusLabel lblClock;
        private System.Windows.Forms.Timer timerClock;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importCsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbUpdate = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbImport = new System.Windows.Forms.ToolStripButton();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();

            this.grpProduct = new System.Windows.Forms.GroupBox();
            this.lblProductId = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.lblUnitPrice = new System.Windows.Forms.Label();
            this.lblProductImage = new System.Windows.Forms.Label();

            this.txtProductId = new System.Windows.Forms.TextBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.nudUnitPrice = new System.Windows.Forms.NumericUpDown();

            this.picProductImage = new System.Windows.Forms.PictureBox();
            this.btnChooseImage = new System.Windows.Forms.Button();

            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();

            this.dgvProducts = new System.Windows.Forms.DataGridView();

            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblTotalQuantity = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTotalValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblClock = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerClock = new System.Windows.Forms.Timer(this.components);

            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.grpProduct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUnitPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProductImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 760);
            this.MinimumSize = new System.Drawing.Size(1000, 650);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventory Manager";
            this.Name = "frmInventoryManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(
                this.frmInventoryManager_FormClosing);

            // MenuStrip
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.systemToolStripMenuItem,
                this.toolsToolStripMenuItem
            });

            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1200, 28);

            // System menu
            this.systemToolStripMenuItem.Text = "System";
            this.systemToolStripMenuItem.DropDownItems.AddRange(
                new System.Windows.Forms.ToolStripItem[]
                {
                    this.openFileToolStripMenuItem,
                    this.saveReportToolStripMenuItem,
                    new System.Windows.Forms.ToolStripSeparator(),
                    this.exitToolStripMenuItem
                });

            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click +=
                new System.EventHandler(this.openFileToolStripMenuItem_Click);

            this.saveReportToolStripMenuItem.Text = "Save Report";
            this.saveReportToolStripMenuItem.Click +=
                new System.EventHandler(this.saveReportToolStripMenuItem_Click);

            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click +=
                new System.EventHandler(this.exitToolStripMenuItem_Click);

            // Tools menu
            this.toolsToolStripMenuItem.Text = "Tools";
            this.toolsToolStripMenuItem.DropDownItems.AddRange(
                new System.Windows.Forms.ToolStripItem[]
                {
                    this.importCsvToolStripMenuItem,
                    this.exportCsvToolStripMenuItem
                });

            this.importCsvToolStripMenuItem.Text = "Import CSV";
            this.importCsvToolStripMenuItem.Click +=
                new System.EventHandler(this.importCsvToolStripMenuItem_Click);

            this.exportCsvToolStripMenuItem.Text = "Export CSV";
            this.exportCsvToolStripMenuItem.Click +=
                new System.EventHandler(this.exportCsvToolStripMenuItem_Click);

            // ToolStrip
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.tsbAdd,
                this.tsbUpdate,
                this.tsbDelete,
                new System.Windows.Forms.ToolStripSeparator(),
                this.tsbImport,
                this.tsbExport
            });

            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1200, 27);

            this.tsbAdd.Text = "➕ Add";
            this.tsbAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.tsbUpdate.Text = "✏ Update";
            this.tsbUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            this.tsbDelete.Text = "✖ Delete";
            this.tsbDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.tsbImport.Text = "📂 Import CSV";
            this.tsbImport.Click +=
                new System.EventHandler(this.importCsvToolStripMenuItem_Click);

            this.tsbExport.Text = "💾 Export CSV";
            this.tsbExport.Click +=
                new System.EventHandler(this.exportCsvToolStripMenuItem_Click);

            // GroupBox
            this.grpProduct.Text = "Product Information";
            this.grpProduct.Location = new System.Drawing.Point(15, 65);
            this.grpProduct.Size = new System.Drawing.Size(1170, 245);
            this.grpProduct.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;

            // Product ID
            this.lblProductId.Text = "Product ID:";
            this.lblProductId.Location = new System.Drawing.Point(20, 35);
            this.lblProductId.AutoSize = true;

            this.txtProductId.Location = new System.Drawing.Point(130, 31);
            this.txtProductId.Size = new System.Drawing.Size(240, 23);
            this.txtProductId.Name = "txtProductId";

            // Product name
            this.lblProductName.Text = "Product Name:";
            this.lblProductName.Location = new System.Drawing.Point(20, 75);
            this.lblProductName.AutoSize = true;

            this.txtProductName.Location = new System.Drawing.Point(130, 71);
            this.txtProductName.Size = new System.Drawing.Size(240, 23);
            this.txtProductName.Name = "txtProductName";

            // Quantity
            this.lblQuantity.Text = "Quantity:";
            this.lblQuantity.Location = new System.Drawing.Point(20, 115);
            this.lblQuantity.AutoSize = true;

            this.nudQuantity.Location = new System.Drawing.Point(130, 111);
            this.nudQuantity.Size = new System.Drawing.Size(240, 23);
            this.nudQuantity.Minimum = 0;
            this.nudQuantity.Maximum = 100000000;
            this.nudQuantity.ThousandsSeparator = true;

            // Unit price
            this.lblUnitPrice.Text = "Unit Price:";
            this.lblUnitPrice.Location = new System.Drawing.Point(20, 155);
            this.lblUnitPrice.AutoSize = true;

            this.nudUnitPrice.Location = new System.Drawing.Point(130, 151);
            this.nudUnitPrice.Size = new System.Drawing.Size(240, 23);
            this.nudUnitPrice.Minimum = 0;
            this.nudUnitPrice.Maximum = 1000000000000M;
            this.nudUnitPrice.DecimalPlaces = 2;
            this.nudUnitPrice.ThousandsSeparator = true;

            // Product image
            this.lblProductImage.Text = "Product Image:";
            this.lblProductImage.Location = new System.Drawing.Point(430, 35);
            this.lblProductImage.AutoSize = true;

            this.picProductImage.Location = new System.Drawing.Point(430, 60);
            this.picProductImage.Size = new System.Drawing.Size(170, 135);
            this.picProductImage.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;
            this.picProductImage.SizeMode =
                System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picProductImage.Name = "picProductImage";
            this.picProductImage.TabStop = false;

            this.btnChooseImage.Text = "Choose Image";
            this.btnChooseImage.Location = new System.Drawing.Point(430, 202);
            this.btnChooseImage.Size = new System.Drawing.Size(170, 28);
            this.btnChooseImage.Click +=
                new System.EventHandler(this.btnChooseImage_Click);

            // Buttons
            this.btnAdd.Text = "Add";
            this.btnAdd.Location = new System.Drawing.Point(680, 60);
            this.btnAdd.Size = new System.Drawing.Size(120, 35);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnUpdate.Text = "Update";
            this.btnUpdate.Location = new System.Drawing.Point(680, 105);
            this.btnUpdate.Size = new System.Drawing.Size(120, 35);
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            this.btnDelete.Text = "Delete";
            this.btnDelete.Location = new System.Drawing.Point(680, 150);
            this.btnDelete.Size = new System.Drawing.Size(120, 35);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnClear.Text = "Clear";
            this.btnClear.Location = new System.Drawing.Point(680, 195);
            this.btnClear.Size = new System.Drawing.Size(120, 35);
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            this.grpProduct.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.lblProductId,
                this.txtProductId,
                this.lblProductName,
                this.txtProductName,
                this.lblQuantity,
                this.nudQuantity,
                this.lblUnitPrice,
                this.nudUnitPrice,
                this.lblProductImage,
                this.picProductImage,
                this.btnChooseImage,
                this.btnAdd,
                this.btnUpdate,
                this.btnDelete,
                this.btnClear
            });

            // DataGridView
            this.dgvProducts.Location = new System.Drawing.Point(15, 325);
            this.dgvProducts.Size = new System.Drawing.Size(1170, 385);
            this.dgvProducts.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;

            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.AutoGenerateColumns = false;
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.MultiSelect = false;
            this.dgvProducts.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.RowHeadersVisible = false;
            this.dgvProducts.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProducts.SelectionChanged +=
                new System.EventHandler(this.dgvProducts_SelectionChanged);

            // StatusStrip
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.lblTotalQuantity,
                this.lblTotalValue,
                this.lblClock
            });

            this.statusStrip1.Location =
                new System.Drawing.Point(0, 738);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1200, 22);

            this.lblTotalQuantity.Text = "Total Quantity: 0";
            this.lblTotalValue.Text = "Total Inventory Value: 0";
            this.lblClock.Text = "Time: --:--:--";

            this.lblClock.Spring = true;
            this.lblClock.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);

            // Add controls to Form
            this.Controls.Add(this.dgvProducts);
            this.Controls.Add(this.grpProduct);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);

            this.MainMenuStrip = this.menuStrip1;

            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.grpProduct.ResumeLayout(false);
            this.grpProduct.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUnitPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProductImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}