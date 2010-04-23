////////////////////////////////////////////////////////////////////////////////
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
		Dictionary<string, int> m_indicesByLongName;

		/// <summary>
		/// Indices to switches in collection, accessed by name.
		/// </summary>
		Dictionary<string, int> m_indicesByName;

		/// <summary>
		/// Switches in collection.
		/// </summary>
		List<Switch> m_switches;

		/// <summary>
		/// Constructor.
		/// </summary>
		public SwitchCollection()
		{
			m_indicesByLongName = new Dictionary<string, int>();
			m_indicesByName = new Dictionary<string, int>();
			m_switches = new List<Switch>();
		}

		////////////////////////////////////////////////////////////////////////
		// Methods

		/// <summary>
		/// Add switch with given name.
		/// </summary>
		public void Add(string name)
		{
			Add(new Switch(name));
		}

		/// <summary>
		/// Add switch with given name and description.
		/// </summary>
		public void Add(string name, string description)
		{
			Add(new Switch(name, description));
		}

		/// <summary>
		/// Add switch with given name, long name and description.
		/// </summary>
		public void Add(string name, string longName, string description)
		{
			Add(new Switch(name, longName, description));
		}

		/// <summary>
		/// Add switch with given name, long name, description and whether
		/// it is required.
		/// </summary>
		public void Add(string name, string longName, string description,
			bool isRequired)
		{
			Add(new Switch(name, longName, description, isRequired));
		}

		/// <summary>
		/// Add switch with given name, long name, description, whether
		/// it has arguments and whether it is required.
		/// </summary>
		public void Add(string name, string longName, string description,
			bool hasArguments, bool isRequired)
		{
			Add(new Switch(name, longName, description, hasArguments,
				isRequired));
		}

		/// <summary>
		/// Add switch with given name, long name, description, number of
		/// expected arguments and whether it is required.
		/// </summary>
		public void Add(string name, string longName, string description,
			int numberArguments, bool isRequired)
		{
			Add(new Switch(name, longName, description, numberArguments,
				isRequired));
		}

		/// <summary>
		/// Add switch with given name, long name, description, whether
		/// it has arguments, whether it is required and name of
		/// expected arguments.
		/// </summary>
		public void Add(string name, string longName, string description,
			bool hasArguments, bool isRequired, string argumentName)
		{
			Add(new Switch(name, longName, description, hasArguments,
				isRequired, argumentName));
		}

		/// <summary>
		/// Add switch with given name, long name, description, whether it is
		/// required and name of expected argument.
		/// </summary>
		public void Add(string name, string longName, string description,
			bool isRequired, string argumentName)
		{
			Add(new Switch(name, longName, description, isRequired,
				argumentName));
		}

		/// <summary>
		/// Add switch with given name, long name, description, whether it is
		/// required and names of expected arguments.
		/// </summary>
		public void Add(string name, string longName, string description,
			bool isRequired, string[] argumentNames)
		{
			Add(new Switch(name, longName, description, isRequired,
				argumentNames));
		}

		/// <summary>
		/// Determine if given switch already added.
		/// </summary>
		public bool HasSwitch(Switch switchObject)
		{
			bool hasSwitch = false;

			if (switchObject != null)
			{
				hasSwitch = m_switches.Contains(switchObject) ||
					m_indicesByName.ContainsKey(switchObject.Name);

				if (switchObject.HasLongName)
					hasSwitch |= m_indicesByLongName.ContainsKey(switchObject.LongName);
			}

			return hasSwitch;
		}

		/// <summary>
		/// Determine if switch with given name already added.
		/// </summary>
		public bool HasSwitch(string name)
		{
			return m_indicesByName.ContainsKey(name) || m_indicesByLongName.ContainsKey(name);
		}

		////////////////////////////////////////////////////////////////////////
		// Properties

		/// <summary>
		/// Get switch with given name from collection.
		/// </summary>
		public Switch this[string name]
		{
			get
			{
				if (HasSwitch(name))
				{
					int index;

					if (m_indicesByName.ContainsKey(name))
						index = m_indicesByName[name];
					else
						index = m_indicesByLongName[name];

					return Switches[index];
				}

				return null;
			}
		}

		/// <summary>
		/// Get all switches in collection sorted by name.
		/// </summary>
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
		public IList<Switch> Switches
		{
			get { return m_switches; }
		}

		////////////////////////////////////////////////////////////////////////
		// ICollection<T>

		/// <summary>
		/// Get number of switches in collection.
		/// </summary>
		public int Count
		{
			get { return m_switches.Count; }
		}

		/// <summary>
		/// Determine if collection is read-only.
		/// </summary>
		public bool IsReadOnly
		{
			get { return false; }
		}

		/// <summary>
		/// Add given switch to collection.
		/// </summary>
		public void Add(Switch item)
		{
			if (item == null)
				return;

			if (!HasSwitch(item))
			{
				int newIndex = Count;

				m_switches.Add(item);

				m_indicesByName.Add(item.Name, newIndex);

				if (item.HasLongName)
					m_indicesByLongName.Add(item.LongName, newIndex);
			}
		}

		/// <summary>
		/// Remove all switches from collection.
		/// </summary>
		public void Clear()
		{
			m_indicesByLongName.Clear();
			m_indicesByName.Clear();
			m_switches.Clear();
		}

		/// <summary>
		/// Determine if given switch contained in collection.
		/// </summary>
		public bool Contains(Switch item)
		{
			return m_switches.Contains(item);
		}

		/// <summary>
		/// Copy collection to given array, starting at given index.
		/// </summary>
		public void CopyTo(Switch[] array, int arrayIndex)
		{
			m_switches.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Remove first occurrence of given switch from collection.
		/// 
		/// Return whether switch successfully removed from collection.
		/// </summary>
		public bool Remove(Switch item)
		{
			return m_switches.Remove(item);
		}

		////////////////////////////////////////////////////////////////////////
		// IEnumerable

		/// <summary>
		/// Get enumerator to use to iterate through collection.
		/// </summary>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return m_switches.GetEnumerator();
		}

		////////////////////////////////////////////////////////////////////////
		// IEnumerable<T>

		/// <summary>
		/// Get enumerator to use to iterate through collection.
		/// </summary>
		public IEnumerator<Switch> GetEnumerator()
		{
			return m_switches.GetEnumerator();
		}

		////////////////////////////////////////////////////////////////////////
		// IList<T>

		/// <summary>
		/// Get switch from collection at given index.
		/// </summary>
		public Switch this[int index]
		{
			get { return m_switches[index]; }
			set { m_switches[index] = value; }
		}

		/// <summary>
		/// Get index of given switch in collection.
		/// </summary>
		public int IndexOf(Switch item)
		{
			return m_switches.IndexOf(item);
		}

		/// <summary>
		/// Insert given switch at given index in collection.
		/// </summary>
		public void Insert(int index, Switch item)
		{
			m_switches.Insert(index, item);
		}

		/// <summary>
		/// Remove switch at given index in collection.
		/// </summary>
		public void RemoveAt(int index)
		{
			m_switches.RemoveAt(index);
		}
	}
}
