using AIMA.Core.Util.Math;
using System;

namespace AIMA.Probability.Example
{
    /**
     * 
     * @author Ciaran O'Reilly
     * @author Ravi Mohan
     * 
     */

    public class HMMExampleFactory
    {

        public static HiddenMarkovModel getUmbrellaWorldModel()
        {
            Matrix transitionModel = new Matrix(new double[][]
                                                    {
                                                        {0.7, 0.3},
                                                        {0.3, 0.7}
                                                    });
            Map<Object, Matrix> sensorModel = new Map<Object, Matrix>();
            sensorModel.put(true, new Matrix(new double[][]
                                                         {
                                                             {0.9, 0.0},
                                                             {0.0, 0.2}
                                                         }));
            sensorModel.put(false, new Matrix(new double[][]
                                                          {
                                                              {0.1, 0.0}, {0.0, 0.8}
                                                          }));
            Matrix prior = new Matrix(new double[] {0.5, 0.5}, 2);
            return new HMM(ExampleRV.RAIN_t_RV, transitionModel, sensorModel, prior);
        }
    }
}