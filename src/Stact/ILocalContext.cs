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
namespace Stact
{
	using System;
	using Context;
	using Data;

	public interface ILocalContext
	{
		object this[object key] { get; set; }

		void Clear();

		TValue Retrieve<TValue>();
		TValue Retrieve<TValue>(object key);
		TValue Retrieve<TValue>(object key, Func<TValue> valueProvider);

		void Remove(object key);

		bool Contains(object key);
		void Store<T>(object key, T value);


		/// <summary>
		/// Stores the value using a <see cref="TypedKey{T}"/> for the key
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		void Store<T>(T value);

	}
}