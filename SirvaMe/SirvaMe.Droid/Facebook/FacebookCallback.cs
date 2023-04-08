using System;
using Android.Runtime;
using Xamarin.Facebook;
using Object = Java.Lang.Object;

namespace SirvaMe.Droid.Facebook
{
    public class FacebookCallback<TResult> : Object, IFacebookCallback where TResult : Object
    {
        public Action HandleCancel { get; set; }
        public Action<FacebookException> HandleError { get; set; }
        public Action<TResult> HandleSuccess { get; set; }

        public void OnCancel()
        {
            var c = HandleCancel;
            if (c != null)
                c();
        }

        public void OnError(FacebookException error)
        {
            var c = HandleError;
            if (c != null)
                c(error);
        }

        public void OnSuccess(Object result)
        {
            var c = HandleSuccess;
            if (c != null)
                c(result.JavaCast<TResult>());
        }
    }
}
