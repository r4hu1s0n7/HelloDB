enum MetaCommandResult {
    SUCCESS,
    COMMAND_ERROR,
    RESULT
}

enum PrepareStatementResult{
    SUCCESS,
    QUERY_ERROR,
    RESULT
}

public enum QueryType{
    SELECT,
    INSERT,
    UPDATE,
    DELETE
}

public class QueryStatement{
    public QueryType queryType; 
}

