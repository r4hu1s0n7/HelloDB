class PrepareStatement{
    public static PrepareStatementResult PrepareQueryStatement(string input, out QueryStatement queryStatement){
            
        queryStatement = new QueryStatement();
        if(input.Substring(0,6).ToLower() == "insert"){
            queryStatement.queryType = QueryType.INSERT;
            return PrepareStatementResult.SUCCESS;
        }else if(input.Substring(0,6).ToLower() == "select"){
            queryStatement.queryType = QueryType.SELECT;
            return PrepareStatementResult.SUCCESS;
        }                 
        return PrepareStatementResult.QUERY_ERROR;
        
    }
}