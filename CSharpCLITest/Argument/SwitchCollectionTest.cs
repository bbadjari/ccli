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
using CSharpCLI.Argument;
using NUnit.Framework;

namespace CSharpCLI.Test.Argument
{
	/// <summary>
	/// NUnit unit tests for SwitchCollection class.
	/// </summary>
	[TestFixture]
	public class SwitchCollectionTest
	{
		/// <summary>
		/// Switch test description.
		/// </summary>
		const string Description = "Test switch.";

		/// <summary>
		/// Switch has arguments.
		/// </summary>
		const bool HasArguments = true;

		/// <summary>
		/// Switch required.
		/// </summary>
		const bool IsRequired = true;

		/// <summary>
		/// Switch test long name.
		/// </summary>
		const string LongName = "testSwitch";

		/// <summary>
		/// Switch test name.
		/// </summary>
		const string Name = "test";

		/// <summary>
		/// No switch argument names.
		/// </summary>
		static readonly string[] NoArgumentNames = new string[NoArguments];

		/// <summary>
		/// No switch argument values.
		/// </summary>
		static readonly string[] NoArgumentValues = new string[NoArguments];

		/// <summary>
		/// No switch arguments.
		/// </summary>
		const int NoArguments = 0;

		/// <summary>
		/// No switches.
		/// </summary>
		const int NoSwitches = 0;

		/// <summary>
		/// One switch argument.
		/// </summary>
		const int OneArgument = 1;

		/// <summary>
		/// One switch in collection.
		/// </summary>
		const int OneSwitch = 1;

		/// <summary>
		/// Switch has unknown number of arguments.
		/// </summary>
		const int UnknownNumberArguments = int.MaxValue;

		////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// SwitchCollection to use for each test.
		/// </summary>
		SwitchCollection m_switches;

		/// <summary>
		/// Initialization for each test.
		/// </summary>
		[SetUp]
		public void Initialize()
		{
			m_switches = new SwitchCollection();
		}

		////////////////////////////////////////////////////////////////////////
		// Methods

		/// <summary>
		/// Test Add() method defining argument name expected to follow
		/// switch.
		/// </summary>
		[Test]
		public void AddWithArgumentName()
		{
			const string ArgumentName = "arg";

			m_switches.Add(Name, LongName, Description, IsRequired,
				ArgumentName);

			Assert.AreEqual(m_switches.Count, OneSwitch);

			Assert.IsTrue(m_switches.HasSwitch(Name));
			Assert.IsTrue(m_switches.HasSwitch(LongName));

			Switch switchObject = m_switches[Name];

			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsOptional);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasArguments);
			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasEnoughNames);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsRequired);

			string[] argumentNames = switchObject.GetArgumentNames();

			Assert.AreEqual(argumentNames.Length, OneArgument);
			Assert.AreEqual(argumentNames[0], ArgumentName);
		}

		/// <summary>
		/// Test Add() method defining argument names expected to follow
		/// switch.
		/// </summary>
		[Test]
		public void AddWithArgumentNames()
		{
			string[] argumentNames = new string[] { "arg1", "arg2", "arg3" };

			m_switches.Add(Name, LongName, Description, IsRequired, argumentNames);

			Assert.AreEqual(m_switches.Count, OneSwitch);

			Assert.IsTrue(m_switches.HasSwitch(Name));
			Assert.IsTrue(m_switches.HasSwitch(LongName));

			Switch switchObject = m_switches[Name];

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), argumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.LongName, LongName);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, argumentNames.Length);

			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsOptional);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasArguments);
			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasEnoughNames);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsRequired);
		}

		/// <summary>
		/// Test Add() method defining name of arguments expected to follow
		/// switch.
		/// </summary>
		[Test]
		public void AddWithArgumentsName()
		{
			const string ArgumentName = "args";

			m_switches.Add(Name, LongName, Description, HasArguments,
				IsRequired, ArgumentName);

			Assert.AreEqual(m_switches.Count, OneSwitch);

			Assert.IsTrue(m_switches.HasSwitch(Name));
			Assert.IsTrue(m_switches.HasSwitch(LongName));

			Switch switchObject = m_switches[Name];

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), new string[] { ArgumentName });
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.LongName, LongName);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, UnknownNumberArguments);

			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsOptional);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasArguments);
			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsRequired);
		}

		/// <summary>
		/// Test Add() method defining switch description.
		/// </summary>
		[Test]
		public void AddWithDescription()
		{
			m_switches.Add(Name, Description);

			Assert.AreEqual(m_switches.Count, OneSwitch);

			Assert.IsFalse(m_switches.HasSwitch(LongName));

			Assert.IsTrue(m_switches.HasSwitch(Name));

			Switch switchObject = m_switches[Name];

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, NoArguments);

			Assert.IsFalse(switchObject.HasArguments);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.HasLongName);
			Assert.IsFalse(switchObject.IsRequired);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsNull(switchObject.LongName);

			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.IsOptional);
		}

		/// <summary>
		/// Test Add() method defining switch name with empty name.
		/// </summary>
		[Test]
		public void AddWithEmptyName()
		{
			const string EmptyName = "";

			Assert.Throws<ArgumentException>(delegate { m_switches.Add(EmptyName); });
		}

		/// <summary>
		/// Test Add() method defining switch names with empty names.
		/// </summary>
		[Test]
		public void AddWithEmptyNames()
		{
			const int NumberSwitches = 3;

			string[] emptyNames = new string[NumberSwitches];

			Assert.Throws<ArgumentException>(delegate { m_switches.Add(emptyNames); });
		}

		/// <summary>
		/// Test Add() method defining whether switch has arguments.
		/// </summary>
		[Test]
		public void AddWithHasArguments()
		{
			m_switches.Add(Name, LongName, Description, HasArguments, IsRequired);

			Assert.AreEqual(m_switches.Count, OneSwitch);

			Assert.IsTrue(m_switches.HasSwitch(Name));
			Assert.IsTrue(m_switches.HasSwitch(LongName));

			Switch switchObject = m_switches[Name];

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.LongName, LongName);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, UnknownNumberArguments);

			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsOptional);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasArguments);
			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsRequired);
		}

		/// <summary>
		/// Test Add() method defining switch names with identical names.
		/// </summary>
		[Test]
		public void AddWithIdenticalNames()
		{
			string[] switchNames = new string[] { Name, Name, Name };

			m_switches.Add(switchNames);

			Assert.AreEqual(m_switches.Count, OneSwitch);

			Assert.IsTrue(m_switches.HasSwitch(Name));

			Switch switchObject = m_switches[Name];

			Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, NoArguments);

			Assert.IsFalse(switchObject.HasArguments);
			Assert.IsFalse(switchObject.HasDescription);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.HasLongName);
			Assert.IsFalse(switchObject.IsRequired);

			Assert.IsNotNull(switchObject.Name);

			Assert.IsNull(switchObject.Description);
			Assert.IsNull(switchObject.LongName);

			Assert.IsTrue(switchObject.IsOptional);
		}

		/// <summary>
		/// Test Add() method defining switch optional.
		/// </summary>
		[Test]
		public void AddWithIsOptional()
		{
			m_switches.Add(Name, LongName, Description, !IsRequired);

			Assert.AreEqual(m_switches.Count, OneSwitch);

			Assert.IsTrue(m_switches.HasSwitch(Name));
			Assert.IsTrue(m_switches.HasSwitch(LongName));

			Switch switchObject = m_switches[Name];

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.LongName, LongName);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, NoArguments);

			Assert.IsFalse(switchObject.HasArguments);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsRequired);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsOptional);
		}

		/// <summary>
		/// Test Add() method defining switch required.
		/// </summary>
		[Test]
		public void AddWithIsRequired()
		{
			m_switches.Add(Name, LongName, Description, IsRequired);

			Assert.AreEqual(m_switches.Count, OneSwitch);

			Assert.IsTrue(m_switches.HasSwitch(Name));
			Assert.IsTrue(m_switches.HasSwitch(LongName));

			Switch switchObject = m_switches[Name];

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.LongName, LongName);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, NoArguments);

			Assert.IsFalse(switchObject.HasArguments);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsOptional);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsRequired);
		}

		/// <summary>
		/// Test Add() method defining switch long name.
		/// </summary>
		[Test]
		public void AddWithLongName()
		{
			m_switches.Add(Name, LongName, Description);

			Assert.AreEqual(m_switches.Count, OneSwitch);

			Assert.IsTrue(m_switches.HasSwitch(Name));
			Assert.IsTrue(m_switches.HasSwitch(LongName));

			Switch switchObject = m_switches[Name];

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.LongName, LongName);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, NoArguments);

			Assert.IsFalse(switchObject.HasArguments);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsRequired);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsOptional);
		}

		/// <summary>
		/// Test Add() method defining switch name.
		/// </summary>
		[Test]
		public void AddWithName()
		{
			m_switches.Add(Name);

			Assert.AreEqual(m_switches.Count, OneSwitch);

			Assert.IsFalse(m_switches.HasSwitch(LongName));

			Assert.IsTrue(m_switches.HasSwitch(Name));

			Switch switchObject = m_switches[Name];

			Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, NoArguments);

			Assert.IsFalse(switchObject.HasArguments);
			Assert.IsFalse(switchObject.HasDescription);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.HasLongName);
			Assert.IsFalse(switchObject.IsRequired);

			Assert.IsNotNull(switchObject.Name);

			Assert.IsNull(switchObject.Description);
			Assert.IsNull(switchObject.LongName);

			Assert.IsTrue(switchObject.IsOptional);
		}

		/// <summary>
		/// Test Add() method defining switch names.
		/// </summary>
		[Test]
		public void AddWithNames()
		{
			const string Name1 = "switch1";
			const string Name2 = "switch2";
			const string Name3 = "switch3";

			string[] switchNames = new string[] { Name1, Name2, Name3 };

			m_switches.Add(switchNames);

			Assert.AreEqual(m_switches.Count, switchNames.Length);

			Assert.IsTrue(m_switches.HasSwitch(Name1));
			Assert.IsTrue(m_switches.HasSwitch(Name2));
			Assert.IsTrue(m_switches.HasSwitch(Name3));

			foreach (string switchName in switchNames)
			{
				Switch switchObject = m_switches[switchName];

				Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
				Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
				Assert.AreEqual(switchObject.Name, switchName);
				Assert.AreEqual(switchObject.NumberArguments, NoArguments);

				Assert.IsFalse(switchObject.HasArguments);
				Assert.IsFalse(switchObject.HasDescription);
				Assert.IsFalse(switchObject.HasEnoughNames);
				Assert.IsFalse(switchObject.HasEnoughValues);
				Assert.IsFalse(switchObject.HasLongName);
				Assert.IsFalse(switchObject.IsRequired);

				Assert.IsNotNull(switchObject.Name);

				Assert.IsNull(switchObject.Description);
				Assert.IsNull(switchObject.LongName);

				Assert.IsTrue(switchObject.IsOptional);
			}
		}

		/// <summary>
		/// Test Add() method defining negative number of arguments expected to
		/// follow switch.
		/// </summary>
		[Test]
		public void AddWithNegativeNumberArguments()
		{
			const int NegativeNumberArguments = -3;

			m_switches.Add(Name, LongName, Description, NegativeNumberArguments,
				IsRequired);

			Assert.AreEqual(m_switches.Count, OneSwitch);

			Assert.IsTrue(m_switches.HasSwitch(Name));
			Assert.IsTrue(m_switches.HasSwitch(LongName));

			Switch switchObject = m_switches[Name];

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.LongName, LongName);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, NoArguments);

			Assert.AreNotEqual(switchObject.NumberArguments, NegativeNumberArguments);

			Assert.IsFalse(switchObject.HasArguments);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsOptional);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsRequired);
		}

		/// <summary>
		/// Test Add() method defining switch name with no name.
		/// </summary>
		[Test]
		public void AddWithNoName()
		{
			const string NoName = null;

			Assert.Throws<ArgumentException>(delegate { m_switches.Add(NoName); });
		}

		/// <summary>
		/// Test Add() method defining switch names with no names.
		/// </summary>
		[Test]
		public void AddWithNoNames()
		{
			string[] nullNames = null;

			Assert.DoesNotThrow(delegate { m_switches.Add(nullNames); });

			Assert.AreEqual(m_switches.Count, NoSwitches);

			string[] noNames = new string[NoSwitches];

			Assert.DoesNotThrow(delegate { m_switches.Add(noNames); });

			Assert.AreEqual(m_switches.Count, NoSwitches);
		}

		/// <summary>
		/// Test Add() method defining no number of arguments expected to follow
		/// switch.
		/// </summary>
		[Test]
		public void AddWithNoNumberArguments()
		{
			m_switches.Add(Name, LongName, Description, NoArguments,
				IsRequired);

			Assert.AreEqual(m_switches.Count, OneSwitch);

			Assert.IsTrue(m_switches.HasSwitch(Name));
			Assert.IsTrue(m_switches.HasSwitch(LongName));

			Switch switchObject = m_switches[Name];

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.LongName, LongName);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, NoArguments);

			Assert.IsFalse(switchObject.HasArguments);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsOptional);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsRequired);
		}

		/// <summary>
		/// Test Add() method defining number of arguments expected to follow
		/// switch.
		/// </summary>
		[Test]
		public void AddWithNumberArguments()
		{
			const int NumberArguments = 4;

			m_switches.Add(Name, LongName, Description, NumberArguments,
				IsRequired);

			Assert.AreEqual(m_switches.Count, OneSwitch);

			Assert.IsTrue(m_switches.HasSwitch(Name));
			Assert.IsTrue(m_switches.HasSwitch(LongName));

			Switch switchObject = m_switches[Name];

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.LongName, LongName);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, NumberArguments);

			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsOptional);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasArguments);
			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsRequired);
		}

		/// <summary>
		/// Test Add() method defining same switch name and long name.
		/// </summary>
		[Test]
		public void AddWithSameNames()
		{
			m_switches.Add(Name, Name, Description);

			Assert.AreEqual(m_switches.Count, OneSwitch);

			Assert.IsTrue(m_switches.HasSwitch(Name));

			Switch switchObject = m_switches[Name];

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, NoArguments);

			Assert.AreNotEqual(switchObject.LongName, Name);

			Assert.IsFalse(switchObject.HasArguments);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.HasLongName);
			Assert.IsFalse(switchObject.IsRequired);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsNull(switchObject.LongName);

			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.IsOptional);
		}

		/// <summary>
		/// Test Add() method adding specified Switch object.
		/// </summary>
		[Test]
		public void AddWithSwitch()
		{
			m_switches.Add(new Switch(Name, LongName, Description));

			Assert.AreEqual(m_switches.Count, OneSwitch);

			Assert.IsTrue(m_switches.HasSwitch(Name));
			Assert.IsTrue(m_switches.HasSwitch(LongName));

			Switch switchObject = m_switches[Name];

			Assert.AreEqual(switchObject.Description, Description);
			Assert.AreEqual(switchObject.GetArgumentNames(), NoArgumentNames);
			Assert.AreEqual(switchObject.GetArgumentValues(), NoArgumentValues);
			Assert.AreEqual(switchObject.LongName, LongName);
			Assert.AreEqual(switchObject.Name, Name);
			Assert.AreEqual(switchObject.NumberArguments, NoArguments);

			Assert.IsFalse(switchObject.HasArguments);
			Assert.IsFalse(switchObject.HasEnoughNames);
			Assert.IsFalse(switchObject.HasEnoughValues);
			Assert.IsFalse(switchObject.IsRequired);

			Assert.IsNotNull(switchObject.Description);
			Assert.IsNotNull(switchObject.LongName);
			Assert.IsNotNull(switchObject.Name);

			Assert.IsTrue(switchObject.HasDescription);
			Assert.IsTrue(switchObject.HasLongName);
			Assert.IsTrue(switchObject.IsOptional);
		}

		/// <summary>
		/// Test Clear() method.
		/// </summary>
		[Test]
		public void Clear()
		{
			string[] switchNames = new string[] { "switch1", "switch2" };

			m_switches.Add(switchNames);

			Assert.AreEqual(m_switches.Count, switchNames.Length);

			m_switches.Clear();

			Assert.AreEqual(m_switches.Count, NoSwitches);
		}

		/// <summary>
		/// Test Contains() method.
		/// </summary>
		[Test]
		public void Contains()
		{
			Switch switchObject = new Switch(Name, LongName, Description);

			Assert.IsFalse(m_switches.Contains(switchObject));

			m_switches.Add(switchObject);

			Assert.IsTrue(m_switches.Contains(switchObject));
		}

		/// <summary>
		/// Test CopyTo() method.
		/// </summary>
		[Test]
		public void CopyTo()
		{
			const int StartIndex = 0;

			string[] switchNames = new string[] { "switch1", "switch2", "switch3" };

			Switch[] switchArray = new Switch[switchNames.Length];

			m_switches.Add(switchNames);

			Assert.AreEqual(m_switches.Count, switchNames.Length);

			m_switches.CopyTo(switchArray, StartIndex);

			for (int index = StartIndex; index < switchArray.Length; index++)
				Assert.AreEqual(switchArray[index].Name, switchNames[index]);
		}

		/// <summary>
		/// Test GetEnumerator() method.
		/// </summary>
		[Test]
		public void GetEnumerator()
		{
			string[] switchNames = new string[] { "switch1", "switch2", "switch3" };

			m_switches.Add(switchNames);

			Assert.AreEqual(m_switches.Count, switchNames.Length);

			IEnumerator<Switch> enumerator = m_switches.GetEnumerator();

			for (int index = 0; enumerator.MoveNext(); index++)
			{
				Switch switchObject = enumerator.Current;

				Assert.AreEqual(switchObject.Name, switchNames[index]);
			}
		}

		/// <summary>
		/// Test HasSwitch() method.
		/// </summary>
		[Test]
		public void HasSwitch()
		{
			m_switches.Add(new Switch(Name, LongName, Description));

			Assert.AreEqual(m_switches.Count, OneSwitch);

			Assert.IsTrue(m_switches.HasSwitch(Name));
			Assert.IsTrue(m_switches.HasSwitch(LongName));
		}

		/// <summary>
		/// Test IndexOf() method.
		/// </summary>
		[Test]
		public void IndexOf()
		{
			const int Index1 = 0;
			const int Index2 = 1;
			const int Index3 = 2;
			const string Name1 = "switch1";
			const string Name2 = "switch2";
			const string Name3 = "switch3";
			const int NumberSwitches = 3;

			Switch switch1 = new Switch(Name1);
			Switch switch2 = new Switch(Name2);
			Switch switch3 = new Switch(Name3);

			m_switches.Add(switch1);
			m_switches.Add(switch2);
			m_switches.Add(switch3);

			Assert.AreEqual(m_switches.Count, NumberSwitches);

			Assert.AreEqual(m_switches.IndexOf(switch1), Index1);
			Assert.AreEqual(m_switches.IndexOf(switch2), Index2);
			Assert.AreEqual(m_switches.IndexOf(switch3), Index3);
		}

		/// <summary>
		/// Test Insert() method.
		/// </summary>
		[Test]
		public void Insert()
		{
			const int Index1 = 0;
			const int Index2 = 1;
			const string Name1 = "switch1";
			const string Name2 = "switch2";
			const int NumberSwitches = 2;

			Switch switch1 = new Switch(Name1);
			Switch switch2 = new Switch(Name2);

			m_switches.Insert(Index1, switch1);

			Assert.AreEqual(m_switches.Count, OneSwitch);
			Assert.AreEqual(m_switches[Index1], switch1);

			Assert.IsTrue(m_switches.Contains(switch1));

			m_switches.Insert(Index2, switch2);

			Assert.AreEqual(m_switches.Count, NumberSwitches);
			Assert.AreEqual(m_switches[Index1], switch1);
			Assert.AreEqual(m_switches[Index2], switch2);

			Assert.IsTrue(m_switches.Contains(switch1));
			Assert.IsTrue(m_switches.Contains(switch2));
		}

		/// <summary>
		/// Test Remove() method.
		/// </summary>
		[Test]
		public void Remove()
		{
			Switch switchObject = new Switch(Name);

			m_switches.Add(switchObject);

			Assert.IsTrue(m_switches.Contains(switchObject));

			m_switches.Remove(switchObject);

			Assert.IsFalse(m_switches.Contains(switchObject));
		}

		/// <summary>
		/// Test RemoveAt() method.
		/// </summary>
		[Test]
		public void RemoveAt()
		{
			const int Index1 = 0;
			const int Index2 = 1;
			const string Name1 = "switch1";
			const string Name2 = "switch2";
			const int NumberSwitches = 2;

			Switch switch1 = new Switch(Name1);
			Switch switch2 = new Switch(Name2);

			m_switches.Insert(Index1, switch1);
			m_switches.Insert(Index2, switch2);

			Assert.AreEqual(m_switches.Count, NumberSwitches);
			Assert.AreEqual(m_switches[Index1], switch1);
			Assert.AreEqual(m_switches[Index2], switch2);

			m_switches.RemoveAt(Index1);

			Assert.AreEqual(m_switches.Count, OneSwitch);
			Assert.AreEqual(m_switches[Index1], switch2);

			Assert.IsFalse(m_switches.Contains(switch1));

			m_switches.RemoveAt(Index1);

			Assert.AreEqual(m_switches.Count, NoSwitches);

			Assert.IsFalse(m_switches.Contains(switch2));
		}

		////////////////////////////////////////////////////////////////////////
		// Properties

		/// <summary>
		/// Test Count property.
		/// </summary>
		[Test]
		public void Count()
		{
			string[] switchNames = new string[] { "switch1", "switch2", "switch3" };

			Assert.AreEqual(m_switches.Count, NoSwitches);

			m_switches.Add(switchNames);

			Assert.AreEqual(m_switches.Count, switchNames.Length);
		}

		/// <summary>
		/// Test indexer on collection index.
		/// </summary>
		[Test]
		public void IndexIndexer()
		{
			const int Index1 = 0;
			const int Index2 = 1;
			const string Name1 = "switch1";
			const string Name2 = "switch2";
			const int NumberSwitches = 2;

			Switch switch1 = new Switch(Name1);
			Switch switch2 = new Switch(Name2);

			m_switches.Insert(Index1, switch1);
			m_switches.Insert(Index2, switch2);

			Assert.AreEqual(m_switches.Count, NumberSwitches);
			Assert.AreEqual(m_switches[Index1], switch1);
			Assert.AreEqual(m_switches[Index2], switch2);
		}

		/// <summary>
		/// Test indexer on switch name.
		/// </summary>
		[Test]
		public void NameIndexer()
		{
			Switch switchObject = new Switch(Name, LongName, Description);

			m_switches.Add(switchObject);

			Assert.AreEqual(m_switches.Count, OneSwitch);
			Assert.AreEqual(m_switches[Name], switchObject);
			Assert.AreEqual(m_switches[LongName], switchObject);
		}

		/// <summary>
		/// Test SortedSwitches property.
		/// </summary>
		[Test]
		public void SortedSwitches()
		{
			string[] switchNames = new string[] { "switch3", "switch1", "switch2" };
			string[] sortedSwitchNames = new string[] { "switch1", "switch2", "switch3" };

			m_switches.Add(switchNames);

			Assert.AreEqual(m_switches.Count, switchNames.Length);

			IList<Switch> switches = m_switches.Switches;

			for (int index = 0; index < switches.Count; index++)
			{
				Switch switchObject = switches[index];

				Assert.AreEqual(switchObject.Name, switchNames[index]);
			}

			IList<Switch> sortedSwitches = m_switches.SortedSwitches;

			for (int index = 0; index < sortedSwitches.Count; index++)
			{
				Switch switchObject = sortedSwitches[index];

				Assert.AreEqual(switchObject.Name, sortedSwitchNames[index]);
			}
		}
	}
}
