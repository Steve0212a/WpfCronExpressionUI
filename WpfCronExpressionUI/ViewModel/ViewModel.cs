using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace WpfCronExpressionUI.ViewModel
{
    internal class ViewModel : ViewModelBase
    {
        private string cronExpression;

        public string CronExpression
        {
            get { return $"{secondCronExpression} {minuteCronExpression} {hourCronExpression} {dayOfMonthCronExpression} {monthCronExpression} {dayOfWeekCronExpression} {yearCronExpression}"; }
            set
            {
                ParseCronExpression(value);
                OnPropertyChanged(nameof(CronExpression));
            }
        }

        public ViewModel()
        {
            RefreshYearCronExpression();
            RefreshYearRanges();
            RefreshMonthRanges();
            RefreshHourRanges();
            RefreshMinuteRanges();
            RefreshSecondRanges();
            RefreshDayRanges();
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


            // clear errors since we are rebuilding the expression
            ErrorMessage = null;
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
                    .Select(ci => GetMonthAbbreviationFromIndex((int)ci.Id))
                );
            else if (MonthRange)
            {
                var month1 = GetMonthAbbreviationFromIndex((int)MonthRangeStartSelectedItem.Id);
                var month2 = GetMonthAbbreviationFromIndex((int)MonthRangeEndSelectedItem.Id);
                monthCronExpression = $"{month1}-{month2}";
            }

            // clear errors since we are rebuilding the expression
            ErrorMessage = null;
            OnPropertyChanged(nameof(CronExpression));
        }

        #endregion

        #region Day

        private bool anyDay = true;

        public bool AnyDay
        {
            get => anyDay;
            set
            {
                // AnyDay
                if (Set(nameof(AnyDay), ref anyDay, value)) RefreshDayCronExpression();
            }
        }

        private bool everyXWeekDays;

        public bool EveryXWeekDays
        {
            get => everyXWeekDays;
            set
            {
                // EveryXWeekDays
                if (Set(nameof(EveryXWeekDays), ref everyXWeekDays, value)) RefreshDayCronExpression();
            }
        }

        private bool everyXMonthDays;

        public bool EveryXMonthDays
        {
            get { return everyXMonthDays; }
            set
            {
                // EveryXMonthDays
                Set(nameof(EveryXMonthDays), ref everyXMonthDays, value);
                RefreshDayCronExpression();
            }
        }

        private int everyXWeekDaysSelectedItem = 1;

        public int EveryXWeekDaysSelectedItem
        {
            get { return everyXWeekDaysSelectedItem; }
            set
            {
                // EveryXWeekDaysSelectedItem
                Set(nameof(EveryXWeekDaysSelectedItem), ref everyXWeekDaysSelectedItem, value);
                RefreshDayCronExpression();
            }
        }

        private int everyXMonthDaysSelectedItem;

        public int EveryXMonthDaysSelectedItem
        {
            get { return everyXMonthDaysSelectedItem; }
            set
            {
                // EveryXMonthDaysSelectedItem
                Set(nameof(EveryXMonthDaysSelectedItem), ref everyXMonthDaysSelectedItem, value);
                RefreshDayCronExpression();
            }
        }

        public List<int> EveryXWeekDaysItems => Enumerable.Range(1, 7).ToList();
        public List<int> EveryXMonthDaysItems => Enumerable.Range(1, 31).ToList();
        

        private bool specificDaysOfWeeks;

        public bool SpecificDaysOfWeeks
        {
            get => specificDaysOfWeeks;
            set
            {
                // SpecificDay
                if (Set(nameof(SpecificDaysOfWeeks), ref specificDaysOfWeeks, value)) RefreshDayCronExpression();
            }
        }

        private bool specificDaysOfMonths;

        public bool SpecificDaysOfMonths
        {
            get { return specificDaysOfMonths; }
            set
            {
                // SpecificDaysOfMonths
                Set(nameof(SpecificDaysOfMonths), ref specificDaysOfMonths, value);
                RefreshDayCronExpression();
            }
        }

        private bool lastDayOfMonth;

        public bool LastDayOfMonth
        {
            get { return lastDayOfMonth; }
            set
            {
                // LastDayOfMonth
                Set(nameof(LastDayOfMonth), ref lastDayOfMonth, value);
                RefreshDayCronExpression();
            }
        }

        private bool lastWeekDayOfMonth;

        public bool LastWeekDayOfMonth
        {
            get { return lastWeekDayOfMonth; }
            set
            {
                // LastWeekDayOfMonth
                Set(nameof(LastWeekDayOfMonth), ref lastWeekDayOfMonth, value);
                RefreshDayCronExpression();
            }
        }

        private bool lastXDayOfMonth;

        public bool LastXDayOfMonth
        {
            get { return lastXDayOfMonth; }
            set
            {
                // LastXDayOfMonth
                Set(nameof(LastXDayOfMonth), ref lastXDayOfMonth, value);
                RefreshDayCronExpression();
            }
        }

        private bool daysBeforeEndOfMonth;

        public bool DaysBeforeEndOfMonth
        {
            get { return daysBeforeEndOfMonth; }
            set
            {
                // DaysBeforeEndOfMonth
                Set(nameof(DaysBeforeEndOfMonth), ref daysBeforeEndOfMonth, value);
                RefreshDayCronExpression();
            }
        }

        private void RefreshDayRanges()
        {
            dayRangeItems = Enumerable.Range(0, 7)
                .Select(ct => new ComboboxItem()
                {
                    Id = ct + 1,    // cron days of week are 1-7
                    Description = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName((DayOfWeek)ct)
                })
                .ToList();
            dayOfWeekRangeCheckedItems = Enumerable.Range(0, 7)
                .Select(ct => new CheckedItem(DayCheckChanged)
                {
                    Id = ct + 1,    // cron days of week are 1-7
                    Description = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName((DayOfWeek)ct)
                })
                .ToList();

            dayOfMonthRangeItems = Enumerable.Range(1, 31)
                .Select(ct =>
                {
                    var suffix = (ct % 10 == 1 && ct != 11) ? "st"
                        : (ct % 10 == 2 && ct != 12) ? "nd"
                        : (ct % 10 == 3 && ct != 13) ? "rd"
                        : "th";
                    return new ComboboxItem()
                    {
                        Id = ct,
                        Description = $"{ct}{suffix}"
                    };
                })
                .ToList();

            dayOfMonthRangeCheckedItems = Enumerable.Range(1, 31)
                .Select(ct => new CheckedItem(DayCheckChanged)
                {
                    Id = ct,
                    Description = ct.ToString()
                })
                .ToList();

            EveryXWeekDaysStartInSelectedItem = DayRangeItems.FirstOrDefault();
            EveryXMonthDaysSelectedItem = EveryXMonthDaysItems.FirstOrDefault();
            EveryXMonthDaysStartInSelectedItem = DayOfMonthRangeItems.FirstOrDefault();
            LastXDayOfMonthSelectedItem = DayRangeItems.FirstOrDefault();
            DaysBeforeEndOfMonthSelectedItem = EveryXMonthDaysItems.FirstOrDefault();

            OnPropertyChanged(nameof(DayRangeItems));
            OnPropertyChanged(nameof(DayOfWeekRangeCheckedItems));
            OnPropertyChanged(nameof(DayOfMonthRangeItems));
        }

        private void DayCheckChanged()
        {
            RefreshDayCronExpression();
        }

        private List<ComboboxItem> dayRangeItems;
        public List<ComboboxItem> DayRangeItems => dayRangeItems;

        private List<CheckedItem> dayOfWeekRangeCheckedItems;
        public List<CheckedItem> DayOfWeekRangeCheckedItems => dayOfWeekRangeCheckedItems;

        private List<ComboboxItem> dayOfMonthRangeItems;
        public List<ComboboxItem> DayOfMonthRangeItems => dayOfMonthRangeItems;

        private List<CheckedItem> dayOfMonthRangeCheckedItems;
        public List<CheckedItem> DayOfMonthRangeCheckedItems => dayOfMonthRangeCheckedItems;


        private ComboboxItem lastXDayOfMonthSelectedItem;

        public ComboboxItem LastXDayOfMonthSelectedItem
        {
            get { return lastXDayOfMonthSelectedItem; }
            set
            {
                // LastXDayOfMonthSelectedItem
                Set(nameof(LastXDayOfMonthSelectedItem), ref lastXDayOfMonthSelectedItem, value);
                RefreshDayCronExpression();
            }
        }

        private int daysBeforeEndOfMonthSelectedItem;

        public int DaysBeforeEndOfMonthSelectedItem
        {
            get { return daysBeforeEndOfMonthSelectedItem; }
            set
            {
                // DaysBeforeEndOfMonthSelectedItem
                Set(nameof(DaysBeforeEndOfMonthSelectedItem), ref daysBeforeEndOfMonthSelectedItem, value);
                RefreshDayCronExpression();
            }
        }

        private ComboboxItem everyXWeekDaysStartInSelectedItem;

        public ComboboxItem EveryXWeekDaysStartInSelectedItem
        {
            get { return everyXWeekDaysStartInSelectedItem; }
            set
            {
                // EveryXWeekDaysStartInSelectedItem
                Set(nameof(EveryXWeekDaysStartInSelectedItem), ref everyXWeekDaysStartInSelectedItem, value);
                RefreshDayCronExpression();
            }
        }

        private ComboboxItem everyXMonthDaysStartInSelectedItem;

        public ComboboxItem EveryXMonthDaysStartInSelectedItem
        {
            get { return everyXMonthDaysStartInSelectedItem; }
            set
            {
                // EveryXMonthDaysStartInSelectedItem
                Set(nameof(EveryXMonthDaysStartInSelectedItem), ref everyXMonthDaysStartInSelectedItem, value);
                RefreshDayCronExpression();
            }
        }

        private string dayOfWeekCronExpression;
        private string dayOfMonthCronExpression;

        private void RefreshDayCronExpression()
        {
            if (AnyDay)
            {
                dayOfWeekCronExpression = "*";
                dayOfMonthCronExpression = "?";
            }
            else if (EveryXWeekDays)
            {
                dayOfWeekCronExpression = $"{EveryXWeekDaysStartInSelectedItem.Id}/{EveryXWeekDaysSelectedItem}";
                dayOfMonthCronExpression = "?";
            }
            else if (EveryXMonthDays)
            {
                dayOfWeekCronExpression = "?";
                dayOfMonthCronExpression = $"{EveryXMonthDaysStartInSelectedItem.Id}/{EveryXMonthDaysSelectedItem}";
            }
            else if (SpecificDaysOfWeeks)
            {
                dayOfWeekCronExpression = string.Join(",", DayOfWeekRangeCheckedItems
                    .Where(ci => ci.IsChecked)
                    .Select(ci => GetDayOfWeekAbbreviationFromIndex((int)ci.Id))
                );
                dayOfMonthCronExpression = "?";
            }
            else if (SpecificDaysOfMonths)
            {
                dayOfWeekCronExpression = "?";
                dayOfMonthCronExpression = string.Join(",", DayOfMonthRangeCheckedItems
                    .Where(ci => ci.IsChecked)
                    .Select(ci => ci.Id)
                );
            }
            else if (LastDayOfMonth)
            {
                dayOfWeekCronExpression = "?";
                dayOfMonthCronExpression = "L";
            }
            else if (LastWeekDayOfMonth)
            {
                dayOfWeekCronExpression = "?";
                dayOfMonthCronExpression = "LW";
            }
            else if (LastXDayOfMonth)
            {
                dayOfWeekCronExpression = $"{LastXDayOfMonthSelectedItem.Id}L";
                dayOfMonthCronExpression = "?";
            }
            else if (DaysBeforeEndOfMonth)
            {
                dayOfWeekCronExpression = "?";
                dayOfMonthCronExpression = $"L-{DaysBeforeEndOfMonthSelectedItem}";
            }
            else
            {
                dayOfWeekCronExpression = "TODO";
                dayOfMonthCronExpression = "TODO";
            }

            // clear errors since we are rebuilding the expression
            ErrorMessage = null;
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

            // clear errors since we are rebuilding the expression
            ErrorMessage = null;
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

            // clear errors since we are rebuilding the expression
            ErrorMessage = null;
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

            // clear errors since we are rebuilding the expression
            ErrorMessage = null;
            OnPropertyChanged(nameof(CronExpression));
        }

        #endregion

        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                // ErrorMessage
                Set(nameof(ErrorMessage), ref errorMessage, value);
            }
        }

        #region Parse Expression

        private void ParseCronExpression(string expression)
        {
            try
            {
                // fill in blank expression if none is specified
                if (string.IsNullOrWhiteSpace(expression))
                    expression = "0 0 0 ? * * *";

                // six parts = no year, seven parts includes year
                var parts = expression.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                if ((parts.Length != 6) && (parts.Length != 7))
                    throw new Exception("Incorrent number of parts in Cron expression");


                // parse the parts
                ParseSecondsPart(parts[0]);
                ParseMinutesPart(parts[1]);
                ParseHoursPart(parts[2]);
                ParseDayOfMonthPart(parts[3]);
                ParseMonthsPart(parts[4]);
                ParseDayOfWeeksPart(parts[5]);
                ParseYearsPart(parts.Length == 7 ? parts[6] : "*");

                // no errors, good to go
                ErrorMessage = null;
                OnPropertyChanged(nameof(CronExpression));
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }

        private void ParseSecondsPart(string part)
        {
            const string partName = "Seconds";
            if (part == "*")
                AnySecond = true;
            else if (IsSlashedPart(part, partName, out var slashedPart1, out var slashedPart2))
            {
                EveryXSeconds = true;
                EveryXSecondsSelectedItem = slashedPart2;
                var item = SecondRangeItems.FirstOrDefault(s => s.Id == slashedPart1);
                EveryXSecondsStartInSelectedItem = item ?? throw new Exception($"Unable to parse {partName}");
            }
            else if (IsDashedPartLongs(part, partName, out var dashedPart1, out var dashedPart2))
            {
                SecondRange = true;
                var itemStart = SecondRangeItems.FirstOrDefault(s => s.Id == dashedPart1);
                var itemEnd = SecondRangeItems.FirstOrDefault(s => s.Id == dashedPart2);
                SecondRangeStartSelectedItem = itemStart ?? throw new Exception($"Unable to parse {partName}");
                SecondRangeEndSelectedItem = itemEnd ?? throw new Exception($"Unable to parse {partName}");
            }
            else
            {
                var selectedSeconds = GetListOfLongsFromCommaSeparatedList(part, partName);

                // specific Seconds
                SpecificSeconds = true;
                SecondRangeCheckedItems.ForEach(sci => { sci.IsChecked = selectedSeconds.Contains((int) sci.Id); });
            }
        }

        private void ParseMinutesPart(string part)
        {
            const string partName = "Minutes";
            if (part == "*")
                AnyMinute = true;
            else if (IsSlashedPart(part, partName, out var slashedPart1, out var slashedPart2))
            {
                EveryXMinutes = true;
                EveryXMinutesSelectedItem = slashedPart2;
                var item = MinuteRangeItems.FirstOrDefault(s => s.Id == slashedPart1);
                EveryXMinutesStartInSelectedItem = item ?? throw new Exception($"Unable to parse {partName}");
            }
            else if (IsDashedPartLongs(part, partName, out var dashedPart1, out var dashedPart2))
            {
                MinuteRange = true;
                var itemStart = MinuteRangeItems.FirstOrDefault(s => s.Id == dashedPart1);
                var itemEnd = MinuteRangeItems.FirstOrDefault(s => s.Id == dashedPart2);
                MinuteRangeStartSelectedItem = itemStart ?? throw new Exception($"Unable to parse {partName}");
                MinuteRangeEndSelectedItem = itemEnd ?? throw new Exception($"Unable to parse {partName}");
            }
            else
            {
                var selectedMinutes = GetListOfLongsFromCommaSeparatedList(part, partName);

                // specific Minutes
                SpecificMinutes = true;
                MinuteRangeCheckedItems.ForEach(sci => { sci.IsChecked = selectedMinutes.Contains((int)sci.Id); });
            }
        }

        private void ParseHoursPart(string part)
        {
            const string partName = "Hours";
            if (part == "*")
                AnyHour = true;
            else if (IsSlashedPart(part, partName, out var slashedPart1, out var slashedPart2))
            {
                EveryXHours = true;
                EveryXHoursSelectedItem = slashedPart2;
                var item = HourRangeItems.FirstOrDefault(s => s.Id == slashedPart1);
                EveryXHoursStartInSelectedItem = item ?? throw new Exception($"Unable to parse {partName}");
            }
            else if (IsDashedPartLongs(part, partName, out var dashedPart1, out var dashedPart2))
            {
                HourRange = true;
                var itemStart = HourRangeItems.FirstOrDefault(s => s.Id == dashedPart1);
                var itemEnd = HourRangeItems.FirstOrDefault(s => s.Id == dashedPart2);
                HourRangeStartSelectedItem = itemStart ?? throw new Exception($"Unable to parse {partName}");
                HourRangeEndSelectedItem = itemEnd ?? throw new Exception($"Unable to parse {partName}");
            }
            else
            {
                var selectedHours = GetListOfLongsFromCommaSeparatedList(part, partName);

                // specific Hours
                SpecificHours = true;
                HourRangeCheckedItems.ForEach(sci => { sci.IsChecked = selectedHours.Contains((int)sci.Id); });
            }
        }

        private void ParseDayOfMonthPart(string part)
        {
            if (part == "?")
                return;

            const string partName = "Day Of Month";
            if (part == "*")
                AnyDay = true;
            else if (IsSlashedPart(part, partName, out var slashedPart1, out var slashedPart2))
            {
                EveryXMonthDays = true;
                EveryXMonthDaysSelectedItem = slashedPart2;
                var item = DayOfMonthRangeItems.FirstOrDefault(s => s.Id == slashedPart1);
                EveryXMonthDaysStartInSelectedItem = item ?? throw new Exception($"Unable to parse {partName}");
            }
            // TODO
            //else if (IsDashedPartLongs(part, partName, out var dashedPart1, out var dashedPart2))
            //{
            //    DayOfMonthRange = true;
            //    var itemStart = DayOfMonthRangeItems.FirstOrDefault(s => s.Id == dashedPart1);
            //    var itemEnd = DayOfMonthRangeItems.FirstOrDefault(s => s.Id == dashedPart2);
            //    DayOfMonthRangeStartSelectedItem = itemStart ?? throw new Exception($"Unable to parse {partName}");
            //    DayOfMonthRangeEndSelectedItem = itemEnd ?? throw new Exception($"Unable to parse {partName}");
            //}
            else if ("L".Equals(part, StringComparison.InvariantCultureIgnoreCase))
            {
                // last day of month
                LastDayOfMonth = true;
            }
            else if ("LW".Equals(part, StringComparison.InvariantCultureIgnoreCase))
            {
                // last day of month
                LastWeekDayOfMonth = true;
            }
            else if (part.StartsWith("L-") || part.Contains("l-"))
            {
                int daysBefore = 0;
                try
                {
                    // x days before last day of month...
                    daysBefore = int.Parse(part.Substring(2));
                }
                catch
                {
                    throw new Exception($"Unable to parse \"{partName}\"");
                }

                // validate item exists in combo box
                if (!EveryXMonthDaysItems.Contains(daysBefore))
                    throw new Exception($"Unable to parse \"{partName}\" - days before not in list");

                // set it
                DaysBeforeEndOfMonth = true;
                DaysBeforeEndOfMonthSelectedItem = daysBefore;
            }
            else
            {
                var selectedDaysOfMonth = GetListOfLongsFromCommaSeparatedList(part, partName);

                // specific days of the month
                SpecificDaysOfMonths = true;
                DayOfMonthRangeCheckedItems.ForEach(domci => { domci.IsChecked = selectedDaysOfMonth.Contains((int)domci.Id); });
            }
        }

        private void ParseMonthsPart(string part)
        {
            const string partName = "Months";
            if (part == "*")
                AnyMonth = true;
            else if (IsSlashedPart(part, partName, out var slashedPart1, out var slashedPart2))
            {
                EveryXMonths = true;
                EveryXMonthsSelectedItem = slashedPart2;
                var item = MonthRangeItems.FirstOrDefault(s => s.Id == slashedPart1);
                EveryXMonthsStartInSelectedItem = item ?? throw new Exception($"Unable to parse {partName}");
            }
            else if (part.IndexOf('-') >= 0)
            {
                if (IsDashedPartLongs(part, partName, out var dashedPart1, out var dashedPart2, false))
                {
                    MonthRange = true;
                    var itemStart = MonthRangeItems.FirstOrDefault(s => s.Id == dashedPart1);
                    var itemEnd = MonthRangeItems.FirstOrDefault(s => s.Id == dashedPart2);
                    MonthRangeStartSelectedItem = itemStart ?? throw new Exception($"Unable to parse {partName}");
                    MonthRangeEndSelectedItem = itemEnd ?? throw new Exception($"Unable to parse {partName}");
                } 
                else if (IsDashedPartStrings(part, partName, out var dashedStringPart1, out var dashedStringPart2))
                {
                    MonthRange = true;
                    var itemStartIndex = GetMonthIndexFromName(dashedStringPart1);
                    var itemEndIndex = GetMonthIndexFromName(dashedStringPart2);
                    var itemStart = MonthRangeItems.FirstOrDefault(s => s.Id == itemStartIndex);
                    var itemEnd = MonthRangeItems.FirstOrDefault(s => s.Id == itemEndIndex);
                    MonthRangeStartSelectedItem = itemStart ?? throw new Exception($"Unable to parse {partName}");
                    MonthRangeEndSelectedItem = itemEnd ?? throw new Exception($"Unable to parse {partName}");
                }
                else
                {
                    throw new Exception($"Invalid expression in {partName}");
                }
            }
            else
            {
                SpecificMonths = true;
                List<int> selectedMonthIndexes;
                try
                {
                    // assume we have a list of numbers
                    selectedMonthIndexes = GetListOfLongsFromCommaSeparatedList(part, partName)
                        .Select(m => (int)m)
                        .ToList();
                }
                catch
                {
                    // not a list of numbers, try a list of month names
                    selectedMonthIndexes = GetListOfStringsFromCommaSeparatedList(part, partName)
                            .Select(GetMonthIndexFromName)
                            .ToList();
                }

                // validate
                if (selectedMonthIndexes.Any(m => m < 1 || m > 12))
                {
                    throw new Exception($"Invalid expression in {partName}");
                }

                // specific Months
                MonthRangeCheckedItems.ForEach(sci => { sci.IsChecked = selectedMonthIndexes.Contains((int)sci.Id); });
            }
        }

        private void ParseDayOfWeeksPart(string part)
        {
            if (part == "?")
                return;

            const string partName = "Day Of Month";
            if (part == "*")
                AnyDay = true;
            else if (IsSlashedPart(part, partName, out var slashedPart1, out var slashedPart2))
            {
                EveryXWeekDays = true;
                EveryXWeekDaysSelectedItem = slashedPart2;
                var item = DayRangeItems.FirstOrDefault(s => s.Id == slashedPart1);
                EveryXWeekDaysStartInSelectedItem = item ?? throw new Exception($"Unable to parse {partName}");
            }
            else if (part.ToUpper().EndsWith("L"))
            {
                int dayOfWeek = 0;
                try
                {
                    // x days before last day of month...
                    dayOfWeek = int.Parse(part.Substring(0,part.Length-1));
                }
                catch
                {
                    throw new Exception($"Unable to parse \"{partName}\"");
                }

                var item = DayRangeItems.FirstOrDefault(s => s.Id == dayOfWeek);
                LastXDayOfMonthSelectedItem = item ?? throw new Exception($"Unable to parse {partName}");

                LastXDayOfMonth = true;
            }
            else
            {
                SpecificDaysOfWeeks = true;
                List<int> selectedDayOfWeekIndexes;
                try
                {
                    // assume we have a list of numbers
                    selectedDayOfWeekIndexes = GetListOfLongsFromCommaSeparatedList(part, partName)
                        .Select(m => (int)m)
                        .ToList();
                }
                catch
                {
                    // not a list of numbers, try a list of month names
                    selectedDayOfWeekIndexes = GetListOfStringsFromCommaSeparatedList(part, partName)
                        .Select(GetDayIndexFromName)
                        .ToList();
                }

                // validate
                if (selectedDayOfWeekIndexes.Any(m => m < 1 || m > 7))
                {
                    throw new Exception($"Invalid expression in {partName}");
                }

                // specific Months
                DayOfWeekRangeCheckedItems.ForEach(dci => { dci.IsChecked = selectedDayOfWeekIndexes.Contains((int)dci.Id); });
            }
        }

        private void ParseYearsPart(string part)
        {
            const string partName = "Years";
            if (part == "*")
                AnyYear = true;
            else if (IsSlashedPart(part, partName, out var slashedPart1, out var slashedPart2))
            {
                EveryXYears = true;
                EveryXYearsSelectedItem = slashedPart2;
                EveryXYearsStartInSelectedItem = slashedPart1;
            }
            else if (IsDashedPartLongs(part, partName, out var dashedPart1, out var dashedPart2))
            {
                YearRange = true;
                YearRangeStartSelectedItem = dashedPart1;
                YearRangeEndSelectedItem = dashedPart2;
            }
            else
            {
                var selectedYears = GetListOfLongsFromCommaSeparatedList(part, partName);

                // specific Years
                SpecificYears = true;
                YearRangeCheckedItems.ForEach(sci => { sci.IsChecked = selectedYears.Contains((int)sci.Id); });
            }
        }

        /// <summary>
        /// look for 5/6 syntax
        /// </summary>
        /// <param name="part"></param>
        /// <param name="partName"></param>
        /// <param name="part1"></param>
        /// <param name="part2"></param>
        /// <returns></returns>
        private bool IsSlashedPart(string part, string partName, out int part1, out int part2)
        {
            // initialize
            part1 = 0;
            part2 = 0;

            var parts = part.Split(new[] { '/', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1)
                return false;
            if (parts.Length != 2)
                throw new Exception($"Invalid expression in {partName}");

            // two parts, try to parse
            if (!int.TryParse(parts[0], out part1))
                throw new Exception($"Unable to parse part 1 of \"{partName}\"");
            if (!int.TryParse(parts[1], out part2))
                throw new Exception($"Unable to parse part 2 of \"{partName}\"");
            return true;
        }

        /// <summary>
        /// look for 5-6 syntax
        /// </summary>
        /// <param name="part"></param>
        /// <param name="partName"></param>
        /// <param name="part1"></param>
        /// <param name="part2"></param>
        /// <returns></returns>
        private bool IsDashedPartLongs(string part, string partName, out int part1, out int part2, bool throwException = true)
        {
            // initialize
            part1 = 0;
            part2 = 0;

            var parts = part.Split(new[] { '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1)
                return false;
            if (parts.Length != 2)
            {
                if (throwException)
                    throw new Exception($"Invalid expression in {partName}");
                else
                    return false;
            }

            // two parts, try to parse
            if (!int.TryParse(parts[0], out part1))
            {
                if (throwException)
                    throw new Exception($"Unable to parse part 1 of \"{partName}\"");
                else
                    return false;
            }
            if (!int.TryParse(parts[1], out part2))
            {
                if (throwException)
                    throw new Exception($"Unable to parse part 2 of \"{partName}\"");
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        /// look for 5-6 syntax
        /// </summary>
        /// <param name="part"></param>
        /// <param name="partName"></param>
        /// <param name="part1"></param>
        /// <param name="part2"></param>
        /// <returns></returns>
        private bool IsDashedPartStrings(string part, string partName, out string part1, out string part2)
        {
            // initialize
            part1 = null;
            part2 = null;

            var parts = part.Split(new[] { '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1)
                return false;
            if (parts.Length != 2)
                throw new Exception($"Invalid expression in {partName}");

            part1 = parts[0];
            part2 = parts[1];

            return true;
        }

        /// <summary>
        /// take a comma separated list and convert them to a list of longs
        /// </summary>
        /// <param name="part"></param>
        /// <param name="partName"></param>
        /// <returns></returns>
        private List<long> GetListOfLongsFromCommaSeparatedList(string part, string partName)
        {
            try
            {
                var parts = part.Split(new[] { ',' });
                return parts
                    .Select(p => long.Parse(p))
                    .ToList();
            }
            catch
            {
                throw new Exception($"Unable to parse numbers of \"{partName}\"");
            }
        }

        /// <summary>
        /// take a comma separated list of strings
        /// </summary>
        /// <param name="part"></param>
        /// <param name="partName"></param>
        /// <returns></returns>
        private List<string> GetListOfStringsFromCommaSeparatedList(string part, string partName)
        {
            try
            {
                return part
                    .Split(new[] { ',' })
                    .ToList();
            }
            catch
            {
                throw new Exception($"Unable to parse strings of \"{partName}\"");
            }
        }

        private readonly string[] monthNames = CultureInfo.InvariantCulture.DateTimeFormat.AbbreviatedMonthNames
            .Where(mn => !string.IsNullOrWhiteSpace(mn))
            .Select(mn => mn.ToUpper())
            .ToArray();

        private int GetMonthIndexFromName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException();
            return Array.IndexOf(monthNames, name.ToUpper()) + 1;
        }

        private string GetMonthAbbreviationFromIndex(int index)
        {
            try
            {
                return monthNames[index - 1];
            }
            catch
            {
                return null;
            }
        }

        private readonly string[] dayNames = CultureInfo.InvariantCulture.DateTimeFormat.AbbreviatedDayNames
            .Where(dn => !string.IsNullOrWhiteSpace(dn))
            .Select(dn => dn.ToUpper())
            .ToArray();

        private int GetDayIndexFromName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException();
            return Array.IndexOf(dayNames, name.ToUpper()) + 1;
        }

        private string GetDayOfWeekAbbreviationFromIndex(int index)
        {
            try
            {
                return dayNames[index - 1];
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}