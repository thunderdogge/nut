using System;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Nut.Core.Application;
using Nut.Droid.Screens;
using Nut.Ioc;
using NutApp.Core.Screens.Models;

namespace NutApp.Droid.Screens
{
    [NutIocIgnore]
    public abstract class BaseActivity<TViewModel> : NutActivity<TViewModel> where TViewModel : BaseViewModel
    {
        private SwipeRefreshLayout swipeRefresh;

        protected abstract int LayoutView { get; }
        protected virtual string LayoutTitle => null;

        protected SwipeRefreshLayout SwipeRefresh
        {
            get { return swipeRefresh ?? (swipeRefresh = FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_refresh)); }
            set { swipeRefresh = value; }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(LayoutView);

            ConfigureToolbar();
            ConfigureSwipeRefresh();
        }

        protected virtual void ConfigureToolbar()
        {
            var toolbar = FindViewById<Toolbar>(Resource.Id.Toolbar);
            if (toolbar == null)
            {
                return;
            }

            SetSupportActionBar(toolbar);

            var supportActionBar = SupportActionBar;
            supportActionBar.SetHomeButtonEnabled(true);
            supportActionBar.SetDisplayHomeAsUpEnabled(true);
            supportActionBar.SetDisplayShowTitleEnabled(false);
            supportActionBar.SetDisplayShowCustomEnabled(true);

            toolbar.Title = LayoutTitle;
        }

        protected virtual void ConfigureSwipeRefresh()
        {
            if (SwipeRefresh != null)
            {
                SwipeRefresh.SetColorSchemeResources(Resource.Color.green, Resource.Color.blue);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (SwipeRefresh != null)
            {
                SwipeRefresh.Refresh += OnSwipeRefresh;
            }
        }

        protected override void OnPause()
        {
            base.OnPause();

            if (SwipeRefresh != null)
            {
                SwipeRefresh.Refresh -= OnSwipeRefresh;
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        protected virtual async void OnSwipeRefresh(object sender, EventArgs e)
        {
            if (ViewModel.IsLoading)
            {
                SwipeRefresh.Refreshing = false;
                return;
            }

            await ViewModel.RefreshAsync().ConfigureAwait(true);

            SwipeRefresh.Refreshing = false;
        }

        protected virtual void ConfigureToolbarTabs(TabLayout tabLayout)
        {
        }

        protected virtual void OnToolbarTabSelected(object sender, TabLayout.TabSelectedEventArgs e)
        {
        }

        public override INutApplicationLauncher CreateLauncher()
        {
            return new Launcher(this);
        }
    }
}