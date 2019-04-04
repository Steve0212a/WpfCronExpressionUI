using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WpfCronExpressionUI.ViewModel
{
    internal class ViewModel : ViewModelBase
    {
        public string CronExpression => $"Cron Expression: {SecondsCronExpression} {MinutesCronExpression} {HourCronExpression} {DayOfMonthCronExpression} {MonthCronExpression} {DayOfWeekCronExpression} {YearCronExpression}";

        private string DayOfWeekCronExpression = "*";
        private string DayOfMonthCronExpression = "*";
        private string MinutesCronExpression = "*";
        private string SecondsCronExpression = "*";

        public ViewModel()
        {
            RefreshYearCronExpression();
            RefreshMonthRanges();
            RefreshHourRanges();
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

        private string YearCronExpression;

        private void RefreshYearCronExpression()
        {
            if (AnyYear)
                YearCronExpression = "*";
            else if (EveryXYears)
                YearCronExpression = $"{EveryXYearsStartInSelectedItem}/{EveryXYearsSelectedItem}";
            else if (SpecificYears)
                YearCronExpression = string.Join(",", YearRangeCheckedItems
                    .Where(ci => ci.IsChecked)
                    .Select(ci => ci.Id)
                );
            else if (YearRange)
                YearCronExpression = $"{YearRangeStartSelectedItem}-{YearRangeEndSelectedItem}";

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

        private string MonthCronExpression;

        private void RefreshMonthCronExpression()
        {
            if (AnyMonth)
                MonthCronExpression = "*";
            else if (EveryXMonths)
                MonthCronExpression = $"{EveryXMonthsStartInSelectedItem.Id}/{EveryXMonthsSelectedItem}";
            else if (SpecificMonths)
                MonthCronExpression = string.Join(",", MonthRangeCheckedItems
                    .Where(ci => ci.IsChecked)
                    .Select(ci => ci.Id)
                );
            else if (MonthRange)
                MonthCronExpression = $"{MonthRangeStartSelectedItem.Id}-{MonthRangeEndSelectedItem.Id}";

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

        private string HourCronExpression;

        private void RefreshHourCronExpression()
        {
            if (AnyHour)
                HourCronExpression = "*";
            else if (EveryXHours)
                HourCronExpression = $"{EveryXHoursStartInSelectedItem.Id}/{EveryXHoursSelectedItem}";
            else if (SpecificHours)
                HourCronExpression = string.Join(",", HourRangeCheckedItems
                    .Where(ci => ci.IsChecked)
                    .Select(ci => ci.Id)
                );
            else if (HourRange)
                HourCronExpression = $"{HourRangeStartSelectedItem.Id}-{HourRangeEndSelectedItem.Id}";

            OnPropertyChanged(nameof(CronExpression));
        }

        #endregion
    }
}