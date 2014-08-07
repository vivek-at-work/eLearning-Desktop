using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coneixement.Infrastructure.Base;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ServiceLocation;
using Coneixement.Infrastructure;
namespace Coneixement.PDFViewer
{
   
        public class PDFViewerModule : BaseModule
        {
            public PDFViewerModule(IRegionManager regionManager, IUnityContainer container)
                : base(container, regionManager) { }
           
            protected override void RegisterTypes()
            {
             //   this.Container.RegisterType<IVideoPlayerViewModel, VideoPlayer.ViewModel.MediaPlayerViewModel>();
              //  this.Container.RegisterType<IVideoPlayerView, VideoPlayer.Views.VideoPlayer>();
            }
        }
    
}
