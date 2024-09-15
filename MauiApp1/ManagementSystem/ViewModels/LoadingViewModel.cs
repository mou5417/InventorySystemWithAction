using ManagementSystem.Views;
using System.IdentityModel.Tokens.Jwt;

namespace ManagementSystem.ViewModels
{
    public partial class LoadingViewModel
    {
        public LoadingViewModel()
        {
          
           CheckUserLoginDetails();

        }

        private async Task CheckUserLoginDetails()
        {
            var token = await SecureStorage.GetAsync("Token");
            if (string.IsNullOrEmpty(token)) await GoToLoginView();
            else
            {
                var jsonToken= new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
                if (jsonToken.ValidTo<DateTime.UtcNow)
                {
                    SecureStorage.Remove("Token");
                    await GoToLoginView();
                }
                else
                {
                    if (App.UserInfo.Role == "1") {await Shell.Current.GoToAsync(nameof(R1MainPage)); }
                    if (App.UserInfo.Role == "2") {await Shell.Current.GoToAsync(nameof(R2MainPage)); }
                   await GoToLoginView();
                }
            }

        }

        private async Task GoToLoginView()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginView)}");
        }

        private async Task GoToMainPage()
        {
            await Shell.Current.GoToAsync($"{nameof(R2MainPage)}");
        }
    }
}
