using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

public class FloodFill : MonoBehaviour
{
    public Color[] colors;

    // Start is called before the first frame update
    void Start()
    {
        int[,] map = new int[100, 100];
        SpreadPoints(map, colors.Length);
        Texture2D texture = new (100,100);
        
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                Color color = colors[Random.Range(0, colors.Length)];
                texture.SetPixel(i, j, color);
                Debug.Log(map[i,j]);
            }
        }
        
        byte[] dataBytes = texture.EncodeToPNG();
        string savePath = Application.dataPath + "/SampleCircle.png";
        FileStream fileStream = File.Open(savePath,FileMode.OpenOrCreate);
        fileStream.Write(dataBytes,0,dataBytes.Length);

    }
    
    private void SpreadPoints(int[,] map,int colorMax)
    {
        // 随机选择一个起点
        System.Random rand = new System.Random();
        int startX = rand.Next(map.GetLength(0));
        int startY = rand.Next(map.GetLength(1));

        // 在起点上随机生成一个值
        map[startX, startY] = rand.Next(colorMax);

        // 扩散值到周围的相邻点
        Queue<(int, int)> queue = new Queue<(int, int)>();
        queue.Enqueue((startX, startY));

        while (queue.Count > 0)
        {
            (int x, int y) = queue.Dequeue();

            // 扩散到上下左右四个方向
            if (x > 0 && map[x - 1, y] == 0)
            {
                map[x - 1, y] = rand.Next(colorMax);
                queue.Enqueue((x - 1, y));
            }
            if (y > 0 && map[x, y - 1] == 0)
            {
                map[x, y - 1] = rand.Next(colorMax);
                queue.Enqueue((x, y - 1));
            }
            if (x < map.GetLength(0) - 1 && map[x + 1, y] == 0)
            {
                map[x + 1, y] = rand.Next(colorMax);
                queue.Enqueue((x + 1, y));
            }
            if (y < map.GetLength(1) - 1 && map[x, y + 1] == 0)
            {
                map[x, y + 1] = rand.Next(colorMax);
                queue.Enqueue((x, y + 1));
            }
        }
    }

}
