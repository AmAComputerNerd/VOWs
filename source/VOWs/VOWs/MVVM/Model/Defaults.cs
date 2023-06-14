using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Windows;
using System.Windows.Interop;
using VOWs.Events;

namespace VOWs.MVVM.Model
{
    public class Defaults
    {
        /// <summary>
        /// The <c>GetUnscaledPageDimensions</c> method will retrieve the default width and height of a certain
        /// <c>size</c> of page with a given <c>orientation</c>. These default dimensions are what would be shown
        /// with 100% zoom (the default).
        /// </summary>
        /// <param name="size">The size of the page (following standard international printing standard page sizes).</param>
        /// <param name="orientation">The orientation of the page (horizontal or vertical).</param>
        /// <returns>The array of dimensions, in pixels.</returns>
        public static int[] GetUnscaledPageDimensions(string size, string orientation)
        {
            switch(size.ToLower())
            {
                case "a4":
                    if (orientation.ToLower().Equals("horizontal")) return new int[] { 0, 0 };
                    else return new int[] { 0, 0 };
                case "a3":
                    if (orientation.ToLower().Equals("horizontal")) return new int[] { 0, 0 };
                    else return new int[] { 0, 0 };
            }
            return new int[] { 0, 0 };
        }

        /// <summary>
        /// The <c>GetScaledPageDimensions</c> method will retrieve the width and height of a certain <c>size</c>
        /// of page with a given <c>orientation</c>, taking into account the current <c>scaleFactor</c>. This is
        /// calculated with the width and height of the application in combination with the zoom level.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="orientation"></param>
        /// <param name="pageDimensions"></param>
        /// <returns></returns>
        public static bool GetScaledPageDimensions(string size, string orientation, out int[] pageDimensions)
        {
            try
            {
                double[] scaleFactor = WeakReferenceMessenger.Default.Send<RequestScaleFactorMessage>();
                pageDimensions = GetUnscaledPageDimensions(size, orientation);
                for (int i = 0; i < 2; i++)
                {
                    pageDimensions[i] = (int)(pageDimensions[i] * scaleFactor[i]);
                }
                return true;
            } 
            catch (Exception)
            {
                pageDimensions = null;
                return false;
            }
        }
    }
}
