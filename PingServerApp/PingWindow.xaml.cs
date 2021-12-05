using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net.NetworkInformation;
using System.Threading;
using System.ComponentModel;

namespace PingServerApp {
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class PingWindow : Window {

        private int Attempts = 0;
        public bool IsClosed = false;
        private CancellationTokenSource? TokenSource;

        public PingWindow(string ip) {
            InitializeComponent();
            pingText.Text = "Failed to ping " + ip + " 0 times";
            PingServer(ip);
        }

        public async void PingServer(string ip) {
            this.TokenSource = new CancellationTokenSource();
            CancellationToken token = TokenSource.Token;

            Task task = Task.Run(() => {
                Ping ping = new();
                try {
                    while (ping.Send(ip).Status != 0) {
                        this.Dispatcher.Invoke(() => pingText.Text = "Failed to ping " + ip + " " + ++Attempts + " time" + (Attempts == 1 ? "" : "s"));

                        Thread.Sleep(500);
                    }
                } catch (PingException) {
                    MessageBox.Show("Invalid IP or name");
                    return;
                }

                ping.Dispose();

                MessageBox.Show(ip + " is online", "Status", MessageBoxButton.OK, MessageBoxImage.Information);
            }, token);

            await task;

            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e) {
            this.IsClosed = true;
            this.TokenSource.Cancel();
            this.TokenSource.Dispose();
        }

    }
}
