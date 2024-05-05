using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BP.LoFiControl
{
    /// <summary>
    /// Provides a Control that acts a mask to provide a lofi effect.
    /// </summary>
    internal class LoFiMask : UserControl, IDisposable
    {
        #region Fields

        private Timer? timer;
        private FrameworkElement? source;
        private uint scale = 2;
        private uint framesPerSecond = 30;
        private bool isRendering;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the source for the mask. This is a dependency property.
        /// </summary>
        public FrameworkElement? Source
        {
            get { return source; }
            set
            {
                source = value;
                Start();
            }
        }

        /// <summary>
        /// Get or set the scale for the mask. This is a dependency property.
        /// </summary>
        public uint Scale
        {
            get { return scale; }
            set
            {
                scale = value;
                Start();
            }
        }

        /// <summary>
        /// Get or set the number of frames per second the mask updates at. This is a dependency property.
        /// </summary>
        public uint FramesPerSecond
        {
            get { return framesPerSecond; }
            set
            {
                framesPerSecond = value;
                Start();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LoFiMask class.
        /// </summary>
        public LoFiMask()
        {
            HorizontalAlignment = HorizontalAlignment.Stretch;
            VerticalAlignment = VerticalAlignment.Stretch;
            IsHitTestVisible = false;

            RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.NearestNeighbor);

            // render whenever the size changes
            SizeChanged += (_, _) => Render();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Render the mask.
        /// </summary>
        private void Render()
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    if (isRendering)
                        return;

                    try
                    {
                        isRendering = true;

                        if (Source == null || Scale == 0)
                            return;

                        var reductionSize = new Size(Source.ActualWidth / Scale, Source.ActualHeight / Scale);

                        if (double.IsNaN(ActualWidth) ||
                            double.IsNaN(ActualHeight) ||
                            double.IsNaN(reductionSize.Width) ||
                            double.IsNaN(reductionSize.Height) ||
                            reductionSize.Width == 0 ||
                            reductionSize.Height == 0)
                            return;

                        var drawingVisual = new DrawingVisual();

                        using (var context = drawingVisual.RenderOpen())
                            context.DrawRectangle(new VisualBrush(Source), null, new Rect(new Point(), reductionSize));

                        var bitmap = new RenderTargetBitmap((int)reductionSize.Width, (int)reductionSize.Height, 96, 96, PixelFormats.Pbgra32);
                        bitmap.Render(drawingVisual);

                        var imageBrush = new ImageBrush(bitmap);

                        Background = imageBrush;
                    }
                    finally
                    {
                        isRendering = false;
                    }
                });
            }
            catch (TaskCanceledException)
            {
                // these can occur if the timer is stopped during a render
            }
            catch (ObjectDisposedException)
            {
                // these can occur if the timer is disposed during a render
            }
        }

        /// <summary>
        /// Start processing updates.
        /// </summary>
        private void Start()
        {
            Stop();

            var frequency = 1000 / FramesPerSecond;
            timer = new Timer(_ => Render(), null, 0, frequency);
        }

        /// <summary>
        /// Stop processing updates.
        /// </summary>
        private void Stop()
        {
            timer?.Change(Timeout.Infinite, Timeout.Infinite);
            timer?.Dispose();
            timer = null;
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Stop();
        }

        #endregion
    }
}
