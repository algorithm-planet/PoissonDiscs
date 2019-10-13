# PoissonDiscs


## points
```c#
        List<Vector2> points = new List<Vector2>(),
                points_o = new List<Vector2>();


        Vector2 scale = Vector2.one * 10;

        radius = disc * 2f;
        float sqr = radius * radius,
                s = radius / Mathf.Sqrt(2);

        w = (int)(scale.x / s);
        h = (int)(scale.y / s

        int[,] grid = new int[w, h];
        points_o.Add(scale / 2f);
```

## loop

### o
```c#
            int I = Random.Range(0, points_o.Count);
            Vector2 o = points_o[I];

            bool found = false;
```

###  found
```c#
            for(int i = 0; i < possibles; i += 1)
            {
      
                float r = radius * (1 + rand()),
                      A = rand() * 2 * Mathf.PI;

                Vector2 v = new Vector2()
                {
                    x = Mathf.Cos(A),
                    y = Mathf.Sin(A)
                }; 
     

                Vector2 pos = o + r * v;

                int X = (int)(pos.x / s),
                    Y = (int)(pos.y / s);

       
                if (!Inside(X, Y))
                    continue;
     

       
                int index = grid[X, Y] - 1;
                if (index != -1)
                    continue; 
     

                found = true;
            
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
     

      
                if (found)
                {
                    points.Add(pos);
                    points_o.Add(pos);
                    grid[X, Y] = points.Count;
                    break;
                } 
     
            }
```
#### break 
```c#
            if (!found)
                points_o.RemoveAt(I);

            if (points_o.Count == 0)
                break;
```
