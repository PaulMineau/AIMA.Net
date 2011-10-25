
using System.Collections.Generic;
using AIMA;
using AIMA.Core.Util;


namespace AIMA.Probability.Bayes.Impl
{

    /**
     * Default implementation of the DynamicBayesianNetwork interface.
     * 
     * @author Ciaran O'Reilly
     */

    public class DynamicBayesNet : BayesNet, DynamicBayesianNetwork
    {

        private Set<RandomVariable> X_0 = new Set<RandomVariable>();
        private Set<RandomVariable> X_1 = new Set<RandomVariable>();
        private Set<RandomVariable> E_1 = new Set<RandomVariable>();
        private Map<RandomVariable, RandomVariable> X_0_to_X_1 = new LinkedHashMap<RandomVariable, RandomVariable>();
        private Map<RandomVariable, RandomVariable> X_1_to_X_0 = new LinkedHashMap<RandomVariable, RandomVariable>();
        private BayesianNetwork priorNetwork = null;
        private List<RandomVariable> X_1_VariablesInTopologicalOrder = new List<RandomVariable>();

        public DynamicBayesNet(BayesianNetwork priorNetwork,
                               Map<RandomVariable, RandomVariable> X_0_to_X_1,
                               Set<RandomVariable> E_1, params Node[] rootNodes)
            : base(rootNodes)
        {


            foreach (RandomVariable rv in X_0_to_X_1.keySet()
                )
            {
                RandomVariable x0 = rv;
                RandomVariable x1 = X_0_to_X_1[rv];
                this.X_0.add(x0);
                this.X_1.add(x1);
                this.X_0_to_X_1.put(x0, x1);
                this.X_1_to_X_0.put(x1, x0);
            }
            this.E_1.addAll(new List<RandomVariable>(E_1));

            // Assert the X_0, X_1, and E_1 sets are of expected sizes
            Set<RandomVariable> combined = new Set<RandomVariable>();
            combined.addAll(new List<RandomVariable>(X_0));
            combined.addAll(new List<RandomVariable>(X_1));
            combined.addAll(new List<RandomVariable>(E_1));
            if (
                SetOps.difference(new List<RandomVariable>(varToNodeMap.keySet()), new List<RandomVariable>(combined)).
                    Count != 0)
            {
                throw new IllegalArgumentException(
                    "X_0, X_1, and E_1 do not map correctly to the Nodes describing this Dynamic Bayesian Network.");
            }
            this.priorNetwork = priorNetwork;

            X_1_VariablesInTopologicalOrder
                .AddRange(getVariablesInTopologicalOrder());
            X_1_VariablesInTopologicalOrder.RemoveAll(X_0);
            X_1_VariablesInTopologicalOrder.RemoveAll(E_1);
        }

        //
        // START-DynamicBayesianNetwork

        public BayesianNetwork getPriorNetwork()
        {
            return priorNetwork;
        }


        public Set<RandomVariable> getX_0()
        {
            return X_0;
        }

        public Set<RandomVariable> getX_1()
        {
            return X_1;
        }


        public List<RandomVariable> getX_1_VariablesInTopologicalOrder()
        {
            return X_1_VariablesInTopologicalOrder;
        }


        public Map<RandomVariable, RandomVariable> getX_0_to_X_1()
        {
            return X_0_to_X_1;
        }


        public Map<RandomVariable, RandomVariable> getX_1_to_X_0()
        {
            return X_1_to_X_0;
        }


        public Set<RandomVariable> getE_1()
        {
            return E_1;
        }

        // END-DynamicBayesianNetwork
        //

        //
        // PRIVATE METHODS
        //
    }
}