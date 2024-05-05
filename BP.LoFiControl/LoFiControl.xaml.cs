using System.Windows;
using System.Windows.Controls;

namespace BP.LoFiControl
{
    /// <summary>
    /// Interaction logic for LoFiControl.xaml
    /// </summary>
    public partial class LoFiControl : UserControl
    {
        #region Properties

        /// <summary>
        /// Get or set the lofi content. This is a dependency property.
        /// </summary>
        public object LoFiContent
        {
            get { return GetValue(LoFiContentProperty); }
            set { SetValue(LoFiContentProperty, value); }
        }

        /// <summary>
        /// Get or set the scale for the mask. This is a dependency property.
        /// </summary>
        public uint Scale
        {
            get { return (uint)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        /// <summary>
        /// Get or set the number of frames per second the mask updates at. This is a dependency property.
        /// </summary>
        public uint FramesPerSecond
        {
            get { return (uint)GetValue(FramesPerSecondProperty); }
            set { SetValue(FramesPerSecondProperty, value); }
        }

        #endregion

        #region DependencyProperties

        /// <summary>
        /// Identifies the LoFiControl.LoFiContent property.
        /// </summary>
        public static readonly DependencyProperty LoFiContentProperty = DependencyProperty.Register(nameof(LoFiContent), typeof(FrameworkElement), typeof(LoFiControl), new PropertyMetadata(OnPixalatedContentPropertyChanged));

        /// <summary>
        /// Identifies the LoFiControl.Scale property.
        /// </summary>
        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(nameof(Scale), typeof(uint), typeof(LoFiControl), new PropertyMetadata((uint)1, OnScalePropertyChanged));

        /// <summary>
        /// Identifies the LoFiControl.FramesPerSecond property.
        /// </summary>
        public static readonly DependencyProperty FramesPerSecondProperty = DependencyProperty.Register(nameof(FramesPerSecond), typeof(uint), typeof(LoFiControl), new PropertyMetadata((uint)30, OnFramesPerSecondPropertyChanged));

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LoFiControl class.
        /// </summary>
        public LoFiControl()
        {
            InitializeComponent();

            Mask.Source = ContentControl;
        }

        #endregion

        #region StaticMethods

        private static void OnPixalatedContentPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var control = obj as LoFiControl;

            if (control == null)
                return;

            control.ContentControl.Content = args.NewValue;
        }

        private static void OnScalePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var control = obj as LoFiControl;

            if (control == null)
                return;

            control.Mask.Scale = (uint)args.NewValue;
        }

        private static void OnFramesPerSecondPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var control = obj as LoFiControl;

            if (control == null)
                return;

            control.Mask.FramesPerSecond = (uint)args.NewValue;
        }

        #endregion
    }
}
