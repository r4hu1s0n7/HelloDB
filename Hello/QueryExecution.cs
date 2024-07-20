public class QueryExecution{

    public static void Execute(QueryStatement queryStatement){
        switch(queryStatement.queryType){

            case(QueryType.INSERT):
                Console.WriteLine("Insert Statement");
                break;
            case(QueryType.SELECT):
                Console.WriteLine("Select Statement");
                break;
        }
    }

}