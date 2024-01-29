const int width = 75, height = 25;

bool[,]
    grid1 = new bool[width, height],
    grid2 = new bool[width, height],

    currentGrid = grid1,
    nextGrid = grid2;

void PrintGrid()
{
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            if (currentGrid[x, y] != nextGrid[x, y])
            {
                Console.SetCursorPosition(x, y);
                Console.Write(currentGrid[x, y] ? "#" : " ");
            }
        }
    }
}

bool InRange(int x, int y)
{
    return x >= 0 && x < width && y >= 0 && y < height;
}

int NeighborWeight(int x, int y)
{
    return InRange(x, y) && currentGrid[x, y] ? 1 : 0;
}

int NeighborCount(int x, int y)
{
    return
        NeighborWeight(x - 1, y) +
        NeighborWeight(x + 1, y) +
        NeighborWeight(x, y - 1) +
        NeighborWeight(x, y + 1) +
        NeighborWeight(x - 1, y - 1) +
        NeighborWeight(x + 1, y - 1) +
        NeighborWeight(x - 1, y + 1) +
        NeighborWeight(x + 1, y + 1);
}

Random random = new();

for (int y = 0; y < height; y++)
{
    for (int x = 0; x < width; x++)
    {
        if (random.Next() % 2 == 0)
        {
            currentGrid[x, y] = true;
        }
    }
}

Console.Clear();

while (true)
{
    PrintGrid();

    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            int neighborCount = NeighborCount(x, y);

            if (currentGrid[x, y])
            {
                if (neighborCount < 2)
                {
                    nextGrid[x, y] = false;
                }
                else if (neighborCount <= 3)
                {
                    nextGrid[x, y] = true;
                }
                else
                {
                    nextGrid[x, y] = false;
                }
            }
            else
            {
                if (neighborCount == 3)
                {
                    nextGrid[x, y] = true;
                }
                else
                {
                    nextGrid[x, y] = false;
                }
            }
        }
    }

    bool[,] oldGrid = currentGrid;

    currentGrid = nextGrid;
    nextGrid = oldGrid;
}