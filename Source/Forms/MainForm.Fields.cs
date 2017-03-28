//----------------------------------------------------------------------------------------------------
//
// <copyright file="MainForm.Fields.cs" company="Aurelitec">
// Copyright (c) 2009-2017 Aurelitec
// http://www.aurelitec.com
// Licensed under the GNU General Public License v3.0. See LICENSE file in the project root for full license information.
// </copyright>
//
// Description: The main CinemaDrape drape form - private fields.
//
//----------------------------------------------------------------------------------------------------
namespace CinemaDrape
{
    using System.Drawing;
    using System.Windows.Forms;
    using Aurelitec.Reuse;

    /// <summary>
    /// The main CinemaDrape drape form.
    /// </summary>
    public partial class MainForm
    {
        // ********************************************************************
        // Fields
        // ********************************************************************

        /// <summary>
        /// The color of the highlighted menu items.
        /// </summary>
        private readonly Color brandColor = ColorTranslator.FromHtml("#1BADEA");

        /// <summary>
        /// The color of the radio check bullet for radio menu items.
        /// </summary>
        private readonly Color radioCheckColor = ColorTranslator.FromHtml("#02709D");

        /// <summary>
        /// The quick start form instance.
        /// </summary>
        private QuickStartForm quickStartForm;

        /// <summary>
        /// Indicates whether the focus areas should be restored from the default configuration file.
        /// </summary>
        private bool autoRestoreAreas = true;

        /// <summary>
        /// The current Pause/Resume hot key.
        /// </summary>
        private GlobalHotkey hotKey;

        /// <summary>
        /// The string representation of the current Pause/Resume hot key.
        /// </summary>
        private string hotKeyString;

        /// <summary>
        /// The currently selected focus control who is the owner of the opened Focus Area context menu.
        /// </summary>
        private FocusControl focusControlMenuOwner;

        /// <summary>
        /// Indicates whether the startup phase has ended or not.
        /// </summary>
        private bool afterStartup = false;

        /// <summary>
        /// Stores the value of the current user selected drape opacity.
        /// </summary>
        private double lastUserOpacity;

        /// <summary>
        /// The Opacity Track Bar from the Opacity menu.
        /// </summary>
        private TrackBar menuOpacityTrackBar;

        /// <summary>
        /// Stores the value of the peek through opacity.
        /// </summary>
        private double peekOpacity = 0.8d;

        /// <summary>
        /// Stores the value of the current user selected drape opacity.
        /// </summary>
        private Point newFocusAreaLocation;

        /// <summary>
        /// The new focus area being drawn by the user.
        /// </summary>
        private FocusControl newFocusArea;

        /// <summary>
        /// Indicates whether we are in the transparent mode for easier adding or editing focus areas.
        /// </summary>
        private bool transparentForEditing;

        /// <summary>
        /// Indicates whether we are in the Auto Detect Area Menu mode.
        /// </summary>
        private bool autoDetectAreaMenuMode = false;

        /// <summary>
        /// Indicates whether we are in the Auto Detect Window Menu mode.
        /// </summary>
        private bool autoDetectWindowMenuMode = false;

        /// <summary>
        /// Indicates whether we are at the first pausing of CinemaDrape, to show the notify icon balloon tooltip.
        /// </summary>
        private bool firstPause = true;
    }
}
