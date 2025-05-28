namespace DonateLife.Web.Options;

public record CreateAccountOptions
(
    string Password,
    Guid PersonId
);