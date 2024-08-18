using System.Security.Cryptography;

class Node{
    public Node Parent;
    public Node Next;
    public List<int> Keys;
    public Dictionary<int, Row> ValuePairs; // Used only for leaf nodes
    public List<Node> Children; // Used only for non-leaf nodes
    public bool IsLeaf;


    public Node(bool isleaf){
        Parent = null; 
        Next = null;
        Keys = new List<int>();
        ValuePairs = new Dictionary<int, Row>();
        Children = new List<Node>();
        IsLeaf = isleaf;
    }

    public void InsertAtLeaf(Row value)
    {   int key = value.id;
        if (Keys.Count == 0)
        {
            Keys.Add(key);
            ValuePairs[key] = value;
        }
        else
        {
            // Find the correct position to insert the new key
            int insertPosition = Keys.BinarySearch(key);
            if (insertPosition < 0)
            {
                insertPosition = ~insertPosition; // Bitwise complement of the return 
            }                                     //value because it will give us last greater indes
            Keys.Insert(insertPosition, key);
            ValuePairs[key] = value;    
        }
    }

    public Node Copy(){
        Node copynode = new Node(this.IsLeaf);
        copynode.Keys = new List<int>(Keys);
        copynode.Children = new List<Node>(Children);
        copynode.ValuePairs = new Dictionary<int, Row>(ValuePairs);
        return copynode;
    }

}


class BPlusTree{
    Node Root;
    public static int Depth = 3;


    public bool Insert(Row row){
        int key = row.id;
        if (Root == null)
        {
            Root = new Node(true);
            Root.Keys.Add(key);
            Root.ValuePairs[key] = row;
            return true;
        }

        Node leafNode = GetLeafNode(key);

        if(leafNode.Keys.Contains(key)) return false;
        leafNode.InsertAtLeaf(row);

        if(leafNode.Keys.Count > Depth){
            Node newLeafNode = SplitLeafNode(leafNode);
             InsertInParent(leafNode, newLeafNode.Keys[0], newLeafNode);
        }
        return true;
    }


    public bool Update(Row newRow){
        int key = newRow.id;
        if(Root == null){
            return false;
        } 

        Node leafNode = GetLeafNode(key);
        if(leafNode.Keys.Contains(key)){
            leafNode.ValuePairs[key] = newRow;
            return true;
        }
        return false;
    }

    public  bool Delete(int key){
        if(Root == null){
            return false;
        } 

        Node leafNode = GetLeafNode(key);
        if(leafNode.Keys.Contains(key)){
            leafNode.Keys.Remove(key);
            leafNode.ValuePairs.Remove(key);
            return true;
        }
        return false;
    }

    public List<Row> Select(){
        List<Row> allRows = new List<Row>();
        if(Root == null){
            return allRows;
        }

        Node LeftMostNode = Root;
        while(!LeftMostNode.IsLeaf){
            LeftMostNode = LeftMostNode.Children[0];
        }

        while(LeftMostNode != null){
            foreach(var Key in LeftMostNode.Keys){
                allRows.Add(LeftMostNode.ValuePairs[Key]);
            }
            LeftMostNode = LeftMostNode.Next;
        }

        return allRows;

    }
    public List<Row> Select(int[] ids){
        HashSet<int> idSet = new HashSet<int>(ids);
        List<Row> selectedRows = new List<Row>();
        if(Root == null){
            return selectedRows;
        }

        Node LeftMostNode = Root;
        while(!LeftMostNode.IsLeaf){
            LeftMostNode = LeftMostNode.Children[0];
        }

        while(LeftMostNode != null){
            foreach(var row in LeftMostNode.ValuePairs){
                if(idSet.Contains(row.Key)){
                    selectedRows.Add(row.Value);
                }
            }
            LeftMostNode = LeftMostNode.Next;
        }
        return selectedRows;
    }



    private Node GetLeafNode(int key)
    {
        Node currentNode = Root;
        while (!currentNode.IsLeaf)
        {
            int i = 0;
            while (i < currentNode.Keys.Count && key > currentNode.Keys[i])
            {
                i++;
            }
            currentNode = currentNode.Children[i];
        }
        return currentNode;
    }

    private Node SplitLeafNode(Node leafNode)
    {
        Node newLeafNode = leafNode.Copy();
        int mid = leafNode.Keys.Count  / 2;

        newLeafNode.Keys = leafNode.Keys.GetRange(mid, leafNode.Keys.Count - mid);
        leafNode.Keys.RemoveRange(mid, leafNode.Keys.Count - mid);

        newLeafNode.ValuePairs.Clear();
        foreach(int key in newLeafNode.Keys){
            newLeafNode.ValuePairs[key] = leafNode.ValuePairs[key];
            leafNode.ValuePairs.Remove(key);
        } 

        newLeafNode.Next = leafNode.Next;
        leafNode.Next = newLeafNode;
        newLeafNode.Parent = leafNode.Parent;
        return newLeafNode;
    }


    private Node SplitInternalNode(Node node)
    {
        Node newNode = node.Copy();
        int mid = node.Keys.Count  / 2;

        newNode.Keys = node.Keys.GetRange(mid +1, node.Keys.Count - mid - 1);
        node.Keys.RemoveRange(mid + 1, node.Keys.Count - mid - 1);

        newNode.Children = node.Children.GetRange(mid + 1, node.Children.Count - mid - 1);
        node.Children.RemoveRange(mid + 1, node.Children.Count - mid - 1);

        foreach (Node child in newNode.Children)
        {
            child.Parent = newNode;
        }

        newNode.Parent = node.Parent;
        return newNode;
    }
 

    private void InsertInParent(Node leftNode, int key, Node rightNode)
    {
        if (leftNode == Root)
        {
            Node newRoot = new Node(false);
            newRoot.Keys.Add(key);
            newRoot.Children.Add(leftNode);
            newRoot.Children.Add(rightNode);
            Root = newRoot;
            leftNode.Parent = newRoot;
            rightNode.Parent = newRoot;
            return;
        }

        Node parent = leftNode.Parent;
        int insertIndex = parent.Children.IndexOf(leftNode) + 1;
        
        parent.Keys.Insert(insertIndex - 1, key);
        parent.Children.Insert(insertIndex, rightNode);
        rightNode.Parent = parent;

        if (parent.Keys.Count > Depth)
        {
            Node newParent = SplitInternalNode(parent);
            InsertInParent(parent, newParent.Keys[0], newParent); // rebalnce again if size increases by depht
        }
    }




    private Node GetRoot(){
        if(Root == null){
            return new Node(true);
        }
        return Root;
    }
    
}