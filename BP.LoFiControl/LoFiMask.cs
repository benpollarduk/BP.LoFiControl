using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BP.LoFiControl
{
    /// <summary>
    /// Provides a Control that acts a mask to provide a lo-fi effect.
    /// </summary>
    internal class LoFiMask : UserControl, IDisposable
    {
        #region Fields

        private Timer? timer;
        private FrameworkElement? source;
        private double strength = 2;
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
        /// Get or set the strength of the lo-fi effect. This is a dependency property.
        /// </summary>
        public double Strength
        {
            get { return strength; }
            set
            {
                strength = value;
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

                        if (Source == null || Strength < 1.0)
                            return;

                        // calculate the reduced size
                        var reductionSize = new Size(Source.ActualWidth / Strength, Source.ActualHeight / Strength);

                        // ensure that rendering is possible with the current sizes
                        if (double.IsNaN(ActualWidth) ||
                            double.IsNaN(ActualHeight) ||
                            double.IsNaN(reductionSize.Width) ||
                            double.IsNaN(reductionSize.Height) ||
                            reductionSize.Width == 0 ||
                            reductionSize.Height == 0)
                            return;

                        // create a visual to host the lo-fi visual
                        var drawingVisual = new DrawingVisual();

                        // render the source at the reduced size
                        using (var context = drawingVisual.RenderOpen())
                            context.DrawRectangle(new VisualBrush(Source), null, new Rect(new Point(), reductionSize));

                        // TODO: resolve - this is extremely inefficient and memory intensive
                        // render the lo-fi visual into a bitmap
                        var bitmap = new RenderTargetBitmap((int)reductionSize.Width, (int)reductionSize.Height, 96, 96, PixelFormats.Pbgra32);
                        bitmap.Render(drawingVisual);

                        // render the lo-fi bitmap as the background of the mask
                        Background = new ImageBrush(bitmap);
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
