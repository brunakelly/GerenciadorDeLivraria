using GerenciadorDeLivraria.Models;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeLivraria.Communication.Requests;

public class RequestCreateBookJson
{
    [Required]
    [StringLength(120, MinimumLength = 2)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(120, MinimumLength = 2)]
    public string Author { get; set; } = string.Empty;

    [Required]
    public Genre Genre { get; set; } 

    [Range(0.1, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(1, int.MaxValue)]
    public int Stock { get; set; }
}
