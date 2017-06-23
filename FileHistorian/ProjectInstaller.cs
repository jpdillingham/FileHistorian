/*
 * Copyright (C) 2017 JP Dillingham (jp@dillingham.ws)
 * The MIT License (MIT)
 */

using System.ComponentModel;

namespace FileHistorian
{
    /// <summary>
    ///     The installer for the Service.
    /// </summary>
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        #region Public Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ProjectInstaller"/> class.
        /// </summary>
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        #endregion Public Constructors
    }
}