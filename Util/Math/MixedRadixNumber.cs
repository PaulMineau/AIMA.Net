namespace CosmicFlow.AIMA.Core.Util.Math
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    /**
     * @author Ciaran O'Reilly see:
     *         http://demonstrations.wolfram.com/MixedRadixNumberRepresentations/
     *         for useful example.
     */
    public class MixedRadixNumber
    {
        //
        private const long serialVersionUID = 1L;
        //
        private long value = 0L;
        private long maxValue = 0L;
        private int[] radixs = null;
        private int[] currentNumeralValue = null;
        private bool recalculate = true;

        public MixedRadixNumber(long value, int[] radixs)
        {
            this.value = value;
            this.radixs = new int[radixs.Length];
            System.Array.Copy(radixs, 0, this.radixs, 0, radixs.Length);
            calculateMaxValue();
        }

        public MixedRadixNumber(long value, List<int> radixs)
        {
            this.value = value;
            this.radixs = new int[radixs.Count];
            for (int i = 0; i < radixs.Count; i++)
            {
                this.radixs[i] = radixs[i];
            }
            calculateMaxValue();
        }

        public long getMaxAllowedValue()
        {
            return maxValue;
        }

        public bool increment()
        {
            if (value < maxValue)
            {
                value++;
                recalculate = true;
                return true;
            }

            return false;
        }

        public bool decrement()
        {
            if (value > 0)
            {
                value--;
                recalculate = true;
                return true;
            }
            return false;
        }

        public int getCurrentNumeralValue(int atPosition)
        {
            if (atPosition >= 0 && atPosition < radixs.Length)
            {
                if (recalculate)
                {
                    long quotient = value;
                    for (int i = 0; i < radixs.Length; i++)
                    {
                        if (0 != quotient)
                        {
                            currentNumeralValue[i] = (int)quotient % radixs[i];
                            quotient = quotient / radixs[i];
                        }
                        else
                        {
                            currentNumeralValue[i] = 0;
                        }

                    }
                    recalculate = false;
                }
                return currentNumeralValue[atPosition];
            }
            throw new InvalidOperationException(
                    "Argument atPosition must be >=0 and < " + radixs.Length);
        }

        //
        // START-Number
        public int intValue()
        {
            return (int)longValue();
        }

        public long longValue()
        {
            return value;
        }

        public float floatValue()
        {
            return longValue();
        }

        public double doubleValue()
        {
            return longValue();
        }

        // END-Number
        //

        public override String ToString()
        {
            if (null == currentNumeralValue)
            {
                return String.Empty;
            }
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < radixs.Length; i++)
            {
                sb.Append("[");
                sb.Append(this.getCurrentNumeralValue(i));
                sb.Append("]");
            }

            return sb.ToString();
        }

        //
        // PRIVATE
        //
        private void calculateMaxValue()
        {
            if (0 == radixs.Length)
            {
                throw new InvalidOperationException(
                        "At least 1 radix must be defined.");
            }
            for (int i = 0; i < radixs.Length; i++)
            {
                if (radixs[i] < 2)
                {
                    throw new InvalidOperationException(
                            "Invalid radix, must be >= 2");
                }
            }

            // Calcualte the maxValue allowed
            maxValue = radixs[0];
            for (int i = 1; i < radixs.Length; i++)
            {
                maxValue *= radixs[i];
            }
            maxValue -= 1;

            if (value > maxValue)
            {
                throw new InvalidOperationException(
                        "The value ["
                                + value
                                + "] cannot be represented with the radixs provided, max value is "
                                + maxValue);
            }

            currentNumeralValue = new int[radixs.Length];
        }
    }
}