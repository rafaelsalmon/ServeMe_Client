using SirvaMe.CustomControls;
using SirvaMe.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyTimePicker), typeof(MyTimePickerRenderer))]

namespace SirvaMe.Droid.Renderer
{
    internal class MyTimePickerRenderer : TimePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);

            var timePicker = (MyTimePicker)Element;

            if (timePicker != null)
            {
                SetTextColor(timePicker);
            }

            if (e.OldElement == null)
            {
                //Wire events
            }

            if (e.NewElement == null)
            {
                //Unwire events
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null)
            {
                return;
            }
            var timePicker = (MyTimePicker)Element;

            if (e.PropertyName == MyTimePicker.TextColorProperty.PropertyName)
            {
                this.Control.SetTextColor(timePicker.TextColor.ToAndroid());
            }
        }

        private void SetTextColor(MyTimePicker timePicker)
        {
            this.Control.SetTextColor(timePicker.TextColor.ToAndroid());
        }
    }
}