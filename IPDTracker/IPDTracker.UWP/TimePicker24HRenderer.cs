using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

[assembly: ExportRenderer(typeof(TimePicker24H), typeof(TimePicker24HRenderer))]
namespace IPDTracker.UWP
{
    public class TimePicker24HRenderer : TimePickerRenderer
    {
        // Override the OnElementChanged method so we can tweak this renderer post-initial setup
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {

                var nativeControl = (Windows.UI.Xaml.Controls.TimePicker)Control;
                nativeControl.Foreground = new SolidColorBrush(Colors.Gray);

                Control.ClockIdentifier = "24HourClock";
            }
        }

    }
}
