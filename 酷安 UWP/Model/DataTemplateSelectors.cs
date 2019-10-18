﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace 酷安_UWP
{
    public class FirstTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DataTemplate1 { get; set; }
        public DataTemplate DataTemplate2 { get; set; }
        protected override DataTemplate SelectTemplateCore(object item)
        {
            Feed feed = item as Feed;
            switch (feed.GetValue("entityType"))
            {
                case "feed": return DataTemplate1;
                case "card":
                default: return DataTemplate2;
            }
        }
    }
    public class SecondTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DataTemplate0 { get; set; }
        public DataTemplate DataTemplate1 { get; set; }
        public DataTemplate DataTemplate2 { get; set; }
        public DataTemplate DataTemplate3 { get; set; }
        public DataTemplate DataTemplate4 { get; set; }
        public DataTemplate DataTemplate5 { get; set; }
        public DataTemplate DataTemplate6 { get; set; }
        protected override DataTemplate SelectTemplateCore(object item)
        {
            Feed feed = item as Feed;
            switch (feed.GetValue("entityTemplate"))
            {
                case "imageCarouselCard_1": return DataTemplate1;
                case "iconMiniGridCard":
                case "iconLinkGridCard":return DataTemplate6;
                case "imageTextScrollCard":
                case "feedScrollCard": return DataTemplate2;
                case "messageCard": return DataTemplate3;
                case "refreshCard": return DataTemplate4;
                case "textLinkListCard": return DataTemplate5;
                default: return DataTemplate0;
            }
        }
    }
    public class ThirdTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DataTemplate0 { get; set; }
        public DataTemplate DataTemplate1 { get; set; }
        public DataTemplate DataTemplate2 { get; set; }
        public DataTemplate DataTemplate3 { get; set; }
        public DataTemplate DataTemplate4 { get; set; }
        public DataTemplate DataTemplate5 { get; set; }
        public DataTemplate DataTemplate6 { get; set; }
        public DataTemplate DataTemplate7 { get; set; }
        protected override DataTemplate SelectTemplateCore(object item)
        {
            Feed feed = item as Feed;
            switch (feed.GetValue("entityType"))
            {
                case "image_1": return DataTemplate1;
                case "iconLink": return DataTemplate2;
                case "dyh": return DataTemplate3;
                case "topic": return DataTemplate4;
                case "feed":
                    if (feed.GetValue("feedType") == "feedArticle") return DataTemplate5;
                    else if (feed.GetValue("feedType") == "question") return DataTemplate6;
                    else return DataTemplate0;
                case "textLink": return DataTemplate7;
                default: return DataTemplate0;
            }
        }
    }
    public class FeedTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DataTemplate1 { get; set; }
        public DataTemplate DataTemplate2 { get; set; }
        protected override DataTemplate SelectTemplateCore(object item)
        {
            Feed feed = item as Feed;
            switch (feed.GetValue("feedType"))
            {
                case "feed": return DataTemplate1;
                case "feedArticle":
                case "answer":
                case "question":
                    return DataTemplate2;
                default: return DataTemplate1;
            }
        }
    }

}
