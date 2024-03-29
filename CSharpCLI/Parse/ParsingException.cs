﻿////////////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2010 Bernard Badjari
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
//
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Runtime.Serialization;

namespace CSharpCLI.Parse
{
	/// <summary>
	/// Exception thrown during parsing of command-line arguments.
	/// </summary>
	[Serializable]
	public class ParsingException : Exception
	{
		////////////////////////////////////////////////////////////////////////
		// Constructors

		/// <summary>
		/// Constructor.
		/// </summary>
		public ParsingException()
			: base()
		{
		}

		/// <summary>
		/// Constructor specifying error message.
		/// </summary>
		/// <param name="message">
		/// String representing error message.
		/// </param>
		public ParsingException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Constructor specifying error message and inner exception.
		/// </summary>
		/// <param name="message">
		/// String representing error message.
		/// </param>
		/// <param name="innerException">
		/// Exception representing inner exception.
		/// </param>
		public ParsingException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Constructor specifying serialization data.
		/// </summary>
		/// <param name="info">
		/// SerializationInfo representing serialized exception data.
		/// </param>
		/// <param name="context">
		/// StreamingContext representing contextual data about exception source/destination.
		/// </param>
		protected ParsingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
