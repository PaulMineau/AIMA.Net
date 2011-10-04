namespace AIMA.Core.Util.DataStructure
{
    using System;
    using System.Collections.Generic;
    /**
     * Represents a directed labeled graph. Vertices are represented by their unique
     * labels and labeled edges by means of nested hashtables. Variant of class
     * {@code aima.util.Table}. This version is more dynamic, it requires no
     * initialization and can add new items whenever needed.
     * 
     * @author R. Lunde
     */
    public class LabeledGraph<VertexLabelType, EdgeLabelType>
    {

        /**
         * Lookup for edge label information. Contains an entry for every vertex
         * label.
         */
        private Dictionary<VertexLabelType, Dictionary<VertexLabelType, EdgeLabelType>> globalEdgeLookup;
        /** List of the labels of all vertices within the graph. */
        private List<VertexLabelType> vertexLabels;

        /** Creates a new empty graph. */
        public LabeledGraph()
        {
            globalEdgeLookup = new Dictionary<VertexLabelType, Dictionary<VertexLabelType, EdgeLabelType>>();
            vertexLabels = new List<VertexLabelType>();
        }

        /**
         * Adds a new vertex to the graph if it is not already present.
         */
        public void addVertex(VertexLabelType v)
        {
            checkForNewVertex(v);
        }

        /**
         * Adds a directed labeled edge to the graph. The end points of the edge are
         * specified by vertex labels. New vertices are automatically identified and
         * added to the graph.
         */
        public void set(VertexLabelType from, VertexLabelType to, EdgeLabelType el)
        {
            Dictionary<VertexLabelType, EdgeLabelType> localEdgeLookup = checkForNewVertex(from);
            localEdgeLookup.put(to, el);
            checkForNewVertex(to);
        }

        /** Handles new vertices. */
        private Dictionary<VertexLabelType, EdgeLabelType> checkForNewVertex(
                VertexLabelType v)
        {
            Dictionary<VertexLabelType, EdgeLabelType> result = globalEdgeLookup
                    .get(v);
            if (result == null)
            {
                result = new Dictionary<VertexLabelType, EdgeLabelType>();
                globalEdgeLookup.put(v, result);
                vertexLabels.Add(v);
            }
            return result;
        }

        /** Removes an edge from the graph. */
        public void remove(VertexLabelType from, VertexLabelType to)
        {
            Dictionary<VertexLabelType, EdgeLabelType> localEdgeLookup = globalEdgeLookup
                    .get(from);
            if (localEdgeLookup != null)
                localEdgeLookup.remove(to);
        }

        /**
         * Returns the label of the edge between the specified vertices and null if
         * there is no edge between them.
         */
        public EdgeLabelType get(VertexLabelType from, VertexLabelType to)
        {
            Dictionary<VertexLabelType, EdgeLabelType> localEdgeLookup = globalEdgeLookup
                    .get(from);
            return localEdgeLookup == null ? null : localEdgeLookup.get(to);
        }

        /**
         * Returns the labels of those vertices which can be obtained by following
         * the edges starting at the specified vertex.
         */
        public List<VertexLabelType> getSuccessors(VertexLabelType v)
        {
            List<VertexLabelType> result = new List<VertexLabelType>();
            Dictionary<VertexLabelType, EdgeLabelType> localEdgeLookup = globalEdgeLookup
                    .get(v);
            if (localEdgeLookup != null)
                result.AddRange(localEdgeLookup.keySet());
            return result;
        }

        /** Returns the labels of all vertices within the graph. */
        public List<VertexLabelType> getVertexLabels()
        {
            return vertexLabels;
        }

        /** Checks whether the given label is the label of one of the vertices. */
        public bool isVertexLabel(VertexLabelType v)
        {
            return globalEdgeLookup.get(v) != null;
        }

        /** Removes all vertices and all edges from the graph. */
        public void clear()
        {
            vertexLabels.clear();
            globalEdgeLookup.clear();
        }
    }
}