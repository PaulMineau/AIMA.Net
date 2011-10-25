using AIMA.Probability.Domain;
using AIMA.Probability.Util;

namespace AIMA.Probability.Example
{

/**
 * Predefined example Random Variables from AIMA3e.
 * 
 * @author Ciaran O'Reilly
 */

    public class ExampleRV
    {
        //
        public static readonly RandVar DICE_1_RV = new RandVar("Dice1",
                                                               new FiniteIntegerDomain(1, 2, 3, 4, 5, 6));
        public static  RandVar 
         DICE_2_RV 
    =
         new RandVar("Dice2",
         new FiniteIntegerDomain(1, 2, 3, 4, 5, 6)
    );
        //
        public static  RandVar 
         TOOTHACHE_RV 
    =
         new RandVar("Toothache",
         new BooleanDomain()
    );
        public static  RandVar 
         CAVITY_RV 
    =
         new RandVar("Cavity",
         new BooleanDomain()
    );
        public static  RandVar 
         CATCH_RV 
    =
         new RandVar("Catch",
         new BooleanDomain()
    );
        //
        public static  RandVar 
         WEATHER_RV 
    =
         new RandVar("Weather",
         new ArbitraryTokenDomain("sunny", "rain", "cloudy", "snow")
    );
        //
        public static  RandVar 
         MENINGITIS_RV 
    =
         new RandVar("Meningitis",
         new BooleanDomain()
    );
        public static  RandVar 
         STIFF_NECK_RV 
    =
         new RandVar("StiffNeck",
         new BooleanDomain()
    );
        //
        public static  RandVar 
         BURGLARY_RV 
    =
       new RandVar("Burglary",
       new BooleanDomain()
    );
        public static  RandVar 
       EARTHQUAKE_RV 
    =
       new RandVar("Earthquake",
       new BooleanDomain()
    );
        public static  RandVar 
       ALARM_RV 
    =
       new RandVar("Alarm",
       new BooleanDomain()
    );
        public static  RandVar 
       JOHN_CALLS_RV 
    =
       new RandVar("JohnCalls",
       new BooleanDomain()
    );
        public static  RandVar 
       MARY_CALLS_RV 
    =
       new RandVar("MaryCalls",
       new BooleanDomain()
    );
        //
        public static  RandVar 
       CLOUDY_RV 
    =
       new RandVar("Cloudy",
       new BooleanDomain()
    );
        public static  RandVar 
       SPRINKLER_RV 
    =
       new RandVar("Sprinkler",
       new BooleanDomain()
    );
        public static  RandVar 
       RAIN_RV 
    =
       new RandVar("Rain",
       new BooleanDomain()
    );
        public static  RandVar 
       WET_GRASS_RV 
    =
       new RandVar("WetGrass",
       new BooleanDomain()
    );
        //
        public static  RandVar 
       RAIN_tm1_RV 
    =
       new RandVar("Rain_t-1",
       new BooleanDomain()
    );
        public static  RandVar 
       RAIN_t_RV 
    =
       new RandVar("Rain_t",
       new BooleanDomain()
    );
        public static  RandVar 
       UMBREALLA_t_RV 
    =
       new RandVar("Umbrella_t",
       new BooleanDomain()
    );
    }
}