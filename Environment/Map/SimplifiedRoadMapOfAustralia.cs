namespace AIMA.Core.Environment.Map
{
    using System;
    using System.Collections.Generic;
    /**
     * Represents a simplified road map of Australia. The initialization method is
     * declared static. So it can also be used to initialize other specialized
     * subclasses of {@link ExtendableMap} with road map data from Australia. The
     * data was extracted from a class developed by Felix Knittel.
     * 
     * @author Ruediger Lunde
     */
    public class SimplifiedRoadMapOfAustralia : ExtendableMap
    {

        public SimplifiedRoadMapOfAustralia()
        {
            initMap(this);
        }

        // Locations
        public const String ADELAIDE = "Adelaide";
        public const String ALBANY = "Albany";
        public const String ALICE_SPRINGS = "AliceSprings";
        public const String BRISBANE = "Brisbane";
        public const String BROKEN_HILL = "BrokenHill";
        public const String BROOME = "Broome";
        public const String CAIRNS = "Cairns";
        public const String CAMARVON = "Camarvon";
        public const String CANBERRA = "Canberra";
        public const String CHARLEVILLE = "Charleville";
        public const String COOBER_PEDY = "CooberPedy";
        public const String DARWIN = "Darwin";
        public const String DUBBO = "Dubbo";
        public const String ESPERANCE = "Esperance";
        public const String GERALDTON = "Geraldton";
        public const String HALLS_CREEK = "HallsCreek";
        public const String HAY = "Hay";
        public const String KALGOORLIE = "Kalgoorlie";
        public const String KATHERINE = "Katherine";
        public const String LAKES_ENTRANCE = "LakesEntrance";
        public const String LONGREACH = "Longreach";
        public const String MACKAY = "Mackay";
        public const String MELBOURNE = "Melbourne";
        public const String MOUNT_GAMBIER = "MountGambier";
        public const String MT_ISA = "MtIsa";
        public const String NEWCASTLE = "Newcastle";
        public const String NORSEMAN = "Norseman";
        public const String NYNGAN = "Nyngan";
        public const String PERTH = "Perth";
        public const String PORT_AUGUSTA = "PortAugusta";
        public const String PORT_HEDLAND = "PortHedland";
        public const String PORT_LINCOLN = "PortLincoln";
        public const String PORT_MACQUARIE = "PortMacquarie";
        public const String ROCKHAMPTON = "Rockhampton";
        public const String SYDNEY = "Sydney";
        public const String TAMWORTH = "Tamworth";
        public const String TENNANT_CREEK = "TennantCreek";
        public const String TOWNSVILLE = "Townsville";
        public const String WAGGA_WAGGA = "WaggaWagga";
        public const String WARNAMBOOL = "Warnambool";
        public const String WYNDHAM = "Wyndham";

        /**
         * Initializes a map with a simplified road map of Australia.
         */
        public static void initMap(ExtendableMap map)
        {
            map.clear();
            // Add links
            // Distances from http://maps.google.com
            map.AddBidirectionalLink(PERTH, ALBANY, 417.0);
            map.AddBidirectionalLink(PERTH, KALGOORLIE, 593.0);
            map.AddBidirectionalLink(PERTH, GERALDTON, 424.0);
            map.AddBidirectionalLink(PERTH, PORT_HEDLAND, 1637.0);
            map.AddBidirectionalLink(ALBANY, ESPERANCE, 478.0);
            map.AddBidirectionalLink(KALGOORLIE, NORSEMAN, 187.0);
            map.AddBidirectionalLink(ESPERANCE, NORSEMAN, 204.0);
            map.AddBidirectionalLink(NORSEMAN, PORT_AUGUSTA, 1668.0);
            map.AddBidirectionalLink(GERALDTON, CAMARVON, 479.0);
            map.AddBidirectionalLink(CAMARVON, PORT_HEDLAND, 872.0);
            map.AddBidirectionalLink(PORT_HEDLAND, BROOME, 589.0);
            map.AddBidirectionalLink(BROOME, HALLS_CREEK, 685.0);
            map.AddBidirectionalLink(HALLS_CREEK, WYNDHAM, 370.0);
            map.AddBidirectionalLink(HALLS_CREEK, KATHERINE, 874.0);
            map.AddBidirectionalLink(WYNDHAM, KATHERINE, 613.0);
            map.AddBidirectionalLink(KATHERINE, DARWIN, 317.0);
            map.AddBidirectionalLink(KATHERINE, TENNANT_CREEK, 673.0);
            map.AddBidirectionalLink(TENNANT_CREEK, MT_ISA, 663.0);
            map.AddBidirectionalLink(TENNANT_CREEK, ALICE_SPRINGS, 508.0);
            map.AddBidirectionalLink(ALICE_SPRINGS, COOBER_PEDY, 688.0);
            map.AddBidirectionalLink(COOBER_PEDY, PORT_AUGUSTA, 539.0);
            map.AddBidirectionalLink(MT_ISA, TOWNSVILLE, 918.0);
            map.AddBidirectionalLink(TOWNSVILLE, CAIRNS, 346.0);
            map.AddBidirectionalLink(MT_ISA, LONGREACH, 647.0);
            map.AddBidirectionalLink(TOWNSVILLE, MACKAY, 388.0);
            map.AddBidirectionalLink(MACKAY, ROCKHAMPTON, 336.0);
            map.AddBidirectionalLink(LONGREACH, ROCKHAMPTON, 687.0);
            map.AddBidirectionalLink(ROCKHAMPTON, BRISBANE, 616.0);
            map.AddBidirectionalLink(LONGREACH, CHARLEVILLE, 515.0);
            map.AddBidirectionalLink(CHARLEVILLE, BRISBANE, 744.0);
            map.AddBidirectionalLink(CHARLEVILLE, NYNGAN, 657.0);
            map.AddBidirectionalLink(NYNGAN, BROKEN_HILL, 588.0);
            map.AddBidirectionalLink(BROKEN_HILL, PORT_AUGUSTA, 415.0);
            map.AddBidirectionalLink(NYNGAN, DUBBO, 166.0);
            map.AddBidirectionalLink(DUBBO, BRISBANE, 860.0);
            map.AddBidirectionalLink(DUBBO, SYDNEY, 466.0);
            map.AddBidirectionalLink(BRISBANE, TAMWORTH, 576.0);
            map.AddBidirectionalLink(BRISBANE, PORT_MACQUARIE, 555.0);
            map.AddBidirectionalLink(PORT_MACQUARIE, NEWCASTLE, 245.0);
            map.AddBidirectionalLink(TAMWORTH, NEWCASTLE, 284.0);
            map.AddBidirectionalLink(NEWCASTLE, SYDNEY, 159.0);
            map.AddBidirectionalLink(SYDNEY, CANBERRA, 287.0);
            map.AddBidirectionalLink(CANBERRA, WAGGA_WAGGA, 243.0);
            map.AddBidirectionalLink(DUBBO, WAGGA_WAGGA, 400.0);
            map.AddBidirectionalLink(SYDNEY, LAKES_ENTRANCE, 706.0);
            map.AddBidirectionalLink(LAKES_ENTRANCE, MELBOURNE, 317.0);
            map.AddBidirectionalLink(WAGGA_WAGGA, MELBOURNE, 476.0);
            map.AddBidirectionalLink(WAGGA_WAGGA, HAY, 269.0);
            map.AddBidirectionalLink(MELBOURNE, WARNAMBOOL, 269.0);
            map.AddBidirectionalLink(WARNAMBOOL, MOUNT_GAMBIER, 185.0);
            map.AddBidirectionalLink(MOUNT_GAMBIER, ADELAIDE, 449.0);
            map.AddBidirectionalLink(HAY, ADELAIDE, 655.0);
            map.AddBidirectionalLink(PORT_AUGUSTA, ADELAIDE, 306.0);
            map.AddBidirectionalLink(MELBOURNE, ADELAIDE, 728.0);
            map.AddBidirectionalLink(PORT_AUGUSTA, PORT_LINCOLN, 341.0);

            // Locations coordinates
            // Alice Springs is taken as central point with coordinates (0|0)
            // Therefore x and y coordinates refer to Alice Springs. Note that
            // the coordinates are not very precise and partly modified to
            // get a more real shape of Australia.
            map.setPosition(ADELAIDE, 417, 1289);
            map.setPosition(ALBANY, -1559, 1231);
            map.setPosition(ALICE_SPRINGS, 0, 0);
            map.setPosition(BRISBANE, 1882, 415);
            map.setPosition(BROKEN_HILL, 709, 873);
            map.setPosition(BROOME, -1189, -645);
            map.setPosition(CAIRNS, 1211, -791);
            map.setPosition(CAMARVON, -2004, -34);
            map.setPosition(CANBERRA, 1524, 1189);
            map.setPosition(CHARLEVILLE, 1256, 268);
            map.setPosition(COOBER_PEDY, 86, 593);
            map.setPosition(DARWIN, -328, -1237);
            map.setPosition(DUBBO, 1474, 881);
            map.setPosition(ESPERANCE, -1182, 1132);
            map.setPosition(GERALDTON, -1958, 405);
            map.setPosition(HALLS_CREEK, -630, -624);
            map.setPosition(HAY, 985, 1143);
            map.setPosition(KALGOORLIE, -1187, 729);
            map.setPosition(KATHERINE, -183, -1025);
            map.setPosition(LAKES_ENTRANCE, 1412, 1609);
            map.setPosition(LONGREACH, 1057, -49);
            map.setPosition(MACKAY, 1553, -316);
            map.setPosition(MELBOURNE, 1118, 1570);
            map.setPosition(MOUNT_GAMBIER, 602, 1531);
            map.setPosition(MT_ISA, 563, -344);
            map.setPosition(NEWCASTLE, 1841, 979);
            map.setPosition(NORSEMAN, -1162, 881);
            map.setPosition(NYNGAN, 1312, 781);
            map.setPosition(PERTH, -1827, 814);
            map.setPosition(PORT_AUGUSTA, 358, 996);
            map.setPosition(PORT_HEDLAND, -1558, -438);
            map.setPosition(PORT_LINCOLN, 169, 1205);
            map.setPosition(PORT_MACQUARIE, 1884, 849);
            map.setPosition(ROCKHAMPTON, 1693, -59);
            map.setPosition(SYDNEY, 1778, 1079);
            map.setPosition(TAMWORTH, 1752, 722);
            map.setPosition(TENNANT_CREEK, 30, -445);
            map.setPosition(TOWNSVILLE, 1318, -520);
            map.setPosition(WAGGA_WAGGA, 1322, 1125);
            map.setPosition(WARNAMBOOL, 761, 1665);
            map.setPosition(WYNDHAM, -572, -932);
        }
    }
}