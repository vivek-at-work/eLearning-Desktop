using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coneixement.Infrastructure.Base;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ServiceLocation;
using Coneixement.Infrastructure;
using Coneixement.ImageViewer.Interfaces;
using Coneixement.ImageViewer.Views;
using Coneixement.ImageViewer.ViewModals;
namespace Coneixement.ImageViewer
{
  public  class ImageViewerModule : BaseModule
    {
      public ImageViewerModule(IRegionManager regionManager, IUnityContainer container)
            : base(container, regionManager) { }
        protected override void RegisterTypes()
        {
            this.Container.RegisterType<IImageViewerView, ImageViwerControl>();
            this.Container.RegisterType<IImageViewerViewModal, ImageViewerViewModal>();
            this.RegionManager.RegisterViewWithRegion(Coneixement.Infrastructure.RegionNames.ActionRegion, typeof(ImageViwerControl));
        }
    }
}
