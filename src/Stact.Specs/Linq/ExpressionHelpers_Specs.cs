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
namespace Stact.Specs.Linq
{
    using System;
    using System.Linq.Expressions;
    using NUnit.Framework;

    [TestFixture]
    public class ExpressionHelpers_Specs
    {
        [Test]
        public void Should_match_a_member_expression_for_reference_types()
        {
            Expression<Func<OneClass, bool>> match = n => n.Name == "Chris";

            Assert.IsTrue(match.Body.IsMemberEqualsValueExpression<OneClass>(x => x.Name));
        }

        [Test]
        public void Should_match_a_member_expression_for_boxed_types()
        {
            Expression<Func<OneClass, bool>> match = n => n.Age == 27;

            Assert.IsTrue(match.Body.IsMemberEqualsValueExpression<OneClass>(x => x.Age));
        }
		
    }

    public static class ExpressionHelpersSTuff
    {

        private static MemberExpression GetMemberExpression<T, V>(this Expression<Func<T, V>> expression)
        {
            MemberExpression memberExpression = null;
            if (expression.Body.NodeType == ExpressionType.Convert)
            {
                var body = (UnaryExpression)expression.Body;
                memberExpression = body.Operand as MemberExpression;
            }
            else if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpression = expression.Body as MemberExpression;
            }

            return memberExpression;
        }
        
        public static bool IsMemberEqualsValueExpression<T>(this Expression expression, Expression<Func<T,object>> memberAccess)
        {
            MemberExpression memberExpression = memberAccess.GetMemberExpression();
            if (memberExpression == null)
                throw new ArgumentException("Unable to determine type of member access");

            return expression.IsMemberEqualsValueExpression(memberExpression.Member.DeclaringType, memberExpression.Member.Name);
        }

        public static bool IsMemberEqualsValueExpression(this Expression expression, Type declaringType, string memberName)
        {
            if (expression.NodeType != ExpressionType.Equal)
                return false;

            BinaryExpression be = (BinaryExpression)expression;

            if (be.Left.IsSpecificMemberExpression(declaringType, memberName) &&
                be.Right.IsSpecificMemberExpression(declaringType, memberName))
                throw new InvalidOperationException("Cannot have 'member' == 'member' in an expression!");

            return (be.Left.IsSpecificMemberExpression(declaringType, memberName) ||
                    be.Right.IsSpecificMemberExpression(declaringType, memberName));
        }

        public static bool IsSpecificMemberExpression(this Expression exp, Type declaringType, string memberName)
        {
            if (!(exp is MemberExpression)) return false;

            return (((MemberExpression)exp).Member.DeclaringType == declaringType) && (((MemberExpression)exp).Member.Name == memberName);
        }
    }

    public class OneClass
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}