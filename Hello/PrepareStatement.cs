class PrepareStatement{
    public static PrepareStatementResult PrepareQueryStatement(string input, out QueryStatement queryStatement){
        input = input.Trim();
        queryStatement = new QueryStatement();
        try{
            if(input.StartsWith("insert", StringComparison.OrdinalIgnoreCase)){
                queryStatement.queryType = QueryType.INSERT;
                string[] inputValues = input.Split(' ');

                if(inputValues.Count() != 4){ // validating default constraint
                    return PrepareStatementResult.SYNTAX_ERROR;
                }
                return ValidateInsertQueryConstraints(input);

            }else if(input.StartsWith("update", StringComparison.OrdinalIgnoreCase)){
                queryStatement.queryType = QueryType.UPDATE;
                string[] inputValues = input.Split(' ');

                if(inputValues.Count() != 4){ //  same like insert
                    return PrepareStatementResult.SYNTAX_ERROR;
                }
                return ValidateInsertQueryConstraints(input); 
            }
            else if(input.StartsWith("select", StringComparison.OrdinalIgnoreCase)){
                if(input.Equals("select", StringComparison.OrdinalIgnoreCase)){
                    queryStatement.queryType = QueryType.SELECT_ALL;
                    return PrepareStatementResult.SUCCESS;
                }else{
                    queryStatement.queryType = QueryType.SELECT_RECORDS;
                    return ValidateSelectQueryConstraints(input);
                }
            }else if(input.StartsWith("delete", StringComparison.OrdinalIgnoreCase)){
                queryStatement.queryType = QueryType.DELETE;
                return ValidateDeleteQueryConstraints(input);
            }
            return PrepareStatementResult.QUERY_UKNOWN;
        }
        catch (Exception)
        {
           return PrepareStatementResult.SYNTAX_ERROR;
        }
    }

    public static PrepareStatementResult ValidateInsertQueryConstraints(string input){
        string[] param = input.Split(' ');

        if(Convert.ToInt32(param[1]) <0) return PrepareStatementResult.NEGATIVE_INDEX;
        return PrepareStatementResult.SUCCESS;
    }

    public static PrepareStatementResult ValidateDeleteQueryConstraints(string input){
        string[] param = input.Split(' ');
        if(param.Count() != 2) return PrepareStatementResult.SYNTAX_ERROR;
        if(Convert.ToInt32(param[1]) <0) return PrepareStatementResult.NEGATIVE_INDEX;
        return PrepareStatementResult.SUCCESS;
    }

    public static PrepareStatementResult ValidateSelectQueryConstraints(string input){
        string[] param = input.Split(' ');
        try{
            for(int i = 1; i < param.Count(); i++ ){
                Convert.ToInt32(param[i].Trim());
            }
        }catch(Exception){
            return PrepareStatementResult.SYNTAX_ERROR;
        }
        return PrepareStatementResult.SUCCESS;
    }
}

