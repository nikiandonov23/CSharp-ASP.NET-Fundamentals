using GameZone.Models.Game;

namespace GameZone.Services.Interfaces;

public interface IGameService
{
 //   //Index GetAllModels
    public Task<IEnumerable<AllGamesViewModel>> GetAllGamesAsync(string? currentUserId);
 
 //   //Add 

    public Task<bool> AddGameAsync(string userId, AddGameViewModel inputModel);
 


 //   //MyZone
    public Task<IEnumerable<MyZoneViewModel>?> GetMyZoneAsync(string userId);
 
 //   //AddToMyZone
    public Task<bool> AddToMyZoneAsync(string userId, int gameId);


    //RemoveFromMyZone
    public Task<bool> RemoveGameFromMyZoneAsync(string userId, int gameId);
 //
 //
 //   //GetDetails
    public Task<DetailsGameViewModel?> GetGameDetailsAsync(int gameId, string? userId);
 
    //GetDeleteViewModel
    public Task<DeleteGameViewModel?> GetDeleteGameViewModelAsync(int gameId, string? userId);
 //
 //   //Delete
    public Task<bool> DeleteGameAsync(DeleteGameViewModel inputModel, string? userId);
 //
 //   //Edit  .Tuka pravq Edit s prazen dropdown model
    public Task<EditGameViewModel?> GetEditGenreViewModelAsync(string userId, int gameId);
 //
 //   //Edit(SAVE)
 //   //tuka ve4e persistvam promenite SAVE
 //   //No predi tova Nov InterfaceDropDown+ServiceDropDown za DROPDOWN podmodela
 //   //Nov [HttpGet] koito zarejda DropDownModela
 //   //public async Task<IActionResult> Create()
 //   //i vrushta View(GLAVNIQ MODE) sus zareden parametur PODMODELA.Koito se vika on podmodel servisa
    public Task<bool> EditGameAsync(EditGameViewModel inputModel, string userId);
}