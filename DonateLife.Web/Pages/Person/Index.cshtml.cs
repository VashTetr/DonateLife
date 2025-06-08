using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DonateLife.Web.Pages.Person
{    public class PersonDto
    {
        public int Id { get; set; }
        public int? AccountID { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string EMail { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public string FullName => $"{Firstname} {Lastname}";
    }

    public class IndexModel : PageModel
    {
        [BindProperty]
        public string? Firstname { get; set; }

        [BindProperty]
        public string? Lastname { get; set; }        [BindProperty]
        public string? EMail { get; set; }

        [BindProperty]
        public int? EditId { get; set; }

        [BindProperty]
        public string? EditFirstname { get; set; }

        [BindProperty]
        public string? EditLastname { get; set; }        [BindProperty]
        public string? EditEMail { get; set; }

        [BindProperty]
        public int? DeleteId { get; set; }

        public List<PersonDto> Persons { get; set; } = new List<PersonDto>();        public void OnGet()
        {
            // Beispielpersonen basierend auf init-db.sql (ohne Patienten-Referenzen)
            Persons =
            [
                new PersonDto 
                { 
                    Id = 1, 
                    AccountID = 1, 
                    Firstname = "John", 
                    Lastname = "Doe", 
                    EMail = "john@example.com", 
                    IsDeleted = false 
                },
                new PersonDto 
                { 
                    Id = 2, 
                    AccountID = 2, 
                    Firstname = "Jane", 
                    Lastname = "Smith", 
                    EMail = "jane@example.com", 
                    IsDeleted = false 
                },
                new PersonDto 
                { 
                    Id = 3, 
                    AccountID = 3, 
                    Firstname = "Max", 
                    Lastname = "Mustermann", 
                    EMail = "max.mustermann@example.com", 
                    IsDeleted = false 
                },
                new PersonDto 
                { 
                    Id = 4, 
                    AccountID = 4, 
                    Firstname = "Anna", 
                    Lastname = "Schmidt", 
                    EMail = "anna.schmidt@example.com", 
                    IsDeleted = false 
                }
            ];
        }public IActionResult OnPostCreatePerson()
        {
            //Daten ausgeben
            Console.WriteLine($"Vorname: {Firstname}");
            Console.WriteLine($"Nachname: {Lastname}");
            Console.WriteLine($"E-Mail: {EMail}");

            //Hier zugehörigen Controller aufrufen 

            TempData["SuccessMessage"] = "Die Person wurde erfolgreich erstellt.";
            //TempData["ErrorMessage"] = "Die Person konnte nicht angelegt werden.";

            return RedirectToPage();
        }        public IActionResult OnPostEditPerson()
        {
            //Daten ausgeben
            Console.WriteLine($"Bearbeiten - ID: {EditId}");
            Console.WriteLine($"Bearbeiten - Vorname: {EditFirstname}");
            Console.WriteLine($"Bearbeiten - Nachname: {EditLastname}");
            Console.WriteLine($"Bearbeiten - E-Mail: {EditEMail}");

            //Hier zugehörigen Controller aufrufen 

            TempData["SuccessMessage"] = "Die Person wurde erfolgreich bearbeitet.";
            //TempData["ErrorMessage"] = "Die Person konnte nicht bearbeitet werden.";

            return RedirectToPage();
        }

        public IActionResult OnPostDeletePerson()
        {
            //Daten ausgeben
            Console.WriteLine($"Löschen - ID: {DeleteId}");

            //Hier zugehörigen Controller aufrufen 

            TempData["SuccessMessage"] = "Die Person wurde erfolgreich gelöscht.";
            //TempData["ErrorMessage"] = "Die Person konnte nicht gelöscht werden.";

            return RedirectToPage();
        }
    }
}
