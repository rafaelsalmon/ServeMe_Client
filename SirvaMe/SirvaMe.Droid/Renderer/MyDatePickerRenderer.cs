using SirvaMe.CustomControls;
using SirvaMe.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyDatePicker), typeof(MyDatePickerRenderer))]

namespace SirvaMe.Droid.Renderer
{
    internal class MyDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            var datePicker = (MyDatePicker)Element;

            if (datePicker != null)
            {
                SetTextColor(datePicker);
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
            var datePicker = (MyDatePicker)Element;

            if (e.PropertyName == MyDatePicker.TextColorProperty.PropertyName)
            {
                this.Control.SetTextColor(datePicker.TextColor.ToAndroid());
            }
        }

        private void SetTextColor(MyDatePicker datePicker)
        {
            this.Control.SetTextColor(datePicker.TextColor.ToAndroid());
        }
    }
}