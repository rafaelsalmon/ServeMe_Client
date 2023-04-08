using SirvaMe.CustomControls;
using SirvaMe.iOS.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MyDatePicker), typeof(MyDatePickerRenderer))]

namespace SirvaMe.iOS.Renderer
{
    internal class MyDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            var datePicker = (MyDatePicker)Element;

            if (datePicker != null)
            {
                SetBorderStyle(datePicker);
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
                this.Control.TextColor = datePicker.TextColor.ToUIColor();
            }
        }

        private void SetBorderStyle(MyDatePicker datePicker)
        {
            this.Control.BorderStyle = (datePicker.HasBorder == true) ? UIKit.UITextBorderStyle.RoundedRect : UIKit.UITextBorderStyle.None;
        }

        private void SetTextColor(MyDatePicker datePicker)
        {
            this.Control.TextColor = datePicker.TextColor.ToUIColor();
        }
    }
}