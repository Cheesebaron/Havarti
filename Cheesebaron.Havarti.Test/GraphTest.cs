using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cheesebaron.Havarti.Test
{
    [TestClass]
    public class GraphTest
    {
        /*
         * var directedGraph = new Graph(GraphType.Directed);
            directedGraph.AddVertex(new Vertex<string> { Data = "Test 1", Id = "1", Label = "Node 1" });
            directedGraph.AddVertex(new Vertex<string> { Data = "Test 2", Id = "2", Label = "Node 2" });
            directedGraph.AddVertex(new Vertex<string> { Data = "Test 3", Id = "3", Label = "Node 3" });
            directedGraph.AddVertex(new Vertex<string> { Data = "Test 4", Id = "4", Label = "Node 4" });
            directedGraph.AddVertex(new Vertex<string> { Data = "Test 5", Id = "5", Label = "Node 5" });
            directedGraph.AddVertex(new Vertex<string> { Data = "Test 6", Id = "6", Label = "Node 6" });
            directedGraph.AddVertex(new Vertex<string> { Data = "Test 7", Id = "7", Label = "Node 7" });
            directedGraph.AddEdge(directedGraph.Vertices["1"], directedGraph.Vertices["2"], "E1", 5);
            directedGraph.AddEdge(directedGraph.Vertices["1"], directedGraph.Vertices["3"], "E2", 3);
            directedGraph.AddEdge(directedGraph.Vertices["1"], directedGraph.Vertices["5"], "E3", 7);
            directedGraph.AddEdge(directedGraph.Vertices["2"], directedGraph.Vertices["4"], "E4", 6);
            directedGraph.AddEdge(directedGraph.Vertices["2"], directedGraph.Vertices["5"], "E5", 3);
            directedGraph.AddEdge(directedGraph.Vertices["3"], directedGraph.Vertices["5"], "E6", 8);
            directedGraph.AddEdge(directedGraph.Vertices["3"], directedGraph.Vertices["6"], "E7", 3);
            directedGraph.AddEdge(directedGraph.Vertices["4"], directedGraph.Vertices["7"], "E8", 9);
            directedGraph.AddEdge(directedGraph.Vertices["5"], directedGraph.Vertices["7"], "E9", 8);
            directedGraph.AddEdge(directedGraph.Vertices["6"], directedGraph.Vertices["7"], "E10", 7);

            foreach (DictionaryEntry node in directedGraph.Vertices)
            {
                Console.WriteLine(node.Value);
            }

            foreach (var edge in directedGraph.Edges)
            {
                Console.WriteLine(edge);
            }

            var dfs = directedGraph.DepthFirstSearch(directedGraph.Vertices["1"]);

            foreach (var df in dfs)
            {
                Console.WriteLine(df);
            }

            var bfs = directedGraph.BreathFirstSearch(directedGraph.Vertices["1"]);

            foreach (var df in bfs)
            {
                Console.WriteLine(df);
            }*/

        [TestMethod]
        public void VertexTest()
        {
        }

        [TestMethod]
        public void VertexListTest()
        {
        }

        [TestMethod]
        public void EdgeTest()
        {
        }
    }
}
