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

        public static readonly DependencyProperty ShowCronExpressionProperty = DependencyProperty.Register(
            "ShowCronExpression", typeof(bool), typeof(WpfCronExpressionUIView), new PropertyMetadata(default(bool)));

        public bool ShowCronExpression
        {
            get { return (bool) GetValue(ShowCronExpressionProperty); }
            set { SetValue(ShowCronExpressionProperty, value); }
        }

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
    }
}
