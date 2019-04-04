using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace WpfCronExpressionUI.ViewModel
{
    internal class ViewModel : ViewModelBase
    {
        public string CronExpression => $"Cron Expression: {secondCronExpression} {minuteCronExpression} {hourCronExpression} {DayOfMonthCronExpression} {monthCronExpression} {DayOfWeekCronExpression} {yearCronExpression}";

        private string DayOfWeekCronExpression = "*";
        private string DayOfMonthCronExpression = "*";

        public ViewModel()
        {
            RefreshYearCronExpression();
            RefreshMonthRanges();
            RefreshHourRanges();
            RefreshMinuteRanges();
            RefreshSecondRanges();
        }

        #region Year

        private int minimumYear = DateTime.Today.Year;

        public int MinimumYear
        {
            get { return minimumYear; }
            set
            {
                // MinimumYear
                Set(nameof(MinimumYear), ref minimumYear, value);
                RefreshYearRanges();
                if (EveryXYearsStartInSelectedItem < MinimumYear)
                    EveryXYearsStartInSelectedItem = MinimumYear;
            }
        }

        private int maximumYear = DateTime.Today.AddYears(25).Year;

        public int MaximumYear
        {
            get { return maximumYear; }
            set
            {
                // MaximumYear
                Set(nameof(MaximumYear), ref maximumYear, value);
                RefreshYearRanges();
                if (EveryXYearsStartInSelectedItem > MaximumYear)
                    EveryXYearsStartInSelectedItem = MaximumYear;
            }
        }

        private bool anyYear = true;

        public bool AnyYear
        {
            get => anyYear;
            set
            {
                // AnyYear
                if (Set(nameof(AnyYear), ref anyYear, value)) RefreshYearCronExpression();
            }
        }

        private bool everyXYears;

        public bool EveryXYears
        {
            get => everyXYears;
            set
            {
                // EveryXYears
                if (Set(nameof(EveryXYears), ref everyXYears, value)) RefreshYearCronExpression();
            }
        }

        private int everyXYearsSelectedItem = 1;

        public int EveryXYearsSelectedItem
        {
            get { return everyXYearsSelectedItem; }
            set
            {
                // EveryXYearsSelectedItem
                Set(nameof(EveryXYearsSelectedItem), ref everyXYearsSelectedItem, value);
                RefreshYearCronExpression();
            }
        }

        public List<int> EveryXYearsItems => Enumerable.Range(1, 10).ToList();

        private bool specificYears;

        public bool SpecificYears
        {
            get => specificYears;
            set
            {
                // SpecificYear
                if (Set(nameof(SpecificYears), ref specificYears, value)) RefreshYearCronExpression();
            }
        }

        private bool yearRange;

        public bool YearRange
        {
            get => yearRange;
            set
            {
                // YearRange
                if (Set(nameof(YearRange), ref yearRange, value)) RefreshYearCronExpression();
            }
        }



        private void RefreshYearRanges()
        {
            yearRangeItems = Enumerable.Range(MinimumYear, Math.Abs(MaximumYear - MinimumYear)).ToList();
            yearRangeCheckedItems = Enumerable.Range(MinimumYear, Math.Abs(MaximumYear - MinimumYear))
                .Select(y => new CheckedItem(YearCheckChanged)
                {
                    Id = y,
                    Description = y.ToString()
                })
                .ToList();

            OnPropertyChanged(nameof(YearRangeItems));
            OnPropertyChanged(nameof(YearRangeCheckedItems));
        }

        private void YearCheckChanged()
        {
            RefreshYearCronExpression();
        }

        private List<int> yearRangeItems;
        public List<int> YearRangeItems => yearRangeItems;

        private List<CheckedItem> yearRangeCheckedItems;
        public List<CheckedItem> YearRangeCheckedItems => yearRangeCheckedItems;



        private int everyXYearsStartInSelectedItem;

        public int EveryXYearsStartInSelectedItem
        {
            get { return everyXYearsStartInSelectedItem; }
            set
            {
                // EveryXYearsStartInSelectedItem
                Set(nameof(EveryXYearsStartInSelectedItem), ref everyXYearsStartInSelectedItem, value);
                RefreshYearCronExpression();
            }
        }

        private int yearRangeStartSelectedItem = DateTime.Today.Year;

        public int YearRangeStartSelectedItem
        {
            get { return yearRangeStartSelectedItem; }
            set
            {
                // YearRangeStartSelectedItem
                Set(nameof(YearRangeStartSelectedItem), ref yearRangeStartSelectedItem, value);
                RefreshYearCronExpression();
            }
        }

        private int yearRangeEndSelectedItem = DateTime.Today.AddYears(10).Year;

        public int YearRangeEndSelectedItem
        {
            get { return yearRangeEndSelectedItem; }
            set
            {
                // YearRangeEndSelectedItem
                Set(nameof(YearRangeEndSelectedItem), ref yearRangeEndSelectedItem, value);
                RefreshYearCronExpression();
            }
        }

        private string yearCronExpression;

        private void RefreshYearCronExpression()
        {
            if (AnyYear)
                yearCronExpression = "*";
            else if (EveryXYears)
                yearCronExpression = $"{EveryXYearsStartInSelectedItem}/{EveryXYearsSelectedItem}";
            else if (SpecificYears)
                yearCronExpression = string.Join(",", YearRangeCheckedItems
                    .Where(ci => ci.IsChecked)
                    .Select(ci => ci.Id)
                );
            else if (YearRange)
                yearCronExpression = $"{YearRangeStartSelectedItem}-{YearRangeEndSelectedItem}";

            OnPropertyChanged(nameof(CronExpression));
        }

        #endregion

        #region Month

        private bool anyMonth = true;

        public bool AnyMonth
        {
            get => anyMonth;
            set
            {
                // AnyMonth
                if (Set(nameof(AnyMonth), ref anyMonth, value)) RefreshMonthCronExpression();
            }
        }

        private bool everyXMonths;

        public bool EveryXMonths
        {
            get => everyXMonths;
            set
            {
                // EveryXMonths
                if (Set(nameof(EveryXMonths), ref everyXMonths, value)) RefreshMonthCronExpression();
            }
        }

        private int everyXMonthsSelectedItem = 1;

        public int EveryXMonthsSelectedItem
        {
            get { return everyXMonthsSelectedItem; }
            set
            {
                // EveryXMonthsSelectedItem
                Set(nameof(EveryXMonthsSelectedItem), ref everyXMonthsSelectedItem, value);
                RefreshMonthCronExpression();
            }
        }

        public List<int> EveryXMonthsItems => Enumerable.Range(1, 10).ToList();

        private bool specificMonths;

        public bool SpecificMonths
        {
            get => specificMonths;
            set
            {
                // SpecificMonth
                if (Set(nameof(SpecificMonths), ref specificMonths, value)) RefreshMonthCronExpression();
            }
        }

        private bool monthRange;

        public bool MonthRange
        {
            get => monthRange;
            set
            {
                // MonthRange
                if (Set(nameof(MonthRange), ref monthRange, value)) RefreshMonthCronExpression();
            }
        }



        private void RefreshMonthRanges()
        {
            monthRangeItems = Enumerable.Range(1, 12)
                .Select(ct => new ComboboxItem() {
                    Id = ct,
                    Description = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(ct)
                })
                .ToList();
            monthRangeCheckedItems = Enumerable.Range(1, 12)
                .Select(ct => new CheckedItem(MonthCheckChanged)
                {
                    Id = ct,
                    Description = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(ct)
                })
                .ToList();

            MonthRangeStartSelectedItem = MonthRangeItems.FirstOrDefault();
            MonthRangeEndSelectedItem = MonthRangeItems.FirstOrDefault();
            EveryXMonthsStartInSelectedItem = MonthRangeItems.FirstOrDefault();

            OnPropertyChanged(nameof(MonthRangeItems));
            OnPropertyChanged(nameof(MonthRangeCheckedItems));
        }

        private void MonthCheckChanged()
        {
            RefreshMonthCronExpression();
        }

        private List<ComboboxItem> monthRangeItems;
        public List<ComboboxItem> MonthRangeItems => monthRangeItems;

        private List<CheckedItem> monthRangeCheckedItems;
        public List<CheckedItem> MonthRangeCheckedItems => monthRangeCheckedItems;



        private ComboboxItem everyXMonthsStartInSelectedItem;

        public ComboboxItem EveryXMonthsStartInSelectedItem
        {
            get { return everyXMonthsStartInSelectedItem; }
            set
            {
                // EveryXMonthsStartInSelectedItem
                Set(nameof(EveryXMonthsStartInSelectedItem), ref everyXMonthsStartInSelectedItem, value);
                RefreshMonthCronExpression();
            }
        }

        private ComboboxItem monthRangeStartSelectedItem;

        public ComboboxItem MonthRangeStartSelectedItem
        {
            get { return monthRangeStartSelectedItem; }
            set
            {
                // MonthRangeStartSelectedItem
                Set(nameof(MonthRangeStartSelectedItem), ref monthRangeStartSelectedItem, value);
                RefreshMonthCronExpression();
            }
        }

        private ComboboxItem monthRangeEndSelectedItem;

        public ComboboxItem MonthRangeEndSelectedItem
        {
            get { return monthRangeEndSelectedItem; }
            set
            {
                // MonthRangeEndSelectedItem
                Set(nameof(MonthRangeEndSelectedItem), ref monthRangeEndSelectedItem, value);
                RefreshMonthCronExpression();
            }
        }

        private string monthCronExpression;

        private void RefreshMonthCronExpression()
        {
            if (AnyMonth)
                monthCronExpression = "*";
            else if (EveryXMonths)
                monthCronExpression = $"{EveryXMonthsStartInSelectedItem.Id}/{EveryXMonthsSelectedItem}";
            else if (SpecificMonths)
                monthCronExpression = string.Join(",", MonthRangeCheckedItems
                    .Where(ci => ci.IsChecked)
                    .Select(ci => ci.Id)
                );
            else if (MonthRange)
                monthCronExpression = $"{MonthRangeStartSelectedItem.Id}-{MonthRangeEndSelectedItem.Id}";

            OnPropertyChanged(nameof(CronExpression));
        }

        #endregion

        #region Hour

        private bool anyHour = true;

        public bool AnyHour
        {
            get => anyHour;
            set
            {
                // AnyHour
                if (Set(nameof(AnyHour), ref anyHour, value)) RefreshHourCronExpression();
            }
        }

        private bool everyXHours;

        public bool EveryXHours
        {
            get => everyXHours;
            set
            {
                // EveryXHours
                if (Set(nameof(EveryXHours), ref everyXHours, value)) RefreshHourCronExpression();
            }
        }

        private int everyXHoursSelectedItem = 1;

        public int EveryXHoursSelectedItem
        {
            get { return everyXHoursSelectedItem; }
            set
            {
                // EveryXHoursSelectedItem
                Set(nameof(EveryXHoursSelectedItem), ref everyXHoursSelectedItem, value);
                RefreshHourCronExpression();
            }
        }

        public List<int> EveryXHoursItems => Enumerable.Range(1, 10).ToList();

        private bool specificHours;

        public bool SpecificHours
        {
            get => specificHours;
            set
            {
                // SpecificHour
                if (Set(nameof(SpecificHours), ref specificHours, value)) RefreshHourCronExpression();
            }
        }

        private bool hourRange;

        public bool HourRange
        {
            get => hourRange;
            set
            {
                // HourRange
                if (Set(nameof(HourRange), ref hourRange, value)) RefreshHourCronExpression();
            }
        }



        private void RefreshHourRanges()
        {
            hourRangeItems = Enumerable.Range(0, 24)
                .Select(ct => new ComboboxItem()
                {
                    Id = ct,
                    Description = ct.ToString()
                })
                .ToList();
            hourRangeCheckedItems = Enumerable.Range(0, 24)
                .Select(ct => new CheckedItem(HourCheckChanged)
                {
                    Id = ct,
                    Description = ct.ToString()
                })
                .ToList();

            HourRangeStartSelectedItem = HourRangeItems.FirstOrDefault();
            HourRangeEndSelectedItem = HourRangeItems.FirstOrDefault();
            EveryXHoursStartInSelectedItem = HourRangeItems.FirstOrDefault();

            OnPropertyChanged(nameof(HourRangeItems));
            OnPropertyChanged(nameof(HourRangeCheckedItems));
        }

        private void HourCheckChanged()
        {
            RefreshHourCronExpression();
        }

        private List<ComboboxItem> hourRangeItems;
        public List<ComboboxItem> HourRangeItems => hourRangeItems;

        private List<CheckedItem> hourRangeCheckedItems;
        public List<CheckedItem> HourRangeCheckedItems => hourRangeCheckedItems;



        private ComboboxItem everyXHoursStartInSelectedItem;

        public ComboboxItem EveryXHoursStartInSelectedItem
        {
            get { return everyXHoursStartInSelectedItem; }
            set
            {
                // EveryXHoursStartInSelectedItem
                Set(nameof(EveryXHoursStartInSelectedItem), ref everyXHoursStartInSelectedItem, value);
                RefreshHourCronExpression();
            }
        }

        private ComboboxItem hourRangeStartSelectedItem;

        public ComboboxItem HourRangeStartSelectedItem
        {
            get { return hourRangeStartSelectedItem; }
            set
            {
                // HourRangeStartSelectedItem
                Set(nameof(HourRangeStartSelectedItem), ref hourRangeStartSelectedItem, value);
                RefreshHourCronExpression();
            }
        }

        private ComboboxItem hourRangeEndSelectedItem;

        public ComboboxItem HourRangeEndSelectedItem
        {
            get { return hourRangeEndSelectedItem; }
            set
            {
                // HourRangeEndSelectedItem
                Set(nameof(HourRangeEndSelectedItem), ref hourRangeEndSelectedItem, value);
                RefreshHourCronExpression();
            }
        }

        private string hourCronExpression;

        private void RefreshHourCronExpression()
        {
            if (AnyHour)
                hourCronExpression = "*";
            else if (EveryXHours)
                hourCronExpression = $"{EveryXHoursStartInSelectedItem.Id}/{EveryXHoursSelectedItem}";
            else if (SpecificHours)
                hourCronExpression = string.Join(",", HourRangeCheckedItems
                    .Where(ci => ci.IsChecked)
                    .Select(ci => ci.Id)
                );
            else if (HourRange)
                hourCronExpression = $"{HourRangeStartSelectedItem.Id}-{HourRangeEndSelectedItem.Id}";

            OnPropertyChanged(nameof(CronExpression));
        }

        #endregion

        #region Minute

        private bool anyMinute = true;

        public bool AnyMinute
        {
            get => anyMinute;
            set
            {
                // AnyMinute
                if (Set(nameof(AnyMinute), ref anyMinute, value)) RefreshMinuteCronExpression();
            }
        }

        private bool everyXMinutes;

        public bool EveryXMinutes
        {
            get => everyXMinutes;
            set
            {
                // EveryXMinutes
                if (Set(nameof(EveryXMinutes), ref everyXMinutes, value)) RefreshMinuteCronExpression();
            }
        }

        private int everyXMinutesSelectedItem = 1;

        public int EveryXMinutesSelectedItem
        {
            get { return everyXMinutesSelectedItem; }
            set
            {
                // EveryXMinutesSelectedItem
                Set(nameof(EveryXMinutesSelectedItem), ref everyXMinutesSelectedItem, value);
                RefreshMinuteCronExpression();
            }
        }

        public List<int> EveryXMinutesItems => Enumerable.Range(1, 10).ToList();

        private bool specificMinutes;

        public bool SpecificMinutes
        {
            get => specificMinutes;
            set
            {
                // SpecificMinute
                if (Set(nameof(SpecificMinutes), ref specificMinutes, value)) RefreshMinuteCronExpression();
            }
        }

        private bool minuteRange;

        public bool MinuteRange
        {
            get => minuteRange;
            set
            {
                // MinuteRange
                if (Set(nameof(MinuteRange), ref minuteRange, value)) RefreshMinuteCronExpression();
            }
        }



        private void RefreshMinuteRanges()
        {
            minuteRangeItems = Enumerable.Range(0, 60)
                .Select(ct => new ComboboxItem()
                {
                    Id = ct,
                    Description = ct.ToString()
                })
                .ToList();
            minuteRangeCheckedItems = Enumerable.Range(0, 60)
                .Select(ct => new CheckedItem(MinuteCheckChanged)
                {
                    Id = ct,
                    Description = ct.ToString()
                })
                .ToList();

            MinuteRangeStartSelectedItem = MinuteRangeItems.FirstOrDefault();
            MinuteRangeEndSelectedItem = MinuteRangeItems.FirstOrDefault();
            EveryXMinutesStartInSelectedItem = MinuteRangeItems.FirstOrDefault();

            OnPropertyChanged(nameof(MinuteRangeItems));
            OnPropertyChanged(nameof(MinuteRangeCheckedItems));
        }

        private void MinuteCheckChanged()
        {
            RefreshMinuteCronExpression();
        }

        private List<ComboboxItem> minuteRangeItems;
        public List<ComboboxItem> MinuteRangeItems => minuteRangeItems;

        private List<CheckedItem> minuteRangeCheckedItems;
        public List<CheckedItem> MinuteRangeCheckedItems => minuteRangeCheckedItems;



        private ComboboxItem everyXMinutesStartInSelectedItem;

        public ComboboxItem EveryXMinutesStartInSelectedItem
        {
            get { return everyXMinutesStartInSelectedItem; }
            set
            {
                // EveryXMinutesStartInSelectedItem
                Set(nameof(EveryXMinutesStartInSelectedItem), ref everyXMinutesStartInSelectedItem, value);
                RefreshMinuteCronExpression();
            }
        }

        private ComboboxItem minuteRangeStartSelectedItem;

        public ComboboxItem MinuteRangeStartSelectedItem
        {
            get { return minuteRangeStartSelectedItem; }
            set
            {
                // MinuteRangeStartSelectedItem
                Set(nameof(MinuteRangeStartSelectedItem), ref minuteRangeStartSelectedItem, value);
                RefreshMinuteCronExpression();
            }
        }

        private ComboboxItem minuteRangeEndSelectedItem;

        public ComboboxItem MinuteRangeEndSelectedItem
        {
            get { return minuteRangeEndSelectedItem; }
            set
            {
                // MinuteRangeEndSelectedItem
                Set(nameof(MinuteRangeEndSelectedItem), ref minuteRangeEndSelectedItem, value);
                RefreshMinuteCronExpression();
            }
        }

        private string minuteCronExpression;

        private void RefreshMinuteCronExpression()
        {
            if (AnyMinute)
                minuteCronExpression = "*";
            else if (EveryXMinutes)
                minuteCronExpression = $"{EveryXMinutesStartInSelectedItem.Id}/{EveryXMinutesSelectedItem}";
            else if (SpecificMinutes)
                minuteCronExpression = string.Join(",", MinuteRangeCheckedItems
                    .Where(ci => ci.IsChecked)
                    .Select(ci => ci.Id)
                );
            else if (MinuteRange)
                minuteCronExpression = $"{MinuteRangeStartSelectedItem.Id}-{MinuteRangeEndSelectedItem.Id}";

            OnPropertyChanged(nameof(CronExpression));
        }

        #endregion

        #region Second

        private bool anySecond = true;

        public bool AnySecond
        {
            get => anySecond;
            set
            {
                // AnySecond
                if (Set(nameof(AnySecond), ref anySecond, value)) RefreshSecondCronExpression();
            }
        }

        private bool everyXSeconds;

        public bool EveryXSeconds
        {
            get => everyXSeconds;
            set
            {
                // EveryXSeconds
                if (Set(nameof(EveryXSeconds), ref everyXSeconds, value)) RefreshSecondCronExpression();
            }
        }

        private int everyXSecondsSelectedItem = 1;

        public int EveryXSecondsSelectedItem
        {
            get { return everyXSecondsSelectedItem; }
            set
            {
                // EveryXSecondsSelectedItem
                Set(nameof(EveryXSecondsSelectedItem), ref everyXSecondsSelectedItem, value);
                RefreshSecondCronExpression();
            }
        }

        public List<int> EveryXSecondsItems => Enumerable.Range(1, 10).ToList();

        private bool specificSeconds;

        public bool SpecificSeconds
        {
            get => specificSeconds;
            set
            {
                // SpecificSecond
                if (Set(nameof(SpecificSeconds), ref specificSeconds, value)) RefreshSecondCronExpression();
            }
        }

        private bool secondRange;

        public bool SecondRange
        {
            get => secondRange;
            set
            {
                // SecondRange
                if (Set(nameof(SecondRange), ref secondRange, value)) RefreshSecondCronExpression();
            }
        }



        private void RefreshSecondRanges()
        {
            secondRangeItems = Enumerable.Range(0, 60)
                .Select(ct => new ComboboxItem()
                {
                    Id = ct,
                    Description = ct.ToString()
                })
                .ToList();
            secondRangeCheckedItems = Enumerable.Range(0, 60)
                .Select(ct => new CheckedItem(SecondCheckChanged)
                {
                    Id = ct,
                    Description = ct.ToString()
                })
                .ToList();

            SecondRangeStartSelectedItem = SecondRangeItems.FirstOrDefault();
            SecondRangeEndSelectedItem = SecondRangeItems.FirstOrDefault();
            EveryXSecondsStartInSelectedItem = SecondRangeItems.FirstOrDefault();

            OnPropertyChanged(nameof(SecondRangeItems));
            OnPropertyChanged(nameof(SecondRangeCheckedItems));
        }

        private void SecondCheckChanged()
        {
            RefreshSecondCronExpression();
        }

        private List<ComboboxItem> secondRangeItems;
        public List<ComboboxItem> SecondRangeItems => secondRangeItems;

        private List<CheckedItem> secondRangeCheckedItems;
        public List<CheckedItem> SecondRangeCheckedItems => secondRangeCheckedItems;



        private ComboboxItem everyXSecondsStartInSelectedItem;

        public ComboboxItem EveryXSecondsStartInSelectedItem
        {
            get { return everyXSecondsStartInSelectedItem; }
            set
            {
                // EveryXSecondsStartInSelectedItem
                Set(nameof(EveryXSecondsStartInSelectedItem), ref everyXSecondsStartInSelectedItem, value);
                RefreshSecondCronExpression();
            }
        }

        private ComboboxItem secondRangeStartSelectedItem;

        public ComboboxItem SecondRangeStartSelectedItem
        {
            get { return secondRangeStartSelectedItem; }
            set
            {
                // SecondRangeStartSelectedItem
                Set(nameof(SecondRangeStartSelectedItem), ref secondRangeStartSelectedItem, value);
                RefreshSecondCronExpression();
            }
        }

        private ComboboxItem secondRangeEndSelectedItem;

        public ComboboxItem SecondRangeEndSelectedItem
        {
            get { return secondRangeEndSelectedItem; }
            set
            {
                // SecondRangeEndSelectedItem
                Set(nameof(SecondRangeEndSelectedItem), ref secondRangeEndSelectedItem, value);
                RefreshSecondCronExpression();
            }
        }

        private string secondCronExpression;

        private void RefreshSecondCronExpression()
        {
            if (AnySecond)
                secondCronExpression = "*";
            else if (EveryXSeconds)
                secondCronExpression = $"{EveryXSecondsStartInSelectedItem.Id}/{EveryXSecondsSelectedItem}";
            else if (SpecificSeconds)
                secondCronExpression = string.Join(",", SecondRangeCheckedItems
                    .Where(ci => ci.IsChecked)
                    .Select(ci => ci.Id)
                );
            else if (SecondRange)
                secondCronExpression = $"{SecondRangeStartSelectedItem.Id}-{SecondRangeEndSelectedItem.Id}";

            OnPropertyChanged(nameof(CronExpression));
        }

        #endregion
    }
}