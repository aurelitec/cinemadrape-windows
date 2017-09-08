namespace CinemaDrape
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuMainDrape = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainHideRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuMainColor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainColorValueTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.menuMainColorCustom = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainColorRandom = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainOpacity = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainResetToBlack = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainCoverMonitors = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuMainFocusAreas = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainAutoDetectArea = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainAutoDetectWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainRandomArea = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainDeleteAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainDeleteAllSure = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainDeleteAllYes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainDeleteAllNo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuMainExtras = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuMainAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainAboutName = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainAboutVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainAboutCopyright = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.menuMainQuickStart = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainAboutOnlineHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainExit = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.saveLayoutFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openLayoutFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.delayedBringToFrontTimer = new System.Windows.Forms.Timer(this.components);
            this.menuFocus = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuFocusDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFocusSize = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFocusSizeTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFocusCommonSizes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFocus640x390 = new System.Windows.Forms.ToolStripMenuItem();
            this.x480ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x490ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x600ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x768ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x800ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x1024ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.focusableListBox = new System.Windows.Forms.ListBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.menuMain.SuspendLayout();
            this.menuFocus.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMainDrape,
            this.menuMainHideRestore,
            this.menuMainSeparator1,
            this.menuMainColor,
            this.menuMainOpacity,
            this.menuMainResetToBlack,
            this.menuMainCoverMonitors,
            this.menuMainSeparator2,
            this.menuMainFocusAreas,
            this.menuMainAutoDetectArea,
            this.menuMainAutoDetectWindow,
            this.menuMainRandomArea,
            this.menuMainDeleteAll,
            this.menuMainSeparator3,
            this.menuMainExtras,
            this.menuMainLoad,
            this.menuMainSave,
            this.menuMainSettings,
            this.menuMainSeparator4,
            this.menuMainAbout,
            this.menuMainExit});
            this.menuMain.Name = "menuContext";
            this.menuMain.Size = new System.Drawing.Size(237, 402);
            this.menuMain.Opening += new System.ComponentModel.CancelEventHandler(this.EventMenuMainOpening);
            // 
            // menuMainDrape
            // 
            this.menuMainDrape.Enabled = false;
            this.menuMainDrape.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuMainDrape.Name = "menuMainDrape";
            this.menuMainDrape.Size = new System.Drawing.Size(236, 22);
            this.menuMainDrape.Text = "Drape:";
            // 
            // menuMainHideRestore
            // 
            this.menuMainHideRestore.Name = "menuMainHideRestore";
            this.menuMainHideRestore.ShortcutKeyDisplayString = "Ctrl+F11";
            this.menuMainHideRestore.Size = new System.Drawing.Size(236, 22);
            this.menuMainHideRestore.Text = "Hide (&Pause)";
            this.menuMainHideRestore.ToolTipText = "Double-Click";
            this.menuMainHideRestore.Click += new System.EventHandler(this.EventMenuMainHideRestoreClick);
            // 
            // menuMainSeparator1
            // 
            this.menuMainSeparator1.Name = "menuMainSeparator1";
            this.menuMainSeparator1.Size = new System.Drawing.Size(233, 6);
            // 
            // menuMainColor
            // 
            this.menuMainColor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMainColorValueTextBox,
            this.toolStripSeparator7,
            this.menuMainColorCustom,
            this.menuMainColorRandom});
            this.menuMainColor.Name = "menuMainColor";
            this.menuMainColor.Size = new System.Drawing.Size(236, 22);
            this.menuMainColor.Text = "Background &Color";
            this.menuMainColor.DropDownOpening += new System.EventHandler(this.EventMenuMainColorDropDownOpening);
            // 
            // menuMainColorValueTextBox
            // 
            this.menuMainColorValueTextBox.BackColor = System.Drawing.Color.Black;
            this.menuMainColorValueTextBox.ForeColor = System.Drawing.Color.White;
            this.menuMainColorValueTextBox.Name = "menuMainColorValueTextBox";
            this.menuMainColorValueTextBox.Size = new System.Drawing.Size(100, 23);
            this.menuMainColorValueTextBox.ToolTipText = "Enter any color by its name, HTML code or RGB triplet, and press Enter.\r\nExamples" +
    ": Red, #112233, or 48,33,55";
            this.menuMainColorValueTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EventMenuMainColorValueTextBoxKeyDown);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(157, 6);
            // 
            // menuMainColorCustom
            // 
            this.menuMainColorCustom.Name = "menuMainColorCustom";
            this.menuMainColorCustom.Size = new System.Drawing.Size(160, 22);
            this.menuMainColorCustom.Text = "&Select...";
            this.menuMainColorCustom.Click += new System.EventHandler(this.EventMenuColorCustomClick);
            // 
            // menuMainColorRandom
            // 
            this.menuMainColorRandom.Name = "menuMainColorRandom";
            this.menuMainColorRandom.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.menuMainColorRandom.Size = new System.Drawing.Size(160, 22);
            this.menuMainColorRandom.Text = "&Random";
            this.menuMainColorRandom.Click += new System.EventHandler(this.EventMenuColorRandomClick);
            // 
            // menuMainOpacity
            // 
            this.menuMainOpacity.Name = "menuMainOpacity";
            this.menuMainOpacity.Size = new System.Drawing.Size(236, 22);
            this.menuMainOpacity.Text = "&Opacity (100%)";
            // 
            // menuMainResetToBlack
            // 
            this.menuMainResetToBlack.Name = "menuMainResetToBlack";
            this.menuMainResetToBlack.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.menuMainResetToBlack.Size = new System.Drawing.Size(236, 22);
            this.menuMainResetToBlack.Text = "Reset to &Black";
            this.menuMainResetToBlack.Click += new System.EventHandler(this.EventMenuMainResetToBlackClick);
            // 
            // menuMainCoverMonitors
            // 
            this.menuMainCoverMonitors.Name = "menuMainCoverMonitors";
            this.menuMainCoverMonitors.Size = new System.Drawing.Size(236, 22);
            this.menuMainCoverMonitors.Text = "Cover &Monitors";
            // 
            // menuMainSeparator2
            // 
            this.menuMainSeparator2.Name = "menuMainSeparator2";
            this.menuMainSeparator2.Size = new System.Drawing.Size(233, 6);
            // 
            // menuMainFocusAreas
            // 
            this.menuMainFocusAreas.Enabled = false;
            this.menuMainFocusAreas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuMainFocusAreas.Name = "menuMainFocusAreas";
            this.menuMainFocusAreas.Size = new System.Drawing.Size(236, 22);
            this.menuMainFocusAreas.Text = "Focus Areas:";
            // 
            // menuMainAutoDetectArea
            // 
            this.menuMainAutoDetectArea.Name = "menuMainAutoDetectArea";
            this.menuMainAutoDetectArea.ShortcutKeyDisplayString = "Ctrl+A";
            this.menuMainAutoDetectArea.Size = new System.Drawing.Size(236, 22);
            this.menuMainAutoDetectArea.Text = "&Area Under Cursor";
            this.menuMainAutoDetectArea.ToolTipText = "Middle-Click (Experimental Feature)";
            this.menuMainAutoDetectArea.Click += new System.EventHandler(this.EventMenuMainAutoDetectAreaClick);
            // 
            // menuMainAutoDetectWindow
            // 
            this.menuMainAutoDetectWindow.Name = "menuMainAutoDetectWindow";
            this.menuMainAutoDetectWindow.ShortcutKeyDisplayString = "Ctrl+W";
            this.menuMainAutoDetectWindow.Size = new System.Drawing.Size(236, 22);
            this.menuMainAutoDetectWindow.Text = "&Window Under Cursor";
            this.menuMainAutoDetectWindow.ToolTipText = "Ctrl+Middle-Click";
            this.menuMainAutoDetectWindow.Click += new System.EventHandler(this.EventMenuMainAutoDetectWindowClick);
            // 
            // menuMainRandomArea
            // 
            this.menuMainRandomArea.Name = "menuMainRandomArea";
            this.menuMainRandomArea.Size = new System.Drawing.Size(236, 22);
            this.menuMainRandomArea.Text = "&New Random Area";
            this.menuMainRandomArea.Click += new System.EventHandler(this.EventMenuMainRandomAreaClick);
            // 
            // menuMainDeleteAll
            // 
            this.menuMainDeleteAll.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMainDeleteAllSure,
            this.menuMainDeleteAllYes,
            this.menuMainDeleteAllNo});
            this.menuMainDeleteAll.Name = "menuMainDeleteAll";
            this.menuMainDeleteAll.Size = new System.Drawing.Size(236, 22);
            this.menuMainDeleteAll.Text = "&Delete All";
            // 
            // menuMainDeleteAllSure
            // 
            this.menuMainDeleteAllSure.Enabled = false;
            this.menuMainDeleteAllSure.Name = "menuMainDeleteAllSure";
            this.menuMainDeleteAllSure.Size = new System.Drawing.Size(146, 22);
            this.menuMainDeleteAllSure.Text = "Are You Sure?";
            // 
            // menuMainDeleteAllYes
            // 
            this.menuMainDeleteAllYes.Name = "menuMainDeleteAllYes";
            this.menuMainDeleteAllYes.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
            this.menuMainDeleteAllYes.Size = new System.Drawing.Size(146, 22);
            this.menuMainDeleteAllYes.Text = "&Yes";
            this.menuMainDeleteAllYes.Click += new System.EventHandler(this.EventMenuMainDeleteAllYesClick);
            // 
            // menuMainDeleteAllNo
            // 
            this.menuMainDeleteAllNo.Name = "menuMainDeleteAllNo";
            this.menuMainDeleteAllNo.Size = new System.Drawing.Size(146, 22);
            this.menuMainDeleteAllNo.Text = "&No";
            // 
            // menuMainSeparator3
            // 
            this.menuMainSeparator3.Name = "menuMainSeparator3";
            this.menuMainSeparator3.Size = new System.Drawing.Size(233, 6);
            // 
            // menuMainExtras
            // 
            this.menuMainExtras.Enabled = false;
            this.menuMainExtras.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuMainExtras.Name = "menuMainExtras";
            this.menuMainExtras.Size = new System.Drawing.Size(236, 22);
            this.menuMainExtras.Text = "Extras:";
            // 
            // menuMainLoad
            // 
            this.menuMainLoad.Name = "menuMainLoad";
            this.menuMainLoad.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.menuMainLoad.Size = new System.Drawing.Size(236, 22);
            this.menuMainLoad.Text = "&Load Layout...";
            this.menuMainLoad.Click += new System.EventHandler(this.EventMenuMainLoadClick);
            // 
            // menuMainSave
            // 
            this.menuMainSave.Name = "menuMainSave";
            this.menuMainSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuMainSave.Size = new System.Drawing.Size(236, 22);
            this.menuMainSave.Text = "&Save Layout...";
            this.menuMainSave.Click += new System.EventHandler(this.EventMenuMainSaveClick);
            // 
            // menuMainSettings
            // 
            this.menuMainSettings.Name = "menuMainSettings";
            this.menuMainSettings.Size = new System.Drawing.Size(236, 22);
            this.menuMainSettings.Text = "Se&ttings...";
            this.menuMainSettings.Click += new System.EventHandler(this.EventMenuMainSettingsClick);
            // 
            // menuMainSeparator4
            // 
            this.menuMainSeparator4.Name = "menuMainSeparator4";
            this.menuMainSeparator4.Size = new System.Drawing.Size(233, 6);
            // 
            // menuMainAbout
            // 
            this.menuMainAbout.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMainAboutName,
            this.menuMainAboutVersion,
            this.menuMainAboutCopyright,
            this.toolStripSeparator8,
            this.menuMainQuickStart,
            this.menuMainAboutOnlineHelp});
            this.menuMainAbout.Name = "menuMainAbout";
            this.menuMainAbout.Size = new System.Drawing.Size(236, 22);
            this.menuMainAbout.Text = "Abo&ut CinemaDrape";
            // 
            // menuMainAboutName
            // 
            this.menuMainAboutName.Enabled = false;
            this.menuMainAboutName.Name = "menuMainAboutName";
            this.menuMainAboutName.Size = new System.Drawing.Size(191, 22);
            this.menuMainAboutName.Text = "CinemaDrape";
            // 
            // menuMainAboutVersion
            // 
            this.menuMainAboutVersion.Enabled = false;
            this.menuMainAboutVersion.Name = "menuMainAboutVersion";
            this.menuMainAboutVersion.Size = new System.Drawing.Size(191, 22);
            this.menuMainAboutVersion.Text = "Version ";
            // 
            // menuMainAboutCopyright
            // 
            this.menuMainAboutCopyright.Enabled = false;
            this.menuMainAboutCopyright.Name = "menuMainAboutCopyright";
            this.menuMainAboutCopyright.Size = new System.Drawing.Size(191, 22);
            this.menuMainAboutCopyright.Text = "Copyright © Aurelitec";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(188, 6);
            // 
            // menuMainQuickStart
            // 
            this.menuMainQuickStart.Name = "menuMainQuickStart";
            this.menuMainQuickStart.Size = new System.Drawing.Size(191, 22);
            this.menuMainQuickStart.Text = "&Quick Start...";
            this.menuMainQuickStart.Click += new System.EventHandler(this.EventMenuMainQuickStartClick);
            // 
            // menuMainAboutOnlineHelp
            // 
            this.menuMainAboutOnlineHelp.Name = "menuMainAboutOnlineHelp";
            this.menuMainAboutOnlineHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.menuMainAboutOnlineHelp.Size = new System.Drawing.Size(191, 22);
            this.menuMainAboutOnlineHelp.Text = "Online &Help";
            this.menuMainAboutOnlineHelp.ToolTipText = "http://www.aurelitec.com/cinemadrape/windows/help/";
            this.menuMainAboutOnlineHelp.Click += new System.EventHandler(this.EventMenuMainOnlineHelpClick);
            // 
            // menuMainExit
            // 
            this.menuMainExit.Name = "menuMainExit";
            this.menuMainExit.ShortcutKeyDisplayString = "";
            this.menuMainExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.menuMainExit.Size = new System.Drawing.Size(236, 22);
            this.menuMainExit.Text = "E&xit";
            this.menuMainExit.Click += new System.EventHandler(this.EventMenuExitClick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.menuMain;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "CinemaDrape";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.EventNotifyIconMouseClick);
            // 
            // saveLayoutFileDialog
            // 
            this.saveLayoutFileDialog.Filter = "CinemaDrape Configuration Files|*.cinemadrape";
            this.saveLayoutFileDialog.Title = "Save Focus Areas and Drape Settings";
            // 
            // openLayoutFileDialog
            // 
            this.openLayoutFileDialog.Filter = "CinemaDrape Configuration Files|*.cinemadrape";
            this.openLayoutFileDialog.Title = "Load Focus Areas and Drape Settings From";
            // 
            // delayedBringToFrontTimer
            // 
            this.delayedBringToFrontTimer.Tick += new System.EventHandler(this.EventDelayedBringToFrontTimerTick);
            // 
            // menuFocus
            // 
            this.menuFocus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFocusDelete,
            this.toolStripSeparator4,
            this.menuFocusSize,
            this.menuFocusSizeTextBox,
            this.toolStripSeparator5,
            this.menuFocusCommonSizes,
            this.menuFocus640x390,
            this.x480ToolStripMenuItem,
            this.x490ToolStripMenuItem,
            this.x600ToolStripMenuItem,
            this.x768ToolStripMenuItem,
            this.x800ToolStripMenuItem,
            this.x1024ToolStripMenuItem});
            this.menuFocus.Name = "menuFocusAreaContext";
            this.menuFocus.Size = new System.Drawing.Size(218, 261);
            this.menuFocus.Opening += new System.ComponentModel.CancelEventHandler(this.EventMenuFocusOpening);
            // 
            // menuFocusDelete
            // 
            this.menuFocusDelete.Name = "menuFocusDelete";
            this.menuFocusDelete.ShortcutKeyDisplayString = "Del";
            this.menuFocusDelete.Size = new System.Drawing.Size(217, 22);
            this.menuFocusDelete.Text = "Delete This Focus Area";
            this.menuFocusDelete.Click += new System.EventHandler(this.EventMenuFocusDeleteClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(214, 6);
            // 
            // menuFocusSize
            // 
            this.menuFocusSize.Enabled = false;
            this.menuFocusSize.Name = "menuFocusSize";
            this.menuFocusSize.Size = new System.Drawing.Size(217, 22);
            this.menuFocusSize.Text = "Current Size:";
            // 
            // menuFocusSizeTextBox
            // 
            this.menuFocusSizeTextBox.AcceptsReturn = true;
            this.menuFocusSizeTextBox.BackColor = System.Drawing.Color.Black;
            this.menuFocusSizeTextBox.ForeColor = System.Drawing.Color.White;
            this.menuFocusSizeTextBox.Name = "menuFocusSizeTextBox";
            this.menuFocusSizeTextBox.Size = new System.Drawing.Size(130, 23);
            this.menuFocusSizeTextBox.ToolTipText = "Enter a new width and height, and press Enter.";
            this.menuFocusSizeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EventMenuFocusSizeTextBoxKeyPress);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(214, 6);
            // 
            // menuFocusCommonSizes
            // 
            this.menuFocusCommonSizes.Enabled = false;
            this.menuFocusCommonSizes.Name = "menuFocusCommonSizes";
            this.menuFocusCommonSizes.Size = new System.Drawing.Size(217, 22);
            this.menuFocusCommonSizes.Text = "Common Sizes:";
            // 
            // menuFocus640x390
            // 
            this.menuFocus640x390.Name = "menuFocus640x390";
            this.menuFocus640x390.Size = new System.Drawing.Size(217, 22);
            this.menuFocus640x390.Text = "640 x 390";
            this.menuFocus640x390.Click += new System.EventHandler(this.EventMenuFocusCommonSizesItemsClick);
            // 
            // x480ToolStripMenuItem
            // 
            this.x480ToolStripMenuItem.Name = "x480ToolStripMenuItem";
            this.x480ToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.x480ToolStripMenuItem.Text = "640 x 480";
            this.x480ToolStripMenuItem.Click += new System.EventHandler(this.EventMenuFocusCommonSizesItemsClick);
            // 
            // x490ToolStripMenuItem
            // 
            this.x490ToolStripMenuItem.Name = "x490ToolStripMenuItem";
            this.x490ToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.x490ToolStripMenuItem.Text = "800 x 490";
            this.x490ToolStripMenuItem.Click += new System.EventHandler(this.EventMenuFocusCommonSizesItemsClick);
            // 
            // x600ToolStripMenuItem
            // 
            this.x600ToolStripMenuItem.Name = "x600ToolStripMenuItem";
            this.x600ToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.x600ToolStripMenuItem.Text = "800 x 600";
            this.x600ToolStripMenuItem.Click += new System.EventHandler(this.EventMenuFocusCommonSizesItemsClick);
            // 
            // x768ToolStripMenuItem
            // 
            this.x768ToolStripMenuItem.Name = "x768ToolStripMenuItem";
            this.x768ToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.x768ToolStripMenuItem.Text = "1024 x 768";
            this.x768ToolStripMenuItem.Click += new System.EventHandler(this.EventMenuFocusCommonSizesItemsClick);
            // 
            // x800ToolStripMenuItem
            // 
            this.x800ToolStripMenuItem.Name = "x800ToolStripMenuItem";
            this.x800ToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.x800ToolStripMenuItem.Text = "1280 x 800";
            this.x800ToolStripMenuItem.Click += new System.EventHandler(this.EventMenuFocusCommonSizesItemsClick);
            // 
            // x1024ToolStripMenuItem
            // 
            this.x1024ToolStripMenuItem.Name = "x1024ToolStripMenuItem";
            this.x1024ToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.x1024ToolStripMenuItem.Text = "1280 x 1024";
            this.x1024ToolStripMenuItem.Click += new System.EventHandler(this.EventMenuFocusCommonSizesItemsClick);
            // 
            // focusableListBox
            // 
            this.focusableListBox.BackColor = System.Drawing.Color.Black;
            this.focusableListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.focusableListBox.ContextMenuStrip = this.menuMain;
            this.focusableListBox.FormattingEnabled = true;
            this.focusableListBox.Location = new System.Drawing.Point(0, 0);
            this.focusableListBox.Margin = new System.Windows.Forms.Padding(2);
            this.focusableListBox.Name = "focusableListBox";
            this.focusableListBox.Size = new System.Drawing.Size(0, 0);
            this.focusableListBox.TabIndex = 0;
            // 
            // statusLabel
            // 
            this.statusLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusLabel.ForeColor = System.Drawing.Color.White;
            this.statusLabel.Location = new System.Drawing.Point(0, 0);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.statusLabel.Size = new System.Drawing.Size(961, 27);
            this.statusLabel.TabIndex = 2;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusLabel.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(961, 512);
            this.ContextMenuStrip = this.menuMain;
            this.ControlBox = false;
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.focusableListBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "CinemaDrape";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EventMainFormFormClosed);
            this.Load += new System.EventHandler(this.EventMainFormLoad);
            this.Shown += new System.EventHandler(this.EventMainFormShown);
            this.Click += new System.EventHandler(this.EventMainFormClick);
            this.DoubleClick += new System.EventHandler(this.EventMainFormDoubleClick);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EventMainFormKeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.EventMainFormKeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.EventMainFormMouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EventMainFormMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.EventMainFormMouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.EventMainFormMouseUp);
            this.menuMain.ResumeLayout(false);
            this.menuFocus.ResumeLayout(false);
            this.menuFocus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem menuMainDrape;
        private System.Windows.Forms.ToolStripSeparator menuMainSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuMainFocusAreas;
        private System.Windows.Forms.ToolStripSeparator menuMainSeparator4;
        private System.Windows.Forms.ToolStripMenuItem menuMainExit;
        private System.Windows.Forms.ToolStripMenuItem menuMainColor;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.ToolStripMenuItem menuMainOpacity;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripSeparator menuMainSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuMainLoad;
        private System.Windows.Forms.ToolStripMenuItem menuMainSave;
        private System.Windows.Forms.ToolStripMenuItem menuMainAbout;
        private System.Windows.Forms.ToolStripMenuItem menuMainAboutOnlineHelp;
        private System.Windows.Forms.SaveFileDialog saveLayoutFileDialog;
        private System.Windows.Forms.OpenFileDialog openLayoutFileDialog;
        private System.Windows.Forms.Timer delayedBringToFrontTimer;
        private System.Windows.Forms.ToolStripMenuItem menuMainCoverMonitors;
        private System.Windows.Forms.ToolStripMenuItem menuMainAboutName;
        private System.Windows.Forms.ToolStripMenuItem menuMainAboutVersion;
        private System.Windows.Forms.ToolStripMenuItem menuMainAboutCopyright;
        private System.Windows.Forms.ContextMenuStrip menuFocus;
        private System.Windows.Forms.ToolStripMenuItem menuFocusDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem menuFocus640x390;
        private System.Windows.Forms.ToolStripMenuItem x480ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x490ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x600ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x768ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x800ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x1024ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuFocusSize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem menuFocusCommonSizes;
        private System.Windows.Forms.ToolStripTextBox menuFocusSizeTextBox;
        private System.Windows.Forms.ListBox focusableListBox;
        private System.Windows.Forms.ToolStripMenuItem menuMainRandomArea;
        private System.Windows.Forms.ToolStripMenuItem menuMainDeleteAll;
        private System.Windows.Forms.ToolStripSeparator menuMainSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuMainDeleteAllSure;
        private System.Windows.Forms.ToolStripMenuItem menuMainDeleteAllYes;
        private System.Windows.Forms.ToolStripMenuItem menuMainDeleteAllNo;
        private System.Windows.Forms.ToolStripMenuItem menuMainHideRestore;
        private System.Windows.Forms.ToolStripMenuItem menuMainSettings;
        private System.Windows.Forms.ToolStripMenuItem menuMainColorCustom;
        private System.Windows.Forms.ToolStripMenuItem menuMainColorRandom;
        private System.Windows.Forms.ToolStripMenuItem menuMainResetToBlack;
        private System.Windows.Forms.ToolStripMenuItem menuMainExtras;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripTextBox menuMainColorValueTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem menuMainQuickStart;
        private System.Windows.Forms.ToolStripMenuItem menuMainAutoDetectArea;
        private System.Windows.Forms.ToolStripMenuItem menuMainAutoDetectWindow;
        private System.Windows.Forms.Label statusLabel;
    }
}

