using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using IPDTracker.iOS.View.Controls;

[assembly: ExportRenderer(typeof(TimePicker), typeof(TimePicker24HRenderer))]

namespace IPDTracker.iOS.View.Controls
{
    public class TimePicker24HRenderer : TimePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);
            var timePicker = (UIDatePicker)Control.InputView;
            timePicker.Locale = new NSLocale("no_nb");
        }
    }
}