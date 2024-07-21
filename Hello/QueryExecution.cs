public class QueryExecution{

    public static ExecuteResult Execute(QueryStatement queryStatement, string input ){
        switch(queryStatement.queryType){

            case(QueryType.INSERT):
                Row record = Row.SerializeRow(input);
                return Table.Insert(record);
                
            case(QueryType.SELECT):
                return Table.Select();
            default:
                Console.WriteLine("No matching Query");
                return ExecuteResult.EXECUTE_ERROR;
        }
    }

}