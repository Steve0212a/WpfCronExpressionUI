using System;
using System.Collections.Generic;

namespace WpfCronExpressionUI.ViewModel
{
    internal class CheckedItem : ViewModelBase, IEqualityComparer<CheckedItem>
    {
        private readonly Action checkChanged;

        internal CheckedItem(Action checkChanged)
        {
            this.checkChanged = checkChanged;
        }

        private long id;

        public long Id
        {
            get { return id; }
            set
            {
                // Id
                Set(nameof(Id), ref id, value);
            }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set
            {
                // Description
                Set(nameof(Description), ref description, value);
            }
        }

        private bool isChecked;

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                // IsChecked
                if (Set(nameof(IsChecked), ref isChecked, value))
                {
                    checkChanged?.Invoke();
                }
            }
        }

        public bool Equals(CheckedItem x, CheckedItem y)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            return x.Id == y.Id;
        }

        public int GetHashCode(CheckedItem obj)
        {
            return Id.GetHashCode();
        }
    }
}
