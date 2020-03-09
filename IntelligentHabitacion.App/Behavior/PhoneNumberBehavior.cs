using Xamarin.Forms;

namespace IntelligentHabitacion.App.Behavior
{
    public class PhoneNumberBehavior : Behavior<Entry>
    {
        public readonly MaskEntryBehavior _maskEntryBehavior;

        public PhoneNumberBehavior()
        {
            _maskEntryBehavior = new MaskEntryBehavior("(XX) X XXXX-XXXX");
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += _maskEntryBehavior.OnEntryTextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= _maskEntryBehavior.OnEntryTextChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}
