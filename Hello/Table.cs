public class Table{
     
    public const int MAX_PAGES = 5;
    public const int PAGE_SIZE = 5;
    public const int MAX_ROWS = MAX_PAGES * PAGE_SIZE;
    public const int ROWS_PER_TABLE = MAX_PAGES / PAGE_SIZE;
    private static Row[][] Rows = new Row[MAX_PAGES][ ];
    private static int currentPage;
    private static int curremtRow;


    public static ExecuteResult Insert(Row row){
        if(currentPage >= MAX_PAGES){
            Console.WriteLine("Memory Full");
            return ExecuteResult.TABLE_FULL;
        }
        if(curremtRow > ROWS_PER_TABLE){
            currentPage += 1;
            curremtRow = 0;
        }
        if(curremtRow == 0){
            Rows[currentPage] = new Row[PAGE_SIZE];
        }
        Rows[currentPage][curremtRow] = row;
        curremtRow += 1;
        return ExecuteResult.EXECUTE_SUCCESS;
    }

    public static ExecuteResult Select(){
        int k = 0;
        for(int i = 0; i<= currentPage; i++){
            for(int j = 0; j<= curremtRow; j++){
                if((curremtRow == 0 && currentPage == 0) || Rows[i][j] == null) break;
                Console.WriteLine(Row.DeserializeRow(Rows[i][j]));
            }
        }


        
        return ExecuteResult.EXECUTE_SUCCESS;
    }

}