
namespace DeciTap
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cboDevices;
        private System.Windows.Forms.TextBox txtCardId;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.CheckBox chkKeyboardWedge;
        private System.Windows.Forms.CheckBox chkEnterAfterOutput;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.ComboBox cboFormat;
        private System.Windows.Forms.Label lblCharLength;
        private System.Windows.Forms.TextBox txtCharLength;
        private System.Windows.Forms.Label lblStripLeading;
        private System.Windows.Forms.TextBox txtStripLeading;
        // New panel for status indication.
        private System.Windows.Forms.Panel pnlStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            cboDevices = new ComboBox();
            txtCardId = new TextBox();
            lblMessage = new Label();
            btnRefresh = new Button();
            chkKeyboardWedge = new CheckBox();
            chkEnterAfterOutput = new CheckBox();
            lblFormat = new Label();
            cboFormat = new ComboBox();
            lblCharLength = new Label();
            txtCharLength = new TextBox();
            lblStripLeading = new Label();
            txtStripLeading = new TextBox();
            pnlStatus = new Panel();
            pnlStatus.SuspendLayout();
            SuspendLayout();
            // 
            // cboDevices
            // 
            cboDevices.FormattingEnabled = true;
            cboDevices.Location = new Point(10, 12);
            cboDevices.Name = "cboDevices";
            cboDevices.Size = new Size(446, 33);
            cboDevices.TabIndex = 0;
            // 
            // txtCardId
            // 
            txtCardId.Location = new Point(10, 53);
            txtCardId.Name = "txtCardId";
            txtCardId.Size = new Size(446, 31);
            txtCardId.TabIndex = 1;
            txtCardId.Text = "Select Device Above, Choose Format, Scan Card";
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Location = new Point(18, 0);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(356, 25);
            lblMessage.TabIndex = 2;
            lblMessage.Text = "Select USB Reader, Format and click Refresh";
            //lblMessage.Click += lblMessage_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(12, 90);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(84, 33);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // chkKeyboardWedge
            // 
            chkKeyboardWedge.AutoSize = true;
            chkKeyboardWedge.Location = new Point(102, 93);
            chkKeyboardWedge.Name = "chkKeyboardWedge";
            chkKeyboardWedge.Size = new Size(175, 29);
            chkKeyboardWedge.TabIndex = 4;
            chkKeyboardWedge.Text = "Keyboard Wedge";
            chkKeyboardWedge.UseVisualStyleBackColor = true;
            // 
            // chkEnterAfterOutput
            // 
            chkEnterAfterOutput.AutoSize = true;
            chkEnterAfterOutput.Location = new Point(283, 94);
            chkEnterAfterOutput.Name = "chkEnterAfterOutput";
            chkEnterAfterOutput.Size = new Size(184, 29);
            chkEnterAfterOutput.TabIndex = 5;
            chkEnterAfterOutput.Text = "Enter After Output";
            chkEnterAfterOutput.UseVisualStyleBackColor = true;
            // 
            // lblFormat
            // 
            lblFormat.AutoSize = true;
            lblFormat.Location = new Point(10, 136);
            lblFormat.Name = "lblFormat";
            lblFormat.Size = new Size(73, 25);
            lblFormat.TabIndex = 6;
            lblFormat.Text = "Format:";
            // 
            // cboFormat
            // 
            cboFormat.FormattingEnabled = true;
            cboFormat.Items.AddRange(new object[] { "Hex", "Reversed Hex", "Decimal", "Reversed Decimal" });
            cboFormat.Location = new Point(80, 133);
            cboFormat.Name = "cboFormat";
            cboFormat.Size = new Size(143, 33);
            cboFormat.TabIndex = 7;
            // 
            // lblCharLength
            // 
            lblCharLength.AutoSize = true;
            lblCharLength.Location = new Point(229, 136);
            lblCharLength.Name = "lblCharLength";
            lblCharLength.Size = new Size(149, 25);
            lblCharLength.TabIndex = 8;
            lblCharLength.Text = "Character Length:";
            // 
            // txtCharLength
            // 
            txtCharLength.Location = new Point(373, 133);
            txtCharLength.Name = "txtCharLength";
            txtCharLength.Size = new Size(83, 31);
            txtCharLength.TabIndex = 9;
            txtCharLength.Text = "10";
            // 
            // lblStripLeading
            // 
            lblStripLeading.AutoSize = true;
            lblStripLeading.Location = new Point(10, 175);
            lblStripLeading.Name = "lblStripLeading";
            lblStripLeading.Size = new Size(244, 25);
            lblStripLeading.TabIndex = 10;
            lblStripLeading.Text = "Strip Leading Characters (set):";
            // 
            // txtStripLeading
            // 
            txtStripLeading.Location = new Point(253, 172);
            txtStripLeading.Name = "txtStripLeading";
            txtStripLeading.Size = new Size(203, 31);
            txtStripLeading.TabIndex = 11;
            // 
            // pnlStatus
            // 
            pnlStatus.BorderStyle = BorderStyle.FixedSingle;
            pnlStatus.Controls.Add(lblMessage);
            pnlStatus.Location = new Point(-9, 209);
            pnlStatus.Name = "pnlStatus";
            pnlStatus.Size = new Size(484, 30);
            pnlStatus.TabIndex = 12;
            // 
            // MainForm
            // 
            ClientSize = new Size(468, 238);
            Controls.Add(pnlStatus);
            Controls.Add(txtStripLeading);
            Controls.Add(lblStripLeading);
            Controls.Add(txtCharLength);
            Controls.Add(lblCharLength);
            Controls.Add(cboFormat);
            Controls.Add(lblFormat);
            Controls.Add(chkEnterAfterOutput);
            Controls.Add(chkKeyboardWedge);
            Controls.Add(btnRefresh);
            Controls.Add(txtCardId);
            Controls.Add(cboDevices);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            Name = "MainForm";
            Text = "DeciTap+";
            TopMost = true;
            Load += MainForm_Load;
            pnlStatus.ResumeLayout(false);
            pnlStatus.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion
    }
}
