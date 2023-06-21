using Syncfusion.UI.Xaml.Editors;
using Windows.Globalization.NumberFormatting;

namespace Sales4Pro.WinUI.CustomControls.CustomControls
{
    public sealed class PercentSfNumberBox : SfNumberBox
    {
        public PercentSfNumberBox()
        {
            IncrementNumberRounder rounder = new();
            rounder.Increment = 0.005;
            rounder.RoundingAlgorithm = RoundingAlgorithm.RoundHalfUp;

            PercentFormatter formatter = new();
            formatter.IntegerDigits = 1;
            formatter.FractionDigits = 2;
            formatter.NumberRounder = rounder;

            Minimum = 0.000d;
            Maximum = 1.000d;
            SmallChange = 0.005d;
            LargeChange = 0.100d;
            AllowNull = false;
            Value = 0.000d;
            ShowClearButton = false;
            UpDownPlacementMode = NumberBoxUpDownPlacementMode.Inline;
            NumberFormatter = formatter;
        }
    }
}
