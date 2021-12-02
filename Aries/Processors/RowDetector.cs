namespace Aries.Processors;

[Serializable]
public class RowDetector : Processor
{
    private bool SameColumn(int[] arr1, int[] arr2)
    {
        int left1 = arr1[0];
        int right1 = arr1[1];
        int width1 = arr1[2];

        int left2 = arr2[0];
        int right2 = arr2[1];
        int width2 = arr2[2];

        //no overlap
        if (left1 > right2) { return false; }
        if (left2 > right1) { return false; }

        // 1 in 2
        if (left1 >= left2 && right1 <= right2) { return true; }

        // 2 in 1
        if (left2 >= left1 && right2 <= right1) { return true; }

        int width3 = Math.Max(right1, right2) - Math.Min(left1, left2);
        if (width1 * 1.0f / width3 < 0.5) { return false; }
        if (width2 * 1.0f / width3 < 0.5) { return false; }

        return true;
    }

    public override XDocument Process(XDocument doc)
    {
        var pages = doc.Root.Elements("page").ToList();
        foreach (var page in pages)
        {
            var texts = page.Elements("text").ToList();
            int n = texts.Count;
            int[] unionFind = new int[n];
            int[][] matrix = new int[n][];
            for (int i = 0; i < n; i++)
            {
                int left = int.Parse(texts[i].Attribute("top").Value);
                int width = int.Parse(texts[i].Attribute("height").Value);
                int right = left + width;
                matrix[i] = new int[] { left, right, width };
                unionFind[i] = i;
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < texts.Count; j++)
                {

                    if( SameColumn(matrix[i], matrix[j]))
                    {
                        Union(unionFind, i, j);
                    }
                }
            }

            Flatten(unionFind);

            Dictionary<int, int> clusterDict = new Dictionary<int, int>();
            int clusterId = 0;
            foreach (int i in unionFind)
            {
                if(!clusterDict.ContainsKey(i))
                {
                    clusterDict.Add(i, clusterId++);
                }
            }

            //right cluster info back to texts
            for(int i = 0; i < n; i++)
            {
                clusterId = clusterDict[unionFind[i]];
                texts[i].Add(new XAttribute("row", clusterId));
            }
        }
        return doc;
    }

    private int Find(int[] unionFind, int i)
    {
        int p = i;
        while(p != unionFind[p])
        {
            p = unionFind[p];
        }
        return p;
    }

    private void Union(int[] unionFind, int i, int j)
    {
        int p1 = Find(unionFind, i);
        int p2 = Find(unionFind, j);
        unionFind[p2] = p1;
    }

    private void Flatten(int[] unionFind)
    {
        for(int i = 0;i < unionFind.Length; i++)
        {
            unionFind[i] = Find(unionFind, i);
        }
    }
}

