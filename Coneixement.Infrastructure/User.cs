using System;
namespace Coneixement.Infrastructure
{
  [Serializable]
    public class User
    {
        public string UserID
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public string Role
        {
            get;
            set;
        }
        public DateTime LastLoginOn
        {
            get;
            set;
        }
        public LastLoginStatus LastLoginStatus
        {
            get;
            set;
        }
    }
  public enum LastLoginStatus
  {
      Success,
      Failure
  }
}
