namespace AIMA.Core.Agent.Impl
{
    using System.Collections.Generic;
    using AIMA.Core.Agent;

    /**
     * @author Ravi Mohan
     * @author Ciaran O'Reilly
     */
    public abstract class AbstractEnvironment : Environment,
            EnvironmentViewNotifier
    {

        // Note: Use LinkedHashSet's in order to ensure order is respected as
        // provide
        // access to these elements via List interface.
        protected HashSet<EnvironmentObject> envObjects = new LinkedHashSet<EnvironmentObject>();

        protected HashSet<IAgent> agents = new LinkedHashSet<IAgent>();

        protected HashSet<EnvironmentView> views = new LinkedHashSet<EnvironmentView>();

        protected Map<IAgent, System.Double> performanceMeasures = new LinkedHashMap<IAgent, System.Double>();

        //
        // PRUBLIC METHODS
        //

        // 
        // Methods to be implemented by subclasses.
        public abstract EnvironmentState getCurrentState();

        public abstract EnvironmentState executeAction(IAgent agent, Action action);

        public abstract Percept getPerceptSeenBy(IAgent anAgent);
        /**
         * Method for implementing dynamic environments in which not all changes
         * are directly caused by agent action execution. The default implementation
         * does nothing.
         */
        public void createExogenousChange() { }

        //
        // START-Environment
        public List<IAgent> getAgents()
        {
            // Return as a List but also ensures the caller cannot modify
            return new List<IAgent>(agents);
        }

        public void addAgent(IAgent a)
        {
            addEnvironmentObject(a);
        }

        public void removeAgent(IAgent a)
        {
            removeEnvironmentObject(a);
        }

        public List<EnvironmentObject> getEnvironmentObjects()
        {
            // Return as a List but also ensures the caller cannot modify
            return new List<EnvironmentObject>(envObjects);
        }

        public void addEnvironmentObject(EnvironmentObject eo) {
		envObjects.Add(eo);
        if (eo is IAgent)
        {
            IAgent a = (IAgent)eo;
			if (!agents.Contains(a)) {
				agents.Add(a);
				this.updateEnvironmentViewsAgentAdded(a);
			}
		}
	}

        public void removeEnvironmentObject(EnvironmentObject eo)
        {
            envObjects.Remove(eo);
           // agents.Remove(eo);
        }

        /**
         * Central template method for controlling agent simulation. The
         * concrete behavior is determined by the primitive operations
         * {@link #getPerceptSeenBy(Agent)}, {@link #executeAction(Agent, Action)},
         * and {@link #createExogenousChange()}.
         */
        public void step() {
		foreach (IAgent agent in agents) {
			if (agent.isAlive()) {
				Action anAction = agent.execute(getPerceptSeenBy(agent));
				EnvironmentState es = executeAction(agent, anAction);
				updateEnvironmentViewsAgentActed(agent, anAction, es);
			}
		}
		createExogenousChange();
	}

        public void step(int n)
        {
            for (int i = 0; i < n; i++)
            {
                step();
            }
        }

        public void stepUntilDone()
        {
            while (!isDone())
            {
                step();
            }
        }

        public bool isDone() {
		foreach (IAgent agent in agents) {
			if (agent.isAlive()) {
				return false;
			}
		}
		return true;
	}

        public double getPerformanceMeasure(IAgent forAgent)
        {
            System.Double pm = performanceMeasures[forAgent];
            if (null == pm)
            {
                pm = 0.0;
                performanceMeasures[forAgent] = pm;
            }

            return pm;
        }

        public void addEnvironmentView(EnvironmentView ev)
        {
            views.Add(ev);
        }

        public void removeEnvironmentView(EnvironmentView ev)
        {
            views.Remove(ev);
        }

        public void notifyViews(System.String msg) {
		foreach (EnvironmentView ev in views) {
			ev.notify(msg);
		}
	}

        // END-Environment
        //

        //
        // PROTECTED METHODS
        //

        protected void updatePerformanceMeasure(IAgent forAgent, double addTo)
        {
            performanceMeasures[forAgent] = getPerformanceMeasure(forAgent)
                    + addTo;
        }

        protected void updateEnvironmentViewsAgentAdded(IAgent agent) {
		foreach (EnvironmentView view in views) {
			view.agentAdded(agent, getCurrentState());
		}
	}

        protected void updateEnvironmentViewsAgentActed(IAgent agent, Action action,
                EnvironmentState state) {
		foreach (EnvironmentView view in views) {
			view.agentActed(agent, action, state);
		}
	}
    }
}