//-----------------------------------------------------------------------------------------------------------------------
//
// <copyright file="MainForm.Conf.cs" company="Aurelitec">
// Copyright (c) 2009-2017 Aurelitec
// http://www.aurelitec.com
// Licensed under the GNU General Public License v3.0. See LICENSE file in the project root for full license information.
// </copyright>
//
// Description: The main CinemaDrape drape form - routines for saving and loading the configuration.
//
//-----------------------------------------------------------------------------------------------------------------------

namespace CinemaDrape
{
    using System;
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
        /// Saves the drape color, opacity, and the current focus areas to a CinemaDrape configuration file.
        /// </summary>
        /// <param name="filePath">The location of the configuration file.</param>
        /// <param name="saveAreas">True to save the current focus areas, false otherwise.</param>
        private void SaveLayout(string filePath, bool saveAreas)
        {
            VerySimpleIni iniFile = string.IsNullOrEmpty(filePath) ?
                new VerySimpleIni(Properties.Resources.StringDefaultSettingsFile, Application.ExecutablePath, Application.CompanyName, Application.ProductName, true) :
                new VerySimpleIni(filePath, true);

            if (iniFile.IsReady)
            {
                iniFile.SetValue(Properties.Resources.StringSettingsDrapeColor, ColorTranslator.ToHtml(this.BackColor));
                int intlastUserOpacity = (int)(this.lastUserOpacity * 100);
                iniFile.SetValue(Properties.Resources.StringSettingsDrapeOpacity, intlastUserOpacity.ToString(CultureInfo.InvariantCulture));
                iniFile.SetValue(Properties.Resources.StringSettingsHotkey, this.hotKeyString);
                int intPeekOpacity = (int)(this.peekOpacity * 100);
                iniFile.SetValue(Properties.Resources.StringSettingsPeekOpacity, intPeekOpacity.ToString(CultureInfo.InvariantCulture));
                iniFile.SetValue(Properties.Resources.StringSettingsAutoRestoreAreas, this.autoRestoreAreas.ToString());

                if (saveAreas)
                {
                    FocusControl.ForEach(
                        this,
                        (control) =>
                        {
                            iniFile.SetValue(Properties.Resources.StringSettingsFocusArea, new RectangleConverter().ConvertToInvariantString(control.RealBounds));
                        });
                }

                iniFile.SetValue(Properties.Resources.StringSettingsShowQuickStart, this.quickStartForm.ShowAtStartupCheckBox.Checked.ToString());
                iniFile.SetValue(
                    Properties.Resources.StringSettingsQuickStartLocation,
                    new PointConverter().ConvertToInvariantString(this.quickStartForm.Location));

                try
                {
                    iniFile.Save();
                }
                catch
                {
                    // Ignore configuration save errors
                }
            }
        }

        /// <summary>
        /// Converts a opacity value from integer to double.
        /// </summary>
        /// <param name="value">The integer value to convert.</param>
        /// <returns>The double opacity value.</returns>
        private double ConvertOpacity(int value)
        {
            value = Math.Min(100, Math.Max(1, value));
            return (double)value / 100;
        }

        /// <summary>
        /// Loads the drape color, opacity, and focus areas from a CinemaDrape configuration file.
        /// </summary>
        /// <param name="filePath">The location of the configuration file.</param>
        /// <param name="loadAreas">True to load the focus areas, false otherwise.</param>
        private void LoadLayout(string filePath, bool loadAreas)
        {
            VerySimpleIni iniFile = string.IsNullOrEmpty(filePath) ?
                new VerySimpleIni(Properties.Resources.StringDefaultSettingsFile, Application.ExecutablePath, Application.CompanyName, Application.ProductName, false) :
                new VerySimpleIni(filePath, true);

            if (iniFile.Load())
            {
                FromString.IfHtmlColor(iniFile.GetValue(Properties.Resources.StringSettingsDrapeColor), this.SetDrapeColor, null);

                // Read the drape opacity as int (and support the legacy double format)
                string drapeOpacityStr = iniFile.GetValue(Properties.Resources.StringSettingsDrapeOpacity);
                FromString.IfInt(
                    drapeOpacityStr,
                    (value) => { this.SetDrapeOpacity(this.ConvertOpacity(value)); },
                    (error) => { FromString.IfDouble(drapeOpacityStr, this.SetDrapeOpacity, null); });

                string peekOpacityStr = iniFile.GetValue(Properties.Resources.StringSettingsPeekOpacity);
                FromString.IfInt(
                    peekOpacityStr,
                    (value) => { this.peekOpacity = this.ConvertOpacity(value); },
                    (error) => { FromString.IfDouble(peekOpacityStr, (value) => { this.peekOpacity = value; }, null); });

                this.hotKeyString = iniFile.GetValue(Properties.Resources.StringSettingsHotkey);

                FromString.IfBool(iniFile.GetValue(Properties.Resources.StringSettingsAutoRestoreAreas), value => { this.autoRestoreAreas = value; }, null);

                if (loadAreas || this.autoRestoreAreas)
                {
                    this.DeleteAllFocusAreas();

                    string focusRectString;
                    while (!string.IsNullOrEmpty(focusRectString = iniFile.GetValue(Properties.Resources.StringSettingsFocusArea)))
                    {
                        FromString.IfRectangle(focusRectString, this.AddNewFocusArea, null);
                    }
                }

                FromString.IfBool(iniFile.GetValue(Properties.Resources.StringSettingsShowQuickStart), value => { this.quickStartForm.ShowAtStartupCheckBox.Checked = value; }, null);
                FromString.IfPoint(iniFile.GetValue(Properties.Resources.StringSettingsQuickStartLocation), value => { this.quickStartForm.Location = value; }, null);
            }
        }
    }
}
