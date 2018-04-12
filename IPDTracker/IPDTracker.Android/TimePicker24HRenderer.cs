using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System.Reflection;
using Android.App;
using AppXamarin.Droid.View.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TimePicker24H), typeof(TimePicker24HRenderer))]
namespace IPDTracker.Droid
{
    public class TimePicker24HRenderer : TimePickerRenderer
    {
        private EditText _TextField { get; set; }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e)
        {
            base.OnElementChanged(e);

            if (_TextField == null)
            {
                _TextField = new EditText(this.Context)
                {
                    Focusable = false,
                    Clickable = false,
                    Tag = this
                };

                _TextField.Click += TextField_Click;
                SetNativeControl(_TextField);
            }

            _TextField.Text = DateTime.Today.Add(Element.Time).ToString(Element.Format);
        }

        private void TextField_Click(object sender, EventArgs e)
        {
            new TimePickerDialog(this.Context, new EventHandler<TimePickerDialog.TimeSetEventArgs>(OnTimeSet), Element.Time.Hours, Element.Time.Minutes, true).Show();
        }
        private void _TextField_FocusChange(object sender, FocusChangeEventArgs e)
        {
            EditText text = (EditText)sender;
            if (text != null && text.HasFocus)
            {
                new TimePickerDialog(this.Context, new EventHandler<TimePickerDialog.TimeSetEventArgs>(OnTimeSet), Element.Time.Hours, Element.Time.Minutes, true).Show();
            }
        }

        private void OnTimeSet(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            Element.Time = new TimeSpan(e.HourOfDay, e.Minute, 0);
            _TextField.Text = DateTime.Today.Add(Element.Time).ToString(Element.Format);
        }
    }
}