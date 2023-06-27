using Syncfusion.UI.Xaml.Editors;
using Windows.Globalization.NumberFormatting;

namespace Sales4Pro.WinUI.CustomControls
{
    public sealed class PercentSfNumberBox : SfNumberBox
    {
        public PercentSfNumberBox()
        {
            IncrementNumberRounder rounder = new();
            rounder.Increment = 0.001;
            rounder.RoundingAlgorithm = RoundingAlgorithm.RoundHalfUp;

            PercentFormatter formatter = new();
            formatter.IntegerDigits = 1;
            formatter.FractionDigits = 2;
            formatter.NumberRounder = rounder;

            Minimum = 0.000d;
            Maximum = 1.000d;
            SmallChange = 0.001d;
            LargeChange = 0.010d;
            AllowNull = false;
            Value = 0.000d;
            ShowClearButton = false;
            UpDownPlacementMode = NumberBoxUpDownPlacementMode.Inline;
            NumberFormatter = formatter;
        }
    }
}
