using System.ComponentModel.DataAnnotations;

namespace GameZone.Models.Game;

public class AllGamesViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public string Genre { get; set; } = null!;


    [DataType(DataType.Date)]
    public DateTime ReleasedOn { get; set; } // Може и DateTime, но в изгледа е string

    public string Publisher { get; set; } = null!;
}