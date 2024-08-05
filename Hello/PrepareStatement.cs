class PrepareStatement{
    public static PrepareStatementResult PrepareQueryStatement(string input, out QueryStatement queryStatement){
        input = input.Trim();
        queryStatement = new QueryStatement();
        if(input.ToLower().Contains("insert") && input.Substring(0,6).ToLower() == "insert"){
            queryStatement.queryType = QueryType.INSERT;
            string[] inputValues = input.Split(' ');

            if(inputValues.Count() != 4){ // validating default constraint
                return PrepareStatementResult.SYNTAX_ERROR;
            }
            return ValidateInsertQueryConstraints(input);

        }else if(input.ToLower().Contains("update") && input.Substring(0,6).ToLower() == "update"){
            queryStatement.queryType = QueryType.UPDATE;
            string[] inputValues = input.Split(' ');

            if(inputValues.Count() != 4){ //  same like insert
                return PrepareStatementResult.SYNTAX_ERROR;
            }
            return ValidateInsertQueryConstraints(input); 
        }
        else if(input.ToLower().Contains("select")){
            if(input.ToLower() == "select"){
                queryStatement.queryType = QueryType.SELECT_ALL;
                return PrepareStatementResult.SUCCESS;
            }else{
                queryStatement.queryType = QueryType.SELECT_RECORDS;
                return PrepareStatementResult.SUCCESS;
            }  
        }else if(input.ToLower().Contains("delete") && input.Substring(0,6).ToLower() == "delete"){
            queryStatement.queryType = QueryType.DELETE;
            return ValidateDeleteQueryConstraints(input);
        }


        return PrepareStatementResult.QUERY_UKNOWN;
        
    }

    public static PrepareStatementResult ValidateInsertQueryConstraints(string input){
        string[] param = input.Split(' ');

        if(Convert.ToInt32(param[1]) <0) return PrepareStatementResult.NEGATIVE_INDEX;
        return PrepareStatementResult.SUCCESS;
    }

    public static PrepareStatementResult ValidateDeleteQueryConstraints(string input){
        string[] param = input.Split(' ');
        if(param.Count() != 2) return PrepareStatementResult.QUERY_ERROR;
        if(Convert.ToInt32(param[1]) <0) return PrepareStatementResult.NEGATIVE_INDEX;
        return PrepareStatementResult.SUCCESS;
    }


}