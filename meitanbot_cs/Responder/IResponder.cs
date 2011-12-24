using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meitanbot
{
    /// <summary>
    /// A interface for classes that can respond.
    /// </summary>
    interface IResponder
    {
        /// <summary>
        /// Gets a response text for the specified input.
        /// </summary>
        /// <param name="input">An input text.</param>
        /// <returns>A response text.</returns>
        string Response(string input, params object[] args);
    }
}
