//-----------------------------------------------------------------------------------------------------------------------
//
// <copyright file="QuickStartForm.cs" company="Aurelitec">
// Copyright (c) 2009-2017 Aurelitec
// http://www.aurelitec.com
// Licensed under the GNU General Public License v3.0. See LICENSE file in the project root for full license information.
// </copyright>
//
// Description: The quick start form.
//
//-----------------------------------------------------------------------------------------------------------------------

namespace CinemaDrape
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Aurelitec.Reuse;

    /// <summary>
    /// The quick start form.
    /// </summary>
    public partial class QuickStartForm : Form
    {
        // ********************************************************************
        // Constructor and Initialization
        // ********************************************************************

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickStartForm"/> class.
        /// </summary>
        public QuickStartForm()
        {
            // Set the form's font to the default operating system font (Segoe UI on Vista)
            this.Font = SystemFonts.MessageBoxFont;

            // Required method for designer support
            this.InitializeComponent();
        }

        /// <summary>
        /// Creates a new instance of the quick start form, with the specified owner.
        /// </summary>
        /// <param name="owner">The owner of the quick start form.</param>
        /// <returns>The quick start form instance.</returns>
        public static QuickStartForm Create(Form owner)
        {
            QuickStartForm quickStartForm = new QuickStartForm();
            quickStartForm.Owner = owner;
            quickStartForm.SetDefaultLocation();
            return quickStartForm;
        }

        // ********************************************************************
        // Form Location
        // ********************************************************************

        /// <summary>
        /// Ensures the quick start form is located on a visible monitor (on multi-monitor configurations).
        /// </summary>
        public void EnsureOnscreen()
        {
            if (!MultiMonitorSupport.IsOnAScreen(this.Bounds))
            {
                this.SetDefaultLocation();
            }
        }

        /// <summary>
        /// Sets the default location of the quick start form.
        /// </summary>
        private void SetDefaultLocation()
        {
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Left + ((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2), 10);
        }

        // ********************************************************************
        // Events
        // ********************************************************************

        /// <summary>
        /// Event: Auto-sizes the quick start form when the form is first loaded.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventQuickStartFormLoad(object sender, EventArgs e)
        {
            this.ClientSize = new Size(
                this.topAdvicePanel.PreferredSize.Width + (this.topAdvicePanel.Left * 2),
                this.topAdvicePanel.PreferredSize.Height + (int)(this.topAdvicePanel.Top * 1.5));
        }

        /// <summary>
        /// Event: Hides the quick start form when the user clicks the OK button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void EventOkButtonClick(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// Event: Ensures the quick start form is not closed by the user, but only made invisible.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Form closing event data.</param>
        private void EventQuickStartFormFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }
    }
}
