using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace DeviceManager
{
    public partial class frmDeviceManager : Form
    {
        private readonly BindingList<Device> _devices = new BindingList<Device>();
        private readonly BindingSource _deviceSource = new BindingSource();
        private readonly ErrorProvider _errorProvider = new ErrorProvider();
        private int _nextId = 1;
        private Device _selectedDevice;

        public frmDeviceManager()
        {
            InitializeComponent();

            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            InitializeCategories();
            ConfigureGrid();
            ConfigureBinding();
            BindInputControls();
            RegisterEvents();
            ClearInputs();
        }

        private void InitializeCategories()
        {
            cboCategory.Items.AddRange(new object[]
            {
                "Máy tính", "Điện thoại", "Máy tính bảng",
                "Thiết bị mạng", "Thiết bị ngoại vi", "Khác"
            });
        }

        private void ConfigureGrid()
        {
            dgvDevices.AutoGenerateColumns = false;
            dgvDevices.AllowUserToAddRows = false;
            dgvDevices.AllowUserToDeleteRows = false;
            dgvDevices.ReadOnly = true;
            dgvDevices.MultiSelect = false;
            dgvDevices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDevices.RowHeadersVisible = false;
            dgvDevices.BackgroundColor = System.Drawing.Color.White;
            dgvDevices.BorderStyle = BorderStyle.Fixed3D;
            dgvDevices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDevices.Columns.Clear();

            dgvDevices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDeviceId", HeaderText = "Mã thiết bị",
                DataPropertyName = nameof(Device.DeviceId), FillWeight = 15
            });
            dgvDevices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDeviceName", HeaderText = "Tên thiết bị",
                DataPropertyName = nameof(Device.DeviceName), FillWeight = 28
            });
            dgvDevices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCategory", HeaderText = "Loại thiết bị",
                DataPropertyName = nameof(Device.Category), FillWeight = 20
            });
            dgvDevices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPrice", HeaderText = "Đơn giá",
                DataPropertyName = nameof(Device.Price), FillWeight = 18,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    FormatProvider = CultureInfo.GetCultureInfo("vi-VN"),
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
            dgvDevices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colQuantity", HeaderText = "Số lượng tồn",
                DataPropertyName = nameof(Device.Quantity), FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
        }

        private void ConfigureBinding()
        {
            _deviceSource.DataSource = _devices;
            dgvDevices.DataSource = _deviceSource;
        }

        private void BindInputControls()
        {
            // Binding 2 chiều: cập nhật control sẽ cập nhật thuộc tính của đối tượng hiện hành.
            txtDeviceId.DataBindings.Add("Text", _deviceSource, nameof(Device.DeviceId),
                true, DataSourceUpdateMode.OnPropertyChanged);
            txtDeviceName.DataBindings.Add("Text", _deviceSource, nameof(Device.DeviceName),
                true, DataSourceUpdateMode.OnPropertyChanged);
            cboCategory.DataBindings.Add("SelectedItem", _deviceSource, nameof(Device.Category),
                true, DataSourceUpdateMode.OnPropertyChanged);
            txtPrice.DataBindings.Add("Text", _deviceSource, nameof(Device.Price),
                true, DataSourceUpdateMode.Never, 0, "N0");
            numQuantity.DataBindings.Add("Value", _deviceSource, nameof(Device.Quantity),
                true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void RegisterEvents()
        {
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnClear.Click += btnClear_Click;
            dgvDevices.SelectionChanged += dgvDevices_SelectionChanged;
            txtDeviceName.Validated += (s, e) => ValidateName();
            cboCategory.Validated += (s, e) => ValidateCategory();
            txtPrice.Validated += (s, e) => ValidatePrice();
            txtDeviceName.TextChanged += (s, e) => _errorProvider.SetError(txtDeviceName, "");
            cboCategory.SelectedIndexChanged += (s, e) => _errorProvider.SetError(cboCategory, "");
            txtPrice.TextChanged += (s, e) => _errorProvider.SetError(txtPrice, "");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            decimal price = ParsePrice(txtPrice.Text);
            Device device = new Device
            {
                DeviceId = "TB" + _nextId.ToString("D4"),
                DeviceName = txtDeviceName.Text.Trim(),
                Category = cboCategory.SelectedItem.ToString(),
                Price = price,
                Quantity = (int)numQuantity.Value
            };
            _nextId++;

            // BindingList thông báo thay đổi nên DataGridView tự cập nhật, không nạp lại dữ liệu.
            _devices.Add(device);
            _deviceSource.Position = _deviceSource.IndexOf(device);
            _selectedDevice = device;
            MessageBox.Show("Đã thêm thiết bị mới.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedDevice == null)
            {
                MessageBox.Show("Vui lòng chọn thiết bị cần cập nhật.", "Chưa chọn thiết bị",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            _selectedDevice.DeviceName = txtDeviceName.Text.Trim();
            _selectedDevice.Category = cboCategory.SelectedItem.ToString();
            _selectedDevice.Price = ParsePrice(txtPrice.Text);
            _selectedDevice.Quantity = (int)numQuantity.Value;

            // INotifyPropertyChanged tự thông báo các thay đổi cho BindingSource/DataGridView.
            _deviceSource.ResetCurrentItem();
            MessageBox.Show("Đã cập nhật thông tin thiết bị.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedDevice == null)
            {
                MessageBox.Show("Vui lòng chọn thiết bị cần xóa.", "Chưa chọn thiết bị",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa thiết bị " + _selectedDevice.DeviceId + " không?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _devices.Remove(_selectedDevice);
                _selectedDevice = null;
                ClearInputs();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvDevices.ClearSelection();
            _deviceSource.Position = -1;
            _selectedDevice = null;
            ClearInputs();
        }

        private void dgvDevices_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDevices.CurrentRow == null || dgvDevices.CurrentRow.IsNewRow)
            {
                _selectedDevice = _deviceSource.Current as Device;
                return;
            }

            _selectedDevice = dgvDevices.CurrentRow.DataBoundItem as Device;
            if (_selectedDevice != null)
            {
                // Đặt Current của BindingSource khiến các control binding đổ dữ liệu đối tượng lên form.
                _deviceSource.Position = _deviceSource.IndexOf(_selectedDevice);
                _errorProvider.Clear();
            }
            btnUpdate.Enabled = _selectedDevice != null;
            btnDelete.Enabled = _selectedDevice != null;
        }

        private bool ValidateInputs()
        {
            bool valid = ValidateName();
            valid = ValidateCategory() && valid;
            valid = ValidatePrice() && valid;
            return valid;
        }

        private bool ValidateName()
        {
            bool valid = !string.IsNullOrWhiteSpace(txtDeviceName.Text);
            _errorProvider.SetError(txtDeviceName, valid ? "" : "Tên thiết bị không được để trống.");
            return valid;
        }

        private bool ValidateCategory()
        {
            bool valid = cboCategory.SelectedIndex >= 0;
            _errorProvider.SetError(cboCategory, valid ? "" : "Vui lòng chọn loại thiết bị.");
            return valid;
        }

        private bool ValidatePrice()
        {
            bool valid = TryParsePrice(txtPrice.Text, out decimal price) && price >= 0;
            _errorProvider.SetError(txtPrice, valid ? "" : "Đơn giá phải là số hợp lệ và không nhỏ hơn 0.");
            return valid;
        }

        private static bool TryParsePrice(string text, out decimal price)
        {
            text = (text ?? "").Trim();
            return decimal.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out price)
                || decimal.TryParse(text, NumberStyles.Number, CultureInfo.GetCultureInfo("vi-VN"), out price)
                || decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out price);
        }

        private static decimal ParsePrice(string text)
        {
            if (TryParsePrice(text, out decimal price)) return price;
            return 0;
        }

        private void ClearInputs()
        {
            _errorProvider.Clear();

            // Tạm dừng đồng bộ khi xóa form để không ghi giá trị rỗng vào đối tượng đang chọn.
            _deviceSource.SuspendBinding();
            try
            {
                txtDeviceId.Clear();
                txtDeviceName.Clear();
                cboCategory.SelectedIndex = -1;
                txtPrice.Clear();
                numQuantity.Value = 0;
            }
            finally
            {
                _deviceSource.ResumeBinding();
            }

            btnUpdate.Enabled = _selectedDevice != null;
            btnDelete.Enabled = _selectedDevice != null;
            txtDeviceName.Focus();
        }
    }
}
