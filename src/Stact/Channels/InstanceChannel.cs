// Copyright 2010 Chris Patterson
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
	using Fibers;


	/// <summary>
	/// An instance channel requests an instance of a channel which can be created/loaded
	/// based on the information in the message being sent on the channel
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class InstanceChannel<T> :
		Channel<T>
	{
		readonly Fiber _fiber;

		public InstanceChannel(Fiber fiber, ChannelProvider<T> instanceProvider)
		{
			_fiber = fiber;
			Provider = instanceProvider;
		}

		public ChannelProvider<T> Provider { get; private set; }

		public void Send(T message)
		{
			_fiber.Add(() =>
				{
					Channel<T> channel = Provider.GetChannel(message);
					if (channel == null)
						return;

					channel.Send(message);
				});
		}
	}
}