using System;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace SirvaMe.Views
{
    public partial class AgendaPage : ContentPage
    {
        public AgendaPage()
        {
            InitializeComponent();
            
            var calendarView = new CalendarView
            {
                //BackgroundColor = Color.Blue,
                MinDate = CalendarView.FirstDayOfMonth(DateTime.Now.AddYears(-1)),
                MaxDate = CalendarView.LastDayOfMonth(DateTime.Now.AddMonths(3)),
                SelectedDateBackgroundColor = (Color)Application.Current.Resources["MenuLinkColor"],
                //DayOfWeekLabelBackgroundColor = Color.Green,
                DayOfWeekLabelForegroundColor = (Color)Application.Current.Resources["FontColor"],
                DateBackgroundColor = Color.White,
                TodayDateBackgroundColor = Color.FromHex("#FAF9D8"),
                HighlightedDateBackgroundColor = Color.FromRgb(227, 227, 227),
                ShouldHighlightDaysOfWeekLabels = false,
                SelectionBackgroundStyle = CalendarView.BackgroundStyle.CircleFill,
                TodayBackgroundStyle = CalendarView.BackgroundStyle.CircleOutline,
                HighlightedDaysOfWeek = new DayOfWeek[] { DayOfWeek.Sunday },
                ShowNavigationArrows = true,
                DateLabelFont = Font.OfSize("DateLabelFont", NamedSize.Micro),
                MonthTitleFont = Font.OfSize("MonthTitleFont", NamedSize.Micro)
            };

            RelativeLayout.Children.Add(calendarView,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent(p => p.Width),
                Constraint.RelativeToParent(p => p.Height * 2 / 3));
            
            RelativeLayout.Children.Add(StackLayout,
                Constraint.Constant(0),
                Constraint.RelativeToParent(p => p.Height * 2 / 3),
                Constraint.RelativeToParent(p => p.Width),
                Constraint.RelativeToParent(p => p.Height * 1 / 3)
            );

            calendarView.DateSelected += (object sender, DateTime e) =>
            {
                App.Current.DataCalendario = e.ToString("d");
                App.Current.RefreshData = true;
                Navigation.PopAsync(true);
            };
        }
    }
}
