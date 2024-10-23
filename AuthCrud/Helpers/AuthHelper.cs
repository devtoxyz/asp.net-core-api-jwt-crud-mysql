namespace AuthCrud.Helpers
{
    public class AuthHelper
    {
        public static string PrivateKey { get; set; } = Environment.GetEnvironmentVariable("PrivateKey")!;
    }
}
