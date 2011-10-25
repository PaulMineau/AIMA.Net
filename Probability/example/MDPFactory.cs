using System;
using System.Collections.Generic;
using AIMA.Environment.CellWorld;
using AIMA.Probability.MDP;

namespace AIMA.Probability.Example
{

    /**
     * 
     * @author Ciaran O'Reilly
     * @author Ravi Mohan
     */

    public class MDPFactory
    {

        /**
	 * Constructs an MDP that can be used to generate the utility values
	 * detailed in Fig 17.3.
	 * 
	 * @param cw
	 *            the cell world from figure 17.1.
	 * @return an MDP that can be used to generate the utility values detailed
	 *         in Fig 17.3.
	 */

        public static MarkovDecisionProcess<Cell<Double>, CellWorldAction> createMDPForFigure17_3(
            CellWorld<Double> cw)
        {

            return new MDP<Cell<Double>, CellWorldAction>(cw.getCells(),
                                                          cw.getCellAt(1, 1), createActionsFunctionForFigure17_1(cw),
                                                          createTransitionProbabilityFunctionForFigure17_1(cw),
                                                          createRewardFunctionForFigure17_1());
        }

        private class MDPActionFunction : ActionsFunction<Cell<Double>, CellWorldAction>
        {
            private Set<Cell<Double>> terminals;

            public MDPActionFunction(Set<Cell<Double>> terminals)
            {
                this.terminals = terminals;
            }

            public Set<CellWorldAction.ActionEnum> actions(Cell<double> s)
            {

                // All actions can be performed in each cell
                // (except terminal states)
                if (terminals.Contains(s))
                {
                    return new Set<CellWorldAction.ActionEnum>();
                }
                return CellWorldAction.actions();
            }



            /**
	 * Returns the allowed actions from a specified cell within the cell world
	 * described in Fig 17.1.
	 * 
	 * @param cw
	 *            the cell world from figure 17.1.
	 * @return the set of actions allowed at a particular cell. This set will be
	 *         empty if at a terminal state.
	 */

            public static ActionsFunction<Cell<Double>, CellWorldAction> createActionsFunctionForFigure17_1(
                CellWorld<Double> cw)
            {
                Set<Cell<Double>> terminals = new Set<Cell<Double>>();
                terminals.add(cw.getCellAt(4, 3));
                terminals.add(cw.getCellAt(4, 2));

                ActionsFunction<Cell<Double>, CellWorldAction> af = null;
                //new ActionsFunction<Cell<Double>, CellWorldAction>() {



                //};
                // TODO
                return af;
            }

            private class TransitionProbabilityFunctionImpl :
                TransitionProbabilityFunction<Cell<Double>, CellWorldAction>
            {
                private CellWorld<Double> cw;

                public TransitionProbabilityFunctionImpl(CellWorld<Double> cw)
                {
                    this.cw = cw;
                }

                public override double probability(Cell<double> sDelta, Cell<double> s, CellWorldAction a)
                {

                    double[] distribution = new double[] {0.8, 0.1, 0.1};

                    double prob = 0;

                    List<Cell<Double>> outcomes = possibleOutcomes(s, a);
                    for (int i = 0; i < outcomes.Count; i++)
                    {
                        if (sDelta.Equals(outcomes[i]))
                        {
                            // Note: You have to sum the matches to
                            // sDelta as the different actions
                            // could have the same effect (i.e.
                            // staying in place due to there being
                            // no adjacent cells), which increases
                            // the probability of the transition for
                            // that state.
                            prob += distribution[i];
                        }
                    }

                    return prob;
                }

                private List<Cell<Double>> possibleOutcomes(Cell<Double> c,
                                                            CellWorldAction a)
                {
                    // There can be three possible outcomes for the planned action
                    List<Cell<Double>> outcomes = new List<Cell<Double>>();

                    outcomes.Add(cw.result(c, a));
                    outcomes.Add(cw.result(c, new CellWorldAction(a.getFirstRightAngledAction())));
                    outcomes.Add(cw.result(c, new CellWorldAction(a.getSecondRightAngledAction())));

                    return outcomes;
                }
            }

            /**
	 * Figure 17.1 (b) Illustration of the transition model of the environment:
	 * the 'intended' outcome occurs with probability 0.8, but with probability
	 * 0.2 the agent moves at right angles to the intended direction. A
	 * collision with a wall results in no movement.
	 * 
	 * @param cw
	 *            the cell world from figure 17.1.
	 * @return the transition probability function as described in figure 17.1.
	 */

            public static TransitionProbabilityFunction<Cell<Double>, CellWorldAction>
                createTransitionProbabilityFunctionForFigure17_1(
                CellWorld<Double> cw)
            {
                TransitionProbabilityFunction<Cell<Double>, CellWorldAction> tf =
                    new TransitionProbabilityFunctionImpl(cw);
                {


                }
                ;

                return tf;
            }

            private class RewardFunctionImpl : RewardFunction<Cell<Double>>
            {

                public double reward(Cell<double> s)
                {

                    return s.getContent();

                }
            }

            /**
	 * 
	 * @return the reward function which takes the content of the cell as being
	 *         the reward value.
	 */

            public static RewardFunction<Cell<Double>> createRewardFunctionForFigure17_1()
            {
                RewardFunction<Cell<Double>> rf = new RewardFunctionImpl()
                                                      {


                                                      }
                    ;
                return rf;
            }
        }
    }
}