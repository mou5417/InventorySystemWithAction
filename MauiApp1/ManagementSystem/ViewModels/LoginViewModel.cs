using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ManagementSystem.Views;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ApiService;

namespace ManagementSystem.ViewModels
{
    public partial class LoginViewModel:ObservableObject
    {
        [ObservableProperty]
        string username;
        [ObservableProperty]
        string password;
        private readonly IServiceGeneric serviceGeneric;

        public LoginViewModel(IServiceGeneric serviceGeneric)
        {
            this.serviceGeneric = serviceGeneric;
        }
        [RelayCommand]
        async Task Login()
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {

                await DisplayLoginMsg("登入資訊錯誤");
            }
            else
            {
                var loginModel= new LoginModel(username, password);
                AuthResponseModel response = default;
                try
                {
                     response = await serviceGeneric.Login(loginModel);
                }
                catch (Exception ex)
                {

                    await DisplayLoginMsg(ex.Message);
                }
               
                if (!string.IsNullOrEmpty(response.Token))
                {
                    await SecureStorage.SetAsync("Token", response.Token);
                    var jsonToken = new JwtSecurityTokenHandler().ReadToken(response.Token) as JwtSecurityToken;
                
                    var role= jsonToken.Claims.FirstOrDefault(c=> c.Type.Equals(ClaimTypes.Role))?.Value;
                    //TODO:get Person Name
                    App.UserInfo = new UserInfo { Username = username, Role = role,UserId=response.UserId };
                    
                    try
                    {
                        if (role == "2") { await Shell.Current.GoToAsync(nameof(R2MainPage)); }
                        if (role == "1") { await Shell.Current.GoToAsync(nameof(R1MainPage)); }
                       
                        
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    
                   
                }
                else
                {
                  await  DisplayLoginMsg("登入失敗");
                }
                
            }
        }
        async Task DisplayLoginMsg(string msg)
        {
            await Shell.Current.DisplayAlert("登入失敗", msg, "確認");
            Password = string.Empty;
        }
    }
}
