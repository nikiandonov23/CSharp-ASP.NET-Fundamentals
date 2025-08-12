using System.ComponentModel.DataAnnotations;

namespace GameZone.Models.Game;

public class MyZoneViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public string Genre { get; set; } = null!;


    [Required]
    [DataType(DataType.Date)]
    public DateTime ReleasedOn { get; set; }

    public bool IsDeleted { get; set; } = false;
    public string Publisher { get; set; } = null!;
}