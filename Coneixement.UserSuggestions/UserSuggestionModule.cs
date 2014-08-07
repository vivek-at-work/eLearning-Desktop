using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Composite.Events;

namespace Coneixement.UserSuggestions
{
    public class UserSuggestionModule
    {
         private IUnityContainer _container;
        private readonly IRegionManager _regionManager;
        IEventAggregator _eventAggrigator;
        public UserSuggestionModule(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggrigator)
             {
             _container = container ;
             _regionManager = regionManager;
             _eventAggrigator = eventAggrigator;
              }
        public void Initialize()
        {
            //_container.RegisterType<IKnowledgeTreeView,KnowledgeTreeView>();
            //_container.RegisterType<IKnowledgeTreeViewModel, KnowledgeTreeViewModel>();
            //_regionManager.RegisterViewWithRegion("MenuBarRegion", typeof(KnowledgeTreeView));
            //_eventAggrigator.GetEvent<KnowledgeTreeSelectionChangedEvent>().Publish(new KnowledgeTreeEntity("Me"));
        }
    }
}
