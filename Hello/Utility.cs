class Utils{
    public static List<(string,string)> GetAllDatabasesList(){
        List<(string,string)> dbinfo = new List<(string,string)>();

        string dbDirectory = Path.Combine(Directory.GetCurrentDirectory(),"dbfiles");
        foreach(var file in Directory.GetFiles(dbDirectory)){
            string filename = Path.GetFileName(file);
            filename = filename.Substring(0,filename.IndexOf('.'));
            string creation = File.GetLastAccessTime(file).ToString("HH:mm, dd-MMM-yy");
            dbinfo.Add((filename,creation));
        } 
        return dbinfo;
    }
}