using Xamarin.Forms;

namespace IntelligentHabitacion.Behavior
{
    public class PhoneNumberBehavior : Behavior<Entry>
    {
        public readonly MaskEntryBehavior _maskEntryBehavior;

        public PhoneNumberBehavior()
        {
            _maskEntryBehavior = new MaskEntryBehavior("(XX) X XXXX-XXXX");
        }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += _maskEntryBehavior.OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= _maskEntryBehavior.OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }
    }
}
