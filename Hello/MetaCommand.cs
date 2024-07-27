class MetaCommand{
    
    public static MetaCommandResult ProcessMetaCommand(string input){
        string command = input.Split(" ")[0];
        switch(command){
            case ".exit":
                Table.Close();
                exit(0);
                return MetaCommandResult.SUCCESS;
            case ".db":
                Table.Open(input);
                return MetaCommandResult.SUCCESS;
            default :               
                return MetaCommandResult.COMMAND_ERROR;
        }
    }

    static void exit(int statusCode){
        System.Environment.Exit(statusCode);
    }
}