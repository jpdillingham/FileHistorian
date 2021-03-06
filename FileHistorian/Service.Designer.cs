﻿/*
 * Copyright (C) 2017 JP Dillingham (jp@dillingham.ws)
 * The MIT License (MIT)
 */

namespace FileHistorian
{
    /// <summary>
    ///     The application Service.
    /// </summary>
    partial class Service
    {
        /// <summary> 
        ///     Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///     Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        ///     Required method for Designer support - do not modify 
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // Service
            // 
            this.ServiceName = "ConsoleService";

        }

        #endregion
    }
}
