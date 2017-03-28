//-----------------------------------------------------------------------------------------------------------------------
//
// <copyright file="MessageBoxReporter.cs" company="Aurelitec">
// Copyright (c) 2009-2017 Aurelitec
// http://www.aurelitec.com
// Licensed under the GNU General Public License v3.0. See LICENSE file in the project root for full license information.
// </copyright>
//
// Description: Provides methods that report any errors, messages or questions to
// the user through message boxes.
//
//-----------------------------------------------------------------------------------------------------------------------

namespace Aurelitec.Reuse
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Windows.Forms;

    /// <summary>
    /// Provides methods that report any errors, messages or questions to the user through message boxes.
    /// </summary>
    public static class MessageBoxReporter
    {
        /// <summary>
        /// Returns an error message from a Win32 error code.
        /// </summary>
        /// <param name="errorCode">The Win32 error code.</param>
        /// <returns>The error message.</returns>
        public static string Win32ErrorMessage(int errorCode)
        {
            Win32Exception exception = new Win32Exception(errorCode);
            return exception != null ? exception.Message : string.Empty;
        }

        /// <summary>
        /// Invokes a method and reports any errors to the user.
        /// </summary>
        /// <param name="stuff">The method to invoke.</param>
        /// <param name="owner">The top-level window and owner of the error message box.</param>
        /// <param name="message">The message describing the action to be displayed in case of an exception.</param>
        /// <param name="argument">The argument of the method to be displayed in case of an exception.</param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "We report the error to the user")]
        public static void Invoke(MethodInvoker stuff, IWin32Window owner, string message, string argument)
        {
            try
            {
                stuff();
            }
            catch (Exception ex)
            {
                MessageBoxReporter.Show(owner, message, argument, ex.Message);
            }
        }

        /// <summary>
        /// Reports an error to the user through a message box.
        /// </summary>
        /// <param name="owner">The top-level window and owner of the error message box.</param>
        /// <param name="message">The error message.</param>
        /// <param name="argument">The error argument.</param>
        /// <param name="exceptionMessage">The exception message.</param>
        /// <returns>The dialog result of the message box.</returns>
        public static DialogResult Show(IWin32Window owner, string message, string argument, string exceptionMessage)
        {
            return MessageBox.Show(
                owner,
                string.Format(CultureInfo.CurrentCulture, "{0}\r\n\r\n{1}\r\n\r\n{2}", message, argument, exceptionMessage),
                Application.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxReporter.RightToLeftMessageBoxOptions(owner));
        }

        /// <summary>
        /// Returns the appropriate MessageBoxOptions value that should be added when showing message boxes on right to left systems.
        /// </summary>
        /// <param name="owner">The top-level window and owner of the message box that will be displayed.</param>
        /// <returns>The MessageBoxOptions value.</returns>
        public static MessageBoxOptions RightToLeftMessageBoxOptions(IWin32Window owner)
        {
            Control control = owner as Control;
            bool isRightToLeft = control != null ? control.RightToLeft == RightToLeft.Yes : CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft;
            return isRightToLeft ? MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign : 0;
        }
    }
}
