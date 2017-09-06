//----------------------------------------------------------------------------------------------------
//
// <copyright file="MainForm.Func.cs" company="Aurelitec">
// Copyright (c) 2009-2017 Aurelitec
// http://www.aurelitec.com
// Licensed under the GNU General Public License v3.0. See LICENSE file in the project root for full license information.
// </copyright>
//
// Description: The main CinemaDrape drape form - functionality helpers.
//
//----------------------------------------------------------------------------------------------------

namespace CinemaDrape
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;
    using Aurelitec.Reuse;

    /// <summary>
    /// The main CinemaDrape drape form.
    /// </summary>
    public partial class MainForm
    {
        /// <summary>
        /// Suspends the current thread by calling the Sleep routine for a number of times.
        /// </summary>
        /// <param name="counter">The number of sleep calls to make.</param>
        private static void SleepAndWait(int counter)
        {
            for (int i = 1; i <= counter; i++)
            {
                System.Threading.Thread.Sleep(1);
            }
        }

        /// <summary>
        /// Deletes all focus areas.
        /// </summary>
        private void DeleteAllFocusAreas()
        {
            List<FocusControl> controlsToDelete = new List<FocusControl>();
            FocusControl.ForEach(this, (control) => { controlsToDelete.Add(control); });
            controlsToDelete.ForEach((control) => { control.Delete(); });
        }

        /// <summary>
        /// Sets the new drape color, but first ensures it's different from the transparency key color value.
        /// </summary>
        /// <param name="color">The new color value for the drape.</param>
        private void SetDrapeColor(Color color)
        {
            this.BackColor = ColorsCommon.EnsureDifferentColor(color, this.TransparencyKey);        // Ensure the color is different from the transparency color
            FocusControl.ForEach(this, (control) => { control.InitBorderPens(); });             // Reinitialize the border pens of each focus control
            this.Invalidate(true);                                                                  // Force the redrawing of the drape
        }

        /// <summary>
        /// Sets a new drape opacity, by modifying the opacity track bar value.
        /// </summary>
        /// <param name="opacity">The new opacity value for the drape.</param>
        private void SetDrapeOpacity(double opacity)
        {
            this.lastUserOpacity = this.Opacity = opacity;
            this.menuOpacityTrackBar.Value = (int)(opacity * 100);
            this.menuMainOpacity.Text = string.Format(CultureInfo.CurrentCulture, Properties.Resources.StringMenuOpacity, this.Opacity);
        }

        /// <summary>
        /// Completes the drawing of a new focus area.
        /// </summary>
        /// <param name="focusArea">The focus area control to complete.</param>
        private void CompleteNewFocusArea(FocusControl focusArea)
        {
            if ((!focusArea.IsDisposed) && (focusArea != null))
            {
                focusArea.SizeMoveBegin += (sender, e) => { this.TransparentForEditing = true; };
                focusArea.SizeMoveEnd += (sender, e) => { this.TransparentForEditing = false; };
                focusArea.OtherFocusableControl = this.focusableListBox;
            }
        }

        /// <summary>
        /// Adds and completes a new focus area with the specified bounds.
        /// </summary>
        /// <param name="rect">The bounds of the new focus area.</param>
        private void AddNewFocusArea(Rectangle rect)
        {
            FocusControl focusArea = new FocusControl(this, rect, this.menuFocus, this.appColor, true);
            this.CompleteNewFocusArea(focusArea);
        }

        /// <summary>
        /// Creates the radio check bitmap to be used on radio menu items.
        /// </summary>
        /// <returns>The radio check bitmap.</returns>
        private Bitmap GetRadioCheckBitmap()
        {
            Bitmap radioCheckBitmap = new Bitmap(16, 16);
            using (Graphics graphics = Graphics.FromImage(radioCheckBitmap))
            {
                graphics.FillEllipse(new SolidBrush(this.radioCheckColor), new Rectangle(3, 3, radioCheckBitmap.Width - 5, radioCheckBitmap.Height - 5));
            }

            return radioCheckBitmap;
        }

        /// <summary>
        /// Auto detects the area under the mouse cursor.
        /// </summary>
        private void AutoDetectArea()
        {
            this.Visible = false;
            Rectangle rect = Rectangle.Empty;
            try
            {
                try
                {
                    // Wait to make sure the context menu gets closed and the drape gets invisible
                    MainForm.SleepAndWait(10);

                    // Get the bounds of the control under cursor using Accessible Objects
                    rect = WinAPIHelper.GetAccessibleObjectBounds(Cursor.Position.X, Cursor.Position.Y);
                }
                catch
                {
                    // ignore errors
                }

                // If we fail to get the bounds, or the area is too large, retry using Window methods
                if (rect.IsEmpty || FocusControl.IsTooLarge(rect, this))
                {
                    try
                    {
                        rect = WinAPIHelper.RectangleFromPoint(Cursor.Position.X, Cursor.Position.Y);
                    }
                    catch
                    {
                        // ignore errors
                    }
                }
            }
            finally
            {
                this.Visible = true;
            }

            // Add the new auto detected focus area, if not empty and not too large
            if ((!rect.IsEmpty) && (!FocusControl.IsTooLarge(rect, this)))
            {
                this.AddNewFocusArea(rect);
            }
        }

        /// <summary>
        /// Auto detects the window under the mouse cursor.
        /// </summary>
        private void AutoDetectWindow()
        {
            this.Visible = false;
            Rectangle rect = Rectangle.Empty;
            try
            {
                try
                {
                    // Wait to make sure the context menu gets closed and the drape gets invisible
                    MainForm.SleepAndWait(10);

                    // Get the bounds of the window under cursor
                    rect = WinAPIHelper.RectangleFromPoint(Cursor.Position.X, Cursor.Position.Y);
                }
                catch
                {
                    // ignore errors
                }
            }
            finally
            {
                this.Visible = true;
            }

            // Add the new auto detected focus area, if not empty and not too large
            if ((!rect.IsEmpty) && (!FocusControl.IsTooLarge(rect, this)))
            {
                this.AddNewFocusArea(rect);
            }
        }

        /// <summary>
        /// Cancels the Auto Detect Menu Mode, restores the opacity and hides the status label.
        /// </summary>
        /// <returns>True if one of the Auto Detect Menu Modes was enabled, false otherwise.</returns>
        private bool CancelAutoDetectMenuMode()
        {
            if (this.autoDetectAreaMenuMode || this.autoDetectWindowMenuMode)
            {
                this.autoDetectAreaMenuMode = this.autoDetectWindowMenuMode = false;
                this.TransparentForEditing = false;
                this.HideStatus();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Shows the status label with the specified text.
        /// </summary>
        /// <param name="text">The text to show in the status label.</param>
        private void ShowStatus(string text)
        {
            this.statusLabel.Text = text;
            this.statusLabel.Visible = true;
            this.statusLabel.BringToFront();
        }

        /// <summary>
        /// Hides the status label.
        /// </summary>
        private void HideStatus()
        {
            this.statusLabel.Text = string.Empty;
            this.statusLabel.Visible = false;
        }

        /// <summary>
        /// Toggles the pause mode: if visible, hides the drape. If hidden, restores the drape.
        /// </summary>
        private void TogglePauseMode()
        {
            this.Visible = !this.Visible;

            if ((!this.Visible) && (this.quickStartForm != null) && (!this.quickStartForm.IsDisposed))
            {
                this.quickStartForm.Visible = false;
            }

            this.menuMainHideRestore.Text = this.Visible ? Properties.Resources.StringMenuPause : Properties.Resources.StringMenuRestore;

            if ((!this.Visible) && this.firstPause)
            {
                this.firstPause = false;
                this.notifyIcon.ShowBalloonTip(10000, Application.ProductName, Properties.Resources.StringPauseBalloonTip, ToolTipIcon.Info);
            }
        }
    }
}
