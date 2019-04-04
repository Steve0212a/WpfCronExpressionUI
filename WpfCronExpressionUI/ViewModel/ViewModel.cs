using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WpfCronExpressionUI.ViewModel
{
    internal class ViewModel : ViewModelBase
    {
        public string CronExpression => $"Cron Expression: {YearCronExpression}";

        public ViewModel()
        {
            RefreshYearCronExpression();
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
    }
}