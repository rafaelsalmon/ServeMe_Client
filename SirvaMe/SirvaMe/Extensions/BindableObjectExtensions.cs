using Xamarin.Forms;

namespace SirvaMe.Extensions
{
    public static class BindableObjectExtensions
    {
        public static T GetValue<T>(this BindableObject bindableObject, BindableProperty property)
        {
            return (T)bindableObject.GetValue(property);
        }
    }
}
