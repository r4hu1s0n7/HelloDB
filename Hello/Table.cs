



public class Table{
     
    public const int MAX_PAGES = 5;
    public const int PAGE_SIZE = 5;
    public const int MAX_ROWS = MAX_PAGES * PAGE_SIZE;
    
    private static Row[][] Rows = new Row[MAX_PAGES][ ];
    private static int currentPage;
    private static int currentRow;

    private static BPlusTree bPlus = new BPlusTree();

    private static string currentFilename = null;

    public static ExecuteResult Insert(Row row){
        bool status = bPlus.Insert(row);
        return status ? ExecuteResult.EXECUTE_SUCCESS : ExecuteResult.EXECUTE_ERROR;
    }

    public static ExecuteResult SelectAll(){
        foreach(var record in bPlus.Select()){
            Console.WriteLine(Row.SerializeRow(record));
        }
        return ExecuteResult.EXECUTE_SUCCESS;
    }

    public static ExecuteResult SelectRecords(int[] keys){
        foreach(var record in bPlus.Select(keys)){
            Console.WriteLine(Row.SerializeRow(record));
        }
        return ExecuteResult.EXECUTE_SUCCESS;
    }

    public static ExecuteResult Update(Row updateRecord)
    {
        bool status = bPlus.Update(updateRecord);
        return status ? ExecuteResult.EXECUTE_SUCCESS : ExecuteResult.EXECUTE_ERROR;    
    }

    public static ExecuteResult Delete(int key)
    {
        bool status = bPlus.Delete(key);
        return status ? ExecuteResult.EXECUTE_SUCCESS : ExecuteResult.RECORD_NOT_FOUND;     }

    internal static void Open(string input)
    {
        if(Rows[0] != null) {Console.WriteLine("Exit current Database file."); return; }
        if(input.Split(" ").Count() != 2){Console.WriteLine("Database Filename missing");return;}
        currentFilename = input.Split(" ")[1];
        bPlus = Pages.LoadPage(currentFilename); // loading tree
    
    }

    internal static void Close(){
        if(string.IsNullOrEmpty(currentFilename)){
            Console.WriteLine("Database file missing.\n Enter filename to Save and Exit/(no) for exit.");
            string output = Console.ReadLine();
            if(output.ToLower().Trim() == "no") return;
            currentFilename = output.Trim();
        }
        Pages.SaveFile(bPlus,currentFilename);
    }

    
}