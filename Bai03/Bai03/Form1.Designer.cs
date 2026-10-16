using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Bai3_MDI_QuanLySieuThi
{
    partial class frmMain
    {
        private IContainer components = null;
        private MenuStrip menuMain;
        private ToolStripMenuItem mnuSystem;
        private ToolStripMenuItem mnuLogout;
        private ToolStripMenuItem mnuExit;
        private ToolStripMenuItem mnuCatalog;
        private ToolStripMenuItem mnuProducts;
        private ToolStripMenuItem mnuInventory;
        private ToolStripMenuItem mnuReports;
        private ToolStripMenuItem mnuRevenue;
        private ToolStripMenuItem mnuWindow;
        private ToolStripMenuItem mnuCascade;
        private ToolStripMenuItem mnuTileHorizontal;
        private ToolStripMenuItem mnuTileVertical;
        private StatusStrip statusMain;
        private ToolStripStatusLabel lblCurrentUser;
        private ToolStripStatusLabel lblSystemStatus;
        private ToolStripProgressBar progressBarMain;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.menuMain = new MenuStrip();
            this.mnuSystem = new ToolStripMenuItem();
            this.mnuLogout = new ToolStripMenuItem();
            this.mnuExit = new ToolStripMenuItem();
            this.mnuCatalog = new ToolStripMenuItem();
            this.mnuProducts = new ToolStripMenuItem();
            this.mnuInventory = new ToolStripMenuItem();
            this.mnuReports = new ToolStripMenuItem();
            this.mnuRevenue = new ToolStripMenuItem();
            this.mnuWindow = new ToolStripMenuItem();
            this.mnuCascade = new ToolStripMenuItem();
            this.mnuTileHorizontal = new ToolStripMenuItem();
            this.mnuTileVertical = new ToolStripMenuItem();
            this.statusMain = new StatusStrip();
            this.lblCurrentUser = new ToolStripStatusLabel();
            this.lblSystemStatus = new ToolStripStatusLabel();
            this.progressBarMain = new ToolStripProgressBar();

            this.menuMain.SuspendLayout();
            this.statusMain.SuspendLayout();
            this.SuspendLayout();

            this.menuMain.Items.AddRange(new ToolStripItem[] {
                this.mnuSystem, this.mnuCatalog, this.mnuReports, this.mnuWindow
            });
            this.menuMain.Location = new Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new Size(1200, 24);
            this.menuMain.TabIndex = 0;

            this.mnuSystem.DropDownItems.AddRange(new ToolStripItem[] { this.mnuLogout, this.mnuExit });
            this.mnuSystem.Name = "mnuSystem";
            this.mnuSystem.Text = "Hệ thống";
            this.mnuLogout.Name = "mnuLogout";
            this.mnuLogout.Text = "Đăng xuất";
            this.mnuLogout.Click += new EventHandler(this.mnuLogout_Click);
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Text = "Thoát";
            this.mnuExit.Click += new EventHandler(this.mnuExit_Click);

            this.mnuCatalog.DropDownItems.AddRange(new ToolStripItem[] { this.mnuProducts, this.mnuInventory });
            this.mnuCatalog.Name = "mnuCatalog";
            this.mnuCatalog.Text = "Danh mục";
            this.mnuProducts.Name = "mnuProducts";
            this.mnuProducts.Text = "Quản lý Sản phẩm";
            this.mnuProducts.Click += new EventHandler(this.mnuProducts_Click);
            this.mnuInventory.Name = "mnuInventory";
            this.mnuInventory.Text = "Quản lý Nhập xuất";
            this.mnuInventory.Click += new EventHandler(this.mnuInventory_Click);

            this.mnuReports.DropDownItems.Add(this.mnuRevenue);
            this.mnuReports.Name = "mnuReports";
            this.mnuReports.Text = "Báo cáo";
            this.mnuRevenue.Name = "mnuRevenue";
            this.mnuRevenue.Text = "Thống kê Doanh thu";
            this.mnuRevenue.Click += new EventHandler(this.mnuRevenue_Click);

            this.mnuWindow.DropDownItems.AddRange(new ToolStripItem[] {
                this.mnuCascade, this.mnuTileHorizontal, this.mnuTileVertical
            });
            this.mnuWindow.Name = "mnuWindow";
            this.mnuWindow.Text = "Cửa sổ";
            this.mnuCascade.Name = "mnuCascade";
            this.mnuCascade.Text = "Cascade";
            this.mnuCascade.Click += new EventHandler(this.mnuCascade_Click);
            this.mnuTileHorizontal.Name = "mnuTileHorizontal";
            this.mnuTileHorizontal.Text = "Tile Horizontal";
            this.mnuTileHorizontal.Click += new EventHandler(this.mnuTileHorizontal_Click);
            this.mnuTileVertical.Name = "mnuTileVertical";
            this.mnuTileVertical.Text = "Tile Vertical";
            this.mnuTileVertical.Click += new EventHandler(this.mnuTileVertical_Click);

            this.statusMain.Items.AddRange(new ToolStripItem[] {
                this.lblCurrentUser, this.lblSystemStatus, this.progressBarMain
            });
            this.statusMain.Location = new Point(0, 739);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new Size(1200, 22);
            this.statusMain.TabIndex = 1;

            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Text = "Người dùng: Admin";
            this.lblSystemStatus.Name = "lblSystemStatus";
            this.lblSystemStatus.Spring = true;
            this.lblSystemStatus.TextAlign = ContentAlignment.MiddleLeft;
            this.lblSystemStatus.Text = "Sẵn sàng";
            this.progressBarMain.Name = "progressBarMain";
            this.progressBarMain.Size = new Size(180, 16);
            this.progressBarMain.Visible = false;
            this.progressBarMain.Style = ProgressBarStyle.Marquee;
            this.progressBarMain.MarqueeAnimationSpeed = 30;

            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuMain;
            this.Name = "frmMain";
            this.Text = "Hệ thống quản lý siêu thị - MDI";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.ClientSize = new Size(1200, 761);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.menuMain);

            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
