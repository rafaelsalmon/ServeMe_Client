using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SirvaMe.CustomControls
{
    public class StarBehavior : Behavior<View>
    {
        TapGestureRecognizer _tapRecognizer;
        static readonly List<StarBehavior> DefaultBehaviors = new List<StarBehavior>();
        static readonly Dictionary<string, List<StarBehavior>> StarGroups = new Dictionary<string, List<StarBehavior>>();

        public static readonly BindableProperty GroupNameProperty = BindableProperty.Create("GroupName", typeof(string), typeof(StarBehavior), null, propertyChanged: OnGroupNameChanged);

        public string GroupName
        {
            set { SetValue(GroupNameProperty, value); }
            get { return (string)GetValue(GroupNameProperty); }
        }

        public static readonly BindableProperty RatingProperty = BindableProperty.Create("Rating", typeof(int), typeof(StarBehavior), default(int));

        public int Rating
        {
            set { SetValue(RatingProperty, value); }
            get { return (int)GetValue(RatingProperty); }
        }

        static void OnGroupNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var behavior = (StarBehavior)bindable;
            var oldGroupName = (string)oldValue;
            var newGroupName = (string)newValue;

            // Remove existing behavior from Group
            if (String.IsNullOrEmpty(oldGroupName))
            {
                DefaultBehaviors.Remove(behavior);
            }
            else
            {
                var behaviors = StarGroups[oldGroupName];
                behaviors.Remove(behavior);

                if (behaviors.Count == 0)
                {
                    StarGroups.Remove(oldGroupName);
                }
            }

            // Add New Behavior to the group
            if (String.IsNullOrEmpty(newGroupName))
            {
                DefaultBehaviors.Add(behavior);
            }
            else
            {
                List<StarBehavior> behaviors = null;

                if (StarGroups.ContainsKey(newGroupName))
                {
                    behaviors = StarGroups[newGroupName];
                }
                else
                {
                    behaviors = new List<StarBehavior>();
                    StarGroups.Add(newGroupName, behaviors);
                }
                behaviors.Add(behavior);
            }
        }

        public static readonly BindableProperty IsStarredProperty = BindableProperty.Create("IsStarred", typeof(bool), typeof(StarBehavior), false, propertyChanged: OnIsStarredChanged);

        public bool IsStarred
        {
            set { SetValue(IsStarredProperty, value); }
            get { return (bool)GetValue(IsStarredProperty); }
        }

        static void OnIsStarredChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var behavior = (StarBehavior)bindable;

            if ((bool)newValue)
            {
                var groupName = behavior.GroupName;
                List<StarBehavior> behaviors = null;

                behaviors = string.IsNullOrEmpty(groupName) ? DefaultBehaviors : StarGroups[groupName];

                var itemReached = false;
                int count = 1, position = 0;
                // all positions to left IsStarred = true and all position to the right is false

                foreach (var item in behaviors)
                {
                    if (item != behavior && !itemReached)
                    {
                        item.IsStarred = true;
                    }
                    if (item == behavior)
                    {
                        itemReached = true;
                        item.IsStarred = true;
                        position = count;
                    }
                    if (item != behavior && itemReached)
                        item.IsStarred = false;

                    item.Rating = position;
                    count++;
                }
            }
        }

        public StarBehavior()
        {
            DefaultBehaviors.Add(this);
        }

        protected override void OnAttachedTo(View view)
        {
            _tapRecognizer = new TapGestureRecognizer();
            _tapRecognizer.Tapped += OnTapRecognizerTapped;
            view.GestureRecognizers.Add(_tapRecognizer);
        }

        protected override void OnDetachingFrom(View view)
        {
            view.GestureRecognizers.Remove(_tapRecognizer);
            _tapRecognizer.Tapped -= OnTapRecognizerTapped;
        }

        void OnTapRecognizerTapped(object sender, EventArgs args)
        {
            //PropertyChange does not fire, if the value is not changed :-(
            IsStarred = false;
            IsStarred = true;
        }
    }
}