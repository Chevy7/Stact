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
namespace Stact.Channels
{
	using System;
	using Internal;


	/// <summary>
	/// Wraps a channel in an IAsyncResult compatible wrapper to support asynchronous usage with
	/// frameworks that support asynchronous callbacks
	/// </summary>
	/// <typeparam name="T">The channel type supported</typeparam>
	public class AsyncResultChannel :
		AsyncResult,
		UntypedChannel
	{
		public AsyncResultChannel(UntypedChannel output, AsyncCallback callback, object state)
			: base(callback, state)
		{
			Magnum.Guard.AgainstNull(output, "output");

			Output = output;
		}

		public UntypedChannel Output { get; private set; }

		public void Send<T>(T message)
		{
			Output.Send(message);

			Complete();
		}
	}


	/// <summary>
	/// Wraps a channel in an IAsyncResult compatible wrapper to support asynchronous usage with
	/// frameworks that support asynchronous callbacks
	/// </summary>
	/// <typeparam name="T">The channel type supported</typeparam>
	public class AsyncResultChannel<T> :
		AsyncResult,
		Channel<T>
	{
		public AsyncResultChannel(Channel<T> output, AsyncCallback callback, object state)
			: base(callback, state)
		{
			Magnum.Guard.AgainstNull(output, "output");

			Output = output;
		}

		public Channel<T> Output { get; private set; }

		public void Send(T message)
		{
			Output.Send(message);

			Complete();
		}
	}
}