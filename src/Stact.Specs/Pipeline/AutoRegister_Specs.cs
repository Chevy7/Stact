// Copyright 2007-2008 The Apache Software Foundation.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Stact.Specs.Pipeline
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using Consumers;
	using Stact.Extensions;
	using Stact.Pipeline.Visitors;
	using Stact.Reflection;
	using Messages;
	using NUnit.Framework;

	[TestFixture]
	public class When_automatically_registering_consumers_from_a_container :
		Given_an_established_pipe
	{
		private Type[] _types;
		private Dictionary<Type, bool> _called;

		protected override void EstablishContext()
		{
			base.EstablishContext();

			_called = new Dictionary<Type, bool>();
			_types = new[] {typeof (SingleMessageConsumer), typeof (MultipleMessageConsumer), typeof (LongRunningMessageConsumer)};

			GetAllTypes().Each(type => { this.FastInvoke(new[] { type }, "SubscribeToScope"); });

			Input.Send(new ClaimModified());
		}

		[Test]
		public void All_consumers_should_be_added()
		{
			new TracePipeVisitor().Trace(Input);
		}

		[Test]
		public void The_single_consumer_should_be_called()
		{
			Assert.IsTrue(_called.ContainsKey(typeof(SingleMessageConsumer)));
		}

		[Test]
		public void The_multiple_consumer_should_be_called()
		{
			Assert.IsTrue(_called.ContainsKey(typeof(MultipleMessageConsumer)));
		}

		[Test]
		public void The_long_running_consumer_should_be_called()
		{
			Thread.Sleep(1500);
			Assert.IsTrue(_called.ContainsKey(typeof(LongRunningMessageConsumer)));
		}

		private void SubscribeToScope<T>() 
			where T : class
		{
			Scope.Subscribe<T>(GetInstance<T>);
		}

		private T GetInstance<T>()
		{
			_called.Retrieve(typeof (T), () => true);

			return FastActivator<T>.Create();
		}

		private IEnumerable<Type> GetAllTypes()
		{
			return _types;
		}
	}
}