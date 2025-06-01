namespace Literateca.Communications.Responses;

public class CreatedLivroResponse
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
}