namespace databasepmapilearn6.ViewModels;

public class ViewModels
{
    public class Login {
        public bool IsWrongPassword {get; set;}
        public string? Token {get; set;}
        public string? RefreshToken {get; set;}
        public string? ErrorMessage {get; set;}

        private Login() {}

        public Login Success(string Token, string RefreshToken) {
            return new Login{
                IsWrongPassword = false,
                Token = Token,
                RefreshToken = RefreshToken
            };
        }
    } 
}