class PrepareStatement{
    public static PrepareStatementResult PrepareQueryStatement(string input, out QueryStatement queryStatement){
            
        queryStatement = new QueryStatement();
        if(input.Substring(0,6).ToLower() == "insert"){
            queryStatement.queryType = QueryType.INSERT;
            string[] inputValues = input.Split(' ');

            if(inputValues.Count() != 4){ // validating default constraint
                return PrepareStatementResult.SYNTAX_ERROR;
            }

            return ValidateInsertQueryConstraints(input);

        }else if(input.Substring(0,6).ToLower() == "select"){
            queryStatement.queryType = QueryType.SELECT;
            return PrepareStatementResult.SUCCESS;
        }                 
        return PrepareStatementResult.QUERY_ERROR;
        
    }

    public static PrepareStatementResult ValidateInsertQueryConstraints(string input){
        string[] param = input.Split(' ');

        if(Convert.ToInt32(param[1]) <0) return PrepareStatementResult.NEGATIVE_INDEX;
        return PrepareStatementResult.SUCCESS;
    }


}