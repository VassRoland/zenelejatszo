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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;


namespace zenelejatszo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        MediaPlayer mediaPlayer = new MediaPlayer();
        DispatcherTimer timer = new DispatcherTimer();
        string fajlnev;
        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;


        private void betoltes_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog filedialog = new OpenFileDialog
            {
                Multiselect = true,
                DefaultExt = ".mp3"

            };
            bool? dialogOk = filedialog.ShowDialog();
            if (dialogOk == true)
            {

                fajlnev = filedialog.FileName;
                zenek.Text = filedialog.SafeFileName;
                mediaPlayer.Open(new Uri(fajlnev));
            }
        }

        private void inditas_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Play();
            csuszka.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            csuszka.Value = mediaPlayer.Position.TotalSeconds;
            if (csuszka.Value == mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds)
            {
                timer.Stop();
                csuszka.Value = 0;
            }
        }

        private void megallit_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
            timer.Stop();
        }




        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.Volume = (double)hangero.Value;
        }

        private void csuszka_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void csuszka_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            mediaPlayer.Position = TimeSpan.FromSeconds(csuszka.Value);
        }
        private void csuszka_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ido.Text = TimeSpan.FromSeconds(csuszka.Value).ToString(@"hh\:mm\:ss");
        }
    }

}
