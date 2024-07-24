using System.Reflection.Metadata;

enum MetaCommandResult {
    SUCCESS,
    COMMAND_ERROR,
    RESULT
}

enum PrepareStatementResult{
    SUCCESS,
    QUERY_ERROR,
    SYNTAX_ERROR,
    NEGATIVE_INDEX,
    RESULT
}

public enum QueryType{
    SELECT,
    INSERT,
    UPDATE,
    DELETE
}

public enum ExecuteResult{
    EXECUTE_SUCCESS,
    EXECUTE_ERROR,
    TABLE_FULL
}

public class QueryStatement{
    public QueryType queryType; 
    public Row row; // insert only
}






