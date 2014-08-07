using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coneixement.Infrastructure.Base;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Coneixement.VideoPlayer.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Coneixement.Infrastructure;
using Coneixement.VideoPlayer.ViewModel;
namespace Coneixement.VideoPlayer
{
    public class VideoPlayerModule : BaseModule
    {
        public VideoPlayerModule(IRegionManager regionManager, IUnityContainer container)
            : base(container, regionManager) { }
        protected override void RegisterTypes()
        {
            this.Container.RegisterType<IVideoPlayerViewModel, VideoPlayer.ViewModel.MediaPlayerViewModel>();
            this.Container.RegisterType<IVideoPlayerView, VideoPlayer.Views.VideoPlayer>();
            this.RegionManager.RegisterViewWithRegion(Coneixement.Infrastructure.RegionNames.ActionRegion, typeof(VideoPlayer.Views.VideoPlayer));
        }
    }
}
