namespace DonateLife.Web.Options;

public record CreatePatientOptions
(
    Guid PersonGuid,
    BloodType BloodType,
    Sex Sex
);