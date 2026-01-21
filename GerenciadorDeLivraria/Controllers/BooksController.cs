using GerenciadorDeLivraria.Communication.Requests;
using GerenciadorDeLivraria.Communication.Responses;
using GerenciadorDeLivraria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeLivraria.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private static readonly List<Book> _books = new();

    /// <summary>
    /// Cria um novo livro.
    /// </summary>
    /// <remarks>
    /// Regras de negócio:
    /// - título e autor não podem estar duplicados
    /// - gênero deve ser um valor válido do enum
    /// - preço não pode ser negativo
    /// - estoque não pode ser negativo
    ///
    /// Exemplo de requisição:
    /// {
    ///   "title": "O Pequeno Príncipe",
    ///   "author": "Antoine de Saint-Exupéry",
    ///   "genre": "Ficção",
    ///   "price": 15.00,
    ///   "stock": 10
    /// }
    /// </remarks>
    /// <response code="201">Livro criado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="409">Já existe um livro com esse título e autor</response>
    [HttpPost]
    [ProducesResponseType(typeof(ResponseCreateBookJson), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult Create([FromBody] RequestCreateBookJson request)
    {
        var alreadyExists = _books.Any(b =>
        b.Title.Equals(request.Title, StringComparison.OrdinalIgnoreCase) &&
        b.Author.Equals(request.Author, StringComparison.OrdinalIgnoreCase)
        );

        if (alreadyExists)
        {
            return Conflict("Já existe um livro com esse título e autor.");
        }

        var book = new Book
        {
            Title = request.Title,
            Author = request.Author,
            Genre = request.Genre,
            Price = request.Price,
            Stock = request.Stock
        };

        _books.Add(book);

        var response = new ResponseCreateBookJson
        {
            Id = book.Id
        };

        return StatusCode(StatusCodes.Status201Created, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Book>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetAll(
        [FromQuery] string? title,
        [FromQuery] string? author,
        [FromQuery] Genre? genre)
    {
        var query = _books.AsQueryable();

        if (!string.IsNullOrWhiteSpace(title))
        {
            query = query.Where(b =>
                b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(author))
        {
            query = query.Where(b =>
                b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
        }

        if (genre.HasValue)
        {
            query = query.Where(b => b.Genre == genre.Value);
        }

        return Ok(query.ToList());
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);

        if (book is null)
        {
            return NotFound("Livro não encontrado.");
        }

        return Ok(book);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Update([FromRoute] Guid id, [FromBody] RequestUpdateBookJson request)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);

        if (book is null)
            return NotFound("Livro não encontrado.");

        var alreadyExists = _books.Any(b =>
            b.Id != id &&
            b.Title.Equals(request.Title, StringComparison.OrdinalIgnoreCase) &&
            b.Author.Equals(request.Author, StringComparison.OrdinalIgnoreCase)
        );

        if (alreadyExists)
        {
            return Conflict("Já existe um livro com esse título e autor.");
        }

        book.Title = request.Title;
        book.Author = request.Author;
        book.Genre = request.Genre;
        book.Price = request.Price;
        book.Stock = request.Stock;
        book.UpdatedAt = DateTime.UtcNow;

        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete([FromRoute] Guid id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);

        if (book is null)
            return NotFound("Livro não encontrado");

        _books.Remove(book);
        return NoContent();
    }

    }
