/*
 * Copyright (C) 2017 JP Dillingham (jp@dillingham.ws)
 * The MIT License (MIT)
 */

using System;

namespace FileHistorian
{
    /// <summary>
    ///     Miscellaneous utility methods.
    /// </summary>
    internal static class Utility
    {
        #region Internal Methods

        /// <summary>
        ///     Determines whether the current platform is Windows using Environment.OSVersion.Platform.
        /// </summary>
        /// <returns>A value indicating whether the current platform is Windows.</returns>
        internal static bool IsWindows()
        {
            int p = (int)Environment.OSVersion.Platform;
            return !((p == 4) || (p == 6) || (p == 128));
        }

        /// <summary>
        ///     Installs or uninstalls the Windows Service using the included ProjectInstaller.
        /// </summary>
        /// <param name="action">If 'uninstall', uninstalls the service. Any other string installs the service.</param>
        internal static void ModifyService(string action)
        {
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string[] args;

            if (action == "uninstall")
            {
                args = new string[] { "/u", path };
            }
            else
            {
                args = new string[] { path };
            }

            System.Configuration.Install.ManagedInstallerClass.InstallHelper(args);
        }

        /// <summary>
        ///     Truncates the given string to the specified length.
        /// </summary>
        /// <param name="value">The string to truncate.</param>
        /// <param name="maxLength">The length to which the string is to be truncated.</param>
        /// <returns>The truncated string.</returns>
        internal static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        #endregion Internal Methods
    }
}