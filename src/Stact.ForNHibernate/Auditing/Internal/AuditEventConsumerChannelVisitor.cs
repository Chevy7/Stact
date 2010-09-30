// Copyright 2007-2010 The Apache Software Foundation.
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
namespace Stact.ForNHibernate.Auditing.Internal
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Magnum.Extensions;
	using Stact.Channels;
	using Stact.Channels.Visitors;


	public class AuditEventConsumerChannelVisitor :
		ChannelVisitor
	{
		readonly IEnumerable<EventListenerConfigurator> _configurators;

		public AuditEventConsumerChannelVisitor(IEnumerable<EventListenerConfigurator> configurators)
		{
			_configurators = configurators;
		}

		public void Configure<T>(Channel<T> channel)
		{
			Visit(channel);
		}

		public void Configure(UntypedChannel channel)
		{
			Visit(channel);
		}

		public override Channel<T> Visit<T>(Channel<T> channel)
		{
			if (typeof(T).Implements<AuditEvent>())
			{
				bool matched = _configurators.Any(x => x.IsHandled<T>());

				if (!matched)
					throw new InvalidOperationException("The audit type is not yet configured: " + typeof(T).ToShortTypeName());
			}

			return base.Visit(channel);
		}
	}
}