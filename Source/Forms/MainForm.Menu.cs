//----------------------------------------------------------------------------------------------------
//
// <copyright file="MainForm.Menu.cs" company="Aurelitec">
// Copyright (c) 2009-2017 Aurelitec
// http://www.aurelitec.com
// Licensed under the GNU General Public License v3.0. See LICENSE file in the project root for full license information.
// </copyright>
//
// Description: The main CinemaDrape drape form - menu event handlers and menu initialization.
//
//----------------------------------------------------------------------------------------------------
namespace CinemaDrape
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;
    using Aurelitec.Reuse;

    /// <summary>
    /// The main CinemaDrape drape form.
    /// </summary>
    public partial class MainForm
    {
        // ***********
        // Events - Init Menus
        // ***********

        /// <summary>
        /// Finalizes the menus - adds the opacity track bar, the multi monitor support items, modernizes the menu, etc.
        /// </summary>
        private void FinalizeMenus()
        {
            // Add the opacity track bar
            this.menuOpacityTrackBar = new TrackBar()
            {
                BackColor = Color.Black,
                Minimum = 1,
                Maximum = 100,
                Value = (int)(this.Opacity * 100),
                TickFrequency = 5,
                Width = this.menuMainAbout.DropDown.Width, // A good default value for the width of the track bar is the width of its parent menu item
            };
            this.menuOpacityTrackBar.Scroll += this.EventMenuOpacityTrackBarScroll;
            this.menuMainOpacity.DropDownItems.Add(new ToolStripControlHost(this.menuOpacityTrackBar));

            // Add the multi monitor support menu items
            MultiMonitorSupport.RadioCheckImage = this.GetRadioCheckBitmap();
            if (!MultiMonitorSupport.AddMonitorMenuItems(this.menuMainCoverMonitors.DropDown, this.EventMenuDrapeAllMonitorsClick, this.EventMenuDrapeMonitorsClick))
            {
                this.menuMainCoverMonitors.Visible = false;
            }

            // Add the current build number to the About->Version menu item
            this.menuMainAboutVersion.Text += Application.ProductVersion;

            // Modernize the menu, with a black background and an Aurelitec blue hightlight
            MenuToolStripCustomizer customizer = MenuToolStripCustomizer.Modernize(
                Color.FromArgb(10, 10, 10),
                this.appColor,
                Color.White,
                10,
                0.8d,
                null,
                this.menuMain,
                this.menuFocus);
            customizer.ColorTable.ColorOfImageMarginGradientBegin =
            customizer.ColorTable.ColorOfImageMarginGradientMiddle =
            customizer.ColorTable.ColorOfImageMarginGradientEnd = Color.FromArgb(20, 20, 20);
        }

        // ********************************************************************
        // Events - Main Menu - Opening/Closing
        // ********************************************************************

        /// <summary>
        /// Enables/disables certain items based on the current state of the program.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Cancelable event data.</param>
        private void EventMenuMainOpening(object sender, CancelEventArgs e)
        {
            this.CancelAutoDetectMenuMode();
            this.menuMainDeleteAll.Visible = FocusControl.GetCount(this) > 0;

            // Disable most of the menu items if on pause
            this.menuMainColor.Enabled = this.menuMainOpacity.Enabled = this.menuMainResetToBlack.Enabled = this.menuMainCoverMonitors.Enabled =
            this.menuMainAutoDetectArea.Enabled = this.menuMainAutoDetectWindow.Enabled =
            this.menuMainRandomArea.Enabled = this.menuMainDeleteAll.Enabled = this.menuMainLoad.Enabled =
            this.menuMainSave.Enabled = this.menuMainSettings.Enabled = this.menuMainQuickStart.Enabled = this.Visible;

            // Hide separators on very small resolutions
            this.menuMainSeparator1.Visible = this.menuMainSeparator2.Visible = this.menuMainSeparator3.Visible = this.menuMain.Height < this.Height - 100;
        }

        // ********************************************************************
        // Events - Menu - Drape
        // ********************************************************************

        /// <summary>
        /// Event - Menu - Shows or hides the drape.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMenuMainHideRestoreClick(object sender, EventArgs e)
        {
            this.TogglePauseMode();
        }

        /// <summary>
        /// Event: Menu -> Color
        /// Updates the Color Value Text Box with the current drape color when the Color drop down menu is opened.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMenuMainColorDropDownOpening(object sender, EventArgs e)
        {
            this.menuMainColorValueTextBox.Text = ColorTranslator.ToHtml(this.BackColor);
        }

        /// <summary>
        /// Event: Menu -> Color -> Value Text Box
        /// Updates the current drape color with the value entered in the Color Value Text Box, when Enter is pressed.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMenuMainColorValueTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                FromString.IfHtmlColor(
                    this.menuMainColorValueTextBox.Text,
                    value =>
                    {
                            this.SetDrapeColor(value);
                            e.SuppressKeyPress = true;
                    },
                    null);
            }
        }

        /// <summary>
        /// Event: Menu -> Color -> Random Color
        /// Sets a random drape color.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMenuColorRandomClick(object sender, EventArgs e)
        {
            this.SetDrapeColor(ColorsCommon.GetRandomColor());
        }

        /// <summary>
        /// Event: Menu -> Color -> Select
        /// Opens a Color dialog box and allows the user to choose a custom drape color.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMenuColorCustomClick(object sender, EventArgs e)
        {
            this.colorDialog.Color = this.BackColor;
            if (this.colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.SetDrapeColor(this.colorDialog.Color);
            }
        }

        /// <summary>
        /// Event: Menu -> Opacity Track Bar
        /// Changes the opacity when the user moves the opacity track bar.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMenuOpacityTrackBarScroll(object sender, EventArgs e)
        {
            double newOpacity = (double)this.menuOpacityTrackBar.Value / 100;
            if (newOpacity != this.Opacity)
            {
                this.SetDrapeOpacity(newOpacity);
            }
        }

        /// <summary>
        /// Event: Menu -> Opaque Black
        /// Resets the drape to black color and full opacity.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMenuMainResetToBlackClick(object sender, EventArgs e)
        {
            this.SetDrapeColor(Color.Black);
            this.SetDrapeOpacity(1d);
        }

        /// <summary>
        /// Event: Menu -> Cover Monitors -> All Monitors
        /// Multi monitor support: sets the drape to cover all monitors.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMenuDrapeAllMonitorsClick(object sender, EventArgs e)
        {
            if (this.afterStartup)
            {
                this.Bounds = SystemInformation.VirtualScreen;
            }
        }

        /// <summary>
        /// Event: Menu -> Cover Monitors -> Single Monitor
        /// Multi monitor support: allows the user to select which single monitor should be covered by the drape.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMenuDrapeMonitorsClick(object sender, EventArgs e)
        {
            if (this.afterStartup)
            {
                this.Bounds = MultiMonitorSupport.ScreenBoundsFromMenuItem(sender);
            }
        }

        // ********************************************************************
        // Events - Menu - Focus Areas
        // ********************************************************************

        /// <summary>
        /// Event - Menu - Automatically detects the control area under cursor.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMenuMainAutoDetectAreaClick(object sender, EventArgs e)
        {
            this.autoDetectAreaMenuMode = true;
            this.TransparentForEditing = true;
            this.ShowStatus(Properties.Resources.StringMenuAutodetectAreaStart);
        }

        /// <summary>
        /// Event - Menu - Automatically detects the window area under cursor.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMenuMainAutoDetectWindowClick(object sender, EventArgs e)
        {
            this.autoDetectWindowMenuMode = true;
            this.TransparentForEditing = true;
            this.ShowStatus(Properties.Resources.StringMenuAutodetectWindowStart);
        }

        /// <summary>
        /// Event - Menu - Creates a new random focus area.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMenuMainRandomAreaClick(object sender, EventArgs e)
        {
            int left = ColorsCommon.RandomGenerator.Next(this.Width - 100);
            int top = ColorsCommon.RandomGenerator.Next(this.Height - 100);
            int width = Math.Max(30, ColorsCommon.RandomGenerator.Next(this.Width - 50 - left));
            int height = Math.Max(30, ColorsCommon.RandomGenerator.Next(this.Height - 50 - top));
            this.AddNewFocusArea(new Rectangle(left, top, width, height));
        }

        /// <summary>
        /// Event - Menu - Deletes all focus areas.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMenuMainDeleteAllYesClick(object sender, EventArgs e)
        {
            this.DeleteAllFocusAreas();
        }

        // ********************************************************************
        // Events - Menu - Load/Save
        // ********************************************************************

        /// <summary>
        /// Event - Menu - Loads a CinemaDrape configuration file.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMenuMainLoadClick(object sender, EventArgs e)
        {
            if (this.openLayoutFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.LoadLayout(this.openLayoutFileDialog.FileName, true);
            }
        }

        /// <summary>
        /// Event - Menu - Saves focus areas and drape settings to a CinemaDrape configuration file.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMenuMainSaveClick(object sender, EventArgs e)
        {
            if (this.saveLayoutFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.SaveLayout(this.saveLayoutFileDialog.FileName, true);
            }
        }

        /// <summary>
        /// Event - Menu - Opens the Settings dialog box.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMenuMainSettingsClick(object sender, EventArgs e)
        {
            string newOrOldHotKeyString = this.HotKeyString;

            // First of all, disable the current hot key, to be able to capture it
            this.HotKeyString = string.Empty;

            using (SettingsForm settingsForm = new SettingsForm())
            {
                settingsForm.hotKeyTextBox.Text = newOrOldHotKeyString;
                settingsForm.peekOpacityTrackBar.Value = (int)(this.peekOpacity * 100);
                settingsForm.AutoRestoreAreasCheckBox.Checked = this.autoRestoreAreas;
                if (settingsForm.ShowDialog(this) == DialogResult.OK)
                {
                    newOrOldHotKeyString = settingsForm.hotKeyTextBox.Text;
                    this.peekOpacity = (double)settingsForm.peekOpacityTrackBar.Value / 100;
                    this.autoRestoreAreas = settingsForm.AutoRestoreAreasCheckBox.Checked;
                }
            }

            this.HotKeyString = newOrOldHotKeyString;
            if (string.IsNullOrEmpty(this.HotKeyString) && !string.IsNullOrEmpty(newOrOldHotKeyString))
            {
                MessageBox.Show("Error registering the shortcut key " + this.hotKeyString + Environment.NewLine + "Please select another keyboard combination.");
            }
        }

        // ********************************************************************
        // Events - Menu - About, Help, Exit
        // ********************************************************************

        /// <summary>
        /// Event - Menu - Shows the quick start form.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMenuMainQuickStartClick(object sender, EventArgs e)
        {
            this.quickStartForm.Visible = true;
        }

        /// <summary>
        /// Event - Menu - Opens the program Web pages.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMenuMainOnlineHelpClick(object sender, EventArgs e)
        {
            ToolStripMenuItem senderMenuItem = sender as ToolStripMenuItem;
            if (senderMenuItem != null)
            {
                MessageBoxReporter.Invoke(
                    () => { Process.Start(senderMenuItem.ToolTipText); },
                    this,
                    Properties.Resources.StringErrorLoadingUrl,
                    senderMenuItem.ToolTipText);
            }
        }

        /// <summary>
        /// Event - Menu - Closes CinemaDrape.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMenuExitClick(object sender, EventArgs e)
        {
            this.Close();
        }

        // ********************************************************************
        // Events - Focus Area Menu
        // ********************************************************************

        /// <summary>
        /// Updates the size of the focus control when its context menu is displayed.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Cancelable event data.</param>
        private void EventMenuFocusOpening(object sender, CancelEventArgs e)
        {
            this.focusControlMenuOwner = this.menuFocus.SourceControl as FocusControl;

            if (this.focusControlMenuOwner != null)
            {
                this.menuFocusSizeTextBox.Text = string.Format(CultureInfo.CurrentCulture, "{0} x {1}", this.focusControlMenuOwner.Width, this.focusControlMenuOwner.Height);
                foreach (ToolStripItem item in this.menuFocus.Items)
                {
                    ToolStripMenuItem menuItem = item as ToolStripMenuItem;
                    if (menuItem != null)
                    {
                        menuItem.Checked = item.Text == this.menuFocusSizeTextBox.Text;
                    }
                }
            }
        }

        /// <summary>
        /// Deletes the current focus control when the user clicks the Delete menu item.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMenuFocusDeleteClick(object sender, EventArgs e)
        {
            if (this.focusControlMenuOwner != null)
            {
                this.focusControlMenuOwner.Delete();
            }
        }

        /// <summary>
        /// Applies a new size when the user presses Enter in the Size text box.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Keyboard event data.</param>
        private void EventMenuFocusSizeTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Return) && (this.focusControlMenuOwner != null))
            {
                FromString.IfIntPair(
                    this.menuFocusSizeTextBox.Text,
                    'x',
                    (width, height) =>
                    {
                        this.focusControlMenuOwner.Width = width;
                        this.focusControlMenuOwner.Height = height;
                    },
                    null);
                e.Handled = true;
            }
        }

        /// <summary>
        /// Applies a new common size to the current focus area.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventMenuFocusCommonSizesItemsClick(object sender, EventArgs e)
        {
            ToolStripMenuItem senderMenu = sender as ToolStripMenuItem;
            if ((senderMenu != null) && (this.focusControlMenuOwner != null))
            {
                FromString.IfIntPair(
                    senderMenu.Text,
                    'x',
                    (width, height) =>
                    {
                        this.focusControlMenuOwner.Width = width;
                        this.focusControlMenuOwner.Height = height;
                    },
                    null);
            }
        }
    }
}
