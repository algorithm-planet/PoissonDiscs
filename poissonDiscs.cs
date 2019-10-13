using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poissonDiscs
{
    public static List<Vector2> make(float disc , int possibles = 10)
    {
        List<Vector2> points = new List<Vector2>(),
                points_o = new List<Vector2>();

        #region const
        Vector2 scale = Vector2.one * 10;

        radius = disc * 2f;
        float sqr = radius * radius,
                s = radius / Mathf.Sqrt(2);

        w = (int)(scale.x / s);
        h = (int)(scale.y / s);
        #endregion

        int[,] grid = new int[w, h];
        points_o.Add(scale / 2f);

        while(true)
        {
            #region o
            int I = Random.Range(0, points_o.Count);
            Vector2 o = points_o[I];
            #endregion

            bool found = false;
            for(int i = 0; i < possibles; i += 1)
            {
                #region pos
                float r = radius * (1 + rand()),
                      A = rand() * 2 * Mathf.PI;

                Vector2 v = new Vector2()
                {
                    x = Mathf.Cos(A),
                    y = Mathf.Sin(A)
                }; 
                #endregion

                Vector2 pos = o + r * v;

                int X = (int)(pos.x / s),
                    Y = (int)(pos.y / s);

                #region rect
                if (!Inside(X, Y))
                    continue;
                #endregion

                #region full
                int index = grid[X, Y] - 1;
                if (index != -1)
                    continue; 
                #endregion

                found = true;
                #region neighbour
                //
                for (int y = -2; y <= 2; y += 1)
                {
                    for (int x = -2; x <= 2; x += 1)
                    {
                        if (x == 0 && y == 0)
                            continue;
                        if (!Inside(X + x, Y + y))
                            continue;

                        index = grid[X + x, Y + y] - 1;
                        if (index == -1)
                            continue;

                        Vector2 neighbour = points[index];
                        if (Z.sqr(neighbour - pos) < sqr)
                        {
                            found = false;
                            break;
                        }
                    }
                }
                // 
                #endregion

                #region pos
                if (found)
                {
                    points.Add(pos);
                    points_o.Add(pos);
                    grid[X, Y] = points.Count;
                    break;
                } 
                #endregion
            }

            #region break
            if (!found)
                points_o.RemoveAt(I);

            if (points_o.Count == 0)
                break;
            #endregion
        }

        return points;
    }

    static int w, h;
    static float radius;
    #region rand
    static float rand()
    {
        return Random.Range(0f, 1f);
    }
    #endregion

    #region Inside
    static bool Inside(int X, int Y)
    {
        return X > 0 + radius && X < w - 1 - radius &&
               Y > 0 + radius && Y < h - 1 - radius;
    } 
    #endregion

}
