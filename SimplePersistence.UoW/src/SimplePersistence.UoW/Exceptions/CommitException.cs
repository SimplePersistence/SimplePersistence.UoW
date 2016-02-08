﻿#region License
// The MIT License (MIT)
// 
// Copyright (c) 2016 SimplePersistence
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion
namespace SimplePersistence.UoW.Exceptions
{
    using System;

    /// <summary>
    /// Exception thrown when a commit to a <see cref="IUnitOfWork"/> fails.
    /// </summary>
    public class CommitException : UnitOfWorkException
    {
        private const string DefaultMessage =
            "Failed to commit the current work. See inner exception for details.";

        /// <summary>
        /// Creates a new instance with the default message
        /// </summary>
        public CommitException() : this(DefaultMessage)
        {

        }

        /// <summary>
        /// Creates a new instance with the default message and the related exception
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public CommitException(Exception innerException) : base(DefaultMessage, innerException)
        {

        }

        /// <summary>
        /// Creates a new instance with the given error message
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public CommitException(string message) : base(message)
        {

        }

        /// <summary>
        /// Creates a new instance with the given error message and the related exception
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public CommitException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}