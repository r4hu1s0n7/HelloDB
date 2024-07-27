
using System.IO.Enumeration;

class Pages
{

    public const int MAX_PAGES = 5;
    public const int PAGE_SIZE = 5;
    public const int MAX_ROWS = MAX_PAGES * PAGE_SIZE;
    public const int ROWS_PER_TABLE = MAX_PAGES / PAGE_SIZE;

    
    internal static Row[][] LoadPage(string input)
    {
        string filepath = SetFilePath(input);        
        Row[][] rows = LoadFile(filepath);
        return rows;
    }

    internal static string SetFilePath(string filename){ // Make or Get file
        string currendDirectory = Directory.GetCurrentDirectory();
        string filepath = Path.Combine(currendDirectory, "dbfiles",$"{filename}.csv");
        if(!Directory.Exists(Path.GetDirectoryName(filepath))) Directory.CreateDirectory(Path.GetDirectoryName(filepath));
        if(!File.Exists(filepath)) File.Create(filepath).Close();

        return filepath;
    }

    internal static Row[][] LoadFile(string filepath){
        int currentRow = 0, currentPage = 0;    
        Row[][] Rows = new Row[MAX_PAGES][ ];
        if(string.IsNullOrEmpty(filepath)){ Console.WriteLine("Database file Missing."); return Rows; }

        using(StreamReader reader = new StreamReader(filepath)){
            while(!reader.EndOfStream){
                string line = reader.ReadLine();
                line = line.Replace(",","");
                var row = Row.DeserializeRow(line);
                if(currentRow == 0){ Rows[currentPage] = new Row[PAGE_SIZE];}
                Rows[currentPage][currentRow] = row;
                currentRow++;
                if(currentRow == 5){ currentRow = 0; currentPage++; }
            }
        } 

        return Rows;
    }

    internal static void SaveFile(Row[][] rows, string filename){

        string filepath = SetFilePath(filename);
        if(string.IsNullOrEmpty(filepath)){ 
            Console.WriteLine("Database file Missing. ");
            return;
         }

        using(StreamWriter writer = new StreamWriter(filepath)){
            foreach(var page in rows){
                foreach(var row in page){
                    if(row == null){
                        writer.Close();
                        return;
                    }
                    string rowline = Row.SerializeRow(row);
                    writer.WriteLine(rowline);
                }
            }
        } 

    }
}