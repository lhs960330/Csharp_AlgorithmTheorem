using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace _12._PathFinding
{
    internal class Astar
    {
        /* A* 알고리즘
         * 
         * 다익스트라 알고리즘을 확장하여 만든 최단경로 탐색알고리즘
         * 경로 탐색의 우선순의를 두고 유망한 해부터 우선적으로 탐색
         */

        // 다익스트라
        // 방문하지 않은 정점 중 가장 가까운 정점 선택

        // A*
        // 방문하지 않은 정점 중 예상 가중치(목표)가 가장 가까운 경우를 선택

        // 멘허튼 거리 : 직선을 통해 이동하는 거리
        // 유클리드 거리 : 대각선을 통해 이동하는 거리 (정확하지만 계산이 오래 걸림)

        // 직선과 곡선의 가중치
        const int Coststraight = 10;
        const int CostDiagonal = 14;

        static Point[] Direction =
        {
            new Point(0, +1),               // 위
            new Point(0, -1),               // 아래
            new Point(-1, 0),               // 좌
            new Point(+1, 0),               // 우
            new Point( -1, +1 ),		    // 좌상
			new Point( -1, -1 ),		    // 좌하
			new Point( +1, +1 ),		    // 우상
			new Point( +1, -1 )		        // 우하
            
        };
        public static bool PathFinding(bool[,] tileMap, Point start, Point end, out List<Point> path)
        {
            // 그래프의 x,y를 정해준다.
            int ySize = tileMap.GetLength(0);
            int xSize = tileMap.GetLength(1);

            ASNode[,] nodes = new ASNode[ySize, xSize];
            bool[,] visited = new bool[ySize, xSize];

            // f가 가장 작은걸 찾게 해줘야됨(우선수위가 가장 높아야됨 즉 우선 순위 큐를 사용)
            // 탐욕 알고리즘
            PriorityQueue<ASNode, int> nextPointPQ = new PriorityQueue<ASNode, int>();

            // 0. 시작 정점을 생성하여 추가
            ASNode startNode = new ASNode(start, new Point(), 0, Heruistic(end, start));
            nodes[startNode.pos.y, startNode.pos.x] = startNode;
            nextPointPQ.Enqueue(startNode, startNode.f);
            nextPointPQ.Enqueue(startNode, startNode.f);

            // 1. 다음으로 탐색할 정점 꺼내기 : f가 가장낮은거
            while (nextPointPQ.Count > 0)
            {
                // 1. 다음으로 탐색할 정점 꺼내기
                ASNode nextNode = nextPointPQ.Dequeue();

                // 2. 방문한 정점은 방문표시
                visited[nextNode.pos.y, nextNode.pos.x] = true;

                // 3. 탐색할 정점이 도착지인 경우
                // 도착했다고 판단해서 경로를 변환
                if (nextNode.pos.x == end.x && nextNode.pos.y == end.y)
                {
                    path = new List<Point>();

                    Point point = end;
                    while (false == (point.x == start.x && point.y == start.y))
                    {
                        path.Add(point);
                        point = nodes[point.y, point.x].parent;
                    }
                    path.Add(start);

                    path.Reverse();
                    return true;
                }

                // 4. 탐색한 정점의 주변을의 정점의 점수 계산
                for (int i = 0; i < Direction.Length; i++)
                {
                    int x = nextNode.pos.x + Direction[i].x;  // 좌우 
                    int y = nextNode.pos.y + Direction[i].y;  // 위 아래

                    // 4-1. 점수계산을 하면 안되는 경우 제외
                    // 맵을 벗어나는 경우
                    if (x < 0 || x >= xSize || y < 0 || y >= ySize)
                    {
                        continue;
                    }
                    // 탐색할 수 없는 정점인 경우
                    else if (false == tileMap[y, x])
                    {
                        continue;
                    }
                    // 이미 탐색한 정점인 경우
                    else if (visited[y, x])
                    {
                        continue;
                    }
                    // 대각선으로 이동이 불가능 지역인 경우
                    else if (i >= 4 && tileMap[y, nextNode.pos.x] == false && tileMap[nextNode.pos.y, x] == false)
                        continue;
                    //4-2. 점수를 계산한 정점 만들기
                    int g = nextNode.g + i < 4 ? Coststraight : CostDiagonal;
                    int h = Heruistic(new Point(x, y), end);
                    ASNode newNode = new ASNode(new Point(x, y), nextNode.pos, g, h);

                    // 4-3. 정점이 갱신이 필요한 경우 새로운 정점으로 할당
                    if (nodes[y, x] == null ||      // 점수계산을 하지 않은 정점이거나
                         nodes[y, x].f > newNode.f)   // 새로운 정점의 f 가중치가 더 낮은 경우
                    {
                        nodes[y, x] = newNode;
                        nextPointPQ.Enqueue(newNode, newNode.f);
                    }
                }
            }

            path = null;
            return false;
        }
        private static int Heruistic(Point start, Point end)
        {
            // 음수가 나오면 안되니 절대값 Math.Abs()를 써준다.
            int xSize = Math.Abs(start.x - end.x);    // 가로로 가야하는 횟수
            int ySize = Math.Abs(start.y - end.y);    // 세로로 가야하는 횟수

            // 맨허튼 거리 : 직선을 통해 이동하는 거리 (빠르지만 정확성이 좀 떨어짐)
            // return Coststraight * (xSize + ySize);

            // 유클리드 거리 : 대각선을 통해 이동하는 거리 (맨허튼 보다 느리지만 좀더 정확하다.)
            // return Coststraight* (int) Math.Sqrt(xSize * xSize +ySize * ySize);

            // 타일맵 유클리드 거리 : 직선과 대각선을 통해 이동하는 거리
            int stratghtCount = Math.Abs(xSize - ySize);
            int didgonalCount = Math.Max(xSize, ySize) - stratghtCount;
            return Coststraight * stratghtCount + CostDiagonal * didgonalCount;

        }

        private class ASNode
        {
            public Point pos;    // 정점의 위치
            public Point parent; // 이 정점을 탐색한 정점의 위치


            public int f;  // 총 예상 거리 f = g + h
            public int g;  // 시작점에서 이동한 거리 경로상의 누적 가중치
            public int h;  // 휴리스틱, 앞으로 도착점까지 예상 거리, 하지만 장애물은 계산 안한

            public ASNode(Point pos, Point parent, int g, int h)
            {
                this.pos = pos;
                this.parent = parent;
                this.f = g + h;
                this.h = h;
                this.g = g;
            }

        }
    }


    public struct Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


    }
}
