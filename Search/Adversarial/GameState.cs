namespace AIMA.Core.Search.Adversarial
{
    using System;
    using System.Collections.Generic;
    /**
     * @author Ravi Mohan
     * 
     */
    public class GameState
    {
        private Dictionary<String, Object> state;

        public GameState()
        {
            state = new Dictionary<String, Object>();
        }

        public override bool Equals(Object anotherState)
        {

            if (this == anotherState)
            {
                return true;
            }
            if ((anotherState == null)
                    || (this.getClass() != anotherState.getClass()))
            {
                return false;
            }
            GameState another = (GameState)anotherState;
            Set keySet1 = state.keySet();
            Iterator i = keySet1.iterator();
            Iterator j = another.state.keySet().iterator();
            while (i.hasNext())
            {
                String key = (String)i.next();
                bool keymatched = false;
                bool valueMatched = false;
                while (j.hasNext())
                {
                    String key2 = (String)j.next();
                    if (key.Equals(key2))
                    {
                        keymatched = true;
                        if (state.get(key).Equals(another.state.get(key2)))
                        {
                            valueMatched = true;
                        }
                        break;
                    }
                }
                if (!((keymatched) && valueMatched))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            int result = 17;
            foreach (String s in state.keySet())
            {
                result = 37 * result + s.GetHashCode();
                result = 37 * result + state.get(s).GetHashCode();
            }

            return result;
        }

        public Object get(String key)
        {
            return state.get(key);
        }

        public void put(String key, Object value)
        {
            state.put(key, value);

        }
    }
}