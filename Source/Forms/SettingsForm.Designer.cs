namespace CinemaDrape
{
    partial class SettingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.hotKeyTextBox = new System.Windows.Forms.TextBox();
            this.AutoRestoreAreasCheckBox = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.peekOpacityTrackBar = new System.Windows.Forms.TrackBar();
            this.peekOpacityLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peekOpacityTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Keyboard shortcut for pausing and resuming:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // hotKeyTextBox
            // 
            this.hotKeyTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.hotKeyTextBox.BackColor = System.Drawing.Color.Black;
            this.hotKeyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hotKeyTextBox.ForeColor = System.Drawing.Color.White;
            this.hotKeyTextBox.Location = new System.Drawing.Point(232, 14);
            this.hotKeyTextBox.Name = "hotKeyTextBox";
            this.hotKeyTextBox.Size = new System.Drawing.Size(216, 22);
            this.hotKeyTextBox.TabIndex = 1;
            this.hotKeyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.hotKeyTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EventHotKeyTextBoxKeyDown);
            // 
            // AutoRestoreAreasCheckBox
            // 
            this.AutoRestoreAreasCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.AutoRestoreAreasCheckBox.AutoSize = true;
            this.AutoRestoreAreasCheckBox.Checked = true;
            this.AutoRestoreAreasCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tableLayoutPanel1.SetColumnSpan(this.AutoRestoreAreasCheckBox, 2);
            this.AutoRestoreAreasCheckBox.ForeColor = System.Drawing.Color.White;
            this.AutoRestoreAreasCheckBox.Location = new System.Drawing.Point(3, 121);
            this.AutoRestoreAreasCheckBox.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.AutoRestoreAreasCheckBox.Name = "AutoRestoreAreasCheckBox";
            this.AutoRestoreAreasCheckBox.Size = new System.Drawing.Size(430, 21);
            this.AutoRestoreAreasCheckBox.TabIndex = 4;
            this.AutoRestoreAreasCheckBox.Text = "Automatically restore the focus areas when CinemaDrape starts";
            this.AutoRestoreAreasCheckBox.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.okButton, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cancelButton, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.AutoRestoreAreasCheckBox, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.peekOpacityTrackBar, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.hotKeyTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.peekOpacityLabel, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(30, 15);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(458, 218);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okButton.ForeColor = System.Drawing.Color.White;
            this.okButton.Location = new System.Drawing.Point(144, 164);
            this.okButton.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 34);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(239, 164);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 34);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // peekOpacityTrackBar
            // 
            this.peekOpacityTrackBar.Location = new System.Drawing.Point(229, 55);
            this.peekOpacityTrackBar.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.peekOpacityTrackBar.Maximum = 100;
            this.peekOpacityTrackBar.Minimum = 1;
            this.peekOpacityTrackBar.Name = "peekOpacityTrackBar";
            this.peekOpacityTrackBar.Size = new System.Drawing.Size(223, 56);
            this.peekOpacityTrackBar.TabIndex = 3;
            this.peekOpacityTrackBar.TickFrequency = 5;
            this.peekOpacityTrackBar.Value = 80;
            this.peekOpacityTrackBar.ValueChanged += new System.EventHandler(this.EventPeekOpacityTrackBarValueChanged);
            this.peekOpacityTrackBar.Enter += new System.EventHandler(this.EventPeekOpacityTrackBarEnter);
            this.peekOpacityTrackBar.Leave += new System.EventHandler(this.EventPeekOpacityTrackBarLeave);
            // 
            // peekOpacityLabel
            // 
            this.peekOpacityLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.peekOpacityLabel.AutoSize = true;
            this.peekOpacityLabel.ForeColor = System.Drawing.Color.White;
            this.peekOpacityLabel.Location = new System.Drawing.Point(3, 72);
            this.peekOpacityLabel.Name = "peekOpacityLabel";
            this.peekOpacityLabel.Size = new System.Drawing.Size(0, 17);
            this.peekOpacityLabel.TabIndex = 2;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(518, 233);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Opacity = 0.8D;
            this.Padding = new System.Windows.Forms.Padding(30, 15, 30, 0);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.EventSettingsFormShown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peekOpacityTrackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        internal System.Windows.Forms.TextBox hotKeyTextBox;
        internal System.Windows.Forms.CheckBox AutoRestoreAreasCheckBox;
        private System.Windows.Forms.Label peekOpacityLabel;
        internal System.Windows.Forms.TrackBar peekOpacityTrackBar;
    }
}