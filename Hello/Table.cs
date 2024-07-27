
public class Table{
     
    public const int MAX_PAGES = 5;
    public const int PAGE_SIZE = 5;
    public const int MAX_ROWS = MAX_PAGES * PAGE_SIZE;
    public const int ROWS_PER_TABLE = MAX_PAGES / PAGE_SIZE;
    private static Row[][] Rows = new Row[MAX_PAGES][ ];
    private static int currentPage;
    private static int currentRow;

    private static string currentFilename = null;

    public static ExecuteResult Insert(Row row){
        if(currentPage >= MAX_PAGES){
            Console.WriteLine("Memory Full");
            return ExecuteResult.TABLE_FULL;
        }
        if(currentRow > ROWS_PER_TABLE){
            currentPage += 1;
            currentRow = 0;
        }
        if(currentRow == 0){
            Rows[currentPage] = new Row[PAGE_SIZE];
        }
        Rows[currentPage][currentRow] = row;
        currentRow += 1;
        return ExecuteResult.EXECUTE_SUCCESS;
    }

    public static ExecuteResult Select(){
        int k = 0;
        for(int i = 0; i<= currentPage; i++){
            for(int j = 0; j<= currentRow; j++){
                if((currentRow == 0 && currentPage == 0) || Rows[i][j] == null) break;
                Console.WriteLine(Row.SerializeRow(Rows[i][j]));
            }
        }


        
        return ExecuteResult.EXECUTE_SUCCESS;
    }

    internal static void Open(string input)
    {

        if(Rows[0] != null) {Console.WriteLine("Exit current Database file."); return; }
        if(input.Split(" ").Count() != 2){Console.WriteLine("Database Filename missing");return;}
        currentFilename = input.Split(" ")[1];
        Rows = Pages.LoadPage(currentFilename);
        // reseting current row pointer
        for(int i  = 0; i < MAX_PAGES; i++){
            for(int j = 0; j < ROWS_PER_TABLE; j++){
                if(Rows[currentPage] == null || Rows[currentPage][currentRow] == null)break;
                currentRow++;
                if(currentRow == 5){ currentRow = 0; currentPage++; }

            }
        }

    }

    internal static void Close(){
        
        if(string.IsNullOrEmpty(currentFilename)){
            Console.WriteLine("Database file missing.\n Enter filename to Save and Exit/(no) for exit.");
            string output = Console.ReadLine();
            if(output.ToLower().Trim() == "no") return;
            currentFilename = output.Trim();
        }
        Pages.SaveFile(Rows,currentFilename);
    }
}