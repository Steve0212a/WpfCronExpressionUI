using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfCronExpressionUI
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class WpfCronExpressionUIView
    {
        #region Styles

        public static readonly DependencyProperty TabControlStyleProperty = DependencyProperty.Register(
            "TabControlStyle", typeof(Style), typeof(WpfCronExpressionUIView), new PropertyMetadata(default(Style)));

        public Style TabControlStyle
        {
            get { return (Style) GetValue(TabControlStyleProperty); }
            set { SetValue(TabControlStyleProperty, value); }
        }

        public static readonly DependencyProperty TabItemStyleProperty = DependencyProperty.Register(
            "TabItemStyle", typeof(Style), typeof(WpfCronExpressionUIView), new PropertyMetadata(default(Style)));

        public Style TabItemStyle
        {
            get { return (Style) GetValue(TabItemStyleProperty); }
            set { SetValue(TabItemStyleProperty, value); }
        }


        public static readonly DependencyProperty RadioButtonStyleProperty = DependencyProperty.Register(
            "RadioButtonStyle", typeof(Style), typeof(WpfCronExpressionUIView), new PropertyMetadata(default(Style)));

        public Style RadioButtonStyle
        {
            get { return (Style)GetValue(RadioButtonStyleProperty); }
            set { SetValue(RadioButtonStyleProperty, value); }
        }

        public static readonly DependencyProperty TextBlockStyleProperty = DependencyProperty.Register(
            "TextBlockStyle", typeof(Style), typeof(WpfCronExpressionUIView), new PropertyMetadata(default(Style)));

        public Style TextBlockStyle
        {
            get { return (Style)GetValue(TextBlockStyleProperty); }
            set { SetValue(TextBlockStyleProperty, value); }
        }

        public static readonly DependencyProperty ComboboxStyleProperty = DependencyProperty.Register(
            "ComboboxStyle", typeof(Style), typeof(WpfCronExpressionUIView), new PropertyMetadata(default(Style)));

        public Style ComboboxStyle
        {
            get { return (Style)GetValue(ComboboxStyleProperty); }
            set { SetValue(ComboboxStyleProperty, value); }
        }

        public static readonly DependencyProperty CheckboxStyleProperty = DependencyProperty.Register(
            "CheckboxStyle", typeof(Style), typeof(WpfCronExpressionUIView), new PropertyMetadata(default(Style)));

        public Style CheckboxStyle
        {
            get { return (Style)GetValue(CheckboxStyleProperty); }
            set { SetValue(CheckboxStyleProperty, value); }
        }

        #endregion

        #region Flags

        public static readonly DependencyProperty ShowCronExpressionProperty = DependencyProperty.Register(
            "ShowCronExpression", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(default(bool)));

        public bool ShowCronExpression
        {
            get { return (bool) GetValue(ShowCronExpressionProperty); }
            set { SetValue(ShowCronExpressionProperty, value); }
        }

        public static readonly DependencyProperty ShowYearTabProperty = DependencyProperty.Register(
            "ShowYearTab", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true, OnShowYearTabChanged));

        private static void OnShowYearTabChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WpfCronExpressionUIView view)
            {
                view.SelectFirstVisibleTab();
            }
        }

        #region Year Flags

        public bool ShowYearTab
        {
            get { return (bool) GetValue(ShowYearTabProperty); }
            set { SetValue(ShowYearTabProperty, value); }
        }

        public static readonly DependencyProperty ShowYearsEveryXYearsProperty = DependencyProperty.Register(
            "ShowYearsEveryXYears", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowYearsEveryXYears
        {
            get { return (bool) GetValue(ShowYearsEveryXYearsProperty); }
            set { SetValue(ShowYearsEveryXYearsProperty, value); }
        }

        public static readonly DependencyProperty ShowYearsSpecificYearsProperty = DependencyProperty.Register(
            "ShowYearsSpecificYears", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowYearsSpecificYears
        {
            get { return (bool) GetValue(ShowYearsSpecificYearsProperty); }
            set { SetValue(ShowYearsSpecificYearsProperty, value); }
        }

        public static readonly DependencyProperty ShowYearsYearRangeProperty = DependencyProperty.Register(
            "ShowYearsYearRange", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowYearsYearRange
        {
            get { return (bool) GetValue(ShowYearsYearRangeProperty); }
            set { SetValue(ShowYearsYearRangeProperty, value); }
        }

        #endregion

        #region Month Flags

        public static readonly DependencyProperty ShowMonthTabProperty = DependencyProperty.Register(
            "ShowMonthTab", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowMonthTab
        {
            get { return (bool) GetValue(ShowMonthTabProperty); }
            set { SetValue(ShowMonthTabProperty, value); }
        }

        public static readonly DependencyProperty ShowMonthsEveryXMonthsProperty = DependencyProperty.Register(
            "ShowMonthsEveryXMonths", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowMonthsEveryXMonths
        {
            get { return (bool) GetValue(ShowMonthsEveryXMonthsProperty); }
            set { SetValue(ShowMonthsEveryXMonthsProperty, value); }
        }

        public static readonly DependencyProperty ShowMonthsSpecificMonthsProperty = DependencyProperty.Register(
            "ShowMonthsSpecificMonths", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowMonthsSpecificMonths
        {
            get { return (bool) GetValue(ShowMonthsSpecificMonthsProperty); }
            set { SetValue(ShowMonthsSpecificMonthsProperty, value); }
        }

        public static readonly DependencyProperty ShowMonthsMonthRangeProperty = DependencyProperty.Register(
            "ShowMonthsMonthRange", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowMonthsMonthRange
        {
            get { return (bool) GetValue(ShowMonthsMonthRangeProperty); }
            set { SetValue(ShowMonthsMonthRangeProperty, value); }
        }

        #endregion

        #region Day Flags

        public static readonly DependencyProperty ShowDayTabProperty = DependencyProperty.Register(
            "ShowDayTab", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowDayTab
        {
            get { return (bool)GetValue(ShowDayTabProperty); }
            set { SetValue(ShowDayTabProperty, value); }
        }

        public static readonly DependencyProperty ShowDaysEveryXWeekDaysProperty = DependencyProperty.Register(
            "ShowDaysEveryXWeekDays", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowDaysEveryXWeekDays
        {
            get { return (bool)GetValue(ShowDaysEveryXWeekDaysProperty); }
            set { SetValue(ShowDaysEveryXWeekDaysProperty, value); }
        }

        public static readonly DependencyProperty ShowDaysEveryXMonthDaysProperty = DependencyProperty.Register(
            "ShowDaysEveryXMonthDays", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowDaysEveryXMonthDays
        {
            get { return (bool) GetValue(ShowDaysEveryXMonthDaysProperty); }
            set { SetValue(ShowDaysEveryXMonthDaysProperty, value); }
        }

        public static readonly DependencyProperty ShowDaysSpecificDaysOfWeekDaysProperty = DependencyProperty.Register(
            "ShowDaysSpecificDaysOfWeekDays", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowDaysSpecificDaysOfWeekDays
        {
            get { return (bool)GetValue(ShowDaysSpecificDaysOfWeekDaysProperty); }
            set { SetValue(ShowDaysSpecificDaysOfWeekDaysProperty, value); }
        }

        public static readonly DependencyProperty ShowDaysSpecificDaysOfMonthDaysProperty = DependencyProperty.Register(
            "ShowDaysSpecificDaysOfMonthDays", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowDaysSpecificDaysOfMonthDays
        {
            get { return (bool) GetValue(ShowDaysSpecificDaysOfMonthDaysProperty); }
            set { SetValue(ShowDaysSpecificDaysOfMonthDaysProperty, value); }
        }

        public static readonly DependencyProperty ShowDaysLastDayOfMonthProperty = DependencyProperty.Register(
            "ShowDaysLastDayOfMonth", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowDaysLastDayOfMonth
        {
            get { return (bool) GetValue(ShowDaysLastDayOfMonthProperty); }
            set { SetValue(ShowDaysLastDayOfMonthProperty, value); }
        }

        public static readonly DependencyProperty ShowDaysLastWeekDayOfMonthProperty = DependencyProperty.Register(
            "ShowDaysLastWeekDayOfMonth", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowDaysLastWeekDayOfMonth
        {
            get { return (bool) GetValue(ShowDaysLastWeekDayOfMonthProperty); }
            set { SetValue(ShowDaysLastWeekDayOfMonthProperty, value); }
        }

        public static readonly DependencyProperty ShowDaysLastXDayOfMonthProperty = DependencyProperty.Register(
            "ShowDaysLastXDayOfMonth", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowDaysLastXDayOfMonth
        {
            get { return (bool) GetValue(ShowDaysLastXDayOfMonthProperty); }
            set { SetValue(ShowDaysLastXDayOfMonthProperty, value); }
        }

        public static readonly DependencyProperty ShowDaysDaysBeforeEndOfMonthProperty = DependencyProperty.Register(
            "ShowDaysDaysBeforeEndOfMonth", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowDaysDaysBeforeEndOfMonth
        {
            get { return (bool) GetValue(ShowDaysDaysBeforeEndOfMonthProperty); }
            set { SetValue(ShowDaysDaysBeforeEndOfMonthProperty, value); }
        }

        #endregion

        #region Hour Flags

        public static readonly DependencyProperty ShowHourTabProperty = DependencyProperty.Register(
            "ShowHourTab", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowHourTab
        {
            get { return (bool)GetValue(ShowHourTabProperty); }
            set { SetValue(ShowHourTabProperty, value); }
        }

        public static readonly DependencyProperty ShowHoursEveryXHoursProperty = DependencyProperty.Register(
            "ShowHoursEveryXHours", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowHoursEveryXHours
        {
            get { return (bool)GetValue(ShowHoursEveryXHoursProperty); }
            set { SetValue(ShowHoursEveryXHoursProperty, value); }
        }

        public static readonly DependencyProperty ShowHoursSpecificHoursProperty = DependencyProperty.Register(
            "ShowHoursSpecificHours", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowHoursSpecificHours
        {
            get { return (bool)GetValue(ShowHoursSpecificHoursProperty); }
            set { SetValue(ShowHoursSpecificHoursProperty, value); }
        }

        public static readonly DependencyProperty ShowHoursHourRangeProperty = DependencyProperty.Register(
            "ShowHoursHourRange", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowHoursHourRange
        {
            get { return (bool)GetValue(ShowHoursHourRangeProperty); }
            set { SetValue(ShowHoursHourRangeProperty, value); }
        }

        #endregion

        #region Minute Flags

        public static readonly DependencyProperty ShowMinuteTabProperty = DependencyProperty.Register(
            "ShowMinuteTab", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowMinuteTab
        {
            get { return (bool)GetValue(ShowMinuteTabProperty); }
            set { SetValue(ShowMinuteTabProperty, value); }
        }

        public static readonly DependencyProperty ShowMinutesEveryXMinutesProperty = DependencyProperty.Register(
            "ShowMinutesEveryXMinutes", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowMinutesEveryXMinutes
        {
            get { return (bool)GetValue(ShowMinutesEveryXMinutesProperty); }
            set { SetValue(ShowMinutesEveryXMinutesProperty, value); }
        }

        public static readonly DependencyProperty ShowMinutesSpecificMinutesProperty = DependencyProperty.Register(
            "ShowMinutesSpecificMinutes", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowMinutesSpecificMinutes
        {
            get { return (bool)GetValue(ShowMinutesSpecificMinutesProperty); }
            set { SetValue(ShowMinutesSpecificMinutesProperty, value); }
        }

        public static readonly DependencyProperty ShowMinutesMinuteRangeProperty = DependencyProperty.Register(
            "ShowMinutesMinuteRange", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowMinutesMinuteRange
        {
            get { return (bool)GetValue(ShowMinutesMinuteRangeProperty); }
            set { SetValue(ShowMinutesMinuteRangeProperty, value); }
        }

        #endregion

        #region Second Flags

        public static readonly DependencyProperty ShowSecondTabProperty = DependencyProperty.Register(
            "ShowSecondTab", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowSecondTab
        {
            get { return (bool)GetValue(ShowSecondTabProperty); }
            set { SetValue(ShowSecondTabProperty, value); }
        }

        public static readonly DependencyProperty ShowSecondsEveryXSecondsProperty = DependencyProperty.Register(
            "ShowSecondsEveryXSeconds", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowSecondsEveryXSeconds
        {
            get { return (bool)GetValue(ShowSecondsEveryXSecondsProperty); }
            set { SetValue(ShowSecondsEveryXSecondsProperty, value); }
        }

        public static readonly DependencyProperty ShowSecondsSpecificSecondsProperty = DependencyProperty.Register(
            "ShowSecondsSpecificSeconds", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowSecondsSpecificSeconds
        {
            get { return (bool)GetValue(ShowSecondsSpecificSecondsProperty); }
            set { SetValue(ShowSecondsSpecificSecondsProperty, value); }
        }

        public static readonly DependencyProperty ShowSecondsSecondRangeProperty = DependencyProperty.Register(
            "ShowSecondsSecondRange", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true));

        public bool ShowSecondsSecondRange
        {
            get { return (bool)GetValue(ShowSecondsSecondRangeProperty); }
            set { SetValue(ShowSecondsSecondRangeProperty, value); }
        }

        #endregion

        public static readonly DependencyProperty CronExpressionProperty = DependencyProperty.Register(
            "CronExpression", typeof(string), typeof(WpfCronExpressionUIView), new PropertyMetadata(default(string)));

        public string CronExpression
        {
            get { return (string) GetValue(CronExpressionProperty); }
            set { SetValue(CronExpressionProperty, value); }
        }

        #endregion

        #region Year Parameters

        public static readonly DependencyProperty MinimumYearProperty = DependencyProperty.Register(
            "MinimumYear", typeof(int), typeof(WpfCronExpressionUIView), new PropertyMetadata(default(int)));

        public int MinimumYear
        {
            get { return (int) GetValue(MinimumYearProperty); }
            set { SetValue(MinimumYearProperty, value); }
        }

        public static readonly DependencyProperty MaximumYearProperty = DependencyProperty.Register(
            "MaximumYear", typeof(int), typeof(WpfCronExpressionUIView), new PropertyMetadata(default(int)));

        public int MaximumYear
        {
            get { return (int) GetValue(MaximumYearProperty); }
            set { SetValue(MaximumYearProperty, value); }
        }

        public static readonly DependencyProperty MaxHeightYearRangeProperty = DependencyProperty.Register(
            "MaxHeightYearRange", typeof(int), typeof(WpfCronExpressionUIView), new PropertyMetadata(150));

        public int MaxHeightYearRange
        {
            get { return (int) GetValue(MaxHeightYearRangeProperty); }
            set { SetValue(MaxHeightYearRangeProperty, value); }
        }

        #endregion

        public WpfCronExpressionUIView()
        {
            InitializeComponent();
            
            // bind dependency properties to view model
            var minimumYearInViewModel = nameof(ViewModel.ViewModel.MinimumYear);
            var bindingMinimumYear = new Binding(minimumYearInViewModel) { Mode = BindingMode.TwoWay };
            SetBinding(MinimumYearProperty, bindingMinimumYear);

            var maximumYearInViewModel = nameof(ViewModel.ViewModel.MaximumYear);
            var bindingMaximumYear = new Binding(maximumYearInViewModel) { Mode = BindingMode.TwoWay };
            SetBinding(MaximumYearProperty, bindingMaximumYear);

            var cronExpressionInViewModel = nameof(ViewModel.ViewModel.CronExpression);
            var bindingCronExpression = new Binding(cronExpressionInViewModel) { Mode = BindingMode.TwoWay };
            SetBinding(CronExpressionProperty, bindingCronExpression);
        }

        private void SelectFirstVisibleTab()
        {
            // use dependency properties, not visibilities on the tab
            if (ShowYearTab)
            {
                TabControl.SelectedItem = TabItemYear;
                return;
            }

            if (ShowMonthTab)
            {
                TabControl.SelectedItem = TabItemMonth;
                return;
            }

            if (ShowDayTab)
            {
                TabControl.SelectedItem = TabItemDay;
                return;
            }

            if (ShowHourTab)
            {
                TabControl.SelectedItem = TabItemHour;
                return;
            }

            if (ShowMinuteTab)
            {
                TabControl.SelectedItem = TabItemMinute;
                return;
            }

            if (ShowSecondTab)
            {
                TabControl.SelectedItem = TabItemSecond;
                return;
            }
        }

        private void WpfCronExpressionUIView_OnLoaded(object sender, RoutedEventArgs e)
        {
            SelectFirstVisibleTab();
        }
    }
}
