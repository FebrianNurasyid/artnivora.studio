namespace Artnivora.Studio.Portal.Web.Shared
{
    using Artnivora.Studio.Portal.Business.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class HVBContext
    {
        private HVBContext()
        {

        }

        public static HVBContext Current
        {
            get
            {
                return new HVBContext();
            }
        }

        public User User
        {
            get
            {
                return new User();
            }
        }
    }
}
