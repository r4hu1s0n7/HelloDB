class MetaCommand{
    
    public static MetaCommandResult ProcessMetaCommand(string input){
        switch(input){
            case ".exit":
                exit(0);
                return MetaCommandResult.SUCCESS;
            default :               
                return MetaCommandResult.COMMAND_ERROR;
        }
    }

    static void exit(int statusCode){
        System.Environment.Exit(statusCode);
    }
}