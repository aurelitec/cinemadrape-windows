//-----------------------------------------------------------------------------------------------------------------------
//
// <copyright file="MultiMonitorSupport.cs" company="Aurelitec">
// Copyright (c) 2009-2017 Aurelitec
// http://www.aurelitec.com
// Licensed under the GNU General Public License v3.0. See LICENSE file in the project root for full license information.
// </copyright>
//
// Description: Provides methods to easily add multi-monitor support to WinForms applications.
//
//-----------------------------------------------------------------------------------------------------------------------

namespace Aurelitec.Reuse
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Provides methods to easily add multi-monitor support to WinForms applications.
    /// </summary>
    public static class MultiMonitorSupport
    {
        /// <summary>
        /// Gets or sets the radio check image for the multi-monitor radio menu items.
        /// </summary>
        public static Image RadioCheckImage { get; set; }

        /// <summary>
        /// Multi-monitor support - creates a submenu and adds menu items for each screen and one for all screens.
        /// </summary>
        /// <param name="menu">The parent menu where to add the menu items.</param>
        /// <param name="allClick">The event handler for the Click event for the "All Monitors" menu item.</param>
        /// <param name="singleClick">The event handler for the CheckedChanged event for single monitors menu items.</param>
        /// <returns>True if at least one single monitor menu item was added, false otherwise.</returns>
        public static bool AddMonitorMenuItems(ToolStrip menu, EventHandler allClick, EventHandler singleClick)
        {
            // Add the All Monitors sub-menu item
            ToolStripMenuItem item = new ToolStripMenuItem("All Monitors") { Image = MultiMonitorSupport.RadioCheckImage };
            item.Click += MultiMonitorSupport.RadioClick;
            item.Click += allClick;
            menu.Items.Add(item);
            menu.Items.Add(new ToolStripSeparator());

            if (Screen.AllScreens.Length > 1)
            {
                foreach (Screen screen in Screen.AllScreens)
                {
                    item = new ToolStripMenuItem("Only " + screen.DeviceName) { Tag = screen };
                    item.Click += MultiMonitorSupport.RadioClick;
                    if (singleClick != null)
                    {
                        item.Click += singleClick;
                    }

                    menu.Items.Add(item);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the bounds of the screen from its corresponding menu item.
        /// </summary>
        /// <param name="sender">The menu item that corresponds to the screen.</param>
        /// <returns>The bounds of the screen.</returns>
        public static Rectangle ScreenBoundsFromMenuItem(object sender)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                Screen screen = menuItem.Tag as Screen;
                if (screen != null)
                {
                    return screen.Bounds;
                }
            }

            return Rectangle.Empty;
        }

        /// <summary>
        /// Checks to see if a bounds rectangle is on a visible screen (for multi-monitor configurations).
        /// </summary>
        /// <param name="bounds">The bounds rectangle.</param>
        /// <returns>True if the bounds rectangle is on a visible screen, false otherwise.</returns>
        public static bool IsOnAScreen(Rectangle bounds)
        {
            return SystemInformation.VirtualScreen.Contains(bounds);
        }

        /// <summary>
        /// Adds the exclusive radio check functionality to the Click event of the multi monitor support menu items.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Empty event data.</param>
        private static void RadioClick(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                ToolStripMenuItem ownerMenuItem = menuItem.OwnerItem as ToolStripMenuItem;
                if (ownerMenuItem != null)
                {
                    foreach (ToolStripItem subitem in ownerMenuItem.DropDownItems)
                    {
                        ToolStripMenuItem menuSubitem = subitem as ToolStripMenuItem;
                        if (menuSubitem != null)
                        {
                            menuSubitem.Image = menuSubitem == sender ? MultiMonitorSupport.RadioCheckImage : null;
                        }
                    }
                }
            }
        }
    }
}