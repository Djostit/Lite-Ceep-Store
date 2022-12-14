namespace Lite_Ceep_Store.Assets
{
    public class NullableZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return 0;
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null
                || value == string.Empty)
                return 0;
            else
                return value;
        }
    }
}
