using Literateca.Communications.Requests;
using Literateca.Communications.Responses;
using Literateca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Literateca.Controllers;

public class LivroController : BaseController
{
    private static readonly List<Livro> _livros = new List<Livro>();
    private static int _proximoId = 0;
    
    // Adiciona o livro
    [HttpPost]
    [ProducesResponseType(typeof(CreatedLivroResponse), StatusCodes.Status201Created)]
    public IActionResult CriaLivro(
        [FromBody]
        CreateLivroRequest request)
    {
        int novoId = _livros.Count + 1;

        var novoLivro = new Livro() 
        {
            Id = novoId,
            Titulo = request.Titulo,
            Autor = request.Autor,
            Genero = request.Genero,
            Preco = request.Preco,
            Quantidade = request.Quantidade,
        };

        _livros.Add(novoLivro);

        var response = new CreatedLivroResponse()
        {
            Id = novoLivro.Id,
            Titulo = novoLivro.Titulo,
            Autor = novoLivro.Autor,
        };
        
        return Created(string.Empty, response);
    }

    // Busca todos os livros
    [ProducesResponseType(typeof(Livro), StatusCodes.Status200OK)]
    [HttpGet]
    public IActionResult ListaLivros()
    {
        return Ok(_livros);
    }
    
    // Busca o livro pelo o Id
    [ProducesResponseType(typeof(Livro), StatusCodes.Status200OK)]
    [HttpGet("{id}")]
    public IActionResult ListaLivrosPorId(
        [FromRoute]int id)
    {
        var livroAchado = _livros.FirstOrDefault(l => l.Id == id);
        
        return Ok(livroAchado);
    }

    // Atualiza o livro
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CreatedLivroResponse), StatusCodes.Status200OK)]
    public IActionResult AtualizaLivro(
        [FromRoute] int id,
        [FromBody] UpdateLivroRequest request)
    {
        var livroParaAtualizar = _livros.Find(livro => livro.Id == id);

        if (livroParaAtualizar != null)
        {
            livroParaAtualizar.Titulo = request.Titulo;
            livroParaAtualizar.Autor = request.Autor;
            livroParaAtualizar.Genero = request.Genero;
            livroParaAtualizar.Preco = request.Preco;
            livroParaAtualizar.Quantidade = request.Quantidade;
        }

        var response = new CreatedLivroResponse()
        {
            Id = livroParaAtualizar.Id,
            Titulo = livroParaAtualizar.Titulo,
            Autor = livroParaAtualizar.Autor,
        };

        return Ok(response);
    }

    // Exclui o livro
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeletaLivro(
        [FromRoute] int id)
    {
        var livroAchado = _livros.FirstOrDefault(l => l.Id == id);
        
        _livros.Remove(livroAchado);
        
        return NoContent();
    }
}

