//-----------------------------------------------------------------------------------------------------------------------
//
// <copyright file="WinAPIHelper.cs" company="Aurelitec">
// Copyright (c) 2009-2017 Aurelitec
// http://www.aurelitec.com
// Licensed under the GNU General Public License v3.0. See LICENSE file in the project root for full license information.
// </copyright>
//
// Description: CinemaDrape helper routines based on Windows API.
//
//-----------------------------------------------------------------------------------------------------------------------

namespace CinemaDrape
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    /// Class of helper routines based on Windows API.
    /// </summary>
    internal static class WinAPIHelper
    {
        /// <summary>
        /// Windows message sent when a window belonging to a different application than the active window is about to be activated.
        /// </summary>
        public const int ActivateAppWindowsMessage = 0x001C;

        /// <summary>
        /// Gets the bounds of the control that contains the specified point.
        /// </summary>
        /// <param name="curX">The x-coordinate of the point.</param>
        /// <param name="curY">The y-coordinate of the point.</param>
        /// <returns>The bounds of the control.</returns>
        public static Rectangle RectangleFromPoint(int curX, int curY)
        {
            IntPtr handle = NativeMethods.RealChildWindowFromPoint(NativeMethods.GetDesktopWindow(), new NativeMethods.POINT() { X = curX, Y = curY });
            if (handle != IntPtr.Zero)
            {
                NativeMethods.RECT rect = default(NativeMethods.RECT);
                if (NativeMethods.GetWindowRect(handle, ref rect) != 0)
                {
                    // In conformance with conventions for the RECT structure, the bottom-right
                    // coordinates of the returned rectangle are exclusive. In other words, the
                    // pixel at (right, bottom) lies immediately outside the rectangle.
                    return new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
                }
            }

            return Rectangle.Empty;
        }

        /// <summary>
        /// Returns the bounds of the accessible object that contains the specified coordinates.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns>The bounds of the object.</returns>
        public static Rectangle GetAccessibleObjectBounds(int x, int y)
        {
            NativeMethods.POINT point = new NativeMethods.POINT() { X = x, Y = y };
            NativeMethods.IAccessible accessibleInterface;
            object varChildID;

            IntPtr success = NativeMethods.AccessibleObjectFromPoint(point, out accessibleInterface, out varChildID);
            if (success == NativeMethods.S_OK)
            {
                if (varChildID == null)
                {
                    varChildID = NativeMethods.CHILDID_SELF;
                }

                int left, top, width, height;
                accessibleInterface.accLocation(out left, out top, out width, out height, varChildID);
                return new Rectangle(left, top, width, height);
            }

            return Rectangle.Empty;
        }

        /// <summary>
        /// Windows API method declarations and structures.
        /// </summary>
        private static class NativeMethods
        {
            /// <summary>
            /// Constant that indicates that information is needed about the object itself.
            /// </summary>
            public const int CHILDID_SELF = 0;

            /// <summary>
            /// Generic HRESULT for success.
            /// </summary>
            public static readonly IntPtr S_OK = IntPtr.Zero;

            /// <summary>
            /// Exposes methods and properties that make a user interface element and its children accessible to client applications.
            /// </summary>
            [ComImport]
            [TypeLibType(0x1050)]
            [Guid("618736E0-3C3D-11CF-810C-00AA00389B71")]
            public interface IAccessible
            {
                /// <summary>
                /// Retrieves the IDispatch of the object's parent. All objects support this property.
                /// </summary>
                /// <returns>The address of the parent object's IDispatch interface.</returns>
                [return: MarshalAs(UnmanagedType.IDispatch)]
                [DispId(unchecked((int)0xFFFFEC78))]
                [TypeLibFunc(0x0040)]
                object get_accParent();

                /// <summary>
                /// Retrieves the number of children that belong to this object. All objects must support this property.
                /// </summary>
                /// <returns>The number of children that belong to this object. The children are accessible objects or child elements.
                /// If the object has no children, this value is zero.</returns>
                [DispId(unchecked((int)0xFFFFEC77))]
                [TypeLibFunc(0x0040)]
                int get_accChildCount();

                /// <summary>
                /// Retrieves an IDispatch for the specified child, if one exists. All objects must support this property.
                /// </summary>
                /// <param name="varChild">Identifies the child whose IDispatch interface is retrieved.</param>
                /// <returns>The child object's IDispatch interface.</returns>
                [return: MarshalAs(UnmanagedType.IDispatch)]
                [TypeLibFunc(0x0040)]
                [DispId(unchecked((int)0xFFFFEC76))]
                object get_accChild([In][MarshalAs(UnmanagedType.Struct)] object varChild);

                /// <summary>
                /// Retrieves the name of the specified object. All objects support this property.
                /// </summary>
                /// <param name="varChild">Specifies whether the retrieved name belongs to the object or one of the object's child elements.
                /// This parameter is either CHILDID_SELF (to obtain information about the object) or a child ID (to obtain information about the
                /// object's child element).</param>
                /// <returns>A string that contains the specified object's name.</returns>
                [return: MarshalAs(UnmanagedType.BStr)]
                [DispId(unchecked((int)0xFFFFEC75))]
                [TypeLibFunc(0x0040)]
                string get_accName([In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

                /// <summary>
                /// Retrieves the value of the specified object. Not all objects have a value.
                /// </summary>
                /// <param name="varChild">Specifies whether the retrieved value information belongs to the object or one of the object's child
                /// elements. This parameter is either CHILDID_SELF (to obtain information about the object) or a child ID (to obtain information
                /// about the object's child element). </param>
                /// <returns>A localized string that contains the object's current value.</returns>
                [return: MarshalAs(UnmanagedType.BStr)]
                [TypeLibFunc(0x0040)]
                [DispId(unchecked((int)0xFFFFEC74))]
                string get_accValue([In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

                /// <summary>
                /// Retrieves a string that describes the visual appearance of the specified object. Not all objects have a description.
                /// </summary>
                /// <param name="varChild">Specifies whether the retrieved description belongs to the object or one of the object's child elements.
                /// This parameter is either CHILDID_SELF (to obtain information about the object) or a child ID (to obtain information about the
                /// object's child element).</param>
                /// <returns>A localized string that describes the specified object, or NULL if this object has no description.</returns>
                [return: MarshalAs(UnmanagedType.BStr)]
                [DispId(unchecked((int)0xFFFFEC73))]
                [TypeLibFunc(0x0040)]
                string get_accDescription([In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

                /// <summary>
                /// Retrieves information that describes the role of the specified object. All objects support this property.
                /// </summary>
                /// <param name="varChild">Specifies whether the retrieved role information belongs to the object or one of the object's child
                /// elements. This parameter is either CHILDID_SELF (to obtain information about the object) or a child ID (to obtain information
                /// about the object's child element). </param>
                /// <returns>An object role constant.</returns>
                [return: MarshalAs(UnmanagedType.Struct)]
                [DispId(unchecked((int)0xFFFFEC72))]
                [TypeLibFunc(0x0040)]
                object get_accRole([In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

                /// <summary>
                /// Retrieves the current state of the specified object. All objects support this property.
                /// </summary>
                /// <param name="varChild">Specifies whether the retrieved state information belongs to the object or of one of the object's child
                /// elements. This parameter is either CHILDID_SELF (to obtain information about the object) or a child ID (to obtain information
                /// about the object's child element). </param>
                /// <returns>A VARIANT structure that receives information that describes the object's state.</returns>
                [return: MarshalAs(UnmanagedType.Struct)]
                [TypeLibFunc(0x0040)]
                [DispId(unchecked((int)0xFFFFEC71))]
                object get_accState([In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

                /// <summary>
                /// Retrieves the Help property string of an object. Not all objects support this property.
                /// </summary>
                /// <param name="varChild">Specifies whether the retrieved help information belongs to the object or one of the object's child
                /// elements. This parameter is either CHILDID_SELF (to obtain information about the object) or a child ID (to obtain information
                /// about one of the object's child elements). </param>
                /// <returns>The localized string that contains the help information for the specified object, or NULL if no help information
                /// is available.</returns>
                [return: MarshalAs(UnmanagedType.BStr)]
                [TypeLibFunc(0x0040)]
                [DispId(unchecked((int)0xFFFFEC70))]
                string get_accHelp([In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

                /// <summary>
                /// retrieves the full path of the WinHelp file that is associated with the specified object; it also retrieves the identifier of
                /// the appropriate topic within that file. Not all objects support this property. This property is rarely supported or used by applications.
                /// </summary>
                /// <param name="pszHelpFile">The full path of the WinHelp file that is associated with the specified object.</param>
                /// <param name="varChild">Specifies whether the retrieved Help topic belongs to the object or one of the object's child elements.
                /// This parameter is either CHILDID_SELF (to obtain a Help topic for the object) or a child ID (to obtain a Help topic for one of
                /// the object's child elements).</param>
                /// <returns>The Help file topic associated with the specified object. This value is used as the context identifier of the desired
                /// topic that passes to the WinHelp function.</returns>
                [DispId(unchecked((int)0xFFFFEC6F))]
                [TypeLibFunc(0x0040)]
                int get_accHelpTopic([Out][MarshalAs(UnmanagedType.BStr)] out string pszHelpFile, [In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

                /// <summary>
                /// Retrieves the specified object's shortcut key or access key, also known as the mnemonic. All objects that have a shortcut
                /// key or an access key support this property.
                /// </summary>
                /// <param name="varChild">Specifies whether the retrieved keyboard shortcut belongs to the object or one of the object's
                /// child elements. This parameter is either CHILDID_SELF (to obtain information about the object) or a child ID (to obtain
                /// information about the object's child element).</param>
                /// <returns>A localized string that identifies the keyboard shortcut, or NULL if no keyboard shortcut is associated with
                /// the specified object.</returns>
                [return: MarshalAs(UnmanagedType.BStr)]
                [DispId(unchecked((int)0xFFFFEC6E))]
                [TypeLibFunc(0x0040)]
                string get_accKeyboardShortcut([In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

                /// <summary>
                /// Retrieves the object that has the keyboard focus. All objects that may receive the keyboard focus must support this property.
                /// </summary>
                /// <returns>A VARIANT structure that receives information about the object that has the focus.</returns>
                [return: MarshalAs(UnmanagedType.Struct)]
                [DispId(unchecked((int)0xFFFFEC6D))]
                [TypeLibFunc(0x0040)]
                object get_accFocus();

                /// <summary>
                /// Retrieves the selected children of this object. All objects that support selection must support this property.
                /// </summary>
                /// <returns>A VARIANT structure that receives information about which children are selected.</returns>
                [return: MarshalAs(UnmanagedType.Struct)]
                [DispId(unchecked((int)0xFFFFEC6C))]
                [TypeLibFunc(0x0040)]
                object get_accSelection();

                /// <summary>
                /// Retrieves a string that indicates the object's default action. Not all objects have a default action.
                /// </summary>
                /// <param name="varChild">Specifies whether the retrieved default action is performed by the object or of one of the object's
                /// child elements. This parameter is either CHILDID_SELF (to obtain information about the object) or a child ID (to obtain
                /// information about the object's child element). </param>
                /// <returns>A localized string that describes the default action for the specified object; if this object has no default action,
                /// the value is NULL.</returns>
                [return: MarshalAs(UnmanagedType.BStr)]
                [TypeLibFunc(0x0040)]
                [DispId(unchecked((int)0xFFFFEC6B))]
                string get_accDefaultAction([In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

                /// <summary>
                /// Modifies the selection or moves the keyboard focus of the specified object. All objects that support selection or
                /// receive the keyboard focus must support this method.
                /// </summary>
                /// <param name="flagsSelect">Specifies which selection or focus operations are to be performed. This parameter must have a
                /// combination of the SELFLAG Constants.</param>
                /// <param name="varChild">Specifies the selected object. If the value is CHILDID_SELF, the object itself is selected; if a
                /// child ID, one of the object's child elements is selected.</param>
                [DispId(unchecked((int)0xFFFFEC6A))]
                [TypeLibFunc(0x0040)]
                void accSelect([In] int flagsSelect, [In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

                /// <summary>
                /// Retrieves the specified object's current screen location. All visual objects must support this method. Sound objects
                /// do not support this method.
                /// </summary>
                /// <param name="pxLeft">Address, in physical screen coordinates, of the variable that receives the x-coordinate of the
                /// upper-left boundary of the object's location.</param>
                /// <param name="pyTop">Address, in physical screen coordinates, of the variable that receives the y-coordinate of the
                /// upper-left boundary of the object's location.</param>
                /// <param name="pcxWidth">Address, in pixels, of the variable that receives the object's width.</param>
                /// <param name="pcyHeight">Address, in pixels, of the variable that receives the object's height.</param>
                /// <param name="varChild">Specifies whether the location that the server returns should be that of the object or that of one of
                /// the object's child elements. This parameter is either CHILDID_SELF (to obtain information about the object) or a child ID
                /// (to obtain information about the object's child element).</param>
                [DispId(unchecked((int)0xFFFFEC69))]
                [TypeLibFunc(0x0040)]
                void accLocation(
                    [Out] out int pxLeft,
                    [Out] out int pyTop,
                    [Out] out int pcxWidth,
                    [Out] out int pcyHeight,
                    [In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

                /// <summary>
                /// Traverses to another UI element within a container and retrieves the object. This method is optional. This method is
                /// deprecated and should not be used. Clients should use other methods and properties.
                /// </summary>
                /// <param name="navDir">Specifies the direction to navigate. This direction is in spatial order, such as left or right,
                /// or logical order, such as next or previous. This value is one of the navigation constants.</param>
                /// <param name="varStart">Specifies whether the starting object of the navigation is the object itself or one of the
                /// object's children. This parameter is either CHILDID_SELF (to start from the object) or a child ID (to start from one of
                /// the object's child elements). </param>
                /// <returns>A VARIANT structure that receives information about the destination object.</returns>
                [return: MarshalAs(UnmanagedType.Struct)]
                [TypeLibFunc(0x0040)]
                [DispId(unchecked((int)0xFFFFEC68))]
                object accNavigate([In] int navDir, [In][Optional][MarshalAs(UnmanagedType.Struct)] object varStart);

                /// <summary>
                /// Retrieves the child element or child object that is displayed at a specific point on the screen. All visual objects support
                /// this method, but sound objects do not. Client applications rarely call this method directly; to get the accessible object
                /// that is displayed at a point, use the AccessibleObjectFromPoint function, which calls this method internally.
                /// </summary>
                /// <param name="xLeft">Specifies the screen coordinates of the point that is hit tested. The x-coordinates increase from left
                /// to right. Note that when screen coordinates are used, the origin is the upper-left corner of the screen.</param>
                /// <param name="yTop">Specifies the screen coordinates of the point that is hit tested. The y-coordinates increase from top
                /// to bottom. Note that when screen coordinates are used, the origin is the upper-left corner of the screen.</param>
                /// <returns>The object displayed at the point specified by xLeft and yTop.</returns>
                [return: MarshalAs(UnmanagedType.Struct)]
                [TypeLibFunc(0x0040)]
                [DispId(unchecked((int)0xFFFFEC67))]
                object accHitTest([In] int xLeft, [In] int yTop);

                /// <summary>
                /// Performs the specified object's default action. Not all objects have a default action.
                /// </summary>
                /// <param name="varChild">Specifies whether the default action belongs to the object or one of the object's child elements.</param>
                [TypeLibFunc(0x0040)]
                [DispId(unchecked((int)0xFFFFEC66))]
                void accDoDefaultAction([In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

                /// <summary>
                /// No longer supported. Client applications should use a control-specific workaround, such as the SetWindowText function.
                /// Servers should return E_NOTIMPL.
                /// </summary>
                /// <param name="varChild">The parameter is not used.</param>
                /// <param name="pszName">The parameter is not used.</param>
                [TypeLibFunc(0x0040)]
                [DispId(unchecked((int)0xFFFFEC75))]
                void set_accName([In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild, [In][MarshalAs(UnmanagedType.BStr)] string pszName);

                /// <summary>
                /// Sets the value of the specified object. Not all objects have a value.
                /// </summary>
                /// <param name="varChild">Specifies whether the value information being set belongs to the object or one of the object's child
                /// elements. This parameter is either CHILDID_SELF (to set information on the object) or a child ID (to set information about the
                /// object's child element).</param>
                /// <param name="pszValue">A localized string that contains the object's value.</param>
                [TypeLibFunc(0x0040)]
                [DispId(unchecked((int)0xFFFFEC74))]
                void set_accValue([In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild, [In][MarshalAs(UnmanagedType.BStr)] string pszValue);
            }

            [DllImport("user32.dll", SetLastError = false)]
            public static extern IntPtr GetDesktopWindow();

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "1", Justification = "Known issue: false positive")]
            [DllImport("user32.dll")]
            public static extern IntPtr RealChildWindowFromPoint(IntPtr hwndParent, POINT ptParentClientCoords);

            [DllImport("user32.dll")]
            public static extern int GetWindowRect(IntPtr hwnd, ref RECT rc);

            [DllImport("oleacc.dll")]
            public static extern IntPtr AccessibleObjectFromPoint(POINT pt, [Out, MarshalAs(UnmanagedType.Interface)] out IAccessible accObj, [Out] out object ChildID);

            /// <summary>
            /// The POINT structure defines the x- and y- coordinates of a point.
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct POINT
            {
                /// <summary>
                /// The x-coordinate of the point.
                /// </summary>
                public int X;

                /// <summary>
                /// The y-coordinate of the point.
                /// </summary>
                public int Y;
            }

            /// <summary>
            /// The RECT structure defines the coordinates of the upper-left and lower-right corners of a rectangle.
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            internal struct RECT
            {
                /// <summary>
                /// The x-coordinate of the upper-left corner of the rectangle.
                /// </summary>
                public int Left;

                /// <summary>
                /// The y-coordinate of the upper-left corner of the rectangle.
                /// </summary>
                public int Top;

                /// <summary>
                /// The x-coordinate of the lower-right corner of the rectangle.
                /// </summary>
                public int Right;

                /// <summary>
                /// The y-coordinate of the lower-right corner of the rectangle.
                /// </summary>
                public int Bottom;
            }
        }
    }
}
