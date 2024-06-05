using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;

namespace DotnetCatalog.Common
{
    /// <summary>
    /// Value converter that translates true to <see cref="Visibility.Visible"/> and false to
    /// <see cref="Visibility.Collapsed"/>.
    /// </summary>
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }
    }



    /// <summary>
    /// Value converter that translates not null to <see cref="Visibility.Visible"/> and null to
    /// <see cref="Visibility.Collapsed"/>.
    /// </summary>
    public sealed class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value != null) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// Value converter that translates null to <see cref="Visibility.Visible"/> and not null to
    /// <see cref="Visibility.Collapsed"/>.
    /// </summary>
    public sealed class NotNullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value == null) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// Value converter that translates null to true and not null to false.
    /// </summary>
    public sealed class NullToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value == null);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// Value converter that translates a number value into a string of stars coded
    /// for Wingdings.
    /// </summary>
    public sealed class RatingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var rating = (int)value;
            var ret = "";
            for (int i = 0; i < rating; i++)
            {
                // 0xAB is the five-point star in the Wingdings font
                ret += (char)0xAB;
            }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Value converter that translates an integer of zero to <see cref="Visibility.Visible"/>, otherwise to
    /// <see cref="Visibility.Collapsed"/>.
    /// </summary>
    public sealed class ZeroToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            return (value is int && (int)value == 0) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// Value converter that translates an integer zero to true, otherwise to false.
    /// </summary>
    public sealed class ZeroToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value is int && (int)value == 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Value converter that translates a binary encoded PNG into a XAML bitmap image.
    /// </summary>
    public sealed class ByteArrayToBitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var bytes = value as byte[];

            if (bytes == null)
                return null;

            // Create the random access stream and copy the byte array into it
            var ras = new InMemoryRandomAccessStream();
            var dw = new DataWriter(ras.GetOutputStreamAt(0));
            dw.WriteBytes(bytes);
            dw.StoreAsync().AsTask().Wait();

            var bitmap = new BitmapImage();
            bitmap.SetSource(ras);

            return bitmap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}