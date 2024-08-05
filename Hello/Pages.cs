
using System.IO.Enumeration;

class Pages
{

    public const int MAX_PAGES = 5;
    
    internal static BPlusTree LoadPage(string input)
    {
        string filepath = SetFilePath(input);        
        return LoadFile(filepath);
    }

    internal static string SetFilePath(string filename){ // Make or Get file
        string currendDirectory = Directory.GetCurrentDirectory();
        string filepath = Path.Combine(currendDirectory, "dbfiles",$"{filename}.csv");
        if(!Directory.Exists(Path.GetDirectoryName(filepath))) Directory.CreateDirectory(Path.GetDirectoryName(filepath));
        if(!File.Exists(filepath)) File.Create(filepath).Close();

        return filepath;
    }

    internal static BPlusTree LoadFile(string filepath){ // improve approach of bulk loading
        BPlusTree tree = new BPlusTree();
        
        if(string.IsNullOrEmpty(filepath)){ Console.WriteLine("Database file Missing."); return tree; }
        using(StreamReader reader = new StreamReader(filepath)){
            while(!reader.EndOfStream){
                string line = reader.ReadLine();
                line = line.Replace(",","");
                var row = Row.DeserializeRow(line);
                tree.Insert(row);
            }
        } 

        return tree;
    }

    internal static void SaveFile(BPlusTree tree, string filename){

        string filepath = SetFilePath(filename);
        if(string.IsNullOrEmpty(filepath)){ 
            Console.WriteLine("Database file Missing. ");
            return;
         }

        using(StreamWriter writer = new StreamWriter(filepath)){ // improve saving too
        foreach(var row in tree.Select()){
            string rowline = Row.SerializeRow(row);
            writer.WriteLine(rowline);
            }
        }
    } 

}
