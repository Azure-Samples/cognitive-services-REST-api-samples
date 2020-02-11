using System;
using System.Windows;
using System.Net;
using System.Diagnostics;
using Contoso.NoteTaker.Services.Ink;
using System.Windows.Threading;
using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
using Windows.Graphics.Display;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NoteTaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        InkRecognizer inkRecognizer;
        DisplayInformation displayInfo;
        private readonly DispatcherTimer dispatcherTimer;
        // Time to wait before triggering ink recognition operation
        const double IDLE_WAITING_TIME = 1000;

        public MainWindow()
        {
            InitializeComponent();
            // Replace the subscriptionKey string value with your valid subscription key
            const string subscriptionKey = "[YOUR SUBSCRIPTION KEY]";
            const string endpoint = "https://api.cognitive.microsoft.com";
            const string inkRecognitionUrl = "/inkrecognizer/v1.0-preview/recognize";

            inkRecognizer = new InkRecognizer(subscriptionKey, endpoint, inkRecognitionUrl);
            displayInfo = DisplayInformation.GetForCurrentView();
            inkRecognizer.SetDisplayInformation(displayInfo);

            inkCanvas.InkPresenter.StrokeInput.StrokeStarted += StrokeInput_StrokeStarted;
            inkCanvas.InkPresenter.StrokeInput.StrokeEnded += StrokeInput_StrokeEnded;

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(IDLE_WAITING_TIME);
        }

        private async void DispatcherTimer_Tick(object sender, object e)
        {
            StopTimer();
            try
            {
                inkRecognizer.ClearStrokes();
                var strokeContainer = inkCanvas.InkPresenter.StrokeContainer;
                foreach (Windows.UI.Input.Inking.InkStroke stroke in strokeContainer.GetStrokes())
                {
                    inkRecognizer.AddStroke(stroke);
                }
                var status = await inkRecognizer.RecognizeAsync();
                if (status == HttpStatusCode.OK)
                {
                    var root = inkRecognizer.GetRecognizerRoot();
                    if (root != null)
                    {
                        output.Text = OutputWriter.Print(root);
                    }
                }
                else
                {
                    output.Text = OutputWriter.PrintError($"Http Status: {status}");
                }
            }
            catch (Exception ex)
            {
                output.Text = OutputWriter.PrintError(ex.ToString());
            }
        }

        public void StartTimer() => dispatcherTimer.Start();
        public void StopTimer() => dispatcherTimer.Stop();

        private void inkCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            inkCanvas.InkPresenter.InputDeviceTypes = CoreInputDeviceTypes.Mouse | CoreInputDeviceTypes.Pen | CoreInputDeviceTypes.Touch;
        }

        private void StrokeInput_StrokeStarted(object sender, Windows.UI.Core.PointerEventArgs args)
        {
            StopTimer();
        }

        private void StrokeInput_StrokeEnded(object sender, Windows.UI.Core.PointerEventArgs args)
        {
            StartTimer();
        }
    }
}

