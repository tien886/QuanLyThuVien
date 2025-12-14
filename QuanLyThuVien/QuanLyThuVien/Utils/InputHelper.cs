using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyThuVien.Utils
{
    public class InputHelper
    {
        // Khai báo Attached Property: IsNumberOnly
        public static readonly DependencyProperty IsNumberOnlyProperty =
            DependencyProperty.RegisterAttached("IsNumberOnly", typeof(bool), typeof(InputHelper), new PropertyMetadata(false, OnIsNumberOnlyChanged));

        public static bool GetIsNumberOnly(DependencyObject obj) => (bool)obj.GetValue(IsNumberOnlyProperty);
        public static void SetIsNumberOnly(DependencyObject obj, bool value) => obj.SetValue(IsNumberOnlyProperty, value);

        // Khi thuộc tính thay đổi -> Đăng ký sự kiện PreviewTextInput
        private static void OnIsNumberOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                if ((bool)e.NewValue)
                {
                    textBox.PreviewTextInput += BlockNonDigitCharacters;
                    textBox.PreviewKeyDown += BlockSpaceKey; // Chặn cả dấu cách
                }
                else
                {
                    textBox.PreviewTextInput -= BlockNonDigitCharacters;
                    textBox.PreviewKeyDown -= BlockSpaceKey;
                }
            }
        }

        private static void BlockNonDigitCharacters(object sender, TextCompositionEventArgs e)
        {
            // Regex chỉ cho phép số 0-9
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private static void BlockSpaceKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }
    }
}