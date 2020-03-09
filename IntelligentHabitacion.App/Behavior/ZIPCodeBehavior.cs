using Xamarin.Forms;

namespace IntelligentHabitacion.App.Behavior
{
    public class ZIPCodeBehavior : Behavior<Entry>
    {
        public readonly MaskEntryBehavior _maskEntryBehavior;

        public ZIPCodeBehavior()
        {
            _maskEntryBehavior = new MaskEntryBehavior("XX.XXX-XXX");            
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
