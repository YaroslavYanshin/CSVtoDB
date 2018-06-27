using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CSVService
{
    public partial class CSVService : ServiceBase
    {
        private CSVController _controller;
        public CSVService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string sourceFolder = ConfigurationManager.AppSettings["CSVSourceFolder"];

            _controller = new CSVController(sourceFolder);
            _controller.Run();
        }

        protected override void OnStop()
        {
            try
            {
                _controller.Stop();
            }
            finally
            {
                _controller.Dispose();
            }
        }
    }
}
