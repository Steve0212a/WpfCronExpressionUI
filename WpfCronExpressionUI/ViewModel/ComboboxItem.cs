using System.Collections.Generic;

namespace WpfCronExpressionUI.ViewModel
{
    internal class ComboboxItem : ViewModelBase, IEqualityComparer<ComboboxItem>
    {
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

        public bool Equals(ComboboxItem x, ComboboxItem y)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(x, y))
            {
                return true;
            }
            
            return x.Id == y.Id;
        }

        public int GetHashCode(ComboboxItem obj)
        {
            return Id.GetHashCode();
        }
    }
}
