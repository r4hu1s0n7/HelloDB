public class QueryExecution{

    public static ExecuteResult Execute(QueryStatement queryStatement, string input ){
        switch(queryStatement.queryType){

            case(QueryType.INSERT):
                string recordValue = input.Remove(0,"insert".Length);
                Row insertRecord = Row.DeserializeRow(recordValue);
                return Table.Insert(insertRecord);    
                
            case(QueryType.SELECT_ALL):
                return Table.SelectAll();
            case(QueryType.SELECT_RECORDS):
                string selectValue = input.Remove(0,"select".Length).Trim();
                int[] keys = selectValue.Split(' ').Select(i => Convert.ToInt32(i.Trim())).ToArray();
                return Table.SelectRecords(keys);
            case(QueryType.UPDATE):
                string updateValue = input.Remove(0,"update".Length);
                Row updateRecord = Row.DeserializeRow(updateValue);
                return Table.Update(updateRecord);    
            case(QueryType.DELETE):
                int key = Convert.ToInt32(input.Split(' ')[1]);
                return Table.Delete(key);       
            default:
                Console.WriteLine("No matching Query");
                return ExecuteResult.EXECUTE_ERROR;
        }
    }

}