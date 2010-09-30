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
namespace Stact.Specs.StateMachine
{
    using System;
    using System.Runtime.Serialization;
    using Stact.StateMachine;

    [Serializable]
    public class AccountManagementWorkflow :
        StateMachine<AccountManagementWorkflow>
    {
        static AccountManagementWorkflow()
        {
            Define(() =>
                       {
                           // eliminated, ceremony, just use the default states with the right names
                           // SetInitialState(Initial);
                           // SetCompletedState(Completed);

                           During(Initial,
                                  When(RequestSubmitted)
                                      .Then(workflow =>
                                                {
                                                    // workflow is your scope
                                                    workflow.RequestApprovalFromManager();
                                                }).TransitionTo(WaitingForManagementResponse),
                                  When(RequestCanceled)
                                      .Then(workflow =>
                                                {
                                                    // nothing has happened yet so we just complete 
                                                }).Complete());

                           During(WaitingForManagementResponse,
                                  When(ManagementApproves)
                                      .Then(workflow =>
                                                {
                                                    workflow.RequestSecurityReview();
                                                }).TransitionTo(WaitingForSecurityResponse),
                                  When(ManagementDeclines)
                                      .Then(workflow =>
                                                {
                                                    workflow.NotifyRequestDeclined();
                                                }).Complete(),
                                  When(RequestCanceled)
                                      .Then(workflow =>
                                                {
                                                    workflow.NotifyRequestCancelled();
                                                }).Complete());

                           During(WaitingForSecurityResponse,
                                  When(SecurityApproves)
                                      .Then(workflow =>
                                                {
                                                    workflow.NotifyWorkTeams(); //possible fork/join here
                                                }).TransitionTo(WaitingForWorkToComplete),
                                  When(SecurityDeclines)
                                      .Then(workflow =>
                                                {
                                                    workflow.NotifyRequestDeclined();
                                                }).Complete());

                           During(WaitingForWorkToComplete,
                                  When(WorkComplete)
                                      .Then(workflow =>
                                                {
                                                    // do stuff
                                                }).Complete());

                           During(Completed,
                                  When(Completed.Enter)
                                      .Then(workflow =>
                                                {
                                                    // complete the transaction if required
                                                }));
                       });
        }

        public AccountManagementWorkflow()
        {
        }

        public AccountManagementWorkflow(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public static State Initial { get; set; }
        public static State WaitingForManagementResponse { get; set; }
        public static State WaitingForSecurityResponse { get; set; }
        public static State WaitingForWorkToComplete { get; set; }
        public static State Completed { get; set; }

        public static Event RequestSubmitted { get; set; }
        public static Event ManagementApproves { get; set; }
        public static Event ManagementDeclines { get; set; }
        public static Event SecurityApproves { get; set; }
        public static Event SecurityDeclines { get; set; }
        public static Event WorkComplete { get; set; }
        public static Event RequestCanceled { get; set; }

        public void SubmitOrder()
        {
            RaiseEvent(RequestSubmitted);
        }

        public void SubmitPayment()
        {
            RaiseEvent(ManagementApproves);
        }

        // you don't want statics since the instance carries your state
        // and any other data you capture
        // what category of thing are these functions?
        private void NotifyRequestCancelled()
        {
            throw new NotImplementedException();
        }
        private void NotifyWorkTeams()
        {
            throw new NotImplementedException();
        }
        private void NotifyRequestDeclined()
        {
            throw new NotImplementedException();
        }
        private void RequestSecurityReview()
        {
            throw new NotImplementedException();
        }
        private void RequestApprovalFromManager()
        {
            throw new NotImplementedException();
        }
    }
}