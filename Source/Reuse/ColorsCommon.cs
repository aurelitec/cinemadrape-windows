//-----------------------------------------------------------------------------------------------------------------------
//
// <copyright file="ColorsCommon.cs" company="Aurelitec">
// Copyright (c) 2009-2017 Aurelitec
// http://www.aurelitec.com
// Licensed under the GNU General Public License v3.0. See LICENSE file in the project root for full license information.
// </copyright>
//
// Description: Provides commonly used methods with Color structures and values.
//
//-----------------------------------------------------------------------------------------------------------------------

namespace Aurelitec.Reuse
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Provides commonly used methods with Color structures and values.
    /// </summary>
    public static class ColorsCommon
    {
        /// <summary>
        /// Gets or sets the private Random number generator used to generate random colors.
        /// </summary>
        public static Random RandomGenerator { get; set; }

        // ********************************************************************
        // Random Color Generation
        // ********************************************************************

        /// <summary>
        /// Initializes the Random number generator used to generate random colors.
        /// </summary>
        public static void Randomize()
        {
            ColorsCommon.RandomGenerator = new Random();
        }

        /// <summary>
        /// Generates a random color, using the specified Random number generator.
        /// </summary>
        /// <param name="random">The random number generator to use.</param>
        /// <returns>The randomly generated color.</returns>
        public static Color GetRandomColor(Random random)
        {
            return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }

        /// <summary>
        /// Generates a random color, using the private static Random number generator.
        /// </summary>
        /// <returns>The randomly generated color.</returns>
        public static Color GetRandomColor()
        {
            return ColorsCommon.RandomGenerator != null ? ColorsCommon.GetRandomColor(ColorsCommon.RandomGenerator) : Color.Empty;
        }

        /// <summary>
        /// Generates a random ARGB color, using the specified Random number generator.
        /// </summary>
        /// <param name="random">The random number generator to use.</param>
        /// <returns>The randomly generated ARGB color.</returns>
        public static Color GetRandomArgbColor(Random random)
        {
            return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256), random.Next(256));
        }

        /// <summary>
        /// Generates a random ARGB color, using the private static Random number generator.
        /// </summary>
        /// <returns>The randomly generated ARGB color.</returns>
        public static Color GetRandomArgbColor()
        {
            return ColorsCommon.RandomGenerator != null ? ColorsCommon.GetRandomArgbColor(ColorsCommon.RandomGenerator) : Color.Empty;
        }

        /// <summary>
        /// Ensures that the specified Color value is different from another specified Color value (e.g.
        /// from a TransparencyKey Color value).
        /// </summary>
        /// <param name="color">The Color value that should differ.</param>
        /// <param name="differentFrom">The Color value to be different from.</param>
        /// <returns>The Color value or the next nearest Color value.</returns>
        public static Color EnsureDifferentColor(Color color, Color differentFrom)
        {
            if ((!differentFrom.IsEmpty) && (color == differentFrom))
            {
                color = Color.FromArgb(color.R + 1, color.G + 1, color.B + 1);
            }

            return color;
        }

        /// <summary>
        /// Gets a darker color.
        /// </summary>
        /// <param name="color">The base color.</param>
        /// <param name="factor">The dark factor.</param>
        /// <returns>The darker color.</returns>
        public static Color DarkerColor(Color color, float factor)
        {
            factor = 1 - factor;
            return Color.FromArgb(color.A, (int)(color.R * factor), (int)(color.G * factor), (int)(color.B * factor));
        }

        /// <summary>
        /// Gets a lighter color.
        /// </summary>
        /// <param name="color">The base color.</param>
        /// <param name="factor">The light factor.</param>
        /// <returns>The lighter color.</returns>
        public static Color LighterColor(Color color, float factor)
        {
            return Color.FromArgb(color.A, (int)(((255 - color.R) * factor) + color.R), (int)(((255 - color.G) * factor) + color.G), (int)(((255 - color.B) * factor) + color.B));
        }
    }
}
