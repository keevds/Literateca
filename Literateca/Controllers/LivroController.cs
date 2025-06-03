using Literateca.Communications.Requests;
using Literateca.Communications.Responses;
using Literateca.Data;
using Literateca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Literateca.Controllers;

public class LivroController : BaseController
{
    // Adiciona o livro
    [HttpPost]
    [ProducesResponseType(typeof(CreatedLivroResponse), StatusCodes.Status201Created)]
    public async Task <IActionResult> CriaLivro(
        [FromBody] CreateLivroRequest request, [FromServices] AppDbContext context)
    {

        var livro = new Livro()
        {
            Titulo = request.Titulo,
            Autor = request.Autor,
            Genero = request.Genero,
            Preco = request.Preco,
            Quantidade = request.Quantidade,
        };
        
        await context.Livros.AddAsync(livro);
        await context.SaveChangesAsync();

        var response = new CreatedLivroResponse()
        {
            Id = livro.Id,
            Titulo = livro.Titulo,
        };
        
        return Created(string.Empty, response);
    }

    // Busca todos os livros
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task <IActionResult> ListaLivros(
        [FromServices] AppDbContext context)
    {
        var livros = await context.Livros.ToListAsync();

        return Ok(livros);
    }
    
    // Busca o livro pelo o Id
    [ProducesResponseType(typeof(Livro), StatusCodes.Status200OK)]
    [HttpGet("{id}")]
    public async Task <IActionResult> ListaLivrosPorId(
        [FromRoute]int id, [FromServices] AppDbContext context)
    {
        var livro = await context.Livros.FirstOrDefaultAsync(x => x.Id == id);
        
        return Ok(livro);
    }

    // Atualiza o livro
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CreatedLivroResponse), StatusCodes.Status200OK)]
    public async Task <IActionResult> AtualizaLivro(
        [FromRoute] int id,
        [FromBody] UpdateLivroRequest request,
        [FromServices] AppDbContext context)
    {
        var livroParaAtualizar = await context.Livros.FirstOrDefaultAsync(x => x.Id == id);

        if (livroParaAtualizar != null)
        {
            livroParaAtualizar.Titulo = request.Titulo;
            livroParaAtualizar.Autor = request.Autor;
            livroParaAtualizar.Genero = request.Genero;
            livroParaAtualizar.Preco = request.Preco;
            livroParaAtualizar.Quantidade = request.Quantidade;
            
            context.Livros.Update(livroParaAtualizar);
            await context.SaveChangesAsync();
        }
    
        return Ok();
    }
    
    // Exclui o livro
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task <IActionResult> DeletaLivro(
        [FromRoute] int id,
        [FromServices] AppDbContext context)
    {
        var livro = await context.Livros.FirstOrDefaultAsync(x => x.Id == id);

        context.Livros.Remove(livro);
        await context.SaveChangesAsync();
        
        return NoContent();
    }
}

