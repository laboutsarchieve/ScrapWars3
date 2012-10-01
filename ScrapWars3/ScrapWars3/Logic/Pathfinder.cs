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
        
        private static Mech mech;
        private static Rectangle mechBox;
        private static Map map;
        private static Vector2 goal;
        private static HashSet<Vector2> explored;
        private static SortedMultiDictionary<float, PathNode> toExplore;
        private static Vector2[] PossibleMoves;

        static Pathfinder( )
        {
            PossibleMoves = new Vector2[8];

            PossibleMoves[0] = new Vector2(0,1);
            PossibleMoves[1] = new Vector2(1,0);
            PossibleMoves[2] = new Vector2(0,-1);
            PossibleMoves[3] = new Vector2(-1,0);
            PossibleMoves[4] = new Vector2(-1,-1);
            PossibleMoves[5] = new Vector2(-1,1);
            PossibleMoves[6] = new Vector2(1,-1);
            PossibleMoves[7] = new Vector2(1,1);
        }

        // TODO: find a more elegant way to get around this naming issue
        public static List<Vector2> FindPath(Mech theMech, Map theMap, Vector2 theGoal)
        {
            mech = theMech;
            map = theMap;
            goal = theGoal;

            mechBox = new Rectangle(0,0,(int)theMech.Size.X/GameSettings.TileSize, (int)theMech.Size.Y/GameSettings.TileSize);

            Vector2 startTile = new Vector2((int)mech.Location.X/GameSettings.TileSize, (int)mech.Location.Y/GameSettings.TileSize);

            PathNode start = new PathNode(startTile);

            // convert the goal to tiles
            goal.X = (int)goal.X/GameSettings.TileSize;
            goal.Y = (int)goal.Y/GameSettings.TileSize;

            if(map[(int)goal.X, (int)goal.Y] == Tile.Water)               
                return start.GetPath( ); // If the goal is water, it can't be reached, return a meaningless path            
            
            explored = new HashSet<Vector2>();
            toExplore = new SortedMultiDictionary<float, PathNode>();

            explored.Add(start.Step);
            toExplore.Add(start.StepCost, start);

            PathNode currentPath = start;
            bool found = false;
            while(toExplore.Count > 0 && currentPath.StepCost < map.Width*map.Height)
            {
                currentPath = toExplore.Pop( );
                if(currentPath.Step == goal)
                { 
                    found = true;
                    break;
                }                
                ExploreNode(currentPath);
            }

            if(found)
                return currentPath.GetPath( );
            else
                return start.GetPath( );
        }

        private static void ExploreNode(PathNode node)
        {
            foreach(Vector2 move in PossibleMoves)
            {
                Vector2 stepToConcider = node.Step + move;

                mechBox.X = (int)stepToConcider.X;
                mechBox.Y = (int)stepToConcider.Y;

                if(map.IsOnMap((int)stepToConcider.X, (int)stepToConcider.Y) &&
                   !map.ContainsTileType(mechBox, Tile.Water) &&
                   !explored.Contains(stepToConcider))
                {
                    explored.Add(stepToConcider);
                    PathNode newNode = new PathNode(node, stepToConcider, move.Length( ));
                    toExplore.Add(GetCost(newNode), newNode);
                }
            }
        }
        private static float GetCost(PathNode node)
        {
            return (node.Step - goal).Length() + node.StepCost;
        }
        private static bool CanStandOnTile(Vector2 location)
        {
            Rectangle boundingRect = mech.BoundingRect;
            boundingRect.X = (int)location.X;
            boundingRect.Y = (int)location.Y;

            return !map.ContainsTileType(boundingRect, Tile.Water);
        }
    }
}

