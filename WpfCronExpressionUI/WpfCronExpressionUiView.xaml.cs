using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfCronExpressionUI
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class WpfCronExpressionUIView : UserControl
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
            "ShowYearTab", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(true, new PropertyChangedCallback(OnShowYearTabChanged)));

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
        }

        private void SelectFirstVisibleTab()
        {
            foreach (TabItem item in TabControl.Items)
            {
                if (item.IsVisible)
                {
                    item.IsSelected = true;
                    break;
                }
            }
        }

        private void WpfCronExpressionUIView_OnLoaded(object sender, RoutedEventArgs e)
        {
            SelectFirstVisibleTab();
        }
    }
}
