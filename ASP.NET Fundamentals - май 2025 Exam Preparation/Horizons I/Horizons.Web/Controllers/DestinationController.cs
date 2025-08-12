using Horizons.Services.Core.Contracts;
using Horizons.Web.ViewModels.Destination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Collections;
using System.Globalization;
using Horizons.Data.Models;
using Horizons.GCommon;
using Microsoft.IdentityModel.Tokens;
using static Horizons.GCommon.ValidationConstants;

namespace Horizons.Web.Controllers
{
    public class DestinationController(IDestinationService destinationService, ITerrainService terrainService)
        : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {


            string? userId = this.GetUserId();


            IEnumerable<DestinationIndexViewModel> allDestinations = await
                destinationService.GetDestinationsIndexAsync(userId);

            return View(allDestinations);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var userId = GetUserId();

            if (string.IsNullOrEmpty(userId)) // Ensure userId is not null or empty
            {
                return RedirectToAction("Index", "Destination");
            }

            var destinationVm = await destinationService.GetDestinationByIdAsync(id, userId);
            if (destinationVm == null)
            {
                return RedirectToAction("Index", "Destination");
            }

            return View(destinationVm);

        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {

            AddDestinationInputModel inputModel = new AddDestinationInputModel()
            {
                PublishedOn = DateTime.UtcNow.ToString(DateFormattt, CultureInfo.InvariantCulture),
                Terrains = await terrainService.GetTerrainsDropDownModelAsync(),


            };


            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDestinationInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Add", "Destination");
            }

            var userId = GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Index", "Destination");
            }

            await destinationService.AddDestinationAsync(userId, inputModel);


            return RedirectToAction("Index", "Destination");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            var userId = GetUserId();
            if (userId != null)
            {
                EditDestinationViewModel editDestination = new EditDestinationViewModel()
                {
                    Terrains = await terrainService.GetTerrainsDropDownModelAsync(),
                    PublishedOn = DateTime.UtcNow.ToString(DateFormattt, CultureInfo.InvariantCulture)

                };
                return View(editDestination);
            }

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditDestinationViewModel inputModel)
        {
            return View();
        }
    }
}
