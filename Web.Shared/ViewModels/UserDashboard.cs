using System;
using System.Collections.Generic;
using System.Text;

namespace Artnivora.Studio.Portal.Web.Shared.ViewModels
{
    public class UserDashboard
    {
        private string _firstName;
        private string _lastName;
        private int _registeredCount;
        private int _unReadCount;
        private int _requestCount;
        private bool _userProfileIsComplete;


        public string FirstName
        {
            get
            {
                return this._firstName;
            }

            set
            {
                this._firstName = value;
            }
        }
        public string LastName
        {
            get
            {
                return this._lastName;
            }

            set
            {
                this._lastName = value;
            }
        }

        public int RegisteredCount
        {
            get
            {
                return this._registeredCount;
            }

            set
            {
                this._registeredCount = value;
            }
        }
        public int UnReadCount
        {
            get
            {
                return this._unReadCount;
            }

            set
            {
                this._unReadCount = value;
            }
        }
        public int RequestCount
        {
            get
            {
                return this._requestCount;
            }

            set
            {
                this._requestCount = value;
            }
        }
        public bool UserProfileIsComplete
        {
            get
            {
                return this._userProfileIsComplete;
            }

            set
            {
                this._userProfileIsComplete = value;
            }
        }
    }
}
