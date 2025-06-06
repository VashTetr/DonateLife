using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DonateLife.Web.Pages.Clinic
{
    public class ClinicDto
    {
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string? Label { get; set; }

        [BindProperty]
        public string? Address { get; set; }

        [BindProperty]
        public int? EditId { get; set; }

        [BindProperty]
        public string? EditTitle { get; set; }

        [BindProperty]
        public string? EditAddress { get; set; }

        [BindProperty]
        public int? DeleteId { get; set; }

        public List<ClinicDto> Clinics { get; set; } = new List<ClinicDto>();

        public void OnGet()
        {
            // Beispielkliniken basierend auf init-db.sql
            Clinics =
            [
                new ClinicDto { Id = 1, Label = "City Hospital", Address = "123 Health St" },
                new ClinicDto { Id = 2, Label = "County Clinic", Address = "456 Wellness Ave" },
                new ClinicDto { Id = 3, Label = "Universitätsklinikum München", Address = "Marchioninistraße 15, 81377 München" },
                new ClinicDto { Id = 4, Label = "Charité Berlin", Address = "Charitéplatz 1, 10117 Berlin" }
            ];
        }
        public IActionResult OnPostCreateClinic()
        {
            //Daten ausgeben
            Console.WriteLine($"Titel: {Label}");
            Console.WriteLine($"Adresse: {Address}");

            //Hier zugehörigen Controller aufrufen 

            TempData["SuccessMessage"] = "Die Klinik wurde erfolgreich erstellt.";
            //TempData["ErrorMessage"] = "Die Klinik konnte nicht angelegt werden.";

            return RedirectToPage();
        }

        public IActionResult OnPostEditClinic()
        {
            //Daten ausgeben
            Console.WriteLine($"Bearbeiten - ID: {EditId}, Titel: {EditTitle}");
            Console.WriteLine($"Bearbeiten - Adresse: {EditAddress}");

            //Hier zugehörigen Controller aufrufen 

            TempData["SuccessMessage"] = "Die Klinik wurde erfolgreich bearbeitet.";
            //TempData["ErrorMessage"] = "Die Klinik konnte nicht bearbeitet werden.";

            return RedirectToPage();
        }

        public IActionResult OnPostDeleteClinic()
        {
            //Daten ausgeben
            Console.WriteLine($"Löschen - ID: {DeleteId}");

            //Hier zugehörigen Controller aufrufen 

            TempData["SuccessMessage"] = "Die Klinik wurde erfolgreich gelöscht.";
            //TempData["ErrorMessage"] = "Die Klinik konnte nicht gelöscht werden.";

            return RedirectToPage();
        }
    }
}
