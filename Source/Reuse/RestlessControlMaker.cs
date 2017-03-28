//-----------------------------------------------------------------------------------------------------------------------
//
// <copyright file="RestlessControlMaker.cs" company="Aurelitec">
// Copyright (c) 2009-2017 Aurelitec
// http://www.aurelitec.com
// Licensed under the GNU General Public License v3.0. See LICENSE file in the project root for full license information.
// </copyright>
//
// Description: Makes any control restless (resizeable and/or moveable with the mouse).
//
//-----------------------------------------------------------------------------------------------------------------------

namespace Aurelitec.Reuse
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    /// Makes any control restless (resizable and/or moveable with the mouse).
    /// </summary>
    public class RestlessControlMaker
    {
        // ********************************************************************
        // Constructor
        // ********************************************************************

        /// <summary>
        /// Initializes a new instance of the <see cref="RestlessControlMaker"/> class.
        /// </summary>
        /// <param name="control">The control to make restless.</param>
        /// <param name="border">The resizing border of the control.</param>
        /// <param name="fullBodyDrag">True if the control can be dragged by its body, false otherwise.</param>
        private RestlessControlMaker(Control control, Padding border, bool fullBodyDrag)
        {
            this.Border = border;
            this.FullBodyDrag = fullBodyDrag;
            this.FullBodyCursor = Cursors.Default;

            control.MouseMove += this.MouseMoveEvent;
            control.MouseDown += this.MouseDownEvent;
            control.MouseLeave += this.MouseLeaveEvent;
        }

        // ********************************************************************
        // Public Properties
        // ********************************************************************

        /// <summary>
        /// Gets or sets the size of the resizable border of the restless control.
        /// </summary>
        public Padding Border { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the restless control can be moved by dragging its body.
        /// </summary>
        public bool FullBodyDrag { get; set; }

        /// <summary>
        /// Gets or sets the mouse cursor to be displayed when the mouse is over the body of the restless control.
        /// </summary>
        public Cursor FullBodyCursor { get; set; }

        // ********************************************************************
        // Public Constructing Methods
        // ********************************************************************

        /// <summary>
        /// Makes a control restless.
        /// </summary>
        /// <param name="control">The control to make restless.</param>
        /// <param name="border">The resizing border of the control.</param>
        /// <param name="fullBodyDrag">True if the control can be dragged by its body, false otherwise.</param>
        /// <returns>A RestlessControlMaker object.</returns>
        public static RestlessControlMaker Make(Control control, Padding border, bool fullBodyDrag)
        {
            return new RestlessControlMaker(control, border, fullBodyDrag);
        }

        /// <summary>
        /// Makes a control restless.
        /// </summary>
        /// <param name="control">The control to make restless.</param>
        /// <returns>A RestlessControlMaker object.</returns>
        public static RestlessControlMaker Make(Control control)
        {
            return new RestlessControlMaker(control, new Padding(8), true);
        }

        // ********************************************************************
        // On-Demand Moving/Resizing
        // ********************************************************************

        /// <summary>
        /// Starts sizing the restless control on demand.
        /// </summary>
        /// <param name="control">The restless control.</param>
        public static void StartSizing(Control control)
        {
            RestlessControlMaker.DoSysCommand(control, (IntPtr)NativeMethods.SC_SIZE);
        }

        /// <summary>
        /// Starts moving the restless control on demand.
        /// </summary>
        /// <param name="control">The restless control.</param>
        public static void StartMoving(Control control)
        {
            RestlessControlMaker.DoSysCommand(control, (IntPtr)NativeMethods.SC_MOVE);
        }

        // ********************************************************************
        // Resize/Move System Commands Functionality
        // ********************************************************************

        /// <summary>
        /// Sends a Resize/Move system command to a control.
        /// </summary>
        /// <param name="control">The control that receives the command.</param>
        /// <param name="command">The system command.</param>
        private static void DoSysCommand(Control control, IntPtr command)
        {
            NativeMethods.ReleaseCapture();
            NativeMethods.SendMessage(control.Handle, NativeMethods.WM_SYSCOMMAND, command, IntPtr.Zero);
        }

        /// <summary>
        /// Calculates the correct move/resize command and mouse cursor based on the position of the mouse
        /// over the restless control.
        /// </summary>
        /// <param name="x">The X-coordinate of the mouse.</param>
        /// <param name="y">The Y-coordinate of the mouse.</param>
        /// <param name="control">The restless control.</param>
        /// <param name="borderCursor">The mouse cursor.</param>
        /// <returns>The move/resize system command.</returns>
        private int WhichBorder(int x, int y, Control control, out Cursor borderCursor)
        {
            Padding border = this.Border;

            // For very small, full body drag restless controls, make the border very slim (1px)
            if (this.FullBodyDrag && ((border.Horizontal >= control.Width) || (border.Vertical >= control.Height)))
            {
                border.All = 1;

                // For full body drag restless controls smaller than 4x4, always return the body drag command (no mouse resize is possible)
                if ((border.Horizontal >= control.Width) || (border.Vertical >= control.Height))
                {
                    borderCursor = this.FullBodyCursor;
                    return NativeMethods.SC_DRAGMOVE;
                }
            }

            if (x < border.Left)
            {
                if (y < border.Top)
                {
                    // North-West Corner
                    borderCursor = Cursors.SizeNWSE;
                    return NativeMethods.SC_DRAGSIZE_NW;
                }
                else if (y < control.Height - border.Bottom)
                {
                    // West Border
                    borderCursor = Cursors.SizeWE;
                    return NativeMethods.SC_DRAGSIZE_W;
                }
                else
                {
                    // South-West Corner
                    borderCursor = Cursors.SizeNESW;
                    return NativeMethods.SC_DRAGSIZE_SW;
                }
            }
            else if (x < control.Width - border.Right)
            {
                if (y < border.Top)
                {
                    // North Border - middle third: drag and move, first and last third: resize north
                    bool drag = (!this.FullBodyDrag) && ((x > control.Width / 4) && (x < 3 * (control.Width / 4)));
                    borderCursor = drag ? Cursors.SizeAll : Cursors.SizeNS;
                    return drag ? NativeMethods.SC_DRAGMOVE : NativeMethods.SC_DRAGSIZE_N;
                }
                else if (y > control.Height - border.Bottom)
                {
                    // South Border
                    borderCursor = Cursors.SizeNS;
                    return NativeMethods.SC_DRAGSIZE_S;
                }
            }
            else
            {
                if (y < border.Top)
                {
                    // North-East Corner
                    borderCursor = Cursors.SizeNESW;
                    return NativeMethods.SC_DRAGSIZE_NE;
                }
                else if (y < control.Height - border.Bottom)
                {
                    // East Border
                    borderCursor = Cursors.SizeWE;
                    return NativeMethods.SC_DRAGSIZE_E;
                }
                else
                {
                    // South-East Corner
                    borderCursor = Cursors.SizeNWSE;
                    return NativeMethods.SC_DRAGSIZE_SE;
                }
            }

            borderCursor = this.FullBodyDrag ? this.FullBodyCursor : Cursors.Default;
            return this.FullBodyDrag ? NativeMethods.SC_DRAGMOVE : 0;
        }

        // ********************************************************************
        // Restless-Making Mouse Events
        // ********************************************************************

        /// <summary>
        /// Show the correct cursor depending on the mouse position on the restless control.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Mouse event data.</param>
        private void MouseMoveEvent(object sender, MouseEventArgs e)
        {
            Control senderControl = sender as Control;
            if (senderControl != null)
            {
                Cursor borderCursor;
                this.WhichBorder(e.X, e.Y, senderControl, out borderCursor);
                senderControl.Cursor = borderCursor;
            }
        }

        /// <summary>
        /// Start the move/resize system command when user left clicks the restless control.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Mouse event data.</param>
        private void MouseDownEvent(object sender, MouseEventArgs e)
        {
            Control senderControl = sender as Control;
            if ((e.Button == MouseButtons.Left) && (e.Clicks == 1) && (senderControl != null))
            {
                Cursor borderCursor;
                int borderCommand = this.WhichBorder(e.X, e.Y, senderControl, out borderCursor);
                if (borderCommand != 0)
                {
                    senderControl.BringToFront();
                    DoSysCommand(senderControl, (IntPtr)borderCommand);
                }
            }
        }

        /// <summary>
        /// Restore the default cursor after the mouse leaves the restless control.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private void MouseLeaveEvent(object sender, EventArgs e)
        {
            Control senderControl = sender as Control;
            if (senderControl != null)
            {
                senderControl.Cursor = Cursors.Default;
            }
        }

        // ********************************************************************
        // Native Methods
        // ********************************************************************

        /// <summary>
        /// Contains Native Windows API method definitions and constants required for the restless functionality.
        /// </summary>
        private static class NativeMethods
        {
            /// <summary>
            /// A window receives this message when the user chooses a command from the Window menu.
            /// </summary>
            public const int WM_SYSCOMMAND = 0x0112;

            /// <summary>
            /// The Drag and Move window system command.
            /// </summary>
            public const int SC_DRAGMOVE = 0xF012;

            /// <summary>
            /// The Drag and Size North Border window system command.
            /// </summary>
            public const int SC_DRAGSIZE_N = 0xF003;

            /// <summary>
            /// The Drag and Size South Border window system command.
            /// </summary>
            public const int SC_DRAGSIZE_S = 0xF006;

            /// <summary>
            /// The Drag and Size East Border window system command.
            /// </summary>
            public const int SC_DRAGSIZE_E = 0xF002;

            /// <summary>
            /// The Drag and Size West Border window system command.
            /// </summary>
            public const int SC_DRAGSIZE_W = 0xF001;

            /// <summary>
            /// The Drag and Size North West Corner window system command.
            /// </summary>
            public const int SC_DRAGSIZE_NW = 0xF004;

            /// <summary>
            /// The Drag and Size North East Corner window system command.
            /// </summary>
            public const int SC_DRAGSIZE_NE = 0xF005;

            /// <summary>
            /// The Drag and Size South West Corner window system command.
            /// </summary>
            public const int SC_DRAGSIZE_SW = 0xF007;

            /// <summary>
            /// The Drag and Size South East Corner window system command.
            /// </summary>
            public const int SC_DRAGSIZE_SE = 0xF008;

            /// <summary>
            /// The Size window system command.
            /// </summary>
            public const int SC_SIZE = 0xF000;

            /// <summary>
            /// The Move window system command.
            /// </summary>
            public const int SC_MOVE = 0xF010;

            /// <summary>
            /// Sends the specified message to a window or windows.
            /// </summary>
            /// <param name="hWnd">A handle to the window whose window procedure will receive the message.</param>
            /// <param name="msg">The message to be sent.</param>
            /// <param name="wParam">Additional message-specific information of integer type.</param>
            /// <param name="lParam">Additional message-specific information of long type.</param>
            /// <returns>The return value specifies the result of the message processing; it depends on the
            /// message sent.</returns>
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

            /// <summary>
            /// Releases the mouse capture from a window in the current thread and restores normal mouse
            /// input processing.
            /// </summary>
            /// <returns>If the function succeeds, the return value is nonzero.</returns>
            [DllImport("user32.dll")]
            public static extern int ReleaseCapture();
        }
    }
}