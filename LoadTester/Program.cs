
public class Container
{
    public int length;
    public int width;
    public int height;
    public int maxHeight;
    public int usedHeight;
    public int loadedItems;
    public List<Item> contents;

    public Container(int length, int width, int height, int maxHeight)
    {
        this.length = length;
        this.width = width;
        this.height = height;
        this.maxHeight = maxHeight;
        this.contents = new List<Item>();
    }
    public bool CanLoadItem(Item item)
    {
        return this.usedHeight + item.height <= this.maxHeight;
    }

    public void AddItem(Item item)
    {
        this.usedHeight += item.height;
        this.loadedItems++;
    }

}

public class Item
{
    public int length;
    public int width;
    public int height;

    public Item(int length, int width, int height)
    {
        this.length = length;
        this.width = width;
        this.height = height;
    }
}

public class LoadTester
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Länge des Containers: ");
        int containerLength = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Breite des Containers: ");
        int containerWidth = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Höhe des Containers: ");
        int containerHeight = Convert.ToInt32(Console.ReadLine());
        int containerMaxHeight;
        do
        {
            Console.WriteLine("Bitte geben Sie die maximal Stapelbare Höhe des Containers an: ");
            containerMaxHeight = Convert.ToInt32(Console.ReadLine());

            if (containerMaxHeight > containerHeight)
            {
                Console.WriteLine("Die maximal Stapelbare Höhe kann nicht über der insgesamt vorhandenen Höhe liegen. Bitte geben Sie einen passenden Höhenwert ein: ");
            }
        }
        while (containerMaxHeight > containerHeight);

        Console.WriteLine("Containerparameter wurden erstellt! Länge: {0}cm, Breite: {1}cm, Höhe: {2}cm.", containerLength, containerWidth, containerHeight);
        Console.WriteLine("Die maximal Stapelbare Höhe beträgt {0}cm.", containerMaxHeight);
        Console.WriteLine("________________________");

        Console.WriteLine("Wie viele Packstücke müssen eingestapelt werden?: ");
        int amountOfItems = Convert.ToInt32(Console.ReadLine());

        Queue<Item> itemsQueue = new Queue<Item>();

        Console.WriteLine("Vielen Dank für die Eingabe! Nun geben Sie bitte die Maße (Länge, Breite, Höhe) für jedes der einzelnen Packstücke an.");
        for (int i = 0; i < amountOfItems; i++)
        {
            Console.WriteLine("Länge des {0}. Packstücks: ", i + 1);
            int itemLength = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Breite des {0}. Packstücks: ", i + 1);
            int itemWidth = Convert.ToInt32(Console.ReadLine());
            int itemHeight;
            do
            {
                Console.WriteLine("Höhe des {0}. Packstücks: ", i + 1);
                itemHeight = Convert.ToInt32(Console.ReadLine());

                if (itemHeight > containerMaxHeight)
                {
                    Console.WriteLine("Die Höhe des Packstücks kann nicht über der maximal Stapelbaren Höhe des Containers liegen. Bitte geben Sie einen passenden Höhenwert ein: ");
                }
            }
            while (itemHeight > containerMaxHeight);

            Item item = new Item(itemLength, itemWidth, itemHeight);
            itemsQueue.Enqueue(item);
        }

        Console.WriteLine("Packstücke wurden erfolgreich erstellt:");
        foreach (Item item in itemsQueue)
        {
            Console.WriteLine("Packstück: Länge: {0}cm, Breite: {1}cm, Höhe: {2}cm", item.length, item.width, item.height);

        }
        Console.WriteLine("________________________");

        List<Container> containers = new List<Container>();
        containers.Add(new Container(containerLength, containerWidth, containerHeight, containerMaxHeight));
        int currentContainerIndex = 0;

        while (itemsQueue.Count > 0)
        {
            Item currentItem = itemsQueue.Dequeue();
            if (!containers[currentContainerIndex].CanLoadItem(currentItem))
            {
                containers.Add(new Container(containerLength, containerWidth, containerHeight, containerMaxHeight));
                currentContainerIndex++;
            }
            containers[currentContainerIndex].AddItem(currentItem);
        }


        for (int i = 0; i < containers.Count; i++)
        {
            Console.WriteLine("Container {0} enthält {1} Packstücke:", i + 1, containers[i].loadedItems);
            foreach (var item in containers[i].contents)
            {
                Console.WriteLine("Packstück: Länge: {0}cm, Breite: {1}cm, Höhe: {2}cm", item.length, item.width, item.height);
            }
            Console.WriteLine("Aktuell genutzte Höhe: {0}cm", containers[i].usedHeight);
            Console.WriteLine("________________________");
        }

    }
}
