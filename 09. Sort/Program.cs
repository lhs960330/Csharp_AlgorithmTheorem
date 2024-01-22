namespace _09._Sort
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Random random = new Random();
            int count = 20;

            List<int> selectionList = new List<int>();
            List<int> insertionList = new List<int>();
            List<int> bubbleList = new List<int>();
            List<int> mergeList = new List<int>();
            List<int> quickList = new List<int>();
            List<int> heapList = new List<int>();
            List<int> list = new List<int>();

            Console.WriteLine("랜덤 데이터 : ");
            for (int i = 0; i < count; i++)
            {
                int rand = random.Next() % 100;
                Console.Write($"{rand,3}");

                selectionList.Add(rand);
                insertionList.Add(rand);
                bubbleList.Add(rand);
                heapList.Add(rand);
                mergeList.Add(rand);
                quickList.Add(rand);
                list.Add(rand);
            }
            Console.WriteLine();


            Console.WriteLine("선택 정렬 결과 : ");
            Sorting.SelectionSort(selectionList);
            foreach (int i in selectionList)
            {
                Console.Write($"{i,3}");
            }
            Console.WriteLine();


            Console.WriteLine("삽입 정렬 결과 : ");
            Sorting.InsertionSort(insertionList);
            foreach (int i in insertionList)
            {
                Console.Write($"{i,3}");
            }
            Console.WriteLine();


            Console.WriteLine("버블 정렬 결과 : ");
            Sorting.BubbleSort(bubbleList);
            foreach (int i in bubbleList)
            {
                Console.Write($"{i,3}");
            }
            Console.WriteLine();


            Console.WriteLine("합병 정렬 결과 : ");
            Sorting.MergeSort(mergeList, 0, mergeList.Count - 1);
            foreach (int i in mergeList)
            {
                Console.Write($"{i,3}");
            }
            Console.WriteLine();


            Console.WriteLine("퀵 정렬 결과 : ");
            Sorting.QuickSort(quickList, 0, quickList.Count - 1);
            foreach (int i in quickList)
            {
                Console.Write($"{i,3}");
            }
            Console.WriteLine();


            Console.WriteLine("힙 정렬 결과 : ");
            Sorting.HeapSort(heapList);
            foreach (int i in heapList)
            {
                Console.Write($"{i,3}");
            }
            Console.WriteLine();


            Console.WriteLine("C# 인트로 정렬 결과 : ");
            list.Sort();
            foreach (int i in list)
            {
                Console.Write($"{i,3}");
            }
            Console.WriteLine();
        }
    }
}
