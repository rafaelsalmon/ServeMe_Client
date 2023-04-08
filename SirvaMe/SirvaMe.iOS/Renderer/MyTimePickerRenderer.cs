using SirvaMe.CustomControls;
using SirvaMe.iOS.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MyTimePicker), typeof(MyTimePickerRenderer))]

namespace SirvaMe.iOS.Renderer
{
    internal class MyTimePickerRenderer : TimePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);

            var timePicker = (MyTimePicker)Element;

            if (timePicker != null)
            {
                SetBorderStyle(timePicker);
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
                this.Control.TextColor = timePicker.TextColor.ToUIColor();
            }
        }

        private void SetBorderStyle(MyTimePicker timePicker)
        {
            this.Control.BorderStyle = (timePicker.HasBorder == true) ? UIKit.UITextBorderStyle.RoundedRect : UIKit.UITextBorderStyle.None;
        }

        private void SetTextColor(MyTimePicker timePicker)
        {
            this.Control.TextColor = timePicker.TextColor.ToUIColor();
        }
    }
}