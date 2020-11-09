/* Copyright (c) 2020 
 * Owned by Sahana. All rights reserved.
 */

namespace Assessment.Interfaces.Logging
{
    /// <summary>
    /// Trace logging interface
    /// </summary>
    public interface ITraceLogger
    {
        /// <summary>
        /// Logs severity as INFO.
        /// </summary>
        /// <param name="msg">The log message.</param>
        void Info(string msg);
    }
}
