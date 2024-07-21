public class Row{
    int id;
    string name;
    string email;  


    public static Row SerializeRow(string input){
        string[] values = input.Split(" ");
        Row row = new Row();
        row.id = Convert.ToInt32(values[1]);
        row.name = values[2];
        row.email = values[3];
        return row;

    }
    public static string DeserializeRow(Row row){
        return $"{row.id}, {row.name}, {row.email}"; 
    }
}