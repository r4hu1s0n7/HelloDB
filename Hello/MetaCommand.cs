class MetaCommand{
    
    public static MetaCommandResult ProcessMetaCommand(string input){
        string command = input.Split(" ")[0];
        switch(command){
            case ".exit":
                Table.Close();
                Exit(0);
                return MetaCommandResult.SUCCESS;
            case ".db":
                Table.Open(input);
                return MetaCommandResult.SUCCESS;
            case ".listdb":
                PrintDBList();
                return MetaCommandResult.SUCCESS;
            default :               
                return MetaCommandResult.COMMAND_NOT_FOUND;
        }
    }

    static void Exit(int statusCode){
        System.Environment.Exit(statusCode);
    }

    static void PrintDBList(){
        List<(string,string)> dbinfo = Utils.GetAllDatabasesList();

        if(dbinfo.Count == 0){
            Console.WriteLine("No database file found");
            return;
        }

        foreach(var db in dbinfo){
            Console.WriteLine($"{db.Item1,-20} {db.Item2,10}");        
        }
    }
}