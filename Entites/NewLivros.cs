namespace API_LIVRARIA.Entites;

public class NewLivros
{
    public int id { get; set; }
    public string name { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;

    public static List<NewLivros> listaDeLivros = new List<NewLivros>
    {


    };
}
