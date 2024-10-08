using System.Reflection.Metadata;

enum MetaCommandResult {
    SUCCESS,
    COMMAND_NOT_FOUND,
    RESULT
}

enum PrepareStatementResult{
    SUCCESS,
    SYNTAX_ERROR,
    NEGATIVE_INDEX,
    QUERY_UKNOWN,
    RESULT
}

public enum QueryType{
    SELECT_ALL,
    SELECT_RECORDS,
    INSERT,
    UPDATE,
    DELETE
}

public enum ExecuteResult{
    EXECUTE_SUCCESS,
    EXECUTE_ERROR,
    TABLE_FULL,
    RECORD_EXIST,
    RECORD_NOT_FOUND
}

public class QueryStatement{
    public QueryType queryType; 
    public Row row; // insert only
}






