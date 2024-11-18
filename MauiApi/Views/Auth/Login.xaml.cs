using MauiApi.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MauiApi.Views.Auth;

public partial class Login : ContentPage
{
	// Инициализация HTTP клиента
	private readonly HttpClient _httpClient = new HttpClient();

	public Login()
	{
		InitializeComponent();
	}

	// Переход на страницу регистрации
	private async void OnRegisterTapped(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Register());
	}

	// Аутентификация
	private async void OnLoginButtonClicked(object sender, EventArgs e)
	{

        

        // Получение почты и пароля из формы
        string email = EmailEntry.Text;
		string password = PasswordEntry.Text;

		if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
			await DisplayAlert("Ошибка", "Введите почту и пароль", "OK");
			return;
		}

		var loginResponse = await AuthenticateUserAsync(email, password);
		if (loginResponse != null) {
			// Передаем данные пользователя и его токен на страницу Home
			await Navigation.PushAsync(new Home(loginResponse.User, loginResponse.Token));
		}
	}

	private async Task<AuthResponse> AuthenticateUserAsync(string email, string password)
	{
		// Формирование тела для отправки
		var loginData = new { email, password };
		// Преобразрования данных в JSON для отправки
		var jsonContent = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

		// Запрос серверу
		try
		{
            // Отправляем запрос и записываем ответ в response
            HttpResponseMessage response = await _httpClient.PostAsync("http://bububu.ru/api/login", jsonContent);

			// Если ответ успех 200
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{

				var content = await response.Content.ReadAsStringAsync();
				var result = JsonSerializer.Deserialize<AuthResponse>(content);

				if (result?.Token != null)
				{
					// Возвращаем весь объект, включая данные пользователя и токен
					return result;
				}

			}
			// Если ответ 401
			else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			{
				await DisplayAlert("Ошибка входа", "Неправильная почта или пароль", "OK");
			}
			else { 
				await DisplayAlert("Ошибка", "Произошла ошибка на сервере", "OK");
            }
	
		}
		catch (Exception ex) {
			await DisplayAlert("Ошибка сети", ex.Message, "ОК");
		}
		return null;

	}
}