using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ScrapWars3.Data;
using GameTools;

namespace ScrapWars3.Logic
{
    class Pathfinder
    {
        class PathNode
        {
            float stepCost;
            Vector2 step;
            PathNode parent;

            public PathNode(Vector2 start)
            {
                parent = this;
                step = start;
                stepCost = 0;
            }
            public PathNode(PathNode parentPath, Vector2 newStep, float newCost)
            {
                parent = parentPath;
                step = newStep;
                stepCost = parentPath.stepCost + newCost;
            }

            public List<Vector2> GetPath()
            {
                Stack<Vector2> reversePath = new Stack<Vector2>();
                AddToStack(reversePath);

                List<Vector2> path = new List<Vector2>();

                while(reversePath.Count > 0)
                    path.Add(reversePath.Pop());

                return path;
            }
            private void AddToStack(Stack<Vector2> reversePath)
            {
                if(parent.step != step)
                {
                    reversePath.Push(step);
                    parent.AddToStack(reversePath);
                }
            }
            public Vector2 Step
            {
                get { return step; }
            }
            public float StepCost
            {
                get { return stepCost; }
            }
        }

        private static float sqrtOfTwo = (float)Math.Sqrt(2);

        private static Mech mech;
        private static Rectangle mechBox;
        private static Map map;
        private static Vector2 goal;
        private static HashSet<Vector2> explored;
        private static SortedMultiDictionary<float, PathNode> toExplore;
        private static Vector2[] PossibleMoves;

        static Pathfinder()
        {
            PossibleMoves = new Vector2[8];

            PossibleMoves[0] = new Vector2(0, 1);
            PossibleMoves[1] = new Vector2(1, 0);
            PossibleMoves[2] = new Vector2(0, -1);
            PossibleMoves[3] = new Vector2(-1, 0);
            PossibleMoves[4] = new Vector2(-1, -1);
            PossibleMoves[5] = new Vector2(-1, 1);
            PossibleMoves[6] = new Vector2(1, -1);
            PossibleMoves[7] = new Vector2(1, 1);
        }

        // TODO: find a more elegant way to get around this naming issue
        public static List<Vector2> FindPath(Mech theMech, Map theMap, Vector2 theGoal)
        {
            mech = theMech;
            map = theMap;
            goal = theGoal;

            mechBox = new Rectangle(0, 0, (int)theMech.Size.X / GameSettings.TileSize + 1, (int)theMech.Size.Y / GameSettings.TileSize + 1);

            Vector2 startTile = new Vector2((int)mech.Position.X / GameSettings.TileSize, (int)mech.Position.Y / GameSettings.TileSize);

            PathNode start = new PathNode(startTile);

            // convert the goal to tiles
            goal.X = (int)goal.X / GameSettings.TileSize;
            goal.Y = (int)goal.Y / GameSettings.TileSize;

            if(map[(int)goal.X, (int)goal.Y] == Tile.Water)
                return start.GetPath(); // If the goal is water, it can't be reached, return a meaningless path            

            explored = new HashSet<Vector2>();
            toExplore = new SortedMultiDictionary<float, PathNode>();

            explored.Add(start.Step);
            toExplore.Add(start.StepCost, start);

            PathNode currentPath = start;
            bool found = false;
            while(toExplore.Count > 0 && currentPath.StepCost < map.Width * 2)
            {
                currentPath = toExplore.Pop();
                if(currentPath.Step == goal)
                {
                    found = true;
                    break;
                }
                ExploreNode(currentPath);
            }

            if(found)
                return currentPath.GetPath();
            else
                return start.GetPath();
        }

        private static void ExploreNode(PathNode node)
        {
            foreach(Vector2 move in PossibleMoves)
            {
                Vector2 stepToConcider = node.Step + move;

                if(map.IsOnMap((int)stepToConcider.X, (int)stepToConcider.Y) &&
                   CanStandOnTile(stepToConcider) &&
                   !explored.Contains(stepToConcider))
                {
                    float stepCost = (Math.Abs(move.X) == Math.Abs(move.Y)) ? sqrtOfTwo : 1;

                    explored.Add(stepToConcider);
                    PathNode newNode = new PathNode(node, stepToConcider, stepCost);
                    toExplore.Add(GetCost(newNode), newNode);
                }
            }
        }
        private static float GetCost(PathNode node)
        {
            Tile tileType = map[(int)node.Step.X, (int)node.Step.Y];
            float multiplier = 1;

            switch(tileType)
            {
                case Tile.Grass:
                    multiplier = 1f;
                    break;
                case Tile.Dirt:
                    multiplier = 1.5f;
                    break;
                case Tile.Sand:
                    multiplier = 2f;
                    break;
                default:                    
                    multiplier = 100;
                    break;
            }

            return (node.Step - goal).Length() + node.StepCost * multiplier;
        }
        private static bool CanStandOnTile(Vector2 location)
        {
            mechBox.X = (int)location.X - (int)mech.Size.X / GameSettings.TileSize / 2 - 1;
            mechBox.Y = (int)location.Y - (int)mech.Size.Y / GameSettings.TileSize / 2 - 1;

            return !map.ContainsTileType(mechBox, Tile.Water) && map.IsInMap(mechBox);
        }
    }
}

