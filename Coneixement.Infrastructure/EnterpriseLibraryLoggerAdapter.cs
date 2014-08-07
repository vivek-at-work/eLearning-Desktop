using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Prism.Logging;
using System;
namespace Coneixement.Infrastructure
{
    public class EnterpriseLibraryLoggerAdapter : ILoggerFacade
    {
        #region ILoggerFacade Members
        public void Log(string message, Category category, Priority priority)
        {
            Logger.Write(message, category.ToString(), (int)priority); // <--Blows up here 
        }
        #endregion
        public void Log(string p)
        {
            throw new NotImplementedException();
        }
    }
}
