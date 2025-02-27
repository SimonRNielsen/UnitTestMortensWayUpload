using System.Numerics;
using System.Reflection;
using Testing;

namespace UnitTestMortensWay
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestAStar()
        {

            #region Grid and cells
            Dictionary<Vector2, Tile> cells = new Dictionary<Vector2, Tile>();
            List<Tile> tiles = new List<Tile>();
            for (int j = 0; j < 15; j++)
            {
                for (int i = 0; i < 15; i++)
                {
                    bool fencePath = false;
                    TileTypes tile;
                    switch (i)
                    {
                        case 0 when j == 13:
                            tile = TileTypes.Portal;
                            break;
                        case 1 when j == 3:
                            tile = TileTypes.TowerKey;
                            break;
                        case 13 when j == 12:
                            tile = TileTypes.TowerPortion;
                            break;
                        case > 2 when i < 12 && (j == 12 || j == 14):
                            tile = TileTypes.Fence;
                            break;
                        case > 2 when i < 12 && j == 13:
                            fencePath = true;
                            if (i == 4 || i == 7 || i == 10)
                                tile = TileTypes.FencePath;
                            else
                                tile = TileTypes.Path;
                            break;
                        case 1 when j == 13:
                        case 2 when j > 10 && j < 14:
                        case 3 when j == 11:
                        case 4 when j > 2 && j < 12:
                        case 5 when j == 3:
                        case 6 when j == 3:
                        case 7 when j == 3:
                        case 8 when j == 3:
                        case 9 when j > 2 && j < 12:
                        case 10 when j == 11:
                        case 11 when j == 11:
                        case 12 when j == 11 || j == 13:
                        case 13 when j == 11 || j == 13:
                            tile = TileTypes.Path;
                            break;
                        case 5 when j > 3 && j < 12:
                        case 6 when j > 3 && j < 12:
                        case 7 when j > 3 && j < 12:
                        case 8 when j > 3 && j < 12:
                            tile = TileTypes.Stone;
                            break;
                        default:
                            tile = TileTypes.Grass;
                            break;
                    }
                    Tile t = new Tile(tile, new Vector2(64 * i, 64 * j), fencePath);
                    cells.Add(t.Position, t);
                    tiles.Add(t);
                }
            }
            #endregion
            Tile startNode = tiles.Find(x => (TileTypes)x.Type == TileTypes.Portal);
            Tile endNode = tiles.Find(y => (TileTypes)y.Type == TileTypes.TowerPortion);
            List<Tile> expectedList = new List<Tile>();
            expectedList.Add(startNode);
            for (int i = 1; i < 13; i++)
            {
                Tile tile = tiles.Find(x => x.Position == new Vector2(i * 64, 13 * 64));
                expectedList.Add(tile);
            }
            expectedList.Add(endNode);
            List<Tile> pathTest = new List<Tile>();
            AStar aStar = new AStar(cells);
            pathTest = AStar.FindPath(startNode.Position, endNode.Position);

            CollectionAssert.AreEqual(expectedList, pathTest);

        }

        [TestMethod]
        public void BFSTest()
        {

            #region Grid & Edges
            List<Tile> tiles = new List<Tile>();
            for (int j = 0; j < 15; j++)
            {
                for (int i = 0; i < 15; i++)
                {
                    bool fencePath = false;
                    TileTypes tile;
                    switch (i)
                    {
                        case 0 when j == 13:
                            tile = TileTypes.Portal;
                            break;
                        case 1 when j == 3:
                            tile = TileTypes.TowerKey;
                            break;
                        case 13 when j == 12:
                            tile = TileTypes.TowerPortion;
                            break;
                        case > 2 when i < 12 && (j == 12 || j == 14):
                            tile = TileTypes.Fence;
                            break;
                        case > 2 when i < 12 && j == 13:
                            fencePath = true;
                            if (i == 4 || i == 7 || i == 10)
                                tile = TileTypes.FencePath;
                            else
                                tile = TileTypes.Path;
                            break;
                        case 1 when j == 13:
                        case 2 when j > 10 && j < 14:
                        case 3 when j == 11:
                        case 4 when j > 2 && j < 12:
                        case 5 when j == 3:
                        case 6 when j == 3:
                        case 7 when j == 3:
                        case 8 when j == 3:
                        case 9 when j > 2 && j < 12:
                        case 10 when j == 11:
                        case 11 when j == 11:
                        case 12 when j == 11 || j == 13:
                        case 13 when j == 11 || j == 13:
                            tile = TileTypes.Path;
                            break;
                        case 5 when j > 3 && j < 12:
                        case 6 when j > 3 && j < 12:
                        case 7 when j > 3 && j < 12:
                        case 8 when j > 3 && j < 12:
                            tile = TileTypes.Stone;
                            break;
                        default:
                            tile = TileTypes.Grass;
                            break;
                    }
                    Tile t = new Tile(tile, new Vector2(64 * i, 64 * j), fencePath);
                    tiles.Add(t);
                }
            }
            foreach (Tile entry in tiles)
            {
                entry.CreateEdges(tiles);
            }
            #endregion
            Tile startNode = tiles.Find(x => (TileTypes)x.Type == TileTypes.Portal);
            Tile endNode = tiles.Find(y => (TileTypes)y.Type == TileTypes.TowerPortion);
            List<Tile> expectedList = new List<Tile>();
            expectedList.Add(startNode);
            for (int i = 1; i < 13; i++)
            {
                if (i <= 2 || i == 12)
                {
                    Tile tile = tiles.Find(x => x.Position == new Vector2(i * 64, 12 * 64));
                    expectedList.Add(tile);
                }
                else
                {
                    Tile tile = tiles.Find(x => x.Position == new Vector2(i * 64, 13 * 64));
                    expectedList.Add(tile);
                }
            }
            expectedList.Add(endNode);
            List<Tile> pathTest = new List<Tile>();
            BFS.BFSMethod(startNode, endNode);
            pathTest = BFS.FindPath(endNode, startNode);

            CollectionAssert.AreEqual(expectedList, pathTest);

        }
    }
}
