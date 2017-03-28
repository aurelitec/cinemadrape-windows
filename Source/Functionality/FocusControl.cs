//-----------------------------------------------------------------------------------------------------------------------
//
// <copyright file="FocusControl.cs" company="Aurelitec">
// Copyright (c) 2009-2017 Aurelitec
// http://www.aurelitec.com
// Licensed under the GNU General Public License v3.0. See LICENSE file in the project root for full license information.
// </copyright>
//
// Description: The CinemaDrape resizeable, transparent, focus control.
//
//-----------------------------------------------------------------------------------------------------------------------

namespace CinemaDrape
{
    using System;
    using System.Drawing;
    using System.Security.Permissions;
    using System.Windows.Forms;
    using Aurelitec.Reuse;

    /// <summary>
    /// The CinemaDrape resizable, transparent, focus control.
    /// </summary>
    public class FocusControl : Control
    {
        // ********************************************************************
        // Fields
        // ********************************************************************

        /// <summary>
        /// The minimum area of a focus control.
        /// </summary>
        private const int MinimumArea = 100;

        /// <summary>
        /// Indicates whether the creation of the focus control has ended.
        /// </summary>
        private bool creationEnded;

        /// <summary>
        /// Indicates whether the focus control has been entered by the mouse.
        /// </summary>
        private bool enteredByMouse;

        /// <summary>
        /// The size of the border.
        /// </summary>
        private int borderSize;

        /// <summary>
        /// The color of the border of the focus control.
        /// </summary>
        private Color borderColor;

        /// <summary>
        /// The pen used to draw the focused border of a focus control.
        /// </summary>
        private Pen borderFocusPen;

        /// <summary>
        /// The pen used to draw the inactive border of a focus control.
        /// </summary>
        private Pen borderClearPen;

        /// <summary>
        /// The brush used to draw the drag portion of the north border.
        /// </summary>
        private Brush borderDragBrush;

        // ********************************************************************
        // Constructors
        // ********************************************************************

        /// <summary>
        /// Initializes static members of the <see cref="FocusControl"/> class.
        /// </summary>
        static FocusControl()
        {
            FocusControl.Current = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FocusControl"/> class.
        /// </summary>
        public FocusControl()
        {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint |
                ControlStyles.Selectable,
                true);
            this.DoubleBuffered = true;
            this.borderSize = 32;
            this.BorderColor = ColorTranslator.FromHtml("#1BADEA");
            this.creationEnded = false;

            // Brings the focus control to front
            this.BringToFront();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FocusControl"/> class, with the specified owner, bounds, and context menu.
        /// </summary>
        /// <param name="owner">The owner of the focus control.</param>
        /// <param name="bounds">The bounds of the focus control.</param>
        /// <param name="menu">The context menu associated with the focus control.</param>
        /// <param name="fullCreate">True to fully create the focus control, false otherwise.</param>
        public FocusControl(Form owner, Rectangle bounds, ContextMenuStrip menu, bool fullCreate)
            : this()
        {
            this.BackColor = owner.TransparencyKey;
            this.Bounds = bounds;
            this.ContextMenuStrip = menu;
            owner.Controls.Add(this);
            this.InitBorderPens();
            this.CreationEnded = fullCreate;
        }

        /// <summary>
        /// The event handler that should be executed when the sizing or moving of the focus control has begun.
        /// </summary>
        public event EventHandler SizeMoveBegin;

        /// <summary>
        /// The event handler that should be executed when the sizing or moving of the focus control has ended.
        /// </summary>
        public event EventHandler SizeMoveEnd;

        // ********************************************************************
        // Properties
        // ********************************************************************

        /// <summary>
        /// Gets or sets the current focus control (that has the mouse over or has been selected with Tab).
        /// </summary>
        public static FocusControl Current { get; set; }

        /// <summary>
        /// Gets or sets the color of the border of the focus control.
        /// </summary>
        public Color BorderColor
        {
            get
            {
                return this.borderColor;
            }

            set
            {
                this.borderColor = value;
                this.InitBorderPens();
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the size of the border of the focus control.
        /// </summary>
        public int BorderSize
        {
            get
            {
                return this.borderSize;
            }

            set
            {
                this.borderSize = value;
                this.InitBorderPens();
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the control that is focusable and can be used to remove the focus from focus controls.
        /// </summary>
        public Control OtherFocusableControl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the creation of the focus control has ended, and complete its creation.
        /// </summary>
        public bool CreationEnded
        {
            get
            {
                return this.creationEnded;
            }

            set
            {
                this.creationEnded = value;
                if (this.creationEnded)
                {
                    // Complete creating the new focus area control, and make sure we don't create empty focus areas.
                    if (((this.Width == 0) || (this.Height == 0)) || (this.Width * this.Height < FocusControl.MinimumArea))
                    {
                        this.Delete();
                    }
                    else
                    {
                        // Add the border size to the bounds
                        this.Bounds = Rectangle.Inflate(this.Bounds, this.BorderSize / 2, this.BorderSize / 2);

                        // Make the focus control resizeable and moveable with the mouse
                        RestlessControlMaker.Make(this, new Padding(this.BorderSize), false);

                        // Redraw the focus control
                        this.Invalidate();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the real bounds of the focus control, without the border size.
        /// </summary>
        public Rectangle RealBounds
        {
            get
            {
                return Rectangle.Inflate(this.Bounds, -this.BorderSize / 2, -this.BorderSize / 2);
            }
        }

        /// <summary>
        /// Checks if a bounds rectangle is not to large to create a focus area (larger than 80% of the parent drape).
        /// </summary>
        /// <param name="bounds">The bounds rectangle.</param>
        /// <param name="parent">The parent (usually the drape form)</param>
        /// <returns>True if the bounds rectangle is too large, false otherwise.</returns>
        public static bool IsTooLarge(Rectangle bounds, Control parent)
        {
            return ((double)bounds.Width * bounds.Height) / (parent.Width * parent.Height) > 0.8d;
        }

        // ********************************************************************
        // Multiple Focus Controls
        // ********************************************************************

        /// <summary>
        /// Counts the number of focus controls that have the specified owner.
        /// </summary>
        /// <param name="owner">The owner of the focus controls.</param>
        /// <returns>The number of focus controls.</returns>
        public static int GetCount(Control owner)
        {
            int count = 0;
            FocusControl.ForEach(owner, (control) => { count++; });
            return count;
        }

        /// <summary>
        /// Performs an action on all focus controls that have the specified owner.
        /// </summary>
        /// <param name="owner">The owner of the focus controls.</param>
        /// <param name="action">The action delegate to perform.</param>
        public static void ForEach(Control owner, Action<FocusControl> action)
        {
            if (action != null)
            {
                foreach (Control control in owner.Controls)
                {
                    FocusControl focusControl = control as FocusControl;
                    if (focusControl != null)
                    {
                        action(focusControl);
                    }
                }
            }
        }

        /// <summary>
        /// Removes the control from the control collection of its parent and releases all its resources.
        /// </summary>
        public void Delete()
        {
            if ((this.Parent != null) && (this.Parent.Controls != null))
            {
                this.Parent.Controls.Remove(this);
                this.Dispose();
            }
        }

        /// <summary>
        /// Support creating focus controls by drawing them like a rectangle.
        /// </summary>
        /// <param name="topLeft">The top left corner of the focus control.</param>
        /// <param name="bottomRight">The bottom right corner of the focus control.</param>
        public void ResizeWhileCreating(Point topLeft, Point bottomRight)
        {
            if (this != null)
            {
                this.BringToFront();
                this.SetBounds(
                    Math.Min(topLeft.X, bottomRight.X),
                    Math.Min(topLeft.Y, bottomRight.Y),
                    Math.Abs(bottomRight.X - topLeft.X),
                    Math.Abs(bottomRight.Y - topLeft.Y));
            }
        }

        /// <summary>
        /// Initializes the border pens and brushes. Should also be called when the parent background color changes.
        /// </summary>
        public void InitBorderPens()
        {
            this.borderFocusPen = new Pen(this.BorderColor, this.BorderSize);
            this.borderClearPen = new Pen(this.Parent != null ? this.Parent.BackColor : Color.Black, this.BorderSize);
            this.borderDragBrush = new SolidBrush(ColorsCommon.DarkerColor(this.borderColor, 0.2f));
        }

        /// <summary>
        /// Overrides the window procedure to intercept and process resizing/moving Windows messages.
        /// </summary>
        /// <param name="m">The Windows message to process.</param>
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            const int WM_ENTERSIZEMOVE = 0x0231;
            const int WM_EXITSIZEMOVE = 0x0232;

            // Listen for operating system messages.
            switch (m.Msg)
            {
                case WM_ENTERSIZEMOVE:
                    if (this.SizeMoveEnd != null)
                    {
                        this.SizeMoveBegin(this, new EventArgs());
                    }

                    break;
                case WM_EXITSIZEMOVE:
                    if (this.SizeMoveEnd != null)
                    {
                        this.SizeMoveEnd(this, new EventArgs());
                    }

                    break;
            }

            // Call the base processing of Windows messages
            base.WndProc(ref m);
        }

        /// <summary>
        /// Ensures that closing the associated context menu strip removes the hovered effect
        /// (highlighted border) that is visible while the context menu is displayed.
        /// </summary>
        /// <param name="e">Empty event data.</param>
        protected override void OnContextMenuStripChanged(EventArgs e)
        {
            base.OnContextMenuStripChanged(e);
            if (this.ContextMenuStrip != null)
            {
                this.ContextMenuStrip.Closed += (sender2, e2) => { this.RemoveFocus(); };
            }
        }

        /// <summary>
        /// Paint the border of the focus control when painting the control.
        /// </summary>
        /// <param name="e">Paint event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw the active or inactive focus border only when the control is fully created, not while creating it with the mouse
            if (this.creationEnded)
            {
                // Draw the active or inactive focus border
                e.Graphics.DrawRectangle(this.Focused ? this.borderFocusPen : this.borderClearPen, this.ClientRectangle);

                // Draw the slightly darker dragging section of the top border
                if (this.Focused && this.Width > this.BorderSize * 2)
                {
                    int left = Math.Max(this.ClientRectangle.Width / 4, this.BorderSize);
                    int width = Math.Min(2 * (this.ClientRectangle.Width / 4), this.Width - (this.BorderSize * 2));
                    Rectangle northDragBounds = new Rectangle(left, 0, width, this.BorderSize / 2);
                    e.Graphics.FillRectangle(this.borderDragBrush, northDragBounds);
                }
            }
        }

        /// <summary>
        /// When the mouse enters the focus control, signal that it the current control, bring it to front and redraw it to show its border.
        /// </summary>
        /// <param name="e">Empty event data.</param>
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            FocusControl.Current = this;

            // Bring the control to the front of the z-order
            this.BringToFront();

            // Redraw the control
            this.Invalidate();
        }

        /// <summary>
        /// When the mouse leaves the focus control, signal that it's no longer the current control, and redraw it to hide its border.
        /// </summary>
        /// <param name="e">Empty event data.</param>
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            FocusControl.Current = null;
            this.Invalidate();
        }

        /// <summary>
        /// Focuses the control when the mouse enters the control.
        /// </summary>
        /// <param name="e">Empty event data.</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (!this.Focused)
            {
                this.enteredByMouse = true;
                this.Focus();
            }
        }

        /// <summary>
        /// Removes the focus of the control when the mouse leaves the control, only if the
        /// associated ContextMenuStrip is not opened.
        /// </summary>
        /// <param name="e">Empty event data.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (!this.enteredByMouse || ((this.ContextMenuStrip != null) && this.ContextMenuStrip.Visible))
            {
                return;
            }

            this.enteredByMouse = false;
            this.RemoveFocus();
        }

        /// <summary>
        /// Overrides the command key processing to implement the moving/resizing of the focus control using the keyboard.
        /// </summary>
        /// <param name="msg">The window message to process.</param>
        /// <param name="keyData">One of the Keys values that represents the key to process.</param>
        /// <returns>True if the character was processed by the control; otherwise, false.</returns>
        [SecurityPermissionAttribute(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                int step = 1;
                switch (keyData)
                {
                    case Keys.Left:
                    case Keys.Left | Keys.Shift:
                        this.Left = this.Left - step;
                        return true;
                    case Keys.Right:
                    case Keys.Right | Keys.Shift:
                        this.Left = this.Left + step;
                        return true;
                    case Keys.Up:
                    case Keys.Up | Keys.Shift:
                        this.Top = this.Top - step;
                        return true;
                    case Keys.Down:
                    case Keys.Down | Keys.Shift:
                        this.Top = this.Top + step;
                        return true;

                    case Keys.Control | Keys.Left:
                    case Keys.Control | Keys.Left | Keys.Shift:
                        this.Left = this.Left - step;
                        this.Width = this.Width + step;
                        return true;
                    case Keys.Control | Keys.Right:
                    case Keys.Control | Keys.Right | Keys.Shift:
                        this.Left = this.Left + step;
                        this.Width = this.Width - step;
                        return true;
                    case Keys.Alt | Keys.Left:
                    case Keys.Alt | Keys.Left | Keys.Shift:
                        this.Width = this.Width - step;
                        return true;
                    case Keys.Alt | Keys.Right:
                    case Keys.Alt | Keys.Right | Keys.Shift:
                        this.Width = this.Width + step;
                        return true;

                    case Keys.Control | Keys.Up:
                    case Keys.Control | Keys.Up | Keys.Shift:
                        this.Top = this.Top - step;
                        this.Height = this.Height + step;
                        return true;
                    case Keys.Control | Keys.Down:
                    case Keys.Control | Keys.Down | Keys.Shift:
                        this.Top = this.Top + step;
                        this.Height = this.Height - step;
                        return true;
                    case Keys.Alt | Keys.Up:
                    case Keys.Alt | Keys.Up | Keys.Shift:
                        this.Height = this.Height - step;
                        return true;
                    case Keys.Alt | Keys.Down:
                    case Keys.Alt | Keys.Down | Keys.Shift:
                        this.Height = this.Height + step;
                        return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Removes the focus from the control by selecting another specified focusable control.
        /// </summary>
        private void RemoveFocus()
        {
            if (this.OtherFocusableControl != null)
            {
                this.OtherFocusableControl.Focus();
            }
        }
    }
}
