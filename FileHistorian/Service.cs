using System.ServiceProcess;

namespace FileHistorian
{
    /// <summary>
    ///     The application Service.
    /// </summary>
    partial class Service : ServiceBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Service"/> class.
        /// </summary>
        public Service()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Invokes the application's startup logic.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        protected override void OnStart(string[] args)
        {
            // set the working directory for the application to the location of the executable. if this is not set here, the
            // application believes it is running from %windir%\system32\.
            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
            Program.Start(args);
        }

        /// <summary>
        ///     Invokes the application's shutdown logic.
        /// </summary>
        protected override void OnStop()
        {
            Program.Stop();
        }
    }
}