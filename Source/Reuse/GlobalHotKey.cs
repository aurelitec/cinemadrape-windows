//-----------------------------------------------------------------------------------------------------------------------
//
// <copyright file="GlobalHotKey.cs" company="Aurelitec">
// Copyright (c) 2009-2017 Aurelitec
// http://www.aurelitec.com
// Licensed under the GNU General Public License v3.0. See LICENSE file in the project root for full license information.
// </copyright>
//
// Description: Registers a global hot (shortcut) key.
//
//-----------------------------------------------------------------------------------------------------------------------

namespace Aurelitec.Reuse
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    /// Provides routines for registering a global hot (shortcut) key.
    /// </summary>
    public class GlobalHotkey
    {
        // ********************************************************************
        // Fields
        // ********************************************************************

        /// <summary>
        /// The Windows hotkey message
        /// </summary>
        public const int HotkeyWindowsMessage = 0x0312;

        /// <summary>
        /// A counter of GlobalHotkey instance creation, for assigning to the id field.
        /// </summary>
        private static int idCounter = 0;

        /// <summary>
        /// The modifiers of the hot key (CTRL, ALT, SHIFT).
        /// </summary>
        private int modifiers;

        /// <summary>
        /// The integer value of the hot key.
        /// </summary>
        private int key;

        /// <summary>
        /// The handle of the owner of the hot key, that receives the message.
        /// </summary>
        private IntPtr handle;

        /// <summary>
        /// The id of the global hot key. Ids start at 0 and incremented with each GlobalHotkey instance creation.
        /// </summary>
        private int id;

        // ********************************************************************
        // Constructors
        // ********************************************************************

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalHotkey"/> class.
        /// </summary>
        /// <param name="modifiers">The modifiers of the hot key.</param>
        /// <param name="key">The hot key value.</param>
        /// <param name="theHandle">The handle of the owner of the hot key, that receives the message.</param>
        public GlobalHotkey(int modifiers, Keys key, IntPtr theHandle)
        {
            this.InitFields(modifiers, key, theHandle);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalHotkey"/> class.
        /// </summary>
        /// <param name="keyText">The string representation of the key.</param>
        /// <param name="theHandle">The handle of the owner of the hot key, that receives the message.</param>
        public GlobalHotkey(string keyText, IntPtr theHandle)
        {
            Keys keys;

            // Convert the key string to a Keys enum value
            try
            {
                keys = (Keys)new KeysConverter().ConvertFromString(keyText);
            }
            catch
            {
                return;
            }

            // Compute the modifiers from the key data
            int modifiers = 0;
            if ((keys & Keys.Control) == Keys.Control)
            {
                modifiers |= (int)GlobalHotkey.Modifiers.Control;
            }

            if ((keys & Keys.Alt) == Keys.Alt)
            {
                modifiers |= (int)GlobalHotkey.Modifiers.Alt;
            }

            if ((keys & Keys.Shift) == Keys.Shift)
            {
                modifiers |= (int)GlobalHotkey.Modifiers.Shift;
            }

            // Remove the modifiers from the key data
            keys = keys & ~Keys.Control;
            keys = keys & ~Keys.Alt;
            keys = keys & ~Keys.Shift;

            this.InitFields(modifiers, keys, theHandle);
        }

        // ********************************************************************
        // Enums
        // ********************************************************************

        /// <summary>
        /// Key modifiers.
        /// </summary>
        [FlagsAttribute]
        public enum Modifiers
        {
            /// <summary>
            /// No modifier.
            /// </summary>
            None = 0x0000,

            /// <summary>
            /// The Alt key modifier.
            /// </summary>
            Alt = 0x0001,

            /// <summary>
            /// The Control key modifier.
            /// </summary>
            Control = 0x0002,

            /// <summary>
            /// The Shift key modifier.
            /// </summary>
            Shift = 0x0004,

            /// <summary>
            /// The Windows key modifier.
            /// </summary>
            Windows = 0x0008
        }

        // ********************************************************************
        // Static Conversion Methods
        // ********************************************************************

        /// <summary>
        /// Converts a Keys enumeration to string.
        /// </summary>
        /// <param name="keys">The Keys enumeration to convert.</param>
        /// <returns>The string representation of the Keys enumeration.</returns>
        public static string KeysToString(Keys keys)
        {
            return new KeysConverter().ConvertToString(keys);
        }

        // ********************************************************************
        // Register/Unregister
        // ********************************************************************

        /// <summary>
        /// Registers the global hot key.
        /// </summary>
        /// <returns>True on success, false otherwise.</returns>
        public bool Register()
        {
            bool result = false;
            if (this.key != 0)
            {
               result = NativeMethods.RegisterHotKey(this.handle, this.id, this.modifiers, this.key);
            }

            return result;
        }

        /// <summary>
        /// Unregisters the global hot key.
        /// </summary>
        /// <returns>True on success, false otherwise.</returns>
        public bool Unregister()
        {
            return NativeMethods.UnregisterHotKey(this.handle, this.id);
        }

        // ********************************************************************
        // Helper Methods
        // ********************************************************************

        /// <summary>
        /// Initializes the fields of the global hot key.
        /// </summary>
        /// <param name="modifiers">The modifiers of the hot key.</param>
        /// <param name="key">The hot key value.</param>
        /// <param name="theHandle">The handle of the owner of the hot key, that should receive the message.</param>
        private void InitFields(int modifiers, Keys key, IntPtr theHandle)
        {
            this.modifiers = modifiers;
            this.key = (int)key;
            this.handle = theHandle;

            // Just number your hot keys, start at 0.
            this.id = GlobalHotkey.idCounter;
            GlobalHotkey.idCounter++;
        }

        // ********************************************************************
        // Native Methods
        // ********************************************************************

        /// <summary>
        /// Native Windows API method declarations.
        /// </summary>
        private static class NativeMethods
        {
            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        }
    }
}