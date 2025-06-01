namespace Literateca.Communications.Requests;

public class UpdateLivroRequest
{
    public string Titulo { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public float Preco { get; set; }
    public int Quantidade { get; set; }
}