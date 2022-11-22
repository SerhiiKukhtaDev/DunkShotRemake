using UniRx;

namespace ScriptableObjects.Settings.Base.Interfaces
{
    public interface ISetting<TType> 
    {
        ReactiveProperty<TType> Setting { get; }
    }
}
