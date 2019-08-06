using System.Windows;

namespace FS_Explorer.AttachedProperties
{
    public class AttachedDebug
    {
        public static readonly DependencyProperty DataProperty = DependencyProperty.RegisterAttached(
            "Data", typeof(object), typeof(AttachedDebug),
            new PropertyMetadata(default(object), Data_PropertyChangedCallback));

        private static void Data_PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dr = e.NewValue;
        }

        public static void SetData(DependencyObject element, object value)
        {
            element.SetValue(DataProperty, value);
        }

        public static object GetData(DependencyObject element)
        {
            return (object)element.GetValue(DataProperty);
        }
    }
}