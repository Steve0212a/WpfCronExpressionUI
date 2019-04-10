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
                // TODO - update control to Cron Express set by this setter
            }
        }

        public ViewModel()
        {
            RefreshYearCronExpression();
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
                    .Select(ci => ci.Id)
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