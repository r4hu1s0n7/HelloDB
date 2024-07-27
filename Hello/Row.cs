public class Row{
    int id;
    string name;
    string email;  


    public static Row DeserializeRow(string input){
        string[] values = input.Trim().Split(" ");
        Row row = new Row();
        row.id = Convert.ToInt32(values[0]);
        row.name = values[1].Trim();
        row.email = values[2].Trim();
        return row;

    }
    public static string SerializeRow(Row row){
        return $"{row.id}, {row.name}, {row.email}"; 
    }
}