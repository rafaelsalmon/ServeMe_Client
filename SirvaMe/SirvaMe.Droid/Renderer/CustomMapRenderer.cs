using System;
using System.Collections.Generic;
using System.ComponentModel;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Widget;
using SirvaMe.Droid.Renderer;
using SirvaMe.Models;
using SirvaMe.Views;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]

namespace SirvaMe.Droid.Renderer
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter, IOnMapReadyCallback
    {
        GoogleMap _map;
        List<CustomPin> _customPins;
        bool _isDrawn;

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                _map.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                _customPins = formsMap.CustomPins;
                ((MapView)Control).GetMapAsync(this);
            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;
            //_map.InfoWindowClick += OnInfoWindowClick;
            //_map.SetInfoWindowAdapter(this);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals("VisibleRegion") && !_isDrawn)
            {
                _map.Clear();

                if (_customPins == null) return;

                foreach (var pin in _customPins)
                {
                    var marker = new MarkerOptions();
                    marker.SetPosition(new LatLng(pin.Pin.Position.Latitude, pin.Pin.Position.Longitude));
                    marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pin2));

                    //marker.SetTitle(pin.Pin.Label);
                    //marker.SetSnippet(pin.Pin.Address);
                    //marker.SetIcon(BitmapDescriptorFactory.FromResource(pin.Pin.Label.Equals("Usuario") ?
                    //                Resource.Drawable.pin1 : Resource.Drawable.pin2));

                    _map.AddMarker(marker);
                }
                _isDrawn = true;
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            if (changed)
            {
                _isDrawn = false;
            }
        }

        void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            var customPin = GetCustomPin(e.Marker);
            if (customPin == null) throw new Exception("Localização não encontrada!");

            if (!string.IsNullOrWhiteSpace(customPin.Url))
            {
                var builder = new AlertDialog.Builder(this.Context);
                builder.SetTitle("Alerta");
                builder.SetMessage("Será direcionado ao Perfil do Prestador!");
                builder.SetCancelable(false);
                builder.SetPositiveButton("OK", delegate { Finish(); });
                builder.Show();

                //var url = Android.Net.Uri.Parse(customPin.Url);
                //var intent = new Intent(Intent.ActionView, url);
                //intent.AddFlags(ActivityFlags.NewTask);
                //Android.App.Application.Context.StartActivity(intent);
            }
        }

        private void Finish()
        {
            
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;

            if (inflater != null)
            {
                var customPin = GetCustomPin(marker);
                if (customPin == null) throw new Exception("Localização não encontrada!");

                var view = inflater.Inflate(customPin.Id == "Xamarin" ? Resource.Layout.XamarinMapInfoWindow
                                                                        : Resource.Layout.MapInfoWindow, null);

                var infoTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowTitle);
                var infoSubtitle = view.FindViewById<TextView>(Resource.Id.InfoWindowSubtitle);

                if (infoTitle != null) infoTitle.Text = marker.Title;
                if (infoSubtitle != null) infoSubtitle.Text = marker.Snippet;

                return view;
            }
            return null;
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }

        CustomPin GetCustomPin(Marker annotation)
        {
            var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);

            foreach (var pin in _customPins)
            {
                if (pin.Pin.Position == position)
                {
                    return pin;
                }
            }
            return null;
        }
    }
}

