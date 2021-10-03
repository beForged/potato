using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : MonoBehaviour
{

    private int wallBoundMin = 0;
    private int wallBoundMax = 10;

    public KitchenWall kitchenWallPrefab;
    
    public int maxWallSize = 20;
    // Start is called before the first frame update
    void Start()
    {
        wallBoundMax = maxWallSize;

        List<Vector2> walls = generateWallList(6);
        Vector2 last = walls[0], first = walls[0];
        for(int i = 1; i < walls.Count; i++)
        {
            createWall(last, walls[i]);
            last = walls[i];
        }
        //TODO install door here
        createWall(last, first);
    }

    private void createWall(Vector2 first, Vector2 second)
    {
        KitchenWall wall = Instantiate(kitchenWallPrefab) as KitchenWall;
        float centerY = (first.y + second.y) / 2f;
        float centerX = (first.x + second.x) / 2f;
        Vector3 position = new Vector3(centerX, 0, centerY);
        wall.transform.position = position;

        float length = Mathf.Sqrt(Mathf.Pow((first.x - second.x), 2) + Mathf.Pow((first.y - second.y), 2));
        wall.transform.localScale= new Vector3(length, 1, 1);

        Vector3 start = new Vector3(first.x, 0, first.y);
        Vector3 end = new Vector3(second.x, 0, second.y);
        Vector3 _direction = (start - end).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(_direction);
        wall.transform.rotation = lookRotation;
        wall.transform.Rotate(new Vector3(0, 1, 0), 90);
    }

    private List<Vector2> generateWallList(int points){
        List<int> xs = new List<int>(points);
        List<int> ys = new List<int>(points);
        for(int i = 0; i < points; i++){
            xs.Add(Random.Range(wallBoundMin, wallBoundMax));
            ys.Add(Random.Range(wallBoundMin, wallBoundMax));
        }

        xs.Sort();
        ys.Sort();

        int minX = xs[0];
        int minY = ys[0];
        int maxX = xs[points - 1];
        int maxY = ys[points - 1];

        List<int> xVec = new List<int>(points);
        List<int> yVec = new List<int>(points);

        int lastTop = minX;
        int lastBot = minX;

        for (int i = 0; i < points - 1; i++)
        {
            int xtick = xs[i];

            if(Random.value > .5f)
            {
                xVec.Add(xtick - lastTop);
                lastTop = xtick;
            } else
            {
                xVec.Add(lastBot - xtick);
                lastBot = xtick;
            }
        }

        xVec.Add(maxX - lastTop);
        xVec.Add(lastBot - maxX);

        int lastLeft = minY;
        int lastRight = minY;

        for (int i = 0; i < points - 1; i++)
        {
            int ytick = ys[i];

            if(Random.value > .5f)
            {
                yVec.Add(ytick - lastLeft);
                lastLeft = ytick;
            } else
            {
                yVec.Add(lastRight - ytick);
                lastRight = ytick;
            }
        }

        yVec.Add(maxY - lastLeft);
        yVec.Add(lastRight- maxY);

        Util.Shuffle(yVec);

        List<Vector2> vec = new List<Vector2>(points);

        for(int i = 0; i < points; i++)
        {
            vec.Add(new Vector2(xVec[i], yVec[i]));
        }

        //sort by angle
        vec.Sort(Comparer<Vector2>.Create((first, second) => Mathf.Atan2(first.x, first.y).CompareTo(Mathf.Atan2(second.x, second.y))));

        float x = 0, y = 0;
        float minPolygonX = 0;
        float minPolygonY = 0;
        List<Vector2> pts = new List<Vector2>();

        for(int i = 0; i < points; i++)
        {
            pts.Add(new Vector2(x, y));

            x += vec[i].x;
            y += vec[i].y;

            minPolygonX = Mathf.Min(minPolygonX, x);
            minPolygonY = Mathf.Min(minPolygonY, y);
        }

        float xShift = minX - minPolygonX;
        float yShift = minY - minPolygonY;

        for(int i = 0; i < points; i++)
        {
            Vector2 v = pts[i];
            pts[i] = new Vector2(v.x + xShift, v.y + yShift);
        }

        return pts;
    }


}

public static class Util
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

}
