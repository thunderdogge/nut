using System;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Java.Lang;
using Nut.Core.Bindings;
using Nut.Ioc;
using Object = Java.Lang.Object;

namespace Nut.Droid.Controls
{
    public abstract class NutFragmentPagerAdapter<TSegment, TPage> : FragmentStatePagerAdapter
    {
        protected const int DefaultTabsScrollThreshold = 5;

        private readonly FragmentManager fm;
        private readonly TabLayout tabLayout;
        private TSegment[] segments;
        private TPage[] pages;

        protected NutFragmentPagerAdapter(FragmentManager fm) : this(fm, null)
        {
        }

        protected NutFragmentPagerAdapter(FragmentManager fm, TabLayout tabLayout) : base(fm)
        {
            this.fm = fm;
            this.tabLayout = tabLayout;
        }

        protected NutFragmentPagerAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public virtual TPage[] Pages
        {
            get { return pages ?? (pages = new TPage[0]); }
            set { pages = value; OnPagesChanged(value); }
        }

        public virtual TSegment[] Segments
        {
            get { return segments ?? (segments = new TSegment[0]); }
            set { segments = value; OnSegmentsChanged(value); }
        }

        public override int Count => Pages.Length;

        protected virtual int TabsScrollThreshold => DefaultTabsScrollThreshold;

        public override int GetItemPosition(Object @object)
        {
            return PositionNone;
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            var tabs = Segments;
            if (position >= tabs.Length)
            {
                return null;
            }

            var title = GetPageTitleFormatted(tabs[position], position, tabs.Length);
            return new Java.Lang.String(title);
        }

        protected virtual string GetPageTitleFormatted(TSegment segment, int position, int totalCount)
        {
            return segment.ToString();
        }

        protected virtual int GetTabLayoutMode(TSegment[] tabs, int threshold)
        {
            return tabs != null && tabs.Length >= threshold ? TabLayout.ModeScrollable : TabLayout.ModeFixed;
        }

        protected virtual void OnPagesChanged(TPage[] value)
        {
            if (fm.Fragments != null)
            {
                var length = System.Math.Min(value.Length, fm.Fragments.Count);
                for (var i = 0; i < length; i++)
                {
                    var bindableFragment = fm.Fragments[i] as INutBindingStore;
                    if (bindableFragment != null)
                    {
                        bindableFragment.BindingContext.DataSource = Pages[i];
                    }
                }
            }

            NotifyDataSetChanged();
        }

        protected virtual void OnSegmentsChanged(TSegment[] value)
        {
            if (tabLayout != null)
            {
                tabLayout.TabMode = GetTabLayoutMode(value, TabsScrollThreshold);
            }
        }
    }
}