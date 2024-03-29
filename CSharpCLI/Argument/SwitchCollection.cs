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

using System.Collections;
using System.Collections.Generic;

namespace CSharpCLI.Argument
{
	/// <summary>
	/// Managed collection of Switch objects.
	/// </summary>
	public class SwitchCollection : ICollection<Switch>, IEnumerable<Switch>, IList<Switch>
	{
		/// <summary>
		/// Indices to switches in collection, accessed by long name.
		/// </summary>
		private Dictionary<string, int> indicesByLongName;

		/// <summary>
		/// Indices to switches in collection, accessed by name.
		/// </summary>
		private Dictionary<string, int> indicesByName;

		/// <summary>
		/// Switches in collection.
		/// </summary>
		private List<Switch> switches;

		////////////////////////////////////////////////////////////////////////
		// Constructors

		/// <summary>
		/// Constructor.
		/// </summary>
		public SwitchCollection()
		{
			indicesByLongName = new Dictionary<string, int>();
			indicesByName = new Dictionary<string, int>();
			switches = new List<Switch>();
		}

		////////////////////////////////////////////////////////////////////////
		// Methods - Printable

		/// <summary>
		/// Add switch to collection with given name and description.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="description">
		/// String representing switch description.
		/// </param>
		public void Add(string name, string description)
		{
			Add(new Switch(name, description));
		}

		/// <summary>
		/// Add switch to collection with given name, long name and description.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="longName">
		/// String representing switch long name.
		/// </param>
		/// <param name="description">
		/// String representing switch description.
		/// </param>
		public void Add(string name, string longName, string description)
		{
			Add(new Switch(name, longName, description));
		}

		/// <summary>
		/// Add switch to collection with given name, long name, description and whether it is required.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="longName">
		/// String representing switch long name.
		/// </param>
		/// <param name="description">
		/// String representing switch description.
		/// </param>
		/// <param name="isRequired">
		/// True if required switch, false otherwise.
		/// </param>
		public void Add(string name, string longName, string description, bool isRequired)
		{
			Add(new Switch(name, longName, description, isRequired));
		}

		/// <summary>
		/// Add switch to collection with given name, long name, description, whether it has arguments and whether it is required.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="longName">
		/// String representing switch long name.
		/// </param>
		/// <param name="description">
		/// String representing switch description.
		/// </param>
		/// <param name="hasArguments">
		/// True if arguments expected to follow switch, false otherwise.
		/// </param>
		/// <param name="isRequired">
		/// True if required switch, false otherwise.
		/// </param>
		public void Add(string name, string longName, string description, bool hasArguments, bool isRequired)
		{
			Add(new Switch(name, longName, description, hasArguments, isRequired));
		}

		/// <summary>
		/// Add switch to collection with given name, long name, description, and number of expected arguments.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="longName">
		/// String representing switch long name.
		/// </param>
		/// <param name="description">
		/// String representing switch description.
		/// </param>
		/// <param name="numberArguments">
		/// Integer representing number of arguments expected to follow switch.
		/// </param>
		public void Add(string name, string longName, string description, int numberArguments)
		{
			Add(new Switch(name, longName, description, numberArguments));
		}

		/// <summary>
		/// Add switch to collection with given name, long name, description, number of expected arguments and whether it is required.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="longName">
		/// String representing switch long name.
		/// </param>
		/// <param name="description">
		/// String representing switch description.
		/// </param>
		/// <param name="numberArguments">
		/// Integer representing number of arguments expected to follow switch.
		/// </param>
		/// <param name="isRequired">
		/// True if required switch, false otherwise.
		/// </param>
		public void Add(string name, string longName, string description, int numberArguments, bool isRequired)
		{
			Add(new Switch(name, longName, description, numberArguments, isRequired));
		}

		/// <summary>
		/// Add switch to collection with given name, long name, description, whether it has arguments, whether it is required and name of expected arguments.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="longName">
		/// String representing switch long name.
		/// </param>
		/// <param name="description">
		/// String representing switch description.
		/// </param>
		/// <param name="hasArguments">
		/// True if arguments expected to follow switch, false otherwise.
		/// </param>
		/// <param name="isRequired">
		/// True if required switch, false otherwise.
		/// </param>
		/// <param name="argumentName">
		/// String representing name of arguments expected to follow switch.
		/// </param>
		public void Add(string name, string longName, string description, bool hasArguments, bool isRequired, string argumentName)
		{
			Add(new Switch(name, longName, description, hasArguments, isRequired, argumentName));
		}

		/// <summary>
		/// Add switch to collection with given name, long name, description, whether it is required and name of expected argument.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="longName">
		/// String representing switch long name.
		/// </param>
		/// <param name="description">
		/// String representing switch description.
		/// </param>
		/// <param name="isRequired">
		/// True if required switch, false otherwise.
		/// </param>
		/// <param name="argumentName">
		/// String representing name of single argument expected to follow switch.
		/// </param>
		public void Add(string name, string longName, string description, bool isRequired, string argumentName)
		{
			Add(new Switch(name, longName, description, isRequired, argumentName));
		}

		/// <summary>
		/// Add switch to collection with given name, long name, description, whether it is required and names of expected arguments.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="longName">
		/// String representing switch long name.
		/// </param>
		/// <param name="description">
		/// String representing switch description.
		/// </param>
		/// <param name="isRequired">
		/// True if required switch, false otherwise.
		/// </param>
		/// <param name="argumentNames">
		/// Array of strings representing names of arguments expected to follow switch.
		/// </param>
		public void Add(string name, string longName, string description, bool isRequired, string[] argumentNames)
		{
			Add(new Switch(name, longName, description, isRequired, argumentNames));
		}

		////////////////////////////////////////////////////////////////////////
		// Methods - Non-Printable

		/// <summary>
		/// Add switch to collection with given name.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		public void Add(string name)
		{
			Add(new Switch(name));
		}

		/// <summary>
		/// Add switches to collection with given names.
		/// </summary>
		/// <param name="names">
		/// Array of strings representing switch names.
		/// </param>
		public void Add(string[] names)
		{
			if (names != null)
			{
				foreach (string name in names)
					Add(new Switch(name));
			}
		}

		/// <summary>
		/// Add switch to collection with given name and whether it is required.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="isRequired">
		/// True if required switch, false otherwise.
		/// </param>
		public void Add(string name, bool isRequired)
		{
			Add(new Switch(name, isRequired));
		}

		/// <summary>
		/// Add switch to collection with given name, whether it has arguments and whether it is required.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="hasArguments">
		/// True if arguments expected to follow switch, false otherwise.
		/// </param>
		/// <param name="isRequired">
		/// True if required switch, false otherwise.
		/// </param>
		public void Add(string name, bool hasArguments, bool isRequired)
		{
			Add(new Switch(name, hasArguments, isRequired));
		}

		/// <summary>
		/// Add switch to collection with given name and number of expected arguments.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="numberArguments">
		/// Integer representing number of arguments expected to follow switch.
		/// </param>
		public void Add(string name, int numberArguments)
		{
			Add(new Switch(name, numberArguments));
		}

		/// <summary>
		/// Add switch to collection with given name, number of expected arguments and whether it is required.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <param name="numberArguments">
		/// Integer representing number of arguments expected to follow switch.
		/// </param>
		/// <param name="isRequired">
		/// True if required switch, false otherwise.
		/// </param>
		public void Add(string name, int numberArguments, bool isRequired)
		{
			Add(new Switch(name, numberArguments, isRequired));
		}

		////////////////////////////////////////////////////////////////////////
		// Methods

		/// <summary>
		/// Determine if switch with given name already added to collection.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <returns>
		/// True if collection contains switch with given name, false otherwise.
		/// </returns>
		public bool HasSwitch(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				return false;

			return indicesByName.ContainsKey(name) || indicesByLongName.ContainsKey(name);
		}

		/// <summary>
		/// Add indices to given switch in collection, by name.
		/// </summary>
		/// <param name="item">
		/// Switch object representing switch to index by name.
		/// </param>
		/// <param name="index">
		/// Integer representing index to switch in collection.
		/// </param>
		private void AddIndices(Switch item, int index)
		{
			indicesByName.Add(item.Name, index);

			if (item.HasLongName)
				indicesByLongName.Add(item.LongName, index);

			ReorderIndices(++index, true);
		}

		/// <summary>
		/// Remove indices to given switch in collection, by name.
		/// </summary>
		/// <param name="item">
		/// Switch object representing switch whose indices to remove.
		/// </param>
		private void RemoveIndices(Switch item)
		{
			int index = indicesByName[item.Name];

			indicesByName.Remove(item.Name);

			if (item.HasLongName)
				indicesByLongName.Remove(item.LongName);

			ReorderIndices(index, false);
		}

		/// <summary>
		/// Reorder indices to switch names in collection, starting at given index and in given order.
		/// </summary>
		/// <param name="index">
		/// Integer representing index to start reordering at.
		/// </param>
		/// <param name="ascending">
		/// True if reordering indices in ascending order, false if in descending order.
		/// </param>
		private void ReorderIndices(int index, bool ascending)
		{
			const int IndexOffset = 1;

			int offset = ascending ? IndexOffset : -IndexOffset;

			while (index < Count)
			{
				Switch item = Switches[index];

				indicesByName[item.Name] += offset;

				if (item.HasLongName)
					indicesByLongName[item.LongName] += offset;

				index++;
			}
		}

		////////////////////////////////////////////////////////////////////////
		// Properties

		/// <summary>
		/// Get switch with given name from collection.
		/// </summary>
		/// <param name="name">
		/// String representing switch name.
		/// </param>
		/// <returns>
		///		<para>
		///		Switch object representing switch with given name.
		///		</para>
		///		<para>
		///		Null if switch with given name not in collection.
		///		</para>
		/// </returns>
		public Switch this[string name]
		{
			get
			{
				if (HasSwitch(name))
				{
					int index;

					if (indicesByName.ContainsKey(name))
						index = indicesByName[name];
					else
						index = indicesByLongName[name];

					return Switches[index];
				}

				return null;
			}
		}

		/// <summary>
		/// Get all switches in collection sorted by name.
		/// </summary>
		/// <value>
		/// List of switches in collection sorted by name.
		/// </value>
		public IList<Switch> SortedSwitches
		{
			get
			{
				List<Switch> sortedSwitches = new List<Switch>(Switches);

				sortedSwitches.Sort(new SwitchComparer());

				return sortedSwitches.ToArray();
			}
		}

		/// <summary>
		/// Get all switches in collection.
		/// </summary>
		/// <value>
		/// List of switches in collection.
		/// </value>
		public IList<Switch> Switches
		{
			get { return switches; }
		}

		////////////////////////////////////////////////////////////////////////
		// ICollection<T>

		/// <summary>
		/// Get number of switches in collection.
		/// </summary>
		/// <value>
		/// Integer representing number of switches in collection.
		/// </value>
		public int Count
		{
			get { return switches.Count; }
		}

		/// <summary>
		/// Determine if collection is read-only.
		/// </summary>
		/// <value>
		/// True if collection is read-only, false otherwise.
		/// </value>
		public bool IsReadOnly
		{
			get { return false; }
		}

		/// <summary>
		/// Add given switch to collection.
		/// </summary>
		/// <param name="item">
		/// Switch object representing switch to add to collection.
		/// </param>
		public void Add(Switch item)
		{
			if (item == null)
				return;

			if (!Contains(item))
			{
				int newIndex = Count;

				switches.Add(item);

				AddIndices(item, newIndex);
			}
		}

		/// <summary>
		/// Remove all switches from collection.
		/// </summary>
		public void Clear()
		{
			indicesByLongName.Clear();
			indicesByName.Clear();
			switches.Clear();
		}

		/// <summary>
		/// Determine if given switch contained in collection.
		/// </summary>
		/// <param name="item">
		/// Switch object representing switch to find in collection.
		/// </param>
		/// <returns>
		/// True if collection contains given switch, false otherwise.
		/// </returns>
		public bool Contains(Switch item)
		{
			bool contains = false;

			if (item != null)
			{
				contains = switches.Contains(item) || indicesByName.ContainsKey(item.Name);

				if (item.HasLongName)
					contains |= indicesByLongName.ContainsKey(item.LongName);
			}

			return contains;
		}

		/// <summary>
		/// Copy collection to given array, starting at given index.
		/// </summary>
		/// <param name="array">
		/// Array of Switch objects to copy collection to.
		/// </param>
		/// <param name="arrayIndex">
		/// Integer representing index to start copying to in given array.
		/// </param>
		public void CopyTo(Switch[] array, int arrayIndex)
		{
			switches.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Remove first occurrence of given switch from collection.
		/// </summary>
		/// <param name="item">
		/// Switch object representing switch to remove from collection.
		/// </param>
		/// <returns>
		/// Return whether switch successfully removed from collection.
		/// </returns>
		public bool Remove(Switch item)
		{
			bool removed = false;

			if (item != null && Contains(item))
			{
				removed = switches.Remove(item);

				RemoveIndices(item);
			}

			return removed;
		}

		////////////////////////////////////////////////////////////////////////
		// IEnumerable

		/// <summary>
		/// Get enumerator to use to iterate through collection.
		/// </summary>
		/// <returns>
		/// Enumerator to use to iterate through collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		////////////////////////////////////////////////////////////////////////
		// IEnumerable<T>

		/// <summary>
		/// Get typed enumerator to use to iterate through collection.
		/// </summary>
		/// <returns>
		/// Typed enumerator to use to iterate through collection.
		/// </returns>
		public IEnumerator<Switch> GetEnumerator()
		{
			return switches.GetEnumerator();
		}

		////////////////////////////////////////////////////////////////////////
		// IList<T>

		/// <summary>
		/// Get switch from collection or set switch in collection at given index.
		/// </summary>
		/// <param name="index">
		/// Integer representing index in collection.
		/// </param>
		/// <returns>
		/// Switch object representing switch at given index in collection.
		/// </returns>
		public Switch this[int index]
		{
			get { return Switches[index]; }
			set { Insert(index, value); }
		}

		/// <summary>
		/// Get index of given switch in collection.
		/// </summary>
		/// <param name="item">
		/// Switch object representing switch in collection.
		/// </param>
		/// <returns>
		/// Integer representing index of given switch in collection.
		/// </returns>
		public int IndexOf(Switch item)
		{
			return switches.IndexOf(item);
		}

		/// <summary>
		/// Insert given switch at given index in collection.
		/// </summary>
		/// <param name="index">
		/// Integer representing index to insert given switch at in collection.
		/// </param>
		/// <param name="item">
		/// Switch object representing switch to insert in collection.
		/// </param>
		public void Insert(int index, Switch item)
		{
			if (item == null)
				return;

			if (!Contains(item))
			{
				switches.Insert(index, item);

				AddIndices(item, index);
			}
		}

		/// <summary>
		/// Remove switch at given index in collection.
		/// </summary>
		/// <param name="index">
		/// Integer representing index to remove switch from in collection.
		/// </param>
		public void RemoveAt(int index)
		{
			Switch item = Switches[index];

			switches.RemoveAt(index);

			RemoveIndices(item);
		}
	}
}
