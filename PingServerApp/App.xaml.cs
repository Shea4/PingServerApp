using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PingServerApp {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        private void Application_Startup(object sender, StartupEventArgs e) {
            Window window;
            if (e.Args.Length == 1) {
                window = new PingWindow(e.Args[0]);
                if (((PingWindow) window).IsClosed) {
                    return;
                }
            } else {
                window = new MainWindow();
            }

            window.Show();
        }

    }
}
