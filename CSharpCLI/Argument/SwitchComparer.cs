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

using System.Collections.Generic;
using System.Globalization;

namespace CSharpCLI.Argument
{
	/// <summary>
	/// Compares Switch objects.
	/// </summary>
	internal class SwitchComparer : IComparer<Switch>
	{
		////////////////////////////////////////////////////////////////////////
		// Methods

		/// <summary>
		/// Compare given Switch objects by name.
		/// </summary>
		/// <param name="x">
		/// Switch object representing first switch to compare by name.
		/// </param>
		/// <param name="y">
		/// Switch object representing second switch to compare by name.
		/// </param>
		/// <returns>
		/// Integer representing the sort order of given switches.
		/// </returns>
		public int Compare(Switch x, Switch y)
		{
			return string.Compare(x.Name, y.Name, true, CultureInfo.CurrentCulture);
		}
	}
}
