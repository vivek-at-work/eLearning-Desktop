using Coneixement.Infrastructure;
using System;
namespace Coneixement.SplashScreen.Interfaces 
{
    interface ISplashScreenViewModal : IViewModel
    {
        string ApplicationName { get; }
        string ApplicationVersion { get; }
    }
}
