using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai3_MDI_QuanLySieuThi
{
    public partial class frmMain : Form
    {
        private bool _allowClose;

        public frmMain()
        {
            InitializeComponent();
            this.FormClosing += frmMain_FormClosing;
        }

        public void SetBusy(bool busy, string message)
        {
            if (IsDisposed) return;

            if (InvokeRequired)
            {
                if (IsHandleCreated)
                    BeginInvoke(new Action<bool, string>(SetBusy), busy, message);
                return;
            }

            lblSystemStatus.Text = message;
            progressBarMain.Visible = busy;
            progressBarMain.Style = ProgressBarStyle.Marquee;
            progressBarMain.MarqueeAnimationSpeed = busy ? 30 : 0;
        }

        private void OpenMdiChild<T>() where T : Form, new()
        {
            foreach (Form child in MdiChildren)
            {
                if (child is T)
                {
                    if (child.WindowState == FormWindowState.Minimized)
                        child.WindowState = FormWindowState.Normal;
                    child.Activate();
                    return;
                }
            }

            T form = new T();
            form.MdiParent = this;
            form.Show();
        }

        private void mnuProducts_Click(object sender, EventArgs e)
        {
            OpenMdiChild<frmProductCatalog>();
        }

        private void mnuInventory_Click(object sender, EventArgs e)
        {
            OpenMdiChild<frmInventoryManager>();
        }

        private void mnuRevenue_Click(object sender, EventArgs e)
        {
            OpenMdiChild<frmAsyncReport>();
        }

        private void mnuCascade_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void mnuTileHorizontal_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void mnuTileVertical_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void mnuLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có muốn đăng xuất khỏi phiên hiện tại không?",
                "Xác nhận đăng xuất", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            foreach (Form child in MdiChildren.ToArray())
                child.Close();

            lblCurrentUser.Text = "Người dùng: Chưa đăng nhập";
            lblSystemStatus.Text = "Đã đăng xuất (mô phỏng).";
            MessageBox.Show("Đã kết thúc phiên làm việc mô phỏng.",
                "Đăng xuất", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_allowClose) return;

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát ứng dụng?",
                "Xác nhận thoát", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }

            _allowClose = true;
        }
    }

    public class frmProductCatalog : Form
    {
        private readonly BindingSource _source = new BindingSource();
        private readonly ErrorProvider _errors = new ErrorProvider();
        private TextBox txtProductId, txtProductName, txtPrice;
        private ComboBox cboCategory;
        private NumericUpDown numQuantity;
        private PictureBox picProductImage;
        private DataGridView dgvProducts;
        private Label lblStockSummary;
        private string _imagePath;

        public frmProductCatalog()
        {
            Text = "Quản lý sản phẩm";
            Width = 1050; Height = 650;
            StartPosition = FormStartPosition.CenterParent;
            BuildUi();
            _source.DataSource = SupermarketRepository.Products;
            dgvProducts.DataSource = _source;
            dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;
            SupermarketRepository.Products.ListChanged += Products_ListChanged;
            RefreshSummary();
        }

        private void BuildUi()
        {
            var input = new GroupBox { Text = "Thông tin sản phẩm", Left = 12, Top = 12, Width = 390, Height = 340 };
            Controls.Add(input);

            AddLabel(input, "Mã sản phẩm:", 18, 32);
            txtProductId = new TextBox { Left = 125, Top = 28, Width = 230, ReadOnly = true };
            input.Controls.Add(txtProductId);

            AddLabel(input, "Tên sản phẩm:", 18, 70);
            txtProductName = new TextBox { Left = 125, Top = 66, Width = 230 };
            input.Controls.Add(txtProductName);

            AddLabel(input, "Loại sản phẩm:", 18, 108);
            cboCategory = new ComboBox { Left = 125, Top = 104, Width = 230, DropDownStyle = ComboBoxStyle.DropDownList };
            cboCategory.Items.AddRange(new object[] { "Thực phẩm", "Đồ uống", "Gia dụng", "Điện tử", "Khác" });
            input.Controls.Add(cboCategory);

            AddLabel(input, "Đơn giá:", 18, 146);
            txtPrice = new TextBox { Left = 125, Top = 142, Width = 230 };
            input.Controls.Add(txtPrice);

            AddLabel(input, "Số lượng tồn:", 18, 184);
            numQuantity = new NumericUpDown { Left = 125, Top = 180, Width = 230, Maximum = 1000000, Minimum = 0 };
            input.Controls.Add(numQuantity);

            var btnAdd = new Button { Text = "Thêm", Left = 18, Top = 225, Width = 78 };
            var btnUpdate = new Button { Text = "Cập nhật", Left = 101, Top = 225, Width = 85 };
            var btnDelete = new Button { Text = "Xóa", Left = 191, Top = 225, Width = 75 };
            var btnClear = new Button { Text = "Làm mới", Left = 271, Top = 225, Width = 85 };
            btnAdd.Click += btnAdd_Click; btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click; btnClear.Click += delegate { ClearInputs(); };

            var btnImage = new Button { Text = "Chọn ảnh...", Left = 18, Top = 270, Width = 100 };
            btnImage.Click += btnImage_Click;
            picProductImage = new PictureBox { Left = 125, Top = 265, Width = 90, Height = 58, BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.Zoom };
            input.Controls.AddRange(new Control[] { btnAdd, btnUpdate, btnDelete, btnClear, btnImage, picProductImage });

            dgvProducts = new DataGridView
            {
                Left = 420, Top = 12, Width = 600, Height = 540,
                ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                AutoGenerateColumns = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductId", HeaderText = "Mã", FillWeight = 55 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Name", HeaderText = "Tên sản phẩm", FillWeight = 130 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Category", HeaderText = "Loại", FillWeight = 80 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Price", HeaderText = "Đơn giá", FillWeight = 80, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Tồn", FillWeight = 45 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ImagePath", HeaderText = "Đường dẫn ảnh", FillWeight = 100 });
            Controls.Add(dgvProducts);

            var btnImport = new Button { Text = "Nhập CSV", Left = 420, Top = 565, Width = 100 };
            var btnExport = new Button { Text = "Xuất CSV", Left = 530, Top = 565, Width = 100 };
            btnImport.Click += btnImport_Click; btnExport.Click += btnExport_Click;
            Controls.Add(btnImport); Controls.Add(btnExport);

            lblStockSummary = new Label { Left = 650, Top = 570, Width = 370, Height = 25, TextAlign = ContentAlignment.MiddleRight };
            Controls.Add(lblStockSummary);
            _errors.ContainerControl = this;
        }

        private static void AddLabel(Control parent, string text, int x, int y)
        {
            parent.Controls.Add(new Label { Text = text, Left = x, Top = y, Width = 105, Height = 24, TextAlign = ContentAlignment.MiddleLeft });
        }

        private bool ValidateInputs()
        {
            bool valid = true;
            _errors.Clear();
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                _errors.SetError(txtProductName, "Tên sản phẩm không được để trống.");
                valid = false;
            }
            if (cboCategory.SelectedIndex < 0)
            {
                _errors.SetError(cboCategory, "Vui lòng chọn loại sản phẩm.");
                valid = false;
            }
            decimal price;
            if (!decimal.TryParse(txtPrice.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out price) &&
                !decimal.TryParse(txtPrice.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out price))
            {
                _errors.SetError(txtPrice, "Đơn giá phải là số hợp lệ.");
                valid = false;
            }
            else if (price < 0)
            {
                _errors.SetError(txtPrice, "Đơn giá không được âm.");
                valid = false;
            }
            return valid;
        }

        private Product ReadProduct(Product existing)
        {
            decimal price;
            if (!decimal.TryParse(txtPrice.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out price))
                decimal.TryParse(txtPrice.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out price);

            if (existing == null)
                existing = new Product { ProductId = SupermarketRepository.CreateProductId() };

            existing.Name = txtProductName.Text.Trim();
            existing.Category = cboCategory.Text;
            existing.Price = price;
            existing.Quantity = (int)numQuantity.Value;
            existing.ImagePath = _imagePath;
            return existing;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;
            SupermarketRepository.Products.Add(ReadProduct(null));
            RefreshSummary();
            ClearInputs();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;
            Product selected = _source.Current as Product;
            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần cập nhật.");
                return;
            }
            ReadProduct(selected);
            _source.ResetCurrentItem();
            RefreshSummary();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Product selected = _source.Current as Product;
            if (selected == null) return;
            if (MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            SupermarketRepository.Products.Remove(selected);
            RefreshSummary();
            ClearInputs();
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Tệp ảnh (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";
                dialog.Title = "Chọn ảnh sản phẩm";
                if (dialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (Image img = Image.FromFile(dialog.FileName))
                    {
                        if (picProductImage.Image != null) picProductImage.Image.Dispose();
                        picProductImage.Image = new Bitmap(img);
                    }
                    _imagePath = dialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể mở ảnh: " + ex.Message);
                }
            }
        }

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            Product p = _source.Current as Product;
            if (p == null) return;
            txtProductId.Text = p.ProductId;
            txtProductName.Text = p.Name;
            cboCategory.SelectedItem = p.Category;
            txtPrice.Text = p.Price.ToString(CultureInfo.CurrentCulture);
            numQuantity.Value = Math.Max(numQuantity.Minimum, Math.Min(numQuantity.Maximum, p.Quantity));
            _imagePath = p.ImagePath;
            try
            {
                if (picProductImage.Image != null) picProductImage.Image.Dispose();
                picProductImage.Image = null;
                if (!string.IsNullOrWhiteSpace(_imagePath) && File.Exists(_imagePath))
                {
                    using (Image img = Image.FromFile(_imagePath)) picProductImage.Image = new Bitmap(img);
                }
            }
            catch { picProductImage.Image = null; }
        }

        private void ClearInputs()
        {
            txtProductId.Clear();
            txtProductName.Clear();
            cboCategory.SelectedIndex = -1;
            txtPrice.Clear();
            numQuantity.Value = 0;
            _imagePath = null;
            if (picProductImage.Image != null) { picProductImage.Image.Dispose(); picProductImage.Image = null; }
            dgvProducts.ClearSelection();
            _errors.Clear();
        }

        private void Products_ListChanged(object sender, ListChangedEventArgs e) { RefreshSummary(); }

        private void RefreshSummary()
        {
            lblStockSummary.Text = "Tổng tồn: " + SupermarketRepository.GetTotalQuantity().ToString("N0") +
                " | Giá trị kho: " + SupermarketRepository.GetStockValue().ToString("N0");
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog { Filter = "CSV (*.csv)|*.csv", Title = "Nhập danh sách sản phẩm" })
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                try
                {
                    int imported = 0;
                    foreach (string line in File.ReadAllLines(dialog.FileName))
                    {
                        if (string.IsNullOrWhiteSpace(line) || line.StartsWith("ProductId,")) continue;
                        string[] parts = line.Split(',');
                        if (parts.Length < 5) continue;
                        decimal price; int quantity;
                        if (!decimal.TryParse(parts[3], NumberStyles.Number, CultureInfo.InvariantCulture, out price)) continue;
                        if (!int.TryParse(parts[4], out quantity)) continue;
                        SupermarketRepository.Products.Add(new Product
                        {
                            ProductId = string.IsNullOrWhiteSpace(parts[0]) ? SupermarketRepository.CreateProductId() : parts[0],
                            Name = parts[1], Category = parts[2], Price = price, Quantity = quantity,
                            ImagePath = parts.Length > 5 ? parts[5] : null
                        });
                        imported++;
                    }
                    MessageBox.Show("Đã nhập " + imported + " sản phẩm.");
                }
                catch (Exception ex) { MessageBox.Show("Lỗi nhập CSV: " + ex.Message); }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog { Filter = "CSV (*.csv)|*.csv", FileName = "products.csv", Title = "Xuất danh sách sản phẩm" })
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                try
                {
                    var lines = new System.Collections.Generic.List<string> { "ProductId,Name,Category,Price,Quantity,ImagePath" };
                    foreach (Product p in SupermarketRepository.Products)
                    {
                        lines.Add(string.Join(",", Csv(p.ProductId), Csv(p.Name), Csv(p.Category),
                            p.Price.ToString(CultureInfo.InvariantCulture), p.Quantity.ToString(), Csv(p.ImagePath)));
                    }
                    File.WriteAllLines(dialog.FileName, lines);
                    MessageBox.Show("Xuất CSV thành công.");
                }
                catch (Exception ex) { MessageBox.Show("Lỗi xuất CSV: " + ex.Message); }
            }
        }

        private static string Csv(string value)
        {
            value = value ?? "";
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            SupermarketRepository.Products.ListChanged -= Products_ListChanged;
            if (picProductImage != null && picProductImage.Image != null) picProductImage.Image.Dispose();
            _errors.Dispose();
            base.OnFormClosed(e);
        }
    }

    public class frmInventoryManager : Form
    {
        private ComboBox cboProduct, cboType;
        private NumericUpDown numQuantity;
        private TextBox txtUnitPrice;
        private DataGridView dgvTransactions;
        private BindingSource _transactionSource = new BindingSource();
        private Label lblSummary;

        public frmInventoryManager()
        {
            Text = "Quản lý nhập xuất";
            Width = 950; Height = 600;
            BuildUi();
            _transactionSource.DataSource = SupermarketRepository.Transactions;
            dgvTransactions.DataSource = _transactionSource;
            RefreshSummary();
        }

        private void BuildUi()
        {
            var panel = new GroupBox { Text = "Tạo giao dịch kho", Left = 12, Top = 12, Width = 900, Height = 130 };
            Controls.Add(panel);
            panel.Controls.Add(new Label { Text = "Sản phẩm:", Left = 12, Top = 28, Width = 75 });
            cboProduct = new ComboBox { Left = 90, Top = 24, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList, DisplayMember = "Name", ValueMember = "ProductId", DataSource = SupermarketRepository.Products };
            panel.Controls.Add(cboProduct);
            panel.Controls.Add(new Label { Text = "Loại:", Left = 355, Top = 28, Width = 45 });
            cboType = new ComboBox { Left = 405, Top = 24, Width = 100, DropDownStyle = ComboBoxStyle.DropDownList };
            cboType.Items.AddRange(new object[] { "Nhập", "Xuất" }); cboType.SelectedIndex = 0; panel.Controls.Add(cboType);
            panel.Controls.Add(new Label { Text = "Số lượng:", Left = 520, Top = 28, Width = 70 });
            numQuantity = new NumericUpDown { Left = 590, Top = 24, Width = 100, Minimum = 1, Maximum = 1000000, Value = 1 }; panel.Controls.Add(numQuantity);
            panel.Controls.Add(new Label { Text = "Đơn giá:", Left = 12, Top = 68, Width = 75 });
            txtUnitPrice = new TextBox { Left = 90, Top = 64, Width = 150 }; panel.Controls.Add(txtUnitPrice);
            var btnSave = new Button { Text = "Ghi giao dịch", Left = 260, Top = 62, Width = 120 }; btnSave.Click += btnSave_Click; panel.Controls.Add(btnSave);
            lblSummary = new Label { Left = 400, Top = 67, Width = 450, Height = 25 }; panel.Controls.Add(lblSummary);

            dgvTransactions = new DataGridView
            {
                Left = 12, Top = 160, Width = 900, Height = 380, ReadOnly = true,
                AllowUserToAddRows = false, AutoGenerateColumns = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };
            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TransactionId", HeaderText = "Mã giao dịch" });
            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "Sản phẩm" });
            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Type", HeaderText = "Loại" });
            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Số lượng" });
            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "UnitPrice", HeaderText = "Đơn giá", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Amount", HeaderText = "Thành tiền", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CreatedAt", HeaderText = "Thời gian", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm:ss" } });
            Controls.Add(dgvTransactions);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Product product = cboProduct.SelectedItem as Product;
            if (product == null) { MessageBox.Show("Vui lòng chọn sản phẩm."); return; }
            decimal price;
            if (!decimal.TryParse(txtUnitPrice.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out price) &&
                !decimal.TryParse(txtUnitPrice.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out price))
            {
                MessageBox.Show("Đơn giá không hợp lệ."); return;
            }
            if (price < 0) { MessageBox.Show("Đơn giá không được âm."); return; }

            int quantity = (int)numQuantity.Value;
            string type = cboType.Text;
            if (type == "Xuất" && product.Quantity < quantity)
            {
                MessageBox.Show("Số lượng xuất vượt quá tồn kho.", "Không đủ hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (type == "Nhập") product.Quantity += quantity;
            else product.Quantity -= quantity;

            if (price == 0) price = product.Price;
            var transaction = new InventoryTransaction
            {
                TransactionId = SupermarketRepository.CreateTransactionId(),
                ProductId = product.ProductId, ProductName = product.Name,
                Type = type, Quantity = quantity, UnitPrice = price, CreatedAt = DateTime.Now
            };
            SupermarketRepository.AddTransaction(transaction);
            _transactionSource.ResetBindings(false);
            RefreshSummary();
            MessageBox.Show("Đã ghi nhận giao dịch " + type.ToLowerInvariant() + ".");
        }

        private void RefreshSummary()
        {
            lblSummary.Text = "Tổng tồn: " + SupermarketRepository.GetTotalQuantity().ToString("N0") +
                " | Giá trị kho: " + SupermarketRepository.GetStockValue().ToString("N0");
        }
    }

    public class frmAsyncReport : Form
    {
        private Button btnLoadRevenue, btnStartSync, btnStopSync;
        private Label lblReportStatus, lblTotalRevenue, lblTransactionCount, lblSyncStatus;
        private DataGridView dgvRevenueReport, dgvSyncLogs;
        private CancellationTokenSource _syncCts;
        private Task _syncTask;
        private int _fakeOrderNumber;
        private bool _closing;
        private bool _allowClose;

        public frmAsyncReport()
        {
            Text = "Báo cáo thống kê & đồng bộ";
            Width = 1050; Height = 700;
            BuildUi();
            FormClosing += frmAsyncReport_FormClosing;
        }

        private void BuildUi()
        {
            var reportGroup = new GroupBox { Text = "Báo cáo doanh thu bất đồng bộ", Left = 12, Top = 12, Width = 1000, Height = 300 };
            Controls.Add(reportGroup);
            btnLoadRevenue = new Button { Text = "Tải Báo Cáo Doanh Thu", Left = 18, Top = 28, Width = 190, Height = 32 };
            btnLoadRevenue.Click += btnLoadRevenue_Click;
            reportGroup.Controls.Add(btnLoadRevenue);
            lblReportStatus = new Label { Text = "Sẵn sàng", Left = 225, Top = 34, Width = 300 };
            reportGroup.Controls.Add(lblReportStatus);
            lblTotalRevenue = new Label { Text = "Tổng doanh thu: 0", Left = 18, Top = 70, Width = 250 };
            reportGroup.Controls.Add(lblTotalRevenue);
            lblTransactionCount = new Label { Text = "Số giao dịch: 0", Left = 290, Top = 70, Width = 200 };
            reportGroup.Controls.Add(lblTransactionCount);

            dgvRevenueReport = new DataGridView
            {
                Left = 18, Top = 105, Width = 960, Height = 175, ReadOnly = true,
                AutoGenerateColumns = false, AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };
            dgvRevenueReport.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TransactionId", HeaderText = "Mã giao dịch" });
            dgvRevenueReport.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "Sản phẩm" });
            dgvRevenueReport.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CreatedAt", HeaderText = "Thời gian", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm:ss" } });
            dgvRevenueReport.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Số lượng" });
            dgvRevenueReport.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Revenue", HeaderText = "Doanh thu", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            reportGroup.Controls.Add(dgvRevenueReport);

            var syncGroup = new GroupBox { Text = "Đồng bộ dữ liệu nền (chu kỳ 2 giây)", Left = 12, Top = 325, Width = 1000, Height = 320 };
            Controls.Add(syncGroup);
            btnStartSync = new Button { Text = "Bắt đầu đồng bộ", Left = 18, Top = 28, Width = 140 };
            btnStopSync = new Button { Text = "Dừng đồng bộ", Left = 168, Top = 28, Width = 130, Enabled = false };
            btnStartSync.Click += btnStartSync_Click;
            btnStopSync.Click += async delegate { await StopSyncAsync(); };
            lblSyncStatus = new Label { Text = "Chưa chạy", Left = 320, Top = 34, Width = 600 };
            syncGroup.Controls.AddRange(new Control[] { btnStartSync, btnStopSync, lblSyncStatus });

            dgvSyncLogs = new DataGridView
            {
                Left = 18, Top = 70, Width = 960, Height = 230, ReadOnly = true,
                AutoGenerateColumns = false, AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };
            dgvSyncLogs.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Thời gian", DataPropertyName = "Time", DefaultCellStyle = new DataGridViewCellStyle { Format = "HH:mm:ss" } });
            dgvSyncLogs.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Thông báo", DataPropertyName = "Message" });
            dgvSyncLogs.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Đơn hàng mới", DataPropertyName = "NewOrderCount" });
            syncGroup.Controls.Add(dgvSyncLogs);
        }

        private async void btnLoadRevenue_Click(object sender, EventArgs e)
        {
            btnLoadRevenue.Enabled = false;
            frmMain main = MdiParent as frmMain;
            try
            {
                lblReportStatus.Text = "Đang tải dữ liệu...";
                if (main != null) main.SetBusy(true, "Đang tải báo cáo doanh thu...");
                var snapshot = SupermarketRepository.GetTransactionSnapshot();

                RevenueReport report = await Task.Run(async () =>
                {
                    await Task.Delay(4000);
                    var result = new RevenueReport();
                    // Doanh thu tính từ giao dịch Xuất; giao dịch Nhập không được tính là doanh thu.
                    var sales = snapshot.Where(t => t.Type == "Xuất").ToList();
                    result.TransactionCount = sales.Count;
                    result.TotalRevenue = sales.Sum(t => t.Amount);
                    result.Lines = sales.Select(t => new RevenueLine
                    {
                        TransactionId = t.TransactionId,
                        ProductName = t.ProductName,
                        CreatedAt = t.CreatedAt,
                        Quantity = t.Quantity,
                        UnitPrice = t.UnitPrice,
                        Revenue = t.Amount
                    }).OrderByDescending(x => x.CreatedAt).ToList();
                    return result;
                });

                lblTotalRevenue.Text = "Tổng doanh thu: " + report.TotalRevenue.ToString("N0");
                lblTransactionCount.Text = "Số giao dịch xuất: " + report.TransactionCount;
                dgvRevenueReport.DataSource = new BindingList<RevenueLine>(report.Lines);
                lblReportStatus.Text = "Tải báo cáo thành công lúc " + DateTime.Now.ToString("HH:mm:ss");
            }
            catch (Exception ex)
            {
                lblReportStatus.Text = "Không thể tải báo cáo.";
                MessageBox.Show("Lỗi tải báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (main != null && !main.IsDisposed) main.SetBusy(false, "Sẵn sàng");
                if (!IsDisposed) btnLoadRevenue.Enabled = true;
            }
        }

        private void btnStartSync_Click(object sender, EventArgs e)
        {
            if (_syncTask != null && !_syncTask.IsCompleted) return;
            _syncCts = new CancellationTokenSource();
            _syncTask = RunSyncLoopAsync(_syncCts.Token);
            btnStartSync.Enabled = false;
            btnStopSync.Enabled = true;
            lblSyncStatus.Text = "Đang đồng bộ...";
        }

        private async Task RunSyncLoopAsync(CancellationToken token)
        {
            try
            {
                while (true)
                {
                    await Task.Delay(2000, token);
                    token.ThrowIfCancellationRequested();

                    // Công việc giả lập trên Thread phụ; không đọc/ghi Control trong Task.Run.
                    SyncLog log = await Task.Run(() =>
                    {
                        Thread.Sleep(100);
                        int newOrders = Interlocked.Increment(ref _fakeOrderNumber) % 4;
                        return new SyncLog
                        {
                            Time = DateTime.Now,
                            NewOrderCount = newOrders,
                            Message = newOrders == 0
                                ? "Đã quét dữ liệu; không có đơn hàng mới."
                                : "Đã đồng bộ " + newOrders + " đơn hàng mới."
                        };
                    }, token);

                    // Bắt buộc chuyển cập nhật giao diện về UI Thread.
                    UpdateSyncUiSafely(log);
                }
            }
            catch (OperationCanceledException)
            {
                // Hủy theo yêu cầu là luồng kết thúc bình thường.
            }
            catch (Exception ex)
            {
                UpdateSyncStatusSafely("Lỗi đồng bộ: " + ex.Message);
            }
            finally
            {
                if (!IsDisposed && IsHandleCreated)
                {
                    if (InvokeRequired)
                        BeginInvoke(new Action(FinishSyncUi));
                    else
                        FinishSyncUi();
                }
            }
        }

        private void UpdateSyncUiSafely(SyncLog log)
        {
            if (IsDisposed || _closing || !IsHandleCreated) return;

            if (InvokeRequired)
            {
                try { Invoke(new Action<SyncLog>(UpdateSyncUiSafely), log); }
                catch (InvalidOperationException) { }
                return;
            }

            lblSyncStatus.Text = log.Message + " (" + log.Time.ToString("HH:mm:ss") + ")";
            dgvSyncLogs.Rows.Insert(0, log.Time.ToString("HH:mm:ss"), log.Message, log.NewOrderCount);
            if (dgvSyncLogs.Rows.Count > 100) dgvSyncLogs.Rows.RemoveAt(dgvSyncLogs.Rows.Count - 1);
        }

        private void UpdateSyncStatusSafely(string message)
        {
            if (IsDisposed || _closing || !IsHandleCreated) return;
            if (InvokeRequired)
            {
                try { BeginInvoke(new Action<string>(UpdateSyncStatusSafely), message); }
                catch { }
                return;
            }
            lblSyncStatus.Text = message;
        }

        private async Task StopSyncAsync()
        {
            if (_syncCts == null) return;
            btnStopSync.Enabled = false;
            _syncCts.Cancel();

            if (_syncTask != null)
            {
                try { await _syncTask; }
                catch (OperationCanceledException) { }
            }

            if (_syncCts != null)
            {
                _syncCts.Dispose();
                _syncCts = null;
            }
            btnStartSync.Enabled = true;
            btnStopSync.Enabled = false;
            if (!_closing) lblSyncStatus.Text = "Đã dừng đồng bộ.";
        }

        private void FinishSyncUi()
        {
            if (IsDisposed || _closing) return;
            btnStartSync.Enabled = true;
            btnStopSync.Enabled = false;
            if (_syncCts != null)
            {
                _syncCts.Dispose();
                _syncCts = null;
            }
        }

        private async void frmAsyncReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_allowClose) return;

            if (_syncCts != null && _syncTask != null && !_syncTask.IsCompleted)
            {
                // Giữ Form sống trong lúc chờ tác vụ dừng, tránh đóng Handle trước khi tác vụ kết thúc.
                e.Cancel = true;
                _closing = true;
                _syncCts.Cancel();

                try { await _syncTask; }
                catch { }

                if (_syncCts != null)
                {
                    _syncCts.Dispose();
                    _syncCts = null;
                }

                _allowClose = true;
                BeginInvoke(new Action(Close));
                return;
            }

            _closing = true;
            if (_syncCts != null)
            {
                _syncCts.Cancel();
                _syncCts.Dispose();
                _syncCts = null;
            }
        }
    }
}
