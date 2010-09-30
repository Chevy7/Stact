﻿// Copyright 2010 Chris Patterson
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
namespace Stact.Actors.Internal
{
	using System;
	using Channels;


	/// <summary>
	/// A decorator for sent requests that enables method chaining
	/// </summary>
	/// <typeparam name="TRequest"></typeparam>
	public class SentRequestImpl<TRequest> :
		SentRequest<TRequest>
	{
		readonly TRequest _body;
		readonly Inbox _inbox;

		public SentRequestImpl(TRequest body, Inbox inbox)
		{
			_body = body;
			_inbox = inbox;
		}

		public TRequest Body
		{
			get { return _body; }
		}

		public void Send<T>(T message)
		{
			_inbox.Send(message);
		}

		public PendingReceive Receive<T>(SelectiveConsumer<T> consumer)
		{
			return _inbox.Receive(consumer);
		}

		public PendingReceive Receive<T>(SelectiveConsumer<T> consumer, TimeSpan timeout, Action timeoutCallback)
		{
			return _inbox.Receive(consumer, timeout, timeoutCallback);
		}

		public PendingReceive Receive<T>(SelectiveConsumer<T> consumer, int timeout, Action timeoutCallback)
		{
			return _inbox.Receive(consumer, timeout, timeoutCallback);
		}
	}
}