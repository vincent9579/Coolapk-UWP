﻿using CoolapkUWP.Helpers;
using CoolapkUWP.Helpers.Providers;
using CoolapkUWP.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CoolapkUWP.ViewModels.FeedRepliesPage
{
    internal enum ListType
    {
        HotReply,
        ReplyReply,
    }

    internal class ViewModel : IViewModel
    {
        private readonly CoolapkListProvider provider;
        private readonly FeedReplyModel replyModel;

        public ObservableCollection<Entity> Models { get => provider?.Models ?? null; }
        public double[] VerticalOffsets { get; set; } = new double[1];
        public string Title { get; } = string.Empty;

        internal ViewModel(string id, ListType type)
        {
            if (string.IsNullOrEmpty(id)) { throw new ArgumentException(nameof(id)); }

            switch (type)
            {
                case ListType.HotReply:
                    provider =
                        new CoolapkListProvider(
                            async (p, page, firstItem, lastItem) =>
                                (JArray)await DataHelper.GetDataAsync(
                                    DataUriType.GetHotReplies,
                                    p == -2 ? true : false,
                                    id,
                                    p < 0 ? ++page : p,
                                    page > 1 ? $"&firstItem={firstItem}&lastItem={lastItem}" : string.Empty),
                            (a, b) => ((FeedReplyModel)a).Id == b.Value<int>("id"),
                            (o) => new Entity[] { new FeedReplyModel(o) },
                            () => Windows.ApplicationModel.Resources.ResourceLoader.GetForViewIndependentUse("FeedShellListControl").GetString("noMoreHotReply"),
                            "id");
                    Title = Windows.ApplicationModel.Resources.ResourceLoader.GetForViewIndependentUse("FeedShellListControl").GetString("hotReplyText");
                    break;
                case ListType.ReplyReply:
                    provider =
                        new CoolapkListProvider(
                            async (p, _page, _, lastItem) =>
                            {
                                var page = p < 0 ? ++_page : p;
                                return (JArray)await DataHelper.GetDataAsync(
                                    DataUriType.GetReplyReplies,
                                    p == -2 ? true : false,
                                    id,
                                    page,
                                    page > 1 ? $"&lastItem={lastItem}" : string.Empty);
                            },
                            (a, b) => ((FeedReplyModel)a).Id == b.Value<int>("id"),
                            (o) => new Entity[] { new FeedReplyModel(o, false) },
                            "id");
                    break;
                default:
                    throw new ArgumentException(nameof(type));
            }
        }

        internal ViewModel(FeedReplyModel reply) : this(reply.Id.ToString(), ListType.ReplyReply)
        {
            Title = string.Format(
                        Windows.ApplicationModel.Resources.ResourceLoader.GetForViewIndependentUse("FeedRepliesPage").GetString("title"),
                        reply.Replynum);

            reply.ShowreplyRows = false;
            reply.EntityFixed = true;
            replyModel = reply;
            provider.Models.Add(reply);
        }

        public async Task Refresh(int p = -1)
        {
            await provider?.Refresh(p);
            if(p == -2)
            {
                provider?.Models.Insert(0, replyModel);
            }
        }
    }
}
