//-----------------------------------------------------------------------------------------------------------------------
//
// <copyright file="MenuToolStripCustomizer.cs" company="Aurelitec">
// Copyright (c) 2009-2017 Aurelitec
// http://www.aurelitec.com
// Licensed under the GNU General Public License v3.0. See LICENSE file in the project root for full license information.
// </copyright>
//
// Description: Allows customizing ToolStrip Menu objects, applying custom background colors, gradients,
// borders and text colors.
//
//-----------------------------------------------------------------------------------------------------------------------

namespace Aurelitec.Reuse
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Allows customizing ToolStrip Menu objects, applying custom background colors, gradients, borders and text colors.
    /// </summary>
    internal class MenuToolStripCustomizer : ToolStripProfessionalRenderer
    {
        // ***********
        // Constructor
        // ***********

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuToolStripCustomizer"/> class.
        /// </summary>
        /// <param name="backColor">The background color of the menus.</param>
        /// <param name="selectedColor">The background color of selected menus.</param>
        /// <param name="foreColor">The foreground (text) color of the menus.</param>
        /// <param name="addedHeight">The extra height to add to each menu.</param>
        private MenuToolStripCustomizer(Color backColor, Color selectedColor, Color foreColor, int addedHeight)
            : base(new MenuStripColorTable())
        {
            // Initialize the color used for rendering of text
            this.ColorTable.SetColorForGradientMenus(backColor);
            this.ColorTable.SetColorForSelectedMenus(selectedColor);
            this.ColorTable.SetColorForText(foreColor);
            this.AddedHeight = addedHeight;
        }

        // *****************
        // Public Properties
        // *****************

        /// <summary>
        /// Gets the extra height added to a menu item to make it more spaced out and tablet-friendly.
        /// </summary>
        /// <value>The extra height added to a menu item to make it more spaced out and tablet-friendly.</value>
        public int AddedHeight { get; private set; }

        /// <summary>
        /// Gets the color palette used for painting.
        /// </summary>
        /// <value>
        /// The color palette used for painting.
        /// </value>
        public new MenuStripColorTable ColorTable
        {
            get { return base.ColorTable as MenuStripColorTable; }
        }

        // **************
        // Public Methods
        // **************

        /// <summary>
        /// Creates a new instance of the MenuToolStripCustomizer class and uses it to modernize the selected
        /// Drop Down Menus and all their child drop downs.
        /// </summary>
        /// <param name="backColor">The background color of the menus.</param>
        /// <param name="selectedColor">The background color of selected menus.</param>
        /// <param name="foreColor">The foreground (text) color of the menus.</param>
        /// <param name="addedHeight">The extra height to add to each menu.</param>
        /// <param name="opacity">The opacity of the menus.</param>
        /// <param name="menuStrip">A main menu to modernize.</param>
        /// <param name="dropDowns">The list of drop down menus to modernize.</param>
        /// <returns>The MenuToolStripCustomizer object that was used to modernize the menus.</returns>
        public static MenuToolStripCustomizer Modernize(
            Color backColor,
            Color selectedColor,
            Color foreColor,
            int addedHeight,
            double opacity,
            ToolStrip menuStrip,
            params ToolStripDropDown[] dropDowns)
        {
            MenuToolStripCustomizer customizer = new MenuToolStripCustomizer(backColor, selectedColor, foreColor, addedHeight);

            // Use a stack to navigate through all-level children dropdowns
            Stack<ToolStripDropDown> dropDownsStack = new Stack<ToolStripDropDown>();
            foreach (ToolStripDropDown dropDown in dropDowns)
            {
                dropDownsStack.Push(dropDown);
            }

            if (menuStrip != null)
            {
                menuStrip.Renderer = customizer;
                if (addedHeight > 0)
                {
                    menuStrip.AutoSize = false;
                    menuStrip.Height += addedHeight;
                }

                foreach (ToolStripItem item in menuStrip.Items)
                {
                    ToolStripMenuItem menuItem = item as ToolStripMenuItem;
                    if (menuItem != null)
                    {
                        dropDownsStack.Push(menuItem.DropDown);
                    }
                }
            }

            while (dropDownsStack.Count > 0)
            {
                ToolStripDropDown currentDropDown = dropDownsStack.Pop();

                currentDropDown.Renderer = customizer;

                if (opacity < 1)
                {
                    currentDropDown.AllowTransparency = true;
                    currentDropDown.Opacity = opacity;
                }

                foreach (ToolStripItem item in currentDropDown.Items)
                {
                    if (addedHeight > 0)
                    {
                        item.AutoSize = false;
                        item.Height += addedHeight;
                    }

                    ToolStripMenuItem menuItem = item as ToolStripMenuItem;
                    if (menuItem != null)
                    {
                        if (menuItem.HasDropDownItems)
                        {
                            dropDownsStack.Push(menuItem.DropDown);
                        }
                    }
                }
            }

            return customizer;
        }

        // *****************
        // Painting Override
        // *****************

        /// <summary>
        /// Overrides the rendering of text in ToolStripItem objects to allow customizing the text color.
        /// </summary>
        /// <param name="e">A ToolStripItemTextRenderEventArgs that contains the event data.</param>
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            // Select the appropriate text color
            if ((e != null) && (e.Item != null))
            {
                e.TextColor = e.Item.ForeColor != SystemColors.ControlText ? e.Item.ForeColor :
                    e.Item.Selected ? this.ColorTable.ColorOfSelectedText : this.ColorTable.ColorOfNormalText;

                if (this.AddedHeight > 0)
                {
                    int addedY = (e.Item.GetCurrentParent() is MenuStrip) ? 0 : this.AddedHeight / 2;
                    e.TextRectangle = new Rectangle(
                        e.TextRectangle.X,
                        e.TextRectangle.Y + addedY,
                        e.TextRectangle.Width,
                        e.TextRectangle.Height);
                }
            }

            // Call the base rendering method (very important)
            base.OnRenderItemText(e);
        }

        /// <summary>
        /// Overrides the rendering of the sub-items arrow in ToolStripItem objects with the correct text color.
        /// </summary>
        /// <param name="e">A ToolStripArrowRenderEventArgs that contains the event data.</param>
        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            // Select the appropriate arrow color
            if ((e != null) && (e.Item != null) && e.Item.Enabled)
            {
                e.ArrowColor = e.Item.Selected ? this.ColorTable.ColorOfSelectedText : this.ColorTable.ColorOfNormalText;
            }

            // Call the base rendering method (very important)
            base.OnRenderArrow(e);
        }
    }

    /// <summary>
    /// Provides colors for customizing ToolStrip Menu objects using a CustomMenuToolStripProfessionalRenderer object.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Avoid having to include two different C# source files to each project for this functionality.")]
    internal class MenuStripColorTable : ProfessionalColorTable
    {
        // ***********
        // Constructor
        // ***********

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuStripColorTable"/> class.
        /// </summary>
        public MenuStripColorTable()
            : base()
        {
            this.ColorOfButtonSelectedBorder = base.ButtonSelectedBorder;
            this.ColorOfButtonSelectedHighlightBorder = base.ButtonSelectedHighlightBorder;

            this.ColorOfCheckBackground = base.CheckBackground;
            this.ColorOfCheckSelectedBackground = base.CheckSelectedBackground;
            this.ColorOfCheckPressedBackground = base.CheckPressedBackground;

            this.ColorOfImageMarginGradientBegin = base.ImageMarginGradientBegin;
            this.ColorOfImageMarginGradientEnd = base.ImageMarginGradientEnd;
            this.ColorOfImageMarginGradientMiddle = base.ImageMarginGradientMiddle;

            this.ColorOfMenuBorder = base.MenuBorder;
            this.ColorOfMenuItemBorder = base.MenuItemBorder;
            this.ColorOfMenuItemSelected = base.MenuItemSelected;
            this.ColorOfMenuItemSelectedGradientBegin = base.MenuItemSelectedGradientBegin;
            this.ColorOfMenuItemSelectedGradientEnd = base.MenuItemSelectedGradientEnd;
            this.ColorOfMenuItemPressedGradientBegin = base.MenuItemPressedGradientBegin;
            this.ColorOfMenuItemPressedGradientEnd = base.MenuItemPressedGradientEnd;
            this.ColorOfMenuItemPressedGradientMiddle = base.MenuItemPressedGradientMiddle;
            this.ColorOfMenuStripGradientBegin = base.MenuStripGradientBegin;
            this.ColorOfMenuStripGradientEnd = base.MenuStripGradientEnd;

            this.ColorOfSeparatorDark = base.SeparatorDark;
            this.ColorOfToolStripDropDownBackground = base.ToolStripDropDownBackground;
        }

        // **********************************************************************
        // Public Colors Properties to Override ProfessionalColorTable Properties
        // **********************************************************************

        /// <summary>
        /// Gets or sets the border color to use with the ButtonSelectedGradientBegin, ButtonSelectedGradientMiddle, and ButtonSelectedGradientEnd colors.
        /// <para>Used by: Menu Item (Selected)</para>
        /// </summary>
        /// <value>
        /// The border color to use with the ButtonSelectedGradientBegin, ButtonSelectedGradientMiddle, and ButtonSelectedGradientEnd colors.
        /// <para>Used by: Menu Item (Selected)</para>
        /// </value>
        public Color ColorOfButtonSelectedBorder { get; set; }

        /// <summary>
        /// Gets or sets the Gets the border color to use with ButtonSelectedHighlight.
        /// <para>Used by: Menu Item (TextBoxes inside)</para>
        /// </summary>
        /// <value>
        /// The Gets the border color to use with ButtonSelectedHighlight.
        /// <para>Used by: Menu Item (TextBoxes inside)</para>
        /// </value>
        public Color ColorOfButtonSelectedHighlightBorder { get; set; }

        /// <summary>
        /// Gets or sets the solid color to use when the button is checked and gradients are being used.
        /// <para>Used by: Menu Item (Left Check Box)</para>
        /// </summary>
        /// <value>
        /// The solid color to use when the button is checked and gradients are being used.
        /// <para>Used by: Menu Item (Left Check Box)</para>
        /// </value>
        public Color ColorOfCheckBackground { get; set; }

        /// <summary>
        /// Gets or sets the solid color to use when the button is checked and selected and gradients are being used.
        /// <para>Used by: Menu Item (Left Check Box)</para>
        /// </summary>
        /// <value>
        /// The solid color to use when the button is checked and selected and gradients are being used.
        /// <para>Used by: Menu Item (Left Check Box)</para>
        /// </value>
        public Color ColorOfCheckPressedBackground { get; set; }

        /// <summary>
        /// Gets or sets the solid color to use when the button is checked and selected and gradients are being used.
        /// <para>Used by: Menu Item (Left Check Box)</para>
        /// </summary>
        /// <value>
        /// The solid color to use when the button is checked and selected and gradients are being used.
        /// <para>Used by: Menu Item (Left Check Box)</para>
        /// </value>
        public Color ColorOfCheckSelectedBackground { get; set; }

        /// <summary>
        /// Gets or sets the starting color of the gradient used in the image margin of a ToolStripDropDownMenu.
        /// <para>Used by: Menu Item (Left Image)</para>
        /// </summary>
        /// <value>
        /// The starting color of the gradient used in the image margin of a ToolStripDropDownMenu.
        /// <para>Used by: Menu Item (Left Image)</para>
        /// </value>
        public Color ColorOfImageMarginGradientBegin { get; set; }

        /// <summary>
        /// Gets or sets the end color of the gradient used in the image margin of a ToolStripDropDownMenu.
        /// <para>Used by: Menu Item (Left Image)</para>
        /// </summary>
        /// <value>
        /// The end color of the gradient used in the image margin of a ToolStripDropDownMenu.
        /// <para>Used by: Menu Item (Left Image)</para>
        /// </value>
        public Color ColorOfImageMarginGradientEnd { get; set; }

        /// <summary>
        /// Gets or sets the middle color of the gradient used in the image margin of a ToolStripDropDownMenu.
        /// <para>Used by: Menu Item (Left Image)</para>
        /// </summary>
        /// <value>
        /// The middle color of the gradient used in the image margin of a ToolStripDropDownMenu.
        /// <para>Used by: Menu Item (Left Image)</para>
        /// </value>
        public Color ColorOfImageMarginGradientMiddle { get; set; }

        /// <summary>
        /// Gets or sets the color that is the border color to use on a MenuStrip.
        /// <para>Used by: Menu Drop Down</para>
        /// </summary>
        /// <value>
        /// The color that is the border color to use on a MenuStrip.
        /// <para>Used by: Menu Drop Down</para>
        /// </value>
        public Color ColorOfMenuBorder { get; set; }

        /// <summary>
        /// Gets or sets the border color to use with a ToolStripMenuItem.
        /// <para>Used by: Menu Item</para>
        /// </summary>
        /// <value>
        /// The border color to use with a ToolStripMenuItem.
        /// <para>Used by: Menu Item</para>
        /// </value>
        public Color ColorOfMenuItemBorder { get; set; }

        /// <summary>
        /// Gets or sets the solid color to use when a ToolStripMenuItem other than the top-level ToolStripMenuItem is selected.
        /// <para>Used by: Menu Item (Selected)</para>
        /// </summary>
        /// <value>
        /// The solid color to use when a ToolStripMenuItem other than the top-level ToolStripMenuItem is selected.
        /// <para>Used by: Menu Item (Selected)</para>
        /// </value>
        public Color ColorOfMenuItemSelected { get; set; }

        /// <summary>
        /// Gets or sets the starting color of the gradient used when the ToolStripMenuItem is selected.
        /// <para>Used by: Menu Item (Selected)</para>
        /// </summary>
        /// <value>
        /// The starting color of the gradient used when the ToolStripMenuItem is selected.
        /// <para>Used by: Menu Item (Selected)</para>
        /// </value>
        public Color ColorOfMenuItemSelectedGradientBegin { get; set; }

        /// <summary>
        /// Gets or sets the end color of the gradient used when the ToolStripMenuItem is selected.
        /// <para>Used by: Menu Item (Selected)</para>
        /// </summary>
        /// <value>
        /// The end color of the gradient used when the ToolStripMenuItem is selected.
        /// <para>Used by: Menu Item (Selected)</para>
        /// </value>
        public Color ColorOfMenuItemSelectedGradientEnd { get; set; }

        /// <summary>
        /// Gets or sets the starting color of the gradient used when a top-level ToolStripMenuItem is pressed.
        /// <para>Used by: Menu Item (Selected)</para>
        /// </summary>
        /// <value>
        /// The starting color of the gradient used when a top-level ToolStripMenuItem is pressed.
        /// <para>Used by: Menu Item (Selected)</para>
        /// </value>
        public Color ColorOfMenuItemPressedGradientBegin { get; set; }

        /// <summary>
        /// Gets or sets the end color of the gradient used when a top-level ToolStripMenuItem is pressed.
        /// <para>Used by: Menu Item (Selected)</para>
        /// </summary>
        /// <value>
        /// The end color of the gradient used when a top-level ToolStripMenuItem is pressed.
        /// <para>Used by: Menu Item (Selected)</para>
        /// </value>
        public Color ColorOfMenuItemPressedGradientEnd { get; set; }

        /// <summary>
        /// Gets or sets the middle color of the gradient used when a top-level ToolStripMenuItem is pressed.
        /// <para>Used by: Menu Item (Selected)</para>
        /// </summary>
        /// <value>
        /// The middle color of the gradient used when a top-level ToolStripMenuItem is pressed.
        /// <para>Used by: Menu Item (Selected)</para>
        /// </value>
        public Color ColorOfMenuItemPressedGradientMiddle { get; set; }

        /// <summary>
        /// Gets or sets the starting color of the gradient used in the MenuStrip.
        /// <para>Used by: Top Menu Bar</para>
        /// </summary>
        /// <value>
        /// The starting color of the gradient used in the MenuStrip.
        /// <para>Used by: Top Menu Bar</para>
        /// </value>
        public Color ColorOfMenuStripGradientBegin { get; set; }

        /// <summary>
        /// Gets or sets the end color of the gradient used in the MenuStrip.
        /// <para>Used by: Top Menu Bar</para>
        /// </summary>
        /// <value>
        /// The end color of the gradient used in the MenuStrip.
        /// <para>Used by: Top Menu Bar</para>
        /// </value>
        public Color ColorOfMenuStripGradientEnd { get; set; }

        /// <summary>
        /// Gets or sets the color to use to for shadow effects on the ToolStripSeparator.
        /// <para>Used by: Menu Item (Separator)</para>
        /// </summary>
        /// <value>
        /// The color to use to for shadow effects on the ToolStripSeparator.
        /// <para>Used by: Menu Item (Separator)</para>
        /// </value>
        public Color ColorOfSeparatorDark { get; set; }

        /// <summary>
        /// Gets or sets the solid background color of the ToolStripDropDown.
        /// <para>Used by: Menu Item</para>
        /// </summary>
        /// <value>
        /// The solid background color of the ToolStripDropDown.
        /// <para>Used by: Menu Item</para>
        /// </value>
        public Color ColorOfToolStripDropDownBackground { get; set; }

        // *********************************************************
        // Additional Public Color Properties For Text Customization
        // *********************************************************

        /// <summary>
        /// Gets or sets the color to use as the foreground color for normal text in all objects.
        /// <para>Used by: All Menu Objects</para>
        /// </summary>
        /// <value>
        /// The color to use as the foreground color for normal text in all objects.
        /// <para>Used by: All Menu Objects</para>
        /// </value>
        public Color ColorOfNormalText { get; set; }

        /// <summary>
        /// Gets or sets the color to use as the foreground color for selected text in all objects.
        /// <para>Used by: All Menu Objects</para>
        /// </summary>
        /// <value>
        /// The color to use as the foreground color for selected text in all objects.
        /// <para>Used by: All Menu Objects</para>
        /// </value>
        public Color ColorOfSelectedText { get; set; }

        // *********
        // Overrides
        // *********

        /// <summary>
        /// Gets the border color to use with the ButtonSelectedGradientBegin, ButtonSelectedGradientMiddle, and
        /// ButtonSelectedGradientEnd colors.
        /// </summary>
        /// <value>
        /// The border color to use with the ButtonSelectedGradientBegin, ButtonSelectedGradientMiddle, and
        /// ButtonSelectedGradientEnd colors.
        /// </value>
        public override Color ButtonSelectedBorder
        {
            get { return this.ColorOfButtonSelectedBorder; }
        }

        /// <summary>
        /// Gets the border color to use with ButtonSelectedHighlight.
        /// </summary>
        /// <value>
        /// The border color to use with ButtonSelectedHighlight.
        /// </value>
        public override Color ButtonSelectedHighlightBorder
        {
            get { return this.ColorOfButtonSelectedHighlightBorder; }
        }

        /// <summary>
        /// Gets the solid color to use when the button is checked and gradients are being used.
        /// </summary>
        /// <value>
        /// The solid color to use when the button is checked and gradients are being used.
        /// </value>
        public override Color CheckBackground
        {
            get { return this.ColorOfCheckBackground; }
        }

        /// <summary>
        /// Gets the solid color to use when the button is checked and selected and gradients are being used.
        /// </summary>
        /// <value>
        /// The solid color to use when the button is checked and selected and gradients are being used.
        /// </value>
        public override Color CheckSelectedBackground
        {
            get { return this.ColorOfCheckSelectedBackground; }
        }

        /// <summary>
        /// Gets the solid color to use when the button is checked and selected and gradients are being used.
        /// </summary>
        /// <value>
        /// The solid color to use when the button is checked and selected and gradients are being used.
        /// </value>
        public override Color CheckPressedBackground
        {
            get { return this.ColorOfCheckPressedBackground; }
        }

        /// <summary>
        /// Gets the starting color of the gradient used in the image margin of a ToolStripDropDownMenu.
        /// </summary>
        /// <value>
        /// The starting color of the gradient used in the image margin of a ToolStripDropDownMenu.
        /// </value>
        public override Color ImageMarginGradientBegin
        {
            get { return this.ColorOfImageMarginGradientBegin; }
        }

        /// <summary>
        /// Gets the end color of the gradient used in the image margin of a ToolStripDropDownMenu.
        /// </summary>
        /// <value>
        /// The end color of the gradient used in the image margin of a ToolStripDropDownMenu.
        /// </value>
        public override Color ImageMarginGradientEnd
        {
            get { return this.ColorOfImageMarginGradientEnd; }
        }

        /// <summary>
        /// Gets the middle color of the gradient used in the image margin of a ToolStripDropDownMenu.
        /// </summary>
        /// <value>
        /// The middle color of the gradient used in the image margin of a ToolStripDropDownMenu.
        /// </value>
        public override Color ImageMarginGradientMiddle
        {
            get { return this.ColorOfImageMarginGradientMiddle; }
        }

        /// <summary>
        /// Gets the color that is the border color to use on a MenuStrip.
        /// </summary>
        /// <value>
        /// The color that is the border color to use on a MenuStrip.
        /// </value>
        public override Color MenuBorder
        {
            get { return this.ColorOfMenuBorder; }
        }

        /// <summary>
        /// Gets the border color to use with a ToolStripMenuItem.
        /// </summary>
        /// <value>
        /// The border color to use with a ToolStripMenuItem.
        /// </value>
        public override Color MenuItemBorder
        {
            get { return this.ColorOfMenuItemBorder; }
        }

        /// <summary>
        /// Gets the solid color to use when a ToolStripMenuItem other than the top-level ToolStripMenuItem
        /// is selected.
        /// </summary>
        /// <value>
        /// The solid color to use when a ToolStripMenuItem other than the top-level ToolStripMenuItem
        /// is selected.
        /// </value>
        public override Color MenuItemSelected
        {
            get { return this.ColorOfMenuItemSelected; }
        }

        /// <summary>
        /// Gets the starting color of the gradient used when the ToolStripMenuItem is selected.
        /// </summary>
        /// <value>
        /// The starting color of the gradient used when the ToolStripMenuItem is selected.
        /// </value>
        public override Color MenuItemSelectedGradientBegin
        {
            get { return this.ColorOfMenuItemSelectedGradientBegin; }
        }

        /// <summary>
        /// Gets the end color of the gradient used when the ToolStripMenuItem is selected.
        /// </summary>
        /// <value>
        /// The end color of the gradient used when the ToolStripMenuItem is selected.
        /// </value>
        public override Color MenuItemSelectedGradientEnd
        {
            get { return this.ColorOfMenuItemSelectedGradientEnd; }
        }

        /// <summary>
        /// Gets the starting color of the gradient used when a top-level ToolStripMenuItem is pressed.
        /// </summary>
        /// <value>
        /// The starting color of the gradient used when a top-level ToolStripMenuItem is pressed.
        /// </value>
        public override Color MenuItemPressedGradientBegin
        {
            get { return this.ColorOfMenuItemPressedGradientBegin; }
        }

        /// <summary>
        /// Gets the end color of the gradient used when a top-level ToolStripMenuItem is pressed.
        /// </summary>
        /// <value>
        /// The end color of the gradient used when a top-level ToolStripMenuItem is pressed.
        /// </value>
        public override Color MenuItemPressedGradientEnd
        {
            get { return this.ColorOfMenuItemPressedGradientEnd; }
        }

        /// <summary>
        /// Gets the middle color of the gradient used when a top-level ToolStripMenuItem is pressed.
        /// </summary>
        /// <value>
        /// The middle color of the gradient used when a top-level ToolStripMenuItem is pressed.
        /// </value>
        public override Color MenuItemPressedGradientMiddle
        {
            get { return this.ColorOfMenuItemPressedGradientMiddle; }
        }

        /// <summary>
        /// Gets the starting color of the gradient used in the MenuStrip.
        /// </summary>
        /// <value>
        /// The starting color of the gradient used in the MenuStrip.
        /// </value>
        public override Color MenuStripGradientBegin
        {
            get { return this.ColorOfMenuStripGradientBegin; }
        }

        /// <summary>
        /// Gets the end color of the gradient used in the MenuStrip.
        /// </summary>
        /// <value>
        /// The end color of the gradient used in the MenuStrip.
        /// </value>
        public override Color MenuStripGradientEnd
        {
            get { return this.ColorOfMenuStripGradientEnd; }
        }

        /// <summary>
        /// Gets the color to use to for shadow effects on the ToolStripSeparator.
        /// </summary>
        /// <value>
        /// The color to use to for shadow effects on the ToolStripSeparator.
        /// </value>
        public override Color SeparatorDark
        {
            get { return this.ColorOfSeparatorDark; }
        }

        /// <summary>
        /// Gets the solid background color of the ToolStripDropDown.
        /// </summary>
        /// <value>The solid background color of the ToolStripDropDown.</value>
        public override Color ToolStripDropDownBackground
        {
            get { return this.ColorOfToolStripDropDownBackground; }
        }

        // ****************************
        // Common Colors Public Methods
        // ****************************

        /// <summary>
        /// Sets the color to use as the background for all menu objects when gradients are being used.
        /// </summary>
        /// <param name="value">A color value.</param>
        public void SetColorForGradientMenus(Color value)
        {
            this.ColorOfMenuStripGradientBegin =
            this.ColorOfMenuStripGradientEnd =
            this.ColorOfImageMarginGradientBegin =
            this.ColorOfImageMarginGradientMiddle =
            this.ColorOfImageMarginGradientEnd = value;

            this.ColorOfToolStripDropDownBackground = value;
            bool isDarkColor = value.GetBrightness() < 0.5;
            this.SetColorForSelectedMenus(isDarkColor ? ControlPaint.Light(value, 0.3f) : ControlPaint.Dark(value, 0.3f));
            this.ColorOfMenuBorder = this.ColorOfSeparatorDark =
                isDarkColor ? ControlPaint.Light(value, 0.9f) : ControlPaint.Dark(value, 0.9f);
            this.SetColorForText(this.ColorOfMenuBorder);
            this.SetColorForMenuCheckBoxes(ControlPaint.LightLight(this.ColorOfMenuItemSelected));
        }

        /// <summary>
        /// Sets the color to use as the background for all menu objects when they are selected.
        /// </summary>
        /// <param name="value">A color value.</param>
        public void SetColorForSelectedMenus(Color value)
        {
            this.ColorOfMenuItemPressedGradientBegin = this.ColorOfMenuItemPressedGradientMiddle =
            this.ColorOfMenuItemPressedGradientEnd = this.ColorOfMenuItemSelected =
            this.ColorOfMenuItemSelectedGradientBegin = this.ColorOfMenuItemSelectedGradientEnd = value;
        }

        /// <summary>
        /// Sets the color to use as the foreground color for text in all objects.
        /// </summary>
        /// <param name="value">A color value.</param>
        public void SetColorForText(Color value)
        {
            this.ColorOfNormalText = this.ColorOfSelectedText = this.ColorOfMenuItemBorder =
            this.ColorOfButtonSelectedHighlightBorder = value;
        }

        /// <summary>
        /// Sets the color to use as the background for all check boxes inside menu objects.
        /// </summary>
        /// <param name="value">A color value.</param>
        public void SetColorForMenuCheckBoxes(Color value)
        {
            this.ColorOfCheckBackground = this.ColorOfCheckPressedBackground = this.ColorOfCheckSelectedBackground = value;
            this.ColorOfButtonSelectedBorder = ControlPaint.Dark(value);
        }
    }
}
