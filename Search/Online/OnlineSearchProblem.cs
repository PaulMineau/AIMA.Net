namespace AIMA.Core.Search.Online
{
    using System;
    using System.Collections.Generic;
    using AIMA.Core.Search.Framework;

    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): page 147.
     * 
     * An online search problem must be solved by an agent executing actions, 
     * rather than by pure computation. We assume a deterministic and fully
     * observable environment (Chapter 17 relaxes these assumptions), but we
     * stipulate that the agent knows only the following: <br>
     * <ul>
     * <li>ACTIONS(s), which returns a list of actions allowed in state s;</li>
     * <li>The step-cost function c(s, a, s') - note that this cannot be used
     * until the agent knows that s' is the outcome; and</li>
     * <li>GOAL-TEST(s).</li>
     * </ul>
     */

    /**
     * @author Ciaran O'Reilly
     */
    public class OnlineSearchProblem
    {

        protected ActionsFunction actionsFunction;

        protected StepCostFunction stepCostFunction;

        protected GoalTest goalTest;

        public OnlineSearchProblem(ActionsFunction actionsFunction,
                GoalTest goalTest)
        {
            this(actionsFunction, goalTest, new DefaultStepCostFunction());
        }

        public OnlineSearchProblem(ActionsFunction actionsFunction,
                GoalTest goalTest, StepCostFunction stepCostFunction)
        {
            this.actionsFunction = actionsFunction;
            this.goalTest = goalTest;
            this.stepCostFunction = stepCostFunction;
        }

        public ActionsFunction getActionsFunction()
        {
            return actionsFunction;
        }

        public bool isGoalState(Object state)
        {

            return goalTest.isGoalState(state);
        }

        public StepCostFunction getStepCostFunction()
        {
            return stepCostFunction;
        }

        //
        // PROTECTED METHODS
        //
        protected OnlineSearchProblem()
        {
        }
    }
}