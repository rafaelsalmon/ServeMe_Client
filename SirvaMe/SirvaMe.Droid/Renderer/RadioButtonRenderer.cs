using System.ComponentModel;
using Android.Widget;
using SirvaMe.CustomControls;
using SirvaMe.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(CustomRadioButton), typeof(RadioButtonRenderer))]
namespace SirvaMe.Droid.Renderer
{
    //  using NativeRadioButton = RadioButton;

    public class RadioButtonRenderer : ViewRenderer<CustomRadioButton, RadioButton>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CustomRadioButton> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged += ElementOnPropertyChanged;
            }

            if (this.Control == null)
            {
                var radButton = new RadioButton(this.Context);
                radButton.CheckedChange += radButton_CheckedChange;

                this.SetNativeControl(radButton);
            }

            Control.Text = e.NewElement.Text;
            Control.Checked = e.NewElement.Checked;
            Control.SetTextColor(Color.Argb(255, 178, 178, 178));
            //Control.SetBackgroundColor(Android.Graphics.Color.Yellow);

            Element.PropertyChanged += ElementOnPropertyChanged;
        }

        void radButton_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            this.Element.Checked = e.IsChecked;
        }

        void ElementOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Checked":
                    Control.Checked = Element.Checked;
                    break;
                case "Text":
                    Control.Text = Element.Text;
                    break;

            }
        }
    }
}