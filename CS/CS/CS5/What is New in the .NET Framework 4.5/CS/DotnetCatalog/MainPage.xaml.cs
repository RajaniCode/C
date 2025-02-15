﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using DotnetCatalog.DataModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DotnetCatalog
{
    /// <summary>
    /// A page that displays the list of features..
    /// </summary>
    public sealed partial class MainPage : DotnetCatalog.Common.LayoutAwarePage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            
            var featureData = FeatureDataSource.Get();
            this.DefaultViewModel["Items"] = featureData;
            
            RoutedEventHandler onLoad = null;
            
            onLoad = (object sender, RoutedEventArgs e) =>
            {
                if (pageState != null && pageState.ContainsKey("ScrollHorizontalOffset") && pageState["ScrollHorizontalOffset"] is double)
                {
                    
                    ScrollViewer sv = (ScrollViewer)((FrameworkElement)VisualTreeHelper.GetChild(itemGridView, 0)).FindName("ScrollViewer");
                    this.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    sv.ScrollToHorizontalOffset((double)pageState["ScrollHorizontalOffset"]);
                      
                }
                
                this.Loaded -= onLoad;
                this.Visibility = Windows.UI.Xaml.Visibility.Visible;
            };
                       
            this.Loaded += onLoad;
        }
                        
        protected override void SaveState(Dictionary<string, object> pageState)
        {
            base.SaveState(pageState);

            ScrollViewer sv = (ScrollViewer)((FrameworkElement)VisualTreeHelper.GetChild(itemGridView, 0)).FindName("ScrollViewer");
            pageState["ScrollHorizontalOffset"] = sv.HorizontalOffset;
        }


        /// <summary>
        /// Invoked when an item within the list is clicked.
        /// </summary>
        /// <param name="sender">The GridView (or ListView when the application is snapped)
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            var itemId = ((FeatureDataItem)e.ClickedItem).Id;
            this.Frame.Navigate(typeof(ItemPage), itemId);
        }
    }
}
