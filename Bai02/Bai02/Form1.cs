using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InventoryManager
{
    public partial class frmInventoryManager : Form
    {
        private readonly BindingList<Product> products =
            new BindingList<Product>();

        private string selectedImagePath = string.Empty;

        private bool allowExit = false;

        public frmInventoryManager()
        {
            InitializeComponent();

            ConfigureDataGridView();

            dgvProducts.DataSource = products;

            UpdateStatistics();

            timerClock.Start();

            UpdateClock();
        }

        // ----------------------------------------------------
        // CẤU HÌNH DATAGRIDVIEW
        // ----------------------------------------------------
        private void ConfigureDataGridView()
        {
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.Columns.Clear();

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colProductId",
                HeaderText = "Product ID",
                DataPropertyName = "ProductId",
                ReadOnly = true
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colProductName",
                HeaderText = "Product Name",
                DataPropertyName = "ProductName",
                ReadOnly = true
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colQuantity",
                HeaderText = "Quantity",
                DataPropertyName = "Quantity",
                ReadOnly = true
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colUnitPrice",
                HeaderText = "Unit Price",
                DataPropertyName = "UnitPrice",
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2"
                }
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTotalValue",
                HeaderText = "Total Value",
                DataPropertyName = "TotalValue",
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2"
                }
            });

            // Chỉ hiển thị đường dẫn ảnh, không hiển thị ảnh trực tiếp.
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colImagePath",
                HeaderText = "Image Path",
                DataPropertyName = "ImagePath",
                ReadOnly = true
            });
        }

        // ----------------------------------------------------
        // KIỂM TRA DỮ LIỆU
        // ----------------------------------------------------
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtProductId.Text))
            {
                MessageBox.Show(
                    "Please enter the product ID.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtProductId.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show(
                    "Please enter the product name.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtProductName.Focus();
                return false;
            }

            if (nudQuantity.Value < 0)
            {
                MessageBox.Show(
                    "Quantity cannot be negative.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                nudQuantity.Focus();
                return false;
            }

            if (nudUnitPrice.Value < 0)
            {
                MessageBox.Show(
                    "Unit price cannot be negative.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                nudUnitPrice.Focus();
                return false;
            }

            return true;
        }

        private Product CreateProductFromInput()
        {
            return new Product
            {
                ProductId = txtProductId.Text.Trim(),
                ProductName = txtProductName.Text.Trim(),
                Quantity = Decimal.ToInt32(nudQuantity.Value),
                UnitPrice = nudUnitPrice.Value,
                ImagePath = selectedImagePath
            };
        }

        // ----------------------------------------------------
        // THÊM SẢN PHẨM
        // ----------------------------------------------------
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            string id = txtProductId.Text.Trim();

            bool exists = products.Any(
                p => string.Equals(
                    p.ProductId,
                    id,
                    StringComparison.OrdinalIgnoreCase));

            if (exists)
            {
                MessageBox.Show(
                    "Product ID already exists.",
                    "Duplicate Product",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtProductId.Focus();
                return;
            }

            Product product = CreateProductFromInput();

            products.Add(product);

            UpdateStatistics();

            ClearInput();

            MessageBox.Show(
                "Product added successfully.",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ----------------------------------------------------
        // SỬA SẢN PHẨM
        // ----------------------------------------------------
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a product to update.",
                    "Notification",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!ValidateInput())
            {
                return;
            }

            Product selectedProduct =
                dgvProducts.CurrentRow.DataBoundItem as Product;

            if (selectedProduct == null)
            {
                return;
            }

            string newId = txtProductId.Text.Trim();

            bool duplicate = products.Any(p =>
                !ReferenceEquals(p, selectedProduct) &&
                string.Equals(
                    p.ProductId,
                    newId,
                    StringComparison.OrdinalIgnoreCase));

            if (duplicate)
            {
                MessageBox.Show(
                    "Another product already uses this ID.",
                    "Duplicate Product",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            selectedProduct.ProductId = newId;
            selectedProduct.ProductName = txtProductName.Text.Trim();
            selectedProduct.Quantity = Decimal.ToInt32(nudQuantity.Value);
            selectedProduct.UnitPrice = nudUnitPrice.Value;
            selectedProduct.ImagePath = selectedImagePath;

            // BindingList không tự thông báo khi thuộc tính của một
            // đối tượng thay đổi, vì vậy làm mới danh sách để cập nhật bảng.
            int index = products.IndexOf(selectedProduct);

            if (index >= 0)
            {
                products.ResetItem(index);
            }

            UpdateStatistics();

            MessageBox.Show(
                "Product updated successfully.",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ----------------------------------------------------
        // XÓA SẢN PHẨM
        // ----------------------------------------------------
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a product to delete.",
                    "Notification",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            Product selectedProduct =
                dgvProducts.CurrentRow.DataBoundItem as Product;

            if (selectedProduct == null)
            {
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete product \"" +
                selectedProduct.ProductName + "\"?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            products.Remove(selectedProduct);

            UpdateStatistics();

            ClearInput();

            MessageBox.Show(
                "Product deleted successfully.",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ----------------------------------------------------
        // XÓA TRẮNG FORM NHẬP
        // ----------------------------------------------------
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void ClearInput()
        {
            txtProductId.Clear();
            txtProductName.Clear();

            nudQuantity.Value = 0;
            nudUnitPrice.Value = 0;

            selectedImagePath = string.Empty;

            ClearPictureBox();

            dgvProducts.ClearSelection();

            txtProductId.Focus();
        }

        private void ClearPictureBox()
        {
            Image oldImage = picProductImage.Image;

            picProductImage.Image = null;

            if (oldImage != null)
            {
                oldImage.Dispose();
            }
        }

        // ----------------------------------------------------
        // CHỌN HÌNH ẢNH
        // ----------------------------------------------------
        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Choose Product Image";

                dialog.Filter =
                    "Image Files (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";

                dialog.Multiselect = false;
                dialog.CheckFileExists = true;

                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    // Đọc ảnh vào MemoryStream để không khóa tệp gốc.
                    using (FileStream stream = new FileStream(
                        dialog.FileName,
                        FileMode.Open,
                        FileAccess.Read))
                    using (MemoryStream memory = new MemoryStream())
                    {
                        stream.CopyTo(memory);
                        memory.Position = 0;

                        using (Image temporaryImage = Image.FromStream(memory))
                        {
                            Image newImage = new Bitmap(temporaryImage);

                            ClearPictureBox();

                            picProductImage.Image = newImage;
                        }
                    }

                    selectedImagePath = dialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Cannot load the selected image.\n" + ex.Message,
                        "Image Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        // ----------------------------------------------------
        // CHỌN SẢN PHẨM TRÊN DATAGRIDVIEW
        // ----------------------------------------------------
        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                return;
            }

            Product product =
                dgvProducts.CurrentRow.DataBoundItem as Product;

            if (product == null)
            {
                return;
            }

            txtProductId.Text = product.ProductId;
            txtProductName.Text = product.ProductName;

            nudQuantity.Value = Math.Max(
                nudQuantity.Minimum,
                Math.Min(nudQuantity.Maximum, product.Quantity));

            nudUnitPrice.Value = Math.Max(
                nudUnitPrice.Minimum,
                Math.Min(nudUnitPrice.Maximum, product.UnitPrice));

            selectedImagePath = product.ImagePath ?? string.Empty;

            LoadProductImage(selectedImagePath);
        }

        private void LoadProductImage(string path)
        {
            ClearPictureBox();

            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return;
            }

            try
            {
                using (FileStream stream = new FileStream(
                    path,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.ReadWrite))
                using (MemoryStream memory = new MemoryStream())
                {
                    stream.CopyTo(memory);
                    memory.Position = 0;

                    using (Image temporaryImage = Image.FromStream(memory))
                    {
                        picProductImage.Image = new Bitmap(temporaryImage);
                    }
                }
            }
            catch
            {
                // Không làm dừng chương trình nếu tệp ảnh không hợp lệ
                // hoặc không còn tồn tại.
                ClearPictureBox();
            }
        }

        // ----------------------------------------------------
        // CẬP NHẬT THỐNG KÊ
        // ----------------------------------------------------
        private void UpdateStatistics()
        {
            int totalQuantity = products.Sum(p => p.Quantity);

            decimal totalValue = products.Sum(p => p.TotalValue);

            lblTotalQuantity.Text =
                "Total Quantity: " +
                totalQuantity.ToString("N0", CultureInfo.CurrentCulture);

            lblTotalValue.Text =
                "Total Inventory Value: " +
                totalValue.ToString("N2", CultureInfo.CurrentCulture);
        }

        // ----------------------------------------------------
        // ĐỒNG HỒ TRÊN STATUSSTRIP
        // ----------------------------------------------------
        private void timerClock_Tick(object sender, EventArgs e)
        {
            UpdateClock();
        }

        private void UpdateClock()
        {
            lblClock.Text = "Time: " +
                DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        // ----------------------------------------------------
        // OPEN FILE
        // Mở tệp CSV và nhập danh sách sản phẩm.
        // ----------------------------------------------------
        private void openFileToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            ImportCsv();
        }

        // ----------------------------------------------------
        // IMPORT CSV
        // ----------------------------------------------------
        private void importCsvToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            ImportCsv();
        }

        private void ImportCsv()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Import Product Data";
                dialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                dialog.Multiselect = false;
                dialog.CheckFileExists = true;

                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    int importedCount = 0;
                    int skippedCount = 0;

                    using (StreamReader reader = new StreamReader(
                        dialog.FileName,
                        Encoding.UTF8,
                        true))
                    {
                        string line;
                        bool firstLine = true;

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (string.IsNullOrWhiteSpace(line))
                            {
                                continue;
                            }

                            // Bỏ qua dòng tiêu đề nếu có.
                            if (firstLine)
                            {
                                firstLine = false;

                                if (line.StartsWith(
                                    "ProductId,",
                                    StringComparison.OrdinalIgnoreCase))
                                {
                                    continue;
                                }
                            }

                            try
                            {
                                string[] fields = ParseCsvLine(line);

                                if (fields.Length < 5)
                                {
                                    skippedCount++;
                                    continue;
                                }

                                string id = fields[0].Trim();
                                string name = fields[1].Trim();

                                int quantity;
                                decimal unitPrice;

                                if (string.IsNullOrWhiteSpace(id) ||
                                    string.IsNullOrWhiteSpace(name) ||
                                    !int.TryParse(
                                        fields[2],
                                        NumberStyles.Integer,
                                        CultureInfo.InvariantCulture,
                                        out quantity) ||
                                    !decimal.TryParse(
                                        fields[3],
                                        NumberStyles.Number,
                                        CultureInfo.InvariantCulture,
                                        out unitPrice) ||
                                    quantity < 0 ||
                                    unitPrice < 0)
                                {
                                    skippedCount++;
                                    continue;
                                }

                                bool duplicate = products.Any(p =>
                                    string.Equals(
                                        p.ProductId,
                                        id,
                                        StringComparison.OrdinalIgnoreCase));

                                if (duplicate)
                                {
                                    skippedCount++;
                                    continue;
                                }

                                string imagePath = fields[4].Trim();

                                Product product = new Product(
                                    id,
                                    name,
                                    quantity,
                                    unitPrice,
                                    imagePath);

                                products.Add(product);

                                importedCount++;
                            }
                            catch
                            {
                                skippedCount++;
                            }
                        }
                    }

                    UpdateStatistics();

                    MessageBox.Show(
                        "Import completed.\n" +
                        "Imported: " + importedCount + "\n" +
                        "Skipped: " + skippedCount,
                        "Import CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Cannot import the CSV file.\n" + ex.Message,
                        "Import Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        // ----------------------------------------------------
        // SAVE REPORT
        // Xuất báo cáo tồn kho ra CSV.
        // ----------------------------------------------------
        private void saveReportToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            ExportCsv();
        }

        // ----------------------------------------------------
        // EXPORT CSV
        // ----------------------------------------------------
        private void exportCsvToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            ExportCsv();
        }

        private void ExportCsv()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Export Inventory Report";
                dialog.Filter = "CSV Files (*.csv)|*.csv";
                dialog.DefaultExt = "csv";
                dialog.AddExtension = true;
                dialog.FileName = "InventoryReport.csv";
                dialog.OverwritePrompt = true;

                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    using (StreamWriter writer = new StreamWriter(
                        dialog.FileName,
                        false,
                        new UTF8Encoding(true)))
                    {
                        writer.WriteLine(
                            "ProductId,ProductName,Quantity,UnitPrice,TotalValue,ImagePath");

                        foreach (Product product in products)
                        {
                            writer.WriteLine(string.Join(",",
                                EscapeCsv(product.ProductId),
                                EscapeCsv(product.ProductName),
                                product.Quantity.ToString(
                                    CultureInfo.InvariantCulture),
                                product.UnitPrice.ToString(
                                    CultureInfo.InvariantCulture),
                                product.TotalValue.ToString(
                                    CultureInfo.InvariantCulture),
                                EscapeCsv(product.ImagePath)));
                        }
                    }

                    MessageBox.Show(
                        "Report exported successfully.",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Cannot export the report.\n" + ex.Message,
                        "Export Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        // ----------------------------------------------------
        // XỬ LÝ GIÁ TRỊ CSV CÓ DẤU PHẨY, DẤU NGOẶC KÉP
        // ----------------------------------------------------
        private string EscapeCsv(string value)
        {
            if (value == null)
            {
                value = string.Empty;
            }

            if (value.Contains(",") ||
                value.Contains("\"") ||
                value.Contains("\r") ||
                value.Contains("\n"))
            {
                value = "\"" + value.Replace("\"", "\"\"") + "\"";
            }

            return value;
        }

        private string[] ParseCsvLine(string line)
        {
            var fields = new System.Collections.Generic.List<string>();
            var field = new StringBuilder();

            bool insideQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    if (insideQuotes &&
                        i + 1 < line.Length &&
                        line[i + 1] == '"')
                    {
                        field.Append('"');
                        i++;
                    }
                    else
                    {
                        insideQuotes = !insideQuotes;
                    }
                }
                else if (c == ',' && !insideQuotes)
                {
                    fields.Add(field.ToString());
                    field.Clear();
                }
                else
                {
                    field.Append(c);
                }
            }

            fields.Add(field.ToString());

            return fields.ToArray();
        }

        // ----------------------------------------------------
        // EXIT
        // ----------------------------------------------------
        private void exitToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            Close();
        }

        private void frmInventoryManager_FormClosing(
            object sender, FormClosingEventArgs e)
        {
            if (allowExit)
            {
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to exit the application?",
                "Confirm Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                allowExit = true;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}