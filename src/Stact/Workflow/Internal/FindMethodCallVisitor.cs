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
namespace Stact.Workflow.Internal
{
	using System.Linq.Expressions;
	using System.Reflection;
	using Magnum.Reflection;


	public class FindMethodCallVisitor :
		ExpressionVisitor
	{
		MethodInfo _methodInfo;

		public MethodInfo Find(Expression e)
		{
			Visit(e);

			return _methodInfo;
		}

		protected override Expression VisitConstant(ConstantExpression c)
		{
			if (c.Type == typeof(MethodInfo))
				_methodInfo = c.Value as MethodInfo;

			return base.VisitConstant(c);
		}
	}
}