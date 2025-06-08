using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DonateLife.Web.Pages.Patient
{
    public class PatientDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string EMail { get; set; } = string.Empty;
        public string BloodtypeDisplay { get; set; } = string.Empty;
        public int BloodtypeId { get; set; }
        public string Sex { get; set; } = string.Empty;
        public int SexId { get; set; }
        public string FullName => $"{Firstname} {Lastname}";
        public List<OrganOfferDto> OrganOffers { get; set; } = new List<OrganOfferDto>();
        public List<OrganRequestDto> OrganRequests { get; set; } = new List<OrganRequestDto>();
        public List<PrevIllnessDto> PrevIllnesses { get; set; } = new List<PrevIllnessDto>();
    }

    public class OrganOfferDto
    {
        public int Id { get; set; }
        public string ClinicName { get; set; } = string.Empty;
        public string OrganType { get; set; } = string.Empty;
        public DateTime AddedDate { get; set; }
        public DateTime? RemovedDate { get; set; }
        public bool IsActive => !RemovedDate.HasValue;
    }
    public class OrganRequestDto
    {
        public int Id { get; set; }
        public string ClinicName { get; set; } = string.Empty;
        public string OrganType { get; set; } = string.Empty;
        public DateTime AddedDate { get; set; }
        public DateTime? RemovedDate { get; set; }
        public bool IsActive => !RemovedDate.HasValue;
        public bool HasMatch { get; set; }
        public MatchedOrganOfferDto? MatchedOffer { get; set; }
    }

    public class MatchedOrganOfferDto
    {
        public int Id { get; set; }
        public string ClinicName { get; set; } = string.Empty;
        public string OrganType { get; set; } = string.Empty;
        public string RecipientName { get; set; } = string.Empty;
        public DateTime AddedDate { get; set; }
        public DateTime? RemovedDate { get; set; }
        public bool IsActive => !RemovedDate.HasValue;
    }

    public class IllnessDto
    {
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
    }

    public class PrevIllnessDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int IllnessId { get; set; }
        public string IllnessLabel { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }

    public class IndexModel : PageModel
    {
        [BindProperty]
        public int? SelectedPatientId { get; set; }

        [BindProperty]
        public int? NewOfferClinicId { get; set; }

        [BindProperty]
        public int? NewOfferOrganType { get; set; }

        [BindProperty]
        public int? NewRequestClinicId { get; set; }

        [BindProperty]
        public int? NewRequestOrganType { get; set; }

        [BindProperty]
        public int? RemoveOfferId { get; set; }

        [BindProperty]
        public int? RemoveRequestId { get; set; }

        [BindProperty]
        public string? NewPatientFirstname { get; set; }

        [BindProperty]
        public string? NewPatientLastname { get; set; }

        [BindProperty]
        public string? NewPatientEmail { get; set; }

        [BindProperty]
        public int? NewPatientBloodtype { get; set; }
        [BindProperty]
        public int? NewPatientSex { get; set; }

        [BindProperty]
        public int? EditPatientId { get; set; }

        [BindProperty]
        public string? EditPatientFirstname { get; set; }

        [BindProperty]
        public string? EditPatientLastname { get; set; }

        [BindProperty]
        public string? EditPatientEmail { get; set; }

        [BindProperty]
        public int? EditPatientBloodtype { get; set; }

        [BindProperty]
        public int? EditPatientSex { get; set; }
        [BindProperty]
        public int? DeletePatientId { get; set; }

        // Illness Management Properties
        [BindProperty]
        public List<int> NewPatientIllnessIds { get; set; } = new List<int>();

        [BindProperty]
        public List<int> EditPatientIllnessIds { get; set; } = new List<int>();

        [BindProperty]
        public int? AddIllnessPatientId { get; set; }

        [BindProperty]
        public int? AddIllnessId { get; set; }

        [BindProperty]
        public DateTime? AddIllnessDate { get; set; }

        [BindProperty]
        public int? RemoveIllnessId { get; set; }

        // Properties for illness dates in new patient creation
        [BindProperty]
        public List<DateTime> NewPatientIllnessDates { get; set; } = new List<DateTime>();

        public List<PatientDto> Patients { get; set; } = new List<PatientDto>();
        public List<ClinicDto> Clinics { get; set; } = new List<ClinicDto>();
        public List<OrganTypeDto> OrganTypes { get; set; } = new List<OrganTypeDto>();
        public List<BloodtypeDto> Bloodtypes { get; set; } = new List<BloodtypeDto>();
        public List<SexDto> Sexes { get; set; } = new List<SexDto>();
        public List<IllnessDto> Illnesses { get; set; } = new List<IllnessDto>();

        public void OnGet()
        {
            LoadMasterData();
            LoadPatients();
        }

        private void LoadMasterData()
        {
            // Kliniken laden
            Clinics = new List<ClinicDto>
            {
                new ClinicDto { Id = 1, Name = "City Hospital", Address = "123 Health St" },
                new ClinicDto { Id = 2, Name = "County Clinic", Address = "456 Wellness Ave" }
            };

            // Organtypen laden
            OrganTypes = new List<OrganTypeDto>
            {
                new OrganTypeDto { Id = 1, Name = "Herz" },
                new OrganTypeDto { Id = 2, Name = "Leber" },
                new OrganTypeDto { Id = 3, Name = "Niere" },
                new OrganTypeDto { Id = 4, Name = "Lunge" }
            };            // Blutgruppen laden
            Bloodtypes = new List<BloodtypeDto>
            {
                new BloodtypeDto { Id = 1, Name = "A+" },
                new BloodtypeDto { Id = 2, Name = "A-" },
                new BloodtypeDto { Id = 3, Name = "B+" },
                new BloodtypeDto { Id = 4, Name = "B-" },
                new BloodtypeDto { Id = 5, Name = "AB+" },
                new BloodtypeDto { Id = 6, Name = "AB-" },
                new BloodtypeDto { Id = 7, Name = "0+" },
                new BloodtypeDto { Id = 8, Name = "0-" }
            };

            // Geschlechter laden
            Sexes = new List<SexDto>
            {
                new SexDto { Id = 1, Name = "Männlich" },
                new SexDto { Id = 2, Name = "Weiblich" },
                new SexDto { Id = 3, Name = "Divers" }
            };

            // Krankheiten laden (basierend auf init-db.sql)
            Illnesses = new List<IllnessDto>
            {
                new IllnessDto { Id = 1, Label = "Diabetes" },
                new IllnessDto { Id = 2, Label = "Hypertension" },
                new IllnessDto { Id = 3, Label = "Herzkrankheit" },
                new IllnessDto { Id = 4, Label = "Nierenkrankheit" },
                new IllnessDto { Id = 5, Label = "Leberkrankheit" },
                new IllnessDto { Id = 6, Label = "Asthma" },
                new IllnessDto { Id = 7, Label = "Allergien" },
                new IllnessDto { Id = 8, Label = "Arthritis" }
            };
        }
        private void LoadPatients()
        {
            // Beispielpatienten basierend auf init-db.sql
            Patients = new List<PatientDto>
            {                new PatientDto
                {
                    Id = 1,
                    PatientId = 1,
                    Firstname = "John",
                    Lastname = "Doe",
                    EMail = "john@example.com",
                    BloodtypeDisplay = "A+",
                    BloodtypeId = 1,
                    Sex = "Männlich",
                    SexId = 1,
                    OrganOffers = new List<OrganOfferDto>
                    {
                        new OrganOfferDto
                        {
                            Id = 1,
                            ClinicName = "City Hospital",
                            OrganType = "Herz",
                            AddedDate = new DateTime(2024, 1, 1),
                            RemovedDate = null
                        }
                    },
                    OrganRequests = new List<OrganRequestDto>(),
                    PrevIllnesses = new List<PrevIllnessDto>
                    {
                        new PrevIllnessDto
                        {
                            Id = 1,
                            PatientId = 1,
                            IllnessId = 1,
                            IllnessLabel = "Diabetes",
                            Date = new DateTime(2020, 1, 1)
                        }
                    }
                },                new PatientDto
                {
                    Id = 2,
                    PatientId = 2,
                    Firstname = "Jane",
                    Lastname = "Smith",
                    EMail = "jane@example.com",
                    BloodtypeDisplay = "B-",
                    BloodtypeId = 4,
                    Sex = "Weiblich",
                    SexId = 2,
                    OrganOffers = new List<OrganOfferDto>(), OrganRequests = new List<OrganRequestDto>
                    {
                        new OrganRequestDto
                        {
                            Id = 1,
                            ClinicName = "County Clinic",
                            OrganType = "Leber",
                            AddedDate = new DateTime(2024, 2, 1),
                            RemovedDate = null,
                            HasMatch = true,
                            MatchedOffer = new MatchedOrganOfferDto
                            {
                                Id = 1,
                                ClinicName = "City Hospital",
                                OrganType = "Herz",
                                RecipientName = "John Doe",
                                AddedDate = new DateTime(2024, 1, 1),
                                RemovedDate = null
                            }
                        }
                    },
                    PrevIllnesses = new List<PrevIllnessDto>
                    {
                        new PrevIllnessDto
                        {
                            Id = 2,
                            PatientId = 2,
                            IllnessId = 2,
                            IllnessLabel = "Hypertension",
                            Date = new DateTime(2021, 6, 15)
                        }
                    }
                }
            };
        }

        // Hilfsmethode für JSON-Serialisierung ohne Referenzen
        public string GetPatientsJson()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = null,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return JsonSerializer.Serialize(Patients, options);
        }

        public IActionResult OnPostAddOrganOffer()
        {
            Console.WriteLine($"Neue Organspende - Patient ID: {SelectedPatientId}");
            Console.WriteLine($"Klinik ID: {NewOfferClinicId}");
            Console.WriteLine($"Organ Typ: {NewOfferOrganType}");

            // Hier zugehörigen Controller aufrufen

            TempData["SuccessMessage"] = "Die Organspende wurde erfolgreich hinzugefügt.";
            return RedirectToPage();
        }

        public IActionResult OnPostAddOrganRequest()
        {
            Console.WriteLine($"Neue Organanfrage - Patient ID: {SelectedPatientId}");
            Console.WriteLine($"Klinik ID: {NewRequestClinicId}");
            Console.WriteLine($"Organ Typ: {NewRequestOrganType}");

            // Hier zugehörigen Controller aufrufen

            TempData["SuccessMessage"] = "Die Organanfrage wurde erfolgreich hinzugefügt.";
            return RedirectToPage();
        }

        public IActionResult OnPostRemoveOrganOffer()
        {
            Console.WriteLine($"Organspende entfernen - ID: {RemoveOfferId}");

            // Hier zugehörigen Controller aufrufen

            TempData["SuccessMessage"] = "Die Organspende wurde erfolgreich entfernt.";
            return RedirectToPage();
        }

        public IActionResult OnPostRemoveOrganRequest()
        {
            Console.WriteLine($"Organanfrage entfernen - ID: {RemoveRequestId}");

            // Hier zugehörigen Controller aufrufen

            TempData["SuccessMessage"] = "Die Organanfrage wurde erfolgreich entfernt.";
            return RedirectToPage();
        }
        public IActionResult OnPostCreatePatient()
        {
            // Daten ausgeben
            Console.WriteLine($"Neuer Patient - Vorname: {NewPatientFirstname}");
            Console.WriteLine($"Neuer Patient - Nachname: {NewPatientLastname}");
            Console.WriteLine($"Neuer Patient - E-Mail: {NewPatientEmail}");
            Console.WriteLine($"Neuer Patient - Blutgruppe: {NewPatientBloodtype}");
            Console.WriteLine($"Neuer Patient - Geschlecht: {NewPatientSex}");
            Console.WriteLine($"Ausgewählte Krankheiten: {string.Join(", ", NewPatientIllnessIds)}");
            Console.WriteLine($"Krankheitsdaten: {string.Join(", ", NewPatientIllnessDates.Select(d => d.ToString("yyyy-MM-dd")))}");

            // Verarbeite ausgewählte Krankheiten mit Daten
            for (int i = 0; i < NewPatientIllnessIds.Count && i < NewPatientIllnessDates.Count; i++)
            {
                var illnessId = NewPatientIllnessIds[i];
                var date = NewPatientIllnessDates[i];
                Console.WriteLine($"Krankheit für neuen Patienten - ID: {illnessId}, Datum: {date:yyyy-MM-dd}");
                // Hier Controller aufrufen um Krankheit dem neuen Patienten zuzuweisen
            }

            // Hier zugehörigen Controller aufrufen

            TempData["SuccessMessage"] = "Der Patient wurde erfolgreich erstellt.";
            //TempData["ErrorMessage"] = "Der Patient konnte nicht erstellt werden.";

            return RedirectToPage();
        }
        public IActionResult OnPostEditPatient()
        {
            // Daten ausgeben
            Console.WriteLine($"Patient bearbeiten - ID: {EditPatientId}");
            Console.WriteLine($"Patient bearbeiten - Vorname: {EditPatientFirstname}");
            Console.WriteLine($"Patient bearbeiten - Nachname: {EditPatientLastname}");
            Console.WriteLine($"Patient bearbeiten - E-Mail: {EditPatientEmail}");
            Console.WriteLine($"Patient bearbeiten - Blutgruppe: {EditPatientBloodtype}");
            Console.WriteLine($"Patient bearbeiten - Geschlecht: {EditPatientSex}");

            // Krankheitsverwaltung - Einfache Liste der Illness IDs
            Console.WriteLine($"Aktuelle Krankheiten: {string.Join(", ", EditPatientIllnessIds)}");

            // Hier würde man die Krankheiten komplett ersetzen
            // 1. Alle bestehenden Krankheiten für den Patienten löschen
            // 2. Die neuen Krankheiten aus EditPatientIllnessIds hinzufügen
            foreach (var illnessId in EditPatientIllnessIds)
            {
                Console.WriteLine($"Krankheit für Patienten - Patient: {EditPatientId}, Krankheit: {illnessId}");
                // Hier Controller aufrufen um Krankheit zu setzen
            }

            // Hier zugehörigen Controller aufrufen

            TempData["SuccessMessage"] = "Der Patient wurde erfolgreich bearbeitet.";
            //TempData["ErrorMessage"] = "Der Patient konnte nicht bearbeitet werden.";

            return RedirectToPage();
        }

        public IActionResult OnPostDeletePatient()
        {
            // Daten ausgeben
            Console.WriteLine($"Patient löschen - ID: {DeletePatientId}");

            // Hier zugehörigen Controller aufrufen

            TempData["SuccessMessage"] = "Der Patient wurde erfolgreich gelöscht.";
            //TempData["ErrorMessage"] = "Der Patient konnte nicht gelöscht werden.";

            return RedirectToPage();
        }

        public IActionResult OnPostAddIllness()
        {
            Console.WriteLine($"Krankheit hinzufügen - Patient ID: {AddIllnessPatientId}");
            Console.WriteLine($"Krankheit ID: {AddIllnessId}");
            Console.WriteLine($"Datum: {AddIllnessDate}");

            // Hier zugehörigen Controller aufrufen

            TempData["SuccessMessage"] = "Die Krankheit wurde erfolgreich hinzugefügt.";
            return RedirectToPage();
        }

        public IActionResult OnPostRemoveIllness()
        {
            Console.WriteLine($"Krankheit entfernen - ID: {RemoveIllnessId}");

            // Hier zugehörigen Controller aufrufen

            TempData["SuccessMessage"] = "Die Krankheit wurde erfolgreich entfernt.";
            return RedirectToPage();
        }
    }

    public class ClinicDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

    public class OrganTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class BloodtypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class SexDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
