using Xamarin.Forms;

namespace IntelligentHabitacion.Behavior
{
    public class ZIPCodeBehavior : Behavior<Entry>
    {
        public readonly MaskEntryBehavior _maskEntryBehavior;

        public ZIPCodeBehavior()
        {
            _maskEntryBehavior = new MaskEntryBehavior("XX.XXX-XXX");            
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
