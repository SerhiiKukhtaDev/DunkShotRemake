using System;
using Lean.Gui;
using UniRx;

namespace Utils
{
    public static class ObservablesExtensions
    {
        public static IObservable<Unit> OnClickAsObservableLimit(this LeanButton button)
        {
            return button.OnClickAsObservableLimit(TimeSpan.FromSeconds(0.3f));
        }
        
        public static IObservable<Unit> OnClickAsObservableLimit(this LeanButton button, TimeSpan timeSpan)
        {
            return button.OnClick.AsObservable().ThrottleFirst(timeSpan);
        }
    }
}