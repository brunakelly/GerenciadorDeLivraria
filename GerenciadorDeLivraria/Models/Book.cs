using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeLivraria.Models;

public class Book
{
    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(120, MinimumLength = 2)]
    public string Title { get; set; }

    [Required]
    [StringLength(120, MinimumLength = 2)]
    public string Author { get; set; }

    [Required]
    public Genre Genre { get; set; }

    [Range(0.1, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(1, int.MaxValue)]
    public int Stock { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
}
