//----------------------------------------------------------------------------------------------------
//
// <copyright file="MainForm.cs" company="Aurelitec">
// Copyright (c) 2009-2017 Aurelitec
// http://www.aurelitec.com
// Licensed under the GNU General Public License v3.0. See LICENSE file in the project root for full license information.
// </copyright>
//
// Description: The main CinemaDrape drape form - main routines.
//
//----------------------------------------------------------------------------------------------------

namespace CinemaDrape
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;
    using Aurelitec.Reuse;

    /// <summary>
    /// The main, drape form of CinemaDrape.
    /// </summary>
    public partial class MainForm : Form
    {
        // ********************************************************************
        // Constructor
        // ********************************************************************

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            // Set the form's font to the default operating system font (Segoe UI on Vista)
            this.Font = SystemFonts.MessageBoxFont;

            // Required method for designer support
            this.InitializeComponent();

            // Wire up the mouse wheel event
            this.focusableListBox.MouseWheel += this.EventMainFormMouseWheel;
        }

        // ********************************************************************
        // Properties
        // ********************************************************************

        /// <summary>
        /// Gets or sets a value indicating whether we are in the transparent mode for easier adding or editing focus areas.
        /// </summary>
        internal bool TransparentForEditing
        {
            get
            {
                return this.transparentForEditing;
            }

            set
            {
                this.transparentForEditing = value;
                if (value)
                {
                    if (this.Opacity > this.peekOpacity)
                    {
                        this.Opacity = this.peekOpacity;
                    }
                }
                else
                {
                    this.Opacity = this.lastUserOpacity;
                }
            }
        }

        /// <summary>
        /// Gets or sets the string representation of the current hot key, and registers or unregisters it as appropriate.
        /// </summary>
        private string HotKeyString
        {
            get
            {
                return this.hotKeyString;
            }

            set
            {
                this.hotKeyString = value;

                // If an empty string was provided, unregister any current hot key.
                if (string.IsNullOrEmpty(this.hotKeyString))
                {
                    if (this.hotKey != null)
                    {
                        this.hotKey.Unregister();
                        this.hotKey = null;
                    }
                }
                else
                {
                    this.hotKey = new GlobalHotkey(this.hotKeyString, this.Handle);

                    // Try to register the global hot key in Windows
                    if (!this.hotKey.Register())
                    {
                        // On failure, disable the hot key by setting it to an empty string
                        this.hotKeyString = string.Empty;
                    }
                }

                // Display the current hot key on the Pause/Resume menu item, and in the Pause label of the Quick Start Form
                this.menuMainHideRestore.ShortcutKeyDisplayString = this.hotKeyString;
                if (this.quickStartForm != null)
                {
                    this.quickStartForm.pauseLabel.Text = string.Format(CultureInfo.CurrentCulture, Properties.Resources.StringQuickPause, this.hotKeyString);
                }
            }
        }

        // ********************************************************************
        // Form Initialization
        // ********************************************************************

        /// <summary>
        /// Overrides the window procedure to intercept and process the Activate App and Hot Key Windows messages.
        /// </summary>
        /// <param name="m">The Windows message to process.</param>
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            // Listen for operating system messages
            switch (m.Msg)
            {
                // Restore the CinemaDrape drape form on top of all other windows, when an external window is about to be activated
                case WinAPIHelper.ActivateAppWindowsMessage:
                    if (this.afterStartup && this.Visible)
                    {
                        this.delayedBringToFrontTimer.Start();
                    }

                    break;

                // Hide or restore the drape when the user presses the global hot key
                case GlobalHotkey.HotkeyWindowsMessage:
                    this.TogglePauseMode();
                    break;
            }

            // Call the base processing of Windows messages
            base.WndProc(ref m);
        }

        /// <summary>
        /// Event: Performs important program initialization tasks when the main form is loaded.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMainFormLoad(object sender, EventArgs e)
        {
            try
            {
                // Initialize the random number generators for generating random areas and random colors
                ColorsCommon.RandomGenerator = new Random();

                // Get the bounds of all monitors
                this.Bounds = SystemInformation.VirtualScreen;

                // Save the current opacity
                this.lastUserOpacity = this.Opacity;

                // Finalize the menus (add the opacity track bar, the multi monitor support items, modernize the menu, etc.)
                this.FinalizeMenus();

                // Create the quick start form
                this.quickStartForm = QuickStartForm.Create(this);

                // Load settings from the default configuration file (and the focus areas if AutoRestoreAreas is true)
                this.LoadLayout(string.Empty, false);
                this.quickStartForm.EnsureOnscreen();

                // Load an optional configuration file if specified on the command line
                string[] args = Environment.GetCommandLineArgs();
                if (args.Length > 1)
                {
                    this.LoadLayout(args[1], true);
                }

                // Register the Pause/Resume hot key read from the configuration file, or the default one
                this.HotKeyString = string.IsNullOrEmpty(this.hotKeyString) ? GlobalHotkey.KeysToString(Keys.Control | Keys.F11) : this.hotKeyString;
            }
            finally
            {
                // Signal that the startup phase has ended
                this.afterStartup = true;
            }
        }

        /// <summary>
        /// Event: Shows the quick start form when the form is first shown, only if the "Show At Startup" setting has not been disabled.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMainFormShown(object sender, EventArgs e)
        {
            if (this.quickStartForm.ShowAtStartupCheckBox.Checked)
            {
                this.quickStartForm.Visible = true;
            }
        }

        // ********************************************************************
        // Events - Mouse (New Focus Area Creation/Drawing)
        // ********************************************************************

        /// <summary>
        /// Save the mouse position for a potential new focus area creation when the user presses the left mouse button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Mouse event data.</param>
        private void EventMainFormMouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    this.newFocusAreaLocation = e.Location;
                    this.TransparentForEditing = true;
                    this.focusableListBox.Focus();
                    break;
            }
        }

        /// <summary>
        /// Create or update the new focus area that is being drawn by the user.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Mouse event data.</param>
        private void EventMainFormMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!this.newFocusAreaLocation.IsEmpty && (e.Location != this.newFocusAreaLocation))
                {
                    if (!this.TransparentForEditing)
                    {
                        this.TransparentForEditing = true;
                    }

                    // Make sure the new focus area control is created.
                    if (this.newFocusArea == null)
                    {
                        this.newFocusArea = new FocusControl(
                            this,
                            new Rectangle(this.newFocusAreaLocation, Size.Empty),
                            this.menuFocus,
                            this.appColor,
                            false);
                    }

                    // Update the size of the new focus area control.
                    if (this.newFocusArea != null)
                    {
                        this.newFocusArea.BringToFront();
                        this.newFocusArea.ResizeWhileCreating(this.newFocusAreaLocation, e.Location);
                    }
                }
            }
        }

        /// <summary>
        /// Complete the creation of a new focus area when the user releases the left mouse button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Mouse event data.</param>
        private void EventMainFormMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.newFocusAreaLocation = Point.Empty;
                this.TransparentForEditing = false;

                // Complete creating the new focus area control.
                if (this.newFocusArea != null)
                {
                    this.newFocusArea.CreationEnded = true;
                    this.CompleteNewFocusArea(this.newFocusArea);
                    this.newFocusArea = null;
                }
            }
        }

        // ********************************************************************
        // Events - Other Mouse Events
        // ********************************************************************

        /// <summary>
        /// Pauses CinemaDrape (hides the drape) when the user double-clicks or double-taps.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMainFormDoubleClick(object sender, EventArgs e)
        {
            this.TogglePauseMode();
        }

        /// <summary>
        /// Increases or decreases the opacity then the mouse wheel is used.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Mouse event data.</param>
        private void EventMainFormMouseWheel(object sender, MouseEventArgs e)
        {
            int wheels = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            if (wheels != 0)
            {
                double newOpacity = wheels > 0 ? Math.Min(1d, this.Opacity + 0.01d) : Math.Max(0.01d, this.Opacity - 0.01d);
                this.SetDrapeOpacity(newOpacity);
            }
        }

        /// <summary>
        /// Disables any Auto Detect Menu Mode when the user clicks the drape form.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMainFormClick(object sender, EventArgs e)
        {
            if (this.autoDetectAreaMenuMode)
            {
                this.autoDetectAreaMenuMode = false;
                this.HideStatus();
                this.AutoDetectArea();
                this.TransparentForEditing = false;
                return;
            }

            if (this.autoDetectWindowMenuMode)
            {
                this.autoDetectWindowMenuMode = false;
                this.HideStatus();
                this.AutoDetectWindow();
                this.TransparentForEditing = false;
                return;
            }
        }

        /// <summary>
        /// Auto detects the area or window under cursor if the user clicks the middle mouse button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Mouse event data.</param>
        private void EventMainFormMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                if ((Control.ModifierKeys & Keys.Control) != 0)
                {
                    this.AutoDetectWindow();
                }
                else
                {
                    this.AutoDetectArea();
                }

                this.TransparentForEditing = false;
            }
        }

        // ********************************************************************
        // Events - Keyboard
        // ********************************************************************

        /// <summary>
        /// Handle key pressing.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Keyboard event data.</param>
        private void EventMainFormKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    // When Escape is pressed, try to cancel the Autodetect Menu Mode, otherwise show the correct context menu
                    if (!this.CancelAutoDetectMenuMode())
                    {
                        if (FocusControl.Current != null)
                        {
                            // If a focus control is focused, show its context menu
                            FocusControl.Current.ContextMenuStrip.Show(FocusControl.Current, FocusControl.Current.PointToClient(Cursor.Position));
                        }
                        else
                        {
                            // Show the main context menu
                            this.menuMain.Show(Cursor.Position);
                        }
                    }

                    e.SuppressKeyPress = true;
                    break;
                case Keys.Delete:
                    // When Delete is pressed, delete the current focus control
                    if (FocusControl.Current != null)
                    {
                        FocusControl.Current.Delete();
                    }

                    e.SuppressKeyPress = true;
                    break;
                case Keys.ShiftKey:
                    this.TransparentForEditing = true;
                    e.SuppressKeyPress = true;
                    break;
                case Keys.A:
                    if (e.Control)
                    {
                        this.AutoDetectArea();
                    }

                    break;
                case Keys.W:
                    if (e.Control)
                    {
                        this.AutoDetectWindow();
                    }

                    break;
            }
        }

        /// <summary>
        /// Handle key releasing.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Keyboard event data.</param>
        private void EventMainFormKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.ShiftKey)
            {
                this.TransparentForEditing = false;
                e.Handled = true;
            }
        }

        // ********************************************************************
        // Events - Delayed Bring To Front Timer
        // ********************************************************************

        /// <summary>
        /// Event: Delayed Bring To Front Timer Tick
        /// Updates the current drape color with the value entered in the Color Value Text Box, when Enter is pressed.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventDelayedBringToFrontTimerTick(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.BringToFront();
            }

            this.delayedBringToFrontTimer.Stop();
        }

        // ********************************************************************
        // Notification Icon
        // ********************************************************************

        /// <summary>
        /// Event: Notification Icon Click
        /// Restores the drape when the user clicks the CinemaDrape notification icon.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Mouse event data.</param>
        private void EventNotifyIconMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.TogglePauseMode();
            }
        }

        // ********************************************************************
        // App Shutdown
        // ********************************************************************

        /// <summary>
        /// Event: Form Closing
        /// Saves the layout to the default configuration file and unregisters the global hot key when the main CinemaDrape form is closed.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Form closed event data.</param>
        private void EventMainFormFormClosed(object sender, FormClosedEventArgs e)
        {
            this.SaveLayout(string.Empty, true);
            this.HotKeyString = string.Empty;
        }
    }
}
