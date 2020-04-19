using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.Behavior
{
    public class EntryTextChangedBehavior : Behavior<Entry>
    {
        public ICommand EntryTextChanged
        {
            get { return (ICommand)GetValue(EntryTextChangedProperty); }
            set
            {
                SetValue(EntryTextChangedProperty, value);
            }
        }

        public static readonly BindableProperty EntryTextChangedProperty = BindableProperty.Create("EntryTextChanged", typeof(ICommand), typeof(EntryTextChangedBehavior), defaultValue: null);

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(bindable);
        }

        public void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = sender as Entry;
            EntryTextChanged?.Execute(entry.Text);
        }
    }
}
