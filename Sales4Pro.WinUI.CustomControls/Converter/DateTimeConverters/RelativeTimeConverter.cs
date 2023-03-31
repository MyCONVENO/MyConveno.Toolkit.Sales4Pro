using Microsoft.UI.Xaml.Data;
using System;
using System.Globalization;
using Windows.ApplicationModel.Resources;

namespace Sales4Pro.WinUI.CustomControls.Converter;

/// <summary>
/// Time converter to display elapsed time relatively to the present.
/// </summary>
/// <QualityBand>Preview</QualityBand>
public class RelativeTimeConverter : IValueConverter
{
    private ResourceLoader loader = ResourceLoader.GetForCurrentView();

    /// <summary>
    /// A minute defined in seconds.
    /// </summary>
    private const double Minute = 60.0;

    /// <summary>
    /// An hour defined in seconds.
    /// </summary>
    private const double Hour = 60.0 * Minute;

    /// <summary>
    /// A day defined in seconds.
    /// </summary>
    private const double Day = 24 * Hour;

    /// <summary>
    /// A week defined in seconds.
    /// </summary>
    private const double Week = 7 * Day;

    /// <summary>
    /// A month defined in seconds.
    /// </summary>
    private const double Month = 30.5 * Day;

    /// <summary>
    /// A year defined in seconds.
    /// </summary>
    private const double Year = 365 * Day;

    /// <summary>
    /// Abbreviation for the default culture used by resources files.
    /// </summary>
    private const string DefaultCulture = "en-US";

    /// <summary>
    /// Four different strings to express hours in plural.
    /// </summary>
    private string[] PluralHourStrings;

    /// <summary>
    /// Four different strings to express minutes in plural.
    /// </summary>
    private string[] PluralMinuteStrings;

    /// <summary>
    /// Four different strings to express seconds in plural.
    /// </summary>
    private string[] PluralSecondStrings;

    /// <summary>
    /// Resources use the culture in the system locale by default.
    /// The converter must use the culture specified the ConverterCulture.
    /// The ConverterCulture defaults to en-US when not specified.
    /// Thus, change the resources culture only if ConverterCulture is set.
    /// </summary>
    /// <param name="culture">The culture to use in the converter.</param>
    //        private void SetLocalizationCulture(CultureInfo culture)
    private void SetLocalizationCulture()
    {
        ResourceLoader loader = ResourceLoader.GetForCurrentView();

        //if (!culture.Name.Equals(DefaultCulture, StringComparison.Ordinal))
        //{
        //    //ControlResources.Culture = culture;
        //}

        PluralHourStrings = new string[4] {
              loader.GetString("XHoursAgo_2To4"),
              loader.GetString("XHoursAgo_EndsIn1Not11"),
              loader.GetString("XHoursAgo_EndsIn2To4Not12To14"),
              loader.GetString("XHoursAgo_Other")
          };

        PluralMinuteStrings = new string[4] {
              loader.GetString("XMinutesAgo_2To4"),
              loader.GetString("XMinutesAgo_EndsIn1Not11"),
              loader.GetString("XMinutesAgo_EndsIn2To4Not12To14"),
              loader.GetString("XMinutesAgo_Other")
          };

        PluralSecondStrings = new string[4] {
              loader.GetString("XSecondsAgo_2To4"),
              loader.GetString("XSecondsAgo_EndsIn1Not11"),
              loader.GetString("XSecondsAgo_EndsIn2To4Not12To14"),
              loader.GetString("XSecondsAgo_Other")
          };
    }

    /// <summary>
    /// Returns a localized text string to express months in plural.
    /// </summary>
    /// <param name="month">Number of months.</param>
    /// <returns>Localized text string.</returns>
    private static string GetPluralMonth(int month)
    {
        ResourceLoader loader = ResourceLoader.GetForCurrentView();

        if (month >= 2 && month <= 4)
        {
            return string.Format(CultureInfo.CurrentUICulture, loader.GetString("XMonthsAgo_2To4"), month.ToString());
        }
        else if (month >= 5 && month <= 12)
        {
            return string.Format(CultureInfo.CurrentUICulture, loader.GetString("XMonthsAgo_5To12"), month.ToString());
        }
        else
        {
            throw new ArgumentException();
        }
    }

    /// <summary>
    /// Returns a localized text string to express time units in plural.
    /// </summary>
    /// <param name="units">
    /// Number of time units, e.g. 5 for five months.
    /// </param>
    /// <param name="resources">
    /// Resources related to the specified time unit.
    /// </param>
    /// <returns>Localized text string.</returns>
    private static string GetPluralTimeUnits(int units, string[] resources)
    {
        int modTen = units % 10;
        int modHundred = units % 100;

        if (units <= 1)
        {
            throw new ArgumentException();
            //throw new ArgumentException(Properties.Resources.InvalidNumberOfTimeUnits);
        }
        else if (units >= 2 && units <= 4)
        {
            return string.Format(CultureInfo.CurrentUICulture, resources[0], units.ToString());
        }
        else if (modTen == 1 && modHundred != 11)
        {
            return string.Format(CultureInfo.CurrentUICulture, resources[1], units.ToString());
        }
        else if ((modTen >= 2 && modTen <= 4) && !(modHundred >= 12 && modHundred <= 14))
        {
            return string.Format(CultureInfo.CurrentUICulture, resources[2], units.ToString());
        }
        else
        {
            return string.Format(CultureInfo.CurrentUICulture, resources[3], units.ToString());
        }
    }

    /// <summary>
    /// Returns a localized text string for the "ast" + "day of week as {0}".
    /// </summary>
    /// <param name="dow">Last Day of week.</param>
    /// <returns>Localized text string.</returns>
    private static string GetLastDayOfWeek(DayOfWeek dow)
    {
        ResourceLoader loader = ResourceLoader.GetForCurrentView();

        string result;

        switch (dow)
        {
            case DayOfWeek.Monday:
                result = loader.GetString("last Monday");
                break;
            case DayOfWeek.Tuesday:
                result = loader.GetString("last Tuesday");
                break;
            case DayOfWeek.Wednesday:
                result = loader.GetString("last Wednesday");
                break;
            case DayOfWeek.Thursday:
                result = loader.GetString("last Thursday");
                break;
            case DayOfWeek.Friday:
                result = loader.GetString("last Friday");
                break;
            case DayOfWeek.Saturday:
                result = loader.GetString("last Saturday");
                break;
            case DayOfWeek.Sunday:
                result = loader.GetString("last Sunday");
                break;
            default:
                result = loader.GetString("last Sunday");
                break;
        }

        return result;
    }


    /// <summary>
    /// Returns a localized text string to express "on {0}"
    /// where {0} is a day of the week, e.g. Sunday.
    /// </summary>
    /// <param name="dow">Day of week.</param>
    /// <returns>Localized text string.</returns>
    private static string GetOnDayOfWeek(DayOfWeek dow)
    {
        ResourceLoader loader = ResourceLoader.GetForCurrentView();

        string result;

        switch (dow)
        {
            case DayOfWeek.Monday:
                result = loader.GetString("on Monday");
                break;
            case DayOfWeek.Tuesday:
                result = loader.GetString("on Tuesday");
                break;
            case DayOfWeek.Wednesday:
                result = loader.GetString("on Wednesday");
                break;
            case DayOfWeek.Thursday:
                result = loader.GetString("on Thursday");
                break;
            case DayOfWeek.Friday:
                result = loader.GetString("on Friday");
                break;
            case DayOfWeek.Saturday:
                result = loader.GetString("on Saturday");
                break;
            case DayOfWeek.Sunday:
                result = loader.GetString("on Sunday");
                break;
            default:
                result = loader.GetString("on Sunday");
                break;
        }

        return result;
    }

    /// <summary>
    /// Converts a 
    /// <see cref="T:System.DateTime"/>
    /// object into a string the represents the elapsed time 
    /// relatively to the present.
    /// </summary>
    /// <param name="value">The given date and time.</param>
    /// <param name="targetType">
    /// The type corresponding to the binding property, which must be of
    /// <see cref="T:System.String"/>.
    /// </param>
    /// <param name="parameter">(Not used).</param>
    /// <param name="culture">
    /// The culture to use in the converter.
    /// When not specified, the converter uses the current culture
    /// as specified by the system locale.
    /// </param>
    /// <returns>The given date and time as a string.</returns>
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        // ----- Exit initialisation here in DesignMode  ------------------------
        if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            return "Vor einer Minute (Design)";
        // ----------------------------------------------------------------------

        ResourceLoader loader = ResourceLoader.GetForCurrentView();

        // Target value must be a System.DateTime object.
        if (!(value is DateTime))
        {
            //  throw new ArgumentException(Properties.Resources.InvalidDateTimeArgument);
        }

        string result;

        //DateTime given = ((DateTime)value).ToLocalTime();
        DateTime given = ((DateTime)value);

        DateTime current = DateTime.Now;

        TimeSpan difference = current - given;

        //  SetLocalizationCulture(culture);
        SetLocalizationCulture();



        if (DateTimeFormatHelper.IsFutureDateTime(current, given))
        {
            // Future dates and times are not supported, but to prevent crashing an app
            // if the time they receive from a server is slightly ahead of the phone's clock
            // we'll just default to the minimum, which is "2 seconds ago".
            result = GetPluralTimeUnits(2, PluralSecondStrings);
        }

        if (difference.TotalSeconds > Year)
        {
            // "over a year ago"
            result = loader.GetString("OverAYearAgo");
        }
        else if (difference.TotalSeconds > (1.5 * Month))
        {
            // "x months ago"
            int nMonths = (int)((difference.TotalSeconds + Month / 2) / Month);
            result = GetPluralMonth(nMonths);
        }
        else if (difference.TotalSeconds >= (3.5 * Week))
        {
            // "about a month ago"
            result = loader.GetString("AboutAMonthAgo");
        }
        else if (difference.TotalSeconds >= Week)
        {
            int nWeeks = (int)(difference.TotalSeconds / Week);
            if (nWeeks > 1)
            {
                // "x weeks ago"
                result = string.Format(CultureInfo.CurrentUICulture, loader.GetString("XWeeksAgo_2To4"), nWeeks.ToString());
            }
            else
            {
                // "about a week ago"
                result = loader.GetString("AboutAWeekAgo");
            }
        }
        else if (difference.TotalSeconds >= (5 * Day))
        {
            // "last <dayofweek>"    
            result = GetLastDayOfWeek(given.DayOfWeek);
        }
        else if (difference.TotalSeconds >= Day)
        {
            // "on <dayofweek>"
            result = GetOnDayOfWeek(given.DayOfWeek);
        }
        else if (difference.TotalSeconds >= (2 * Hour))
        {
            // "x hours ago"
            int nHours = (int)(difference.TotalSeconds / Hour);
            result = GetPluralTimeUnits(nHours, PluralHourStrings);
        }
        else if (difference.TotalSeconds >= Hour)
        {
            // "about an hour ago"
            result = loader.GetString("AboutAnHourAgo");
        }
        else if (difference.TotalSeconds >= (2 * Minute))
        {
            // "x minutes ago"
            int nMinutes = (int)(difference.TotalSeconds / Minute);
            result = GetPluralTimeUnits(nMinutes, PluralMinuteStrings);
        }
        else if (difference.TotalSeconds >= Minute)
        {
            // "about a minute ago"
            result = loader.GetString("AboutAMinuteAgo");
        }
        else
        {
            // "x seconds ago" or default to "2 seconds ago" if less than two seconds.
            int nSeconds = ((int)difference.TotalSeconds > 1.0) ? (int)difference.TotalSeconds : 2;
            result = GetPluralTimeUnits(nSeconds, PluralSecondStrings);
        }

        //return "Letzte Synchronisation ".ToUpper() + result.ToUpper();
        return result;
    }

    /// <summary>
    /// Not implemented.
    /// </summary>
    /// <param name="value">(Not used).</param>
    /// <param name="targetType">(Not used).</param>
    /// <param name="parameter">(Not used).</param>
    /// <param name="culture">(Not used).</param>
    /// <returns>null</returns>
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
