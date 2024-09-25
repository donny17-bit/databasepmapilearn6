namespace databasepmapilearn6.Configurations;

public class ConfJwt
{
    // Issuer
    public string Issuer {get; set;} = null!;

    // Audience
    public string Audience {get; set;} = null!;

    // Signed key
    public string Key {get; set;} = null!;
}