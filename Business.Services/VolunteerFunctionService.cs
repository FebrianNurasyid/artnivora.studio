namespace Artnivora.Studio.Portal.Business.Services
{
    using Artnivora.Studio.Portal.Business.Models;
    using Artnivora.Studio.Portal.Data.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Object representing business service to manage volunteer function
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class VolunteerFunctionService : IDisposable
    {
        private readonly IVolunteerFunctionDataService _volunteerFunctionDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VolunteerFunctionService"/> class.
        /// </summary>
        public VolunteerFunctionService(IVolunteerFunctionDataService volunteerFunctionDataService)
        {
            _volunteerFunctionDataService = volunteerFunctionDataService;
        }

        /// <summary>
        /// Gets all functions.
        /// </summary>
        /// <returns>List with volunteer functions</returns>
        public IEnumerable<VolunteerFunction> GetAllFunctions()
        {
            return _volunteerFunctionDataService.Get();
        }

        /// <summary>
        /// Gets all functions.
        /// </summary>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <returns>List with volunteer functions filtered on target audiences</returns>
        /// <exception cref="ArgumentNullException">targetAudiences</exception>
        public IEnumerable<VolunteerFunction> GetAllFunctions(IEnumerable<TargetAudience> targetAudiences)
        {
            if (targetAudiences == null)
                throw new ArgumentNullException("targetAudiences");

            var functions = _volunteerFunctionDataService.Get();
            var listFunctions = new List<VolunteerFunction>();

            foreach(var function in _volunteerFunctionDataService.Get())
            {
                var functionTargetAudiences = new List<TargetAudience>(function.TargetAudiences);
                foreach(var targetAudience in targetAudiences)
                {
                    if(functionTargetAudiences.Contains(targetAudience))
                    {
                        listFunctions.Add(function);
                        continue;
                    }
                }
            }
            return listFunctions;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
