namespace _05._BinarySearchTree
{
    internal class Program
    {
        /* 이진탐색트리 (BinatySearchTree)
         * 
         * 이진속성과 탐색속성을 적용한 트리
         * 이진탐색을 통한 탐색영역을 절반으로 줄여가며 탐색 가능
         * 이진 : 부모노드는 최대 2개의 자식노드를 가질 수 있음
         * 탐색 : 자신의 노드보다 작은 값들은 왼쪽, 큰 값들은 오른쪽에 위치
         */

        // <이진탐색트리 구현>
        // 이진탐색트리는 모든 노드들이 최대 2개의 자식노드를 가질 수 있으며
        // 자신의 노드보다 작은 값들은 왼쪽, 큰 값들은 오른쪽에 위치시킴
        //
        //             23
        //      ┌──────┴──────┐
        //      11            38
        //   ┌──┴──┐       ┌──┴──┐
        //   3     19      31    65
        //   └─┐ ┌─┴─┐   ┌─┘     └─┐
        //     6 17  22  24        87

        // <이진탐색트리 탐색>
        // 아래의 이진탐색트리에서 17 탐색
        // 루트 노드부터 시작하여 탐색하는 값과 비교하여,
        // 작은 경우 왼쪽자식노드로, 큰 경우 오른쪽자식노드를 탐색
        //
        //             23(↙)
        //      ┌──────┴──────┐
        //      11(↘)         38
        //   ┌──┴──┐       ┌──┴──┐
        //   3     19(↙)   31    65
        //   └┐  ┌─┴─┐   ┌─┘
        //    6 (17)  22  24

        // <이진탐색트리 삽입>
        // 아래의 이진탐색트리에서 35 삽입
        // 루트 노드부터 시작하여 삽입하는 값과 비교하여,
        // 작은 경우 왼쪽자식노드로, 큰 경우 오른쪽자식노드로 하강
        // 만약 빈공간이라면 빈공간에 삽입
        //
        //             23(↘)                          23
        //      ┌──────┴──────┐                ┌──────┴──────┐
        //      11            38(↙)            11            38
        //   ┌──┴──┐       ┌──┴──┐      =>  ┌──┴──┐       ┌──┴──┐ 
        //   3     19      31(↘) 65         3     19      31    65
        //   └─┐ ┌─┴─┐   ┌─┘                └─┐ ┌─┴─┐   ┌─┴─┐
        //     6 17  22  24                   6 17  22  24 (35)


        // <이진탐색트리 삭제>
        // 1. 자식이 0개인 노드의 삭제 : 단순 삭제 진행
        // 아래의 이진탐색트리에서 22 삭제
        //
        //             23                             23
        //      ┌──────┴──────┐                ┌──────┴──────┐
        //      11            38               11            38
        //   ┌──┴──┐       ┌──┴──┐    =>    ┌──┴──┐       ┌──┴──┐
        //   3     19      31    65         3     19      31    65
        //   └─┐ ┌─┴─┐   ┌─┘                └─┐ ┌─┘     ┌─┴─┐
        //     6 17 (22) 24                   6 17      24  35
        //
        // 2. 자식이 1개인 노드의 삭제 : 삭제하는 노드의 부모와 자식을 연결 후 삭제
        // 아래의 이진탐색트리에서 38 삭제
        //
        //            23                              23
        //     ┌──────┴──────┐                 ┌──────┴──────┐
        //     11            (38)              11            31
        //  ┌──┴──┐       ┌──┘        =>    ┌──┴──┐       ┌──┴──┐ 
        //  3     19      31                3     19      24    35
        //  └─┐ ┌─┴─┐   ┌─┴─┐               └─┐ ┌─┴─┐
        //    6 17  22  24  35                6 17  22
        //
        // 3. 자식이 2개인 노드의 삭제 : 삭제하는 노드를 기준으로 오른쪽자식 중 가장 작은 값 노드와 교체 후 삭제
        // 아래의 이진탐색트리에서 23 삭제
        //
        //           (23)                             24                              24
        //     ┌──────┴──────┐                 ┌──────┴──────┐                 ┌──────┴──────┐
        //     11            38                11            38                11            38
        //  ┌──┴──┐       ┌──┴──┐     =>    ┌──┴──┐       ┌──┴──┐     =>    ┌──┴──┐       ┌──┴──┐ 
        //  3     19      24    49          3     19     (23)   49          3     19      35   49
        //  └─┐ ┌─┴─┐     └─┐               └─┐ ┌─┴─┐     └─┐               └─┐ ┌─┴─┐
        //    6 17  22      35                6 17  22      35                6 17  22


        // <이진탐색트리 정렬>
        // 이진탐색트리는 중위순회시 오름차순으로 정렬됨
        //
        //             7
        //      ┌──────┴──────┐
        //      4             11
        //   ┌──┴──┐       ┌──┴──┐
        //   2     5       9     12
        // ┌─┴─┐   └─┐   ┌─┴─┐
        // 1   3     6   8   10
        //
        // 중위순회 : ((1, 2, 3), 4, (5, 6)), 7, ((8, 9, 10), 11, 12)
        //            => 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12


        // <이진탐색트리 주의점>
        // 이진탐색트리는 최악의 상황에 노드들이 한쪽 자식으로만 추가되는 불균형 현상이 발생 가능
        // 이 경우 탐색영역이 절반으로 줄여지지 않기 때문에 시간복잡도 증가
        //
        //           5
        //         ┌─┘
        //         4
        //       ┌─┘
        //       3
        //     ┌─┘
        //     2
        //   ┌─┘
        //   1
        //
        // 이러한 현상을 막기 위해 자가균형기능을 추가한 트리의 사용이 일반적
        // 자가균형트리는 회전을 이용하여 불균형이 있는 상황을 해결
        //
        //       8                        5
        //    ┌──┴──┐   -- 우회전 ->   ┌──┴──┐
        //    5     9                  3     8
        //  ┌─┴─┐       <- 좌회전 --       ┌─┴─┐      
        //  3   6                          6   9 
        //
        // 대표적인 방식으로 Red-Black Tree, AVL Tree 등을 통해 불균형상황을 파악


        // <이진탐색트리 시간복잡도>
        // - 평균 -
        // 접근       탐색       삽입       삭제
        // O(logn)    O(logn)    O(logn)    O(logn)
        //
        // - 최악 -
        // 접근       탐색       삽입       삭제
        // O(n)       O(n)       O(n)       O(n)




        static void Main(string[] args)
        {
            // 이진 탐색트리 기반의 SortedSet 자료구조
            // 중복이 없는 정렬을 보장한 저장소
            SortedSet<int> sortedSet = new SortedSet<int>();
            
         
            // 삽입
            sortedSet.Add(1);
            sortedSet.Add(4);
            sortedSet.Add(5);
            sortedSet.Add(2);
            sortedSet.Add(3);
            sortedSet.Add(6);
            sortedSet.Add(3);  // 3을 또 추가하면 중복이기 때문에 무시한다.

            // 삭제
            sortedSet.Remove(3);   

            // 탐색
            bool sucess= sortedSet.Contains(3);
            bool sucess1= sortedSet.Contains(4);


            // 순서대로 출력시 정렬된 결과 확인
            foreach(int value in sortedSet)
            {
                Console.WriteLine(value);   // output : 작은 순서대로 나옴
            }
            Console.WriteLine();
            
            // 이진탐색트리 기반의 SortedDictionary 자료구조
            // 중복을 허용하지 않는 key를 기준으로 정렬을 보장한 value 저장소
            // 문자열은 사전 순이다.
            SortedDictionary<string, Item> sortedDictionary = new SortedDictionary<string, Item>();

            // SortedSet<Item> set = new SortedSet<Item>();

            // set.Add(new Item("포션", 3));
            // set.Add(new Item("검", 2));   // 오류가 난다. 비교할게 없어서 처음은 저장해주지만 2번째부터는 저장할려고하면 예외가 발생함
            
            // 삽입
            sortedDictionary.Add("포션", new Item("포션", 3));
            sortedDictionary.Add("소드", new Item("소드", 10));
            sortedDictionary.Add("방패", new Item("방패", 1));
            sortedDictionary.Add("신발", new Item("신발", 100));
            sortedDictionary.Add("롱소드", new Item("롱소드", 20));
            sortedDictionary.Add("망토", new Item("망토", 30));
            // sortedDictionary.Add("포션", new Item("포션", 20));    // 이미 있는거는 예외로 발생한다.
            sortedDictionary.TryAdd("방패", new Item("방패", 3));    //예외처리까지 감안하여 키 추가

            // 삭제
            sortedDictionary.Remove("롱소드");    // Key값으로 지워주면된다.

            // 탐색
            sortedDictionary.ContainsKey("방패");  // key로 찾으니 뒤에 key를 써주자.
            sortedDictionary.TryGetValue("방패", out Item shied);   // 예외처리를 같이 해줄 수 있다.
            Item item = sortedDictionary["방패"];  // 인덱서[key]를 통한 탐색

            // 순서대로 출력시 정렬된 결과 확인
            foreach( string name in sortedDictionary.Keys)
            {
                Console.WriteLine(name);
            }

            foreach(Item item1 in sortedDictionary.Values)
            {
                Console.WriteLine($"{item1.name}, {item1.value}");
            }




        }
        public class Item
        {
            public string name;
            public int value;

            public Item(string name, int value)
            {
                this.name = name;
                this.value = value;
            }
        }
    }
}
