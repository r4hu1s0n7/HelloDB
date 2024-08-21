internal class Program
{
    private static void Main(string[] args)
    {
        string input = null;
        while(true)
        {
            Console.Write("command > ");
            input = Console.ReadLine();

            if(string.IsNullOrEmpty(input)) continue;

            if(input[0] == '.'){ // meta commands will be followed by '.'
                MetaCommandResult result = MetaCommand.ProcessMetaCommand(input);
                switch(result){
                    case MetaCommandResult.SUCCESS:
                        continue;
                    case MetaCommandResult.COMMAND_NOT_FOUND:
                        Console.WriteLine("Incorrect or Unrecognised Command");
                        continue;
                }
            }else{ // query statement
                QueryStatement queryStatement = new QueryStatement();
                PrepareStatementResult prepareStatement = PrepareStatement.PrepareQueryStatement(input, out queryStatement);
                switch(prepareStatement){
                    case PrepareStatementResult.SUCCESS:
                        break;
                    case PrepareStatementResult.SYNTAX_ERROR:
                        Console.WriteLine("Parameters Mismatch");
                        continue;
                    case PrepareStatementResult.QUERY_UKNOWN:
                        Console.WriteLine("Unrecognised Command");
                        continue;
                    case PrepareStatementResult.NEGATIVE_INDEX:
                        Console.WriteLine("Negative Index Id");
                        continue;
                }

                ExecuteResult executeResult = QueryExecution.Execute(queryStatement, input);
                switch(executeResult){
                    case ExecuteResult.EXECUTE_ERROR:
                        Console.WriteLine("Command could not be executed");
                        break;
                    case ExecuteResult.RECORD_NOT_FOUND:
                        Console.WriteLine("No records found");
                        break;
                    case ExecuteResult.RECORD_EXIST:
                        Console.WriteLine("Duplicate record id");
                        break;
                    case ExecuteResult.EXECUTE_SUCCESS :
                        Console.WriteLine("Executed");
                        break;
                }
            }
        }
    }

  

}