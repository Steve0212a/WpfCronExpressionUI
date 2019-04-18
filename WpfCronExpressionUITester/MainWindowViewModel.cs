using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfCronExpressionUITester
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string cronExpressionFromControl = "0 0 0/1 ? * SUN,FRI *";

        public string CronExpressionFromControl
        {
            get { return cronExpressionFromControl; }
            set
            {
                // CronExpression
                cronExpressionFromControl = value;
                OnPropertyChanged(nameof(CronExpressionFromControl));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
