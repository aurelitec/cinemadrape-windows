//-----------------------------------------------------------------------------------------------------------------------
//
// <copyright file="FromString.cs" company="Aurelitec">
// Copyright (c) 2009-2017 Aurelitec
// http://www.aurelitec.com
// Licensed under the GNU General Public License v3.0. See LICENSE file in the project root for full license information.
// </copyright>
//
// Description: Translates string representations to different objects (Colors, etc.).
//
//-----------------------------------------------------------------------------------------------------------------------

namespace Aurelitec.Reuse
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    /// <summary>
    /// A method to execute on the success of parsing a string to two integer values.
    /// </summary>
    /// <param name="firstValue">The first integer in the pair.</param>
    /// <param name="secondValue">The second integer in the pair.</param>
    public delegate void IntPairAction(int firstValue, int secondValue);

    /// <summary>
    /// Translates string representations to different objects (Colors, etc.).
    /// </summary>
    public static class FromString
    {
        /// <summary>
        /// Invokes an action delegate if a string is not null and not empty, or another action delegate otherwise.
        /// </summary>
        /// <param name="value">The string value to test.</param>
        /// <param name="successAction">The action delegate to be executed if the string is not null and not empty.</param>
        /// <param name="failAction">The action delegate to be executed if the string is null or empty.</param>
        public static void IfNonemptyString(string value, Action<string> successAction, Action<string> failAction)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (successAction != null)
                {
                    successAction(value);
                }
            }
            else
            {
                if (failAction != null)
                {
                    failAction(value);
                }
            }
        }

        /// <summary>
        /// Tries to parse a string value to an integer value, and executes a specified action delegate on success, or
        /// another action delegate on failure.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <param name="successAction">The action delegate to execute on success.</param>
        /// <param name="failAction">The action delegate to execute on failure.</param>
        public static void IfInt(string value, Action<int> successAction, Action<string> failAction)
        {
            int i;
            if (int.TryParse(value, out i))
            {
                FromString.SafeInvoke<int>(successAction, i);
            }
            else
            {
                FromString.SafeInvoke<string>(failAction, value);
            }
        }

        /// <summary>
        /// Tries to parse a string value to two integer value, and executes a specified action delegate on success, or
        /// another action delegate on failure.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <param name="separator">The character that separates the two integer values.</param>
        /// <param name="successAction">The action delegate to execute on success.</param>
        /// <param name="failAction">The action delegate to execute on failure.</param>
        public static void IfIntPair(string value, char separator, IntPairAction successAction, Action<string> failAction)
        {
            string[] pair = value.Split(separator);
            int i, j;
            if ((pair.Length > 1) && int.TryParse(pair[0].Trim(), out i) && int.TryParse(pair[1].Trim(), out j))
            {
                if (successAction != null)
                {
                    successAction(i, j);
                    return;
                }
            }

            FromString.SafeInvoke<string>(failAction, value);
        }

        /// <summary>
        /// Tries to parse a string value to an boolean value, and executes a specified action delegate on success, or
        /// another action delegate on failure.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <param name="successAction">The action delegate to execute on success.</param>
        /// <param name="failAction">The action delegate to execute on failure.</param>
        public static void IfBool(string value, Action<bool> successAction, Action<string> failAction)
        {
            bool b;
            if (bool.TryParse(value, out b))
            {
                FromString.SafeInvoke<bool>(successAction, b);
            }
            else
            {
                FromString.SafeInvoke<string>(failAction, value);
            }
        }

        /// <summary>
        /// Tries to parse a string value to an double value, and executes a specified action delegate on success, or
        /// another action delegate on failure.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <param name="successAction">The action delegate to execute on success.</param>
        /// <param name="failAction">The action delegate to execute on failure.</param>
        public static void IfDouble(string value, Action<double> successAction, Action<string> failAction)
        {
            double d;
            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out d))
            {
                FromString.SafeInvoke<double>(successAction, d);
            }
            else
            {
                FromString.SafeInvoke<string>(failAction, value);
            }
        }

        /// <summary>
        /// Tries to parse a string value to a enumeration value, and executes a specified action delegate on success, or
        /// another action delegate on failure.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration to parse.</typeparam>
        /// <param name="value">The string value to parse.</param>
        /// <param name="successAction">The action delegate to execute on success.</param>
        /// <param name="failAction">The action delegate to execute on failure.</param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "We invoke an failure handler on exceptions.")]
        public static void IfEnum<T>(string value, Action<T> successAction, Action<string> failAction)
        {
            try
            {
                T enumValue = (T)Enum.Parse(typeof(T), value);
                FromString.SafeInvoke<T>(successAction, enumValue);
            }
            catch (Exception)
            {
                FromString.SafeInvoke<string>(failAction, value);
            }
        }

        /// <summary>
        /// Tries to parse a string value to a TimeSpan value, and executes a specified action delegate on success, or
        /// another action delegate on failure.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <param name="successAction">The action delegate to execute on success.</param>
        /// <param name="failAction">The action delegate to execute on failure.</param>
        public static void IfTimeSpan(string value, Action<TimeSpan> successAction, Action<string> failAction)
        {
            TimeSpan timeSpan;
            if (TimeSpan.TryParse(value, out timeSpan))
            {
                FromString.SafeInvoke<TimeSpan>(successAction, timeSpan);
            }
            else
            {
                FromString.SafeInvoke<string>(failAction, value);
            }
        }

        /// <summary>
        /// Tries to translate an HTML color representation to a Color structure, and executes a specified
        /// delegate on success, or another specified delegate on failure.
        /// </summary>
        /// <param name="value">The HTML color representation to translate.</param>
        /// <param name="successAction">The delegate to execute on success.</param>
        /// <param name="failAction">The delegate to execute on failure.</param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "We invoke an failure handler on exceptions.")]
        public static void IfHtmlColor(string value, Action<Color> successAction, Action<string> failAction)
        {
            try
            {
                Color color = ColorTranslator.FromHtml(value);
                if (!color.IsEmpty)
                {
                    FromString.SafeInvoke<Color>(successAction, color);
                }
                else
                {
                    FromString.SafeInvoke<string>(failAction, value);
                }
            }
            catch
            {
                FromString.SafeInvoke<string>(failAction, value);
            }
        }

        /// <summary>
        /// Tries to parse a string value to a Point value, and executes a specified action delegate on success, or
        /// another action delegate on failure.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <param name="successAction">The action delegate to execute on success.</param>
        /// <param name="failAction">The action delegate to execute on failure.</param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "We invoke an failure handler on exceptions.")]
        public static void IfPoint(string value, Action<Point> successAction, Action<string> failAction)
        {
            try
            {
                Point point = (Point)new PointConverter().ConvertFromInvariantString(value);
                FromString.SafeInvoke<Point>(successAction, point);
            }
            catch (Exception)
            {
                FromString.SafeInvoke<string>(failAction, value);
            }
        }

        /// <summary>
        /// Tries to parse a string value to a Rectangle value, and executes a specified action delegate on success, or
        /// another action delegate on failure.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <param name="successAction">The action delegate to execute on success.</param>
        /// <param name="failAction">The action delegate to execute on failure.</param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "We invoke an failure handler on exceptions.")]
        public static void IfRectangle(string value, Action<Rectangle> successAction, Action<string> failAction)
        {
            try
            {
                Rectangle rect = (Rectangle)new RectangleConverter().ConvertFromInvariantString(value);
                FromString.SafeInvoke<Rectangle>(successAction, rect);
            }
            catch (Exception)
            {
                FromString.SafeInvoke<string>(failAction, value);
            }
        }

        /// <summary>
        /// Tries to parse a string value to a Keys value, and executes a specified action delegate on success, or
        /// another action delegate on failure.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <param name="successAction">The action delegate to execute on success.</param>
        /// <param name="failAction">The action delegate to execute on failure.</param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "We invoke an failure handler on exceptions.")]
        public static void IfKeys(string value, Action<Keys> successAction, Action<string> failAction)
        {
            try
            {
                Keys keys = (Keys)new KeysConverter().ConvertFromString(value);
                FromString.SafeInvoke<Keys>(successAction, keys);
            }
            catch (Exception)
            {
                FromString.SafeInvoke<string>(failAction, value);
            }
        }

        /// <summary>
        /// Invokes an action delegate only if the delegate is not null.
        /// </summary>
        /// <typeparam name="T">The type of the parameter for the action delegate.</typeparam>
        /// <param name="action">The action delegate to invoke.</param>
        /// <param name="parameter">The parameter for the action delegate.</param>
        private static void SafeInvoke<T>(Action<T> action, T parameter)
        {
            if (action != null)
            {
                action(parameter);
            }
        }
    }
}
