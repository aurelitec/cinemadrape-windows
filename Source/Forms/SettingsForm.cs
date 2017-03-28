//-----------------------------------------------------------------------------------------------------------------------
//
// <copyright file="SettingsForm.cs" company="Aurelitec">
// Copyright (c) 2009-2017 Aurelitec
// http://www.aurelitec.com
// Licensed under the GNU General Public License v3.0. See LICENSE file in the project root for full license information.
// </copyright>
//
// Description: The CinemaDrape Settings form.
//
//-----------------------------------------------------------------------------------------------------------------------

namespace CinemaDrape
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    /// <summary>
    /// The CinemaDrape Settings form.
    /// </summary>
    public partial class SettingsForm : Form
    {
        // ********************************************************************
        // Constructor and Form Initialization
        // ********************************************************************

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsForm"/> class.
        /// </summary>
        public SettingsForm()
        {
            // Set the form's font to the default operating system font (Segoe UI on Vista)
            this.Font = SystemFonts.MessageBoxFont;

            // Required method for designer support
            this.InitializeComponent();
        }

        /// <summary>
        /// Event: Form First Shown
        /// Makes sure the Hot Key text box has no text selected when the form is displayed (for aesthetic purposes).
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventSettingsFormShown(object sender, EventArgs e)
        {
            this.UpdatePeekOpacityLabel();
            this.hotKeyTextBox.SelectionLength = 0;
        }

        // ********************************************************************
        // Form Events
        // ********************************************************************

        /// <summary>
        /// Event: Hot Key Text Box Key Down
        /// Translates the pressed key code and modifiers into a string representation that is displayed into the Hot Key text box.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Keyboard event data.</param>
        private void EventHotKeyTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Back)
            {
                // Remove the modifier keys from the key data
                Keys pressedKey = e.KeyData ^ e.Modifiers;

                if (pressedKey != Keys.None)
                {
                    // Show the modifiers and the key in the text box
                    this.hotKeyTextBox.Text = new KeysConverter().ConvertToString(e.KeyData);
                    e.SuppressKeyPress = true;
                }
            }
            else
            {
                this.hotKeyTextBox.Text = string.Empty;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Event: Updates the Peek Opacity label and preview when the user changes the value of the Peek Opacity track bar.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventPeekOpacityTrackBarValueChanged(object sender, EventArgs e)
        {
            this.UpdatePeekOpacityLabel();
            this.PeekOpacityPreview(true);
        }

        /// <summary>
        /// Event: Shows the Peek Opacity preview when the user leaves the track bar control.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventPeekOpacityTrackBarEnter(object sender, EventArgs e)
        {
            this.PeekOpacityPreview(true);
            this.UpdatePeekOpacityLabel();
        }

        /// <summary>
        /// Event: Hides the Peek Opacity preview when the user leaves the track bar control.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventPeekOpacityTrackBarLeave(object sender, EventArgs e)
        {
            this.PeekOpacityPreview(false);
        }

        // ********************************************************************
        // Private Methods
        // ********************************************************************

        /// <summary>
        /// Updates the Peek Opacity label.
        /// </summary>
        private void UpdatePeekOpacityLabel()
        {
            this.peekOpacityLabel.Text = string.Format(CultureInfo.CurrentCulture, Properties.Resources.StringSettingsFormPeekOpacity, this.peekOpacityTrackBar.Value);
        }

        /// <summary>
        /// Shows or hides the Peek Opacity preview on the drape form.
        /// </summary>
        /// <param name="enabled">True to show preview, false otherwise.</param>
        private void PeekOpacityPreview(bool enabled)
        {
            MainForm mainForm = this.Owner as MainForm;
            if (mainForm != null)
            {
                if (enabled)
                {
                    mainForm.Opacity = (double)this.peekOpacityTrackBar.Value / 100;
                }
                else
                {
                    mainForm.TransparentForEditing = false;
                }
            }
        }
    }
}
