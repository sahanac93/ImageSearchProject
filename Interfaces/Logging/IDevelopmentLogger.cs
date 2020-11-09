/* Copyright (c) 2020 
 * Owned by Sahana. All rights reserved.
 */

namespace Assessment.Interfaces.Logging
{
    /// <summary>
    /// Development logging
    /// </summary>
    public interface IDevelopmentLogger
    {
        /// <summary>
        /// Logs severity as ERROR.
        /// </summary>
        /// <param name="msg">Error message.</param>
        void Error(object msg);

        /// <summary>
        /// Logs severity as INFORMATIONAL.
        /// </summary>
        /// <param name="msg">Info message.</param>
        void Info(string msg);

        /// <summary>
        /// Logs severity as WARNING.
        /// </summary>
        /// <param name="msg">Warning message.</param>
        void Warning(string msg);
    }
}
