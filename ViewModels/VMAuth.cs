namespace databasepmapilearn6.ViewModels;

public class VMAuth
{
    public class Login
    {
        public bool is_wrong_password { get; set; }
        public string? token { get; set; }
        public string? refresh_token { get; set; }
        public string? error_message { get; set; }

        // private Login() {}

        public static Login Success(string Token, string RefreshToken)
        {
            return new Login
            {
                is_wrong_password = false,
                token = Token,
                refresh_token = RefreshToken
            };
        }
    }
}