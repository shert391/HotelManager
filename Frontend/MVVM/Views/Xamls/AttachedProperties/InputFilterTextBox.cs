using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HotelManager.MVVM.Views.Xamls.AttachedProperties;

public enum AllowedType
{
    None,
    Integer,
}

public class InputFilterTextBox
{
    public static AllowedType GetAllowedType(DependencyObject obj) => (AllowedType)obj.GetValue(AllowedTypeProperty);
    public static void SetAllowedType(DependencyObject obj, AllowedType value) => obj.SetValue(AllowedTypeProperty, value);
    public static double GetMaxValue(DependencyObject obj) => (double)obj.GetValue(MaxValueProperty);
    public static void SetMaxValue(DependencyObject obj, double value) => obj.SetValue(MaxValueProperty, value);

    private static readonly Regex _regexForIntegerFilter = new("[0-9]");

    public static readonly DependencyProperty AllowedTypeProperty = DependencyProperty.RegisterAttached(
        "AllowedType",
        typeof(AllowedType),
        typeof(InputFilterTextBox),
        new FrameworkPropertyMetadata(AllowedType.None, AllowedTypeChangedCallback));

    public static readonly DependencyProperty MaxValueProperty = DependencyProperty.RegisterAttached(
        "MaxValue",
        typeof(double),
        typeof(InputFilterTextBox),
        new FrameworkPropertyMetadata(double.NaN));

    private static void AllowedTypeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ConfigurateMaxValue(d, (AllowedType)e.NewValue);

        if (d is not TextBox textBox)
            throw new ArgumentException("failed");

        if (e.NewValue is not AllowedType allowedType)
            throw new ArgumentException("failed");

        if (allowedType == AllowedType.Integer)
            textBox.PreviewTextInput += IntegerFilterCallback;
    }

    private static void ConfigurateMaxValue(DependencyObject d, AllowedType typeFilter)
    {
        if (!double.IsNaN(GetMaxValue(d)))
            return;

        switch (typeFilter)
        {
            case AllowedType.Integer:
                SetMaxValue(d, int.MaxValue);
                return;
        }
    }

    private static void IntegerFilterCallback(object sender, TextCompositionEventArgs e)
    {
        if (sender is not TextBox textBox)
            throw new ArgumentException("failed");

        var bufferText = textBox.Text.Insert(textBox.SelectionStart, e.Text);
        double.TryParse(bufferText, out double value);

        if (!double.IsNaN(GetMaxValue(textBox)) && value > GetMaxValue(textBox) && textBox.SelectionLength == 0)
        {
            textBox.Text = GetMaxValue(textBox).ToString();
            e.Handled = true;
            return;
        }

        e.Handled = !(_regexForIntegerFilter.IsMatch(e.Text) || (e.Text == ""));
    }
}

