using System;
using Android.Views;
using Nut.Core.Bindings;

namespace Nut.Droid.Controls
{
    public interface INutCollectionItem : INutBindingStore, IDisposable
    {
        View ItemView { get; set; }
        object DataSource { get; set; }
    }
}