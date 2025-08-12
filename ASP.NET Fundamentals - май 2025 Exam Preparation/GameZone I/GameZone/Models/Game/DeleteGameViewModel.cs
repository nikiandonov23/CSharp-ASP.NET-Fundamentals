using System.ComponentModel.DataAnnotations;

namespace GameZone.Models.Game;
using static GameZone.GCommon.ValidationConstants;
public class DeleteGameViewModel
{

    [Required]
    public int Id { get; set; }


    [Required]
    [MinLength(GameTitleMinLength)]
    [MaxLength(GameTitleMaxLength)]
    public string Title { get; set; } = null!;

    [Required]
    public string Publisher { get; set; } = null!;

    public string? ImageUrl { get; set; }



}