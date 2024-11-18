using MauiApi.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MauiApi.Views.Auth;

public partial class Login : ContentPage
{
	// ������������� HTTP �������
	private readonly HttpClient _httpClient = new HttpClient();

	public Login()
	{
		InitializeComponent();
	}

	// ������� �� �������� �����������
	private async void OnRegisterTapped(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Register());
	}

	// ��������������
	private async void OnLoginButtonClicked(object sender, EventArgs e)
	{

        

        // ��������� ����� � ������ �� �����
        string email = EmailEntry.Text;
		string password = PasswordEntry.Text;

		if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
			await DisplayAlert("������", "������� ����� � ������", "OK");
			return;
		}

		var loginResponse = await AuthenticateUserAsync(email, password);
		if (loginResponse != null) {
			// �������� ������ ������������ � ��� ����� �� �������� Home
			await Navigation.PushAsync(new Home(loginResponse.User, loginResponse.Token));
		}
	}

	private async Task<AuthResponse> AuthenticateUserAsync(string email, string password)
	{
		// ������������ ���� ��� ��������
		var loginData = new { email, password };
		// ��������������� ������ � JSON ��� ��������
		var jsonContent = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

		// ������ �������
		try
		{
            // ���������� ������ � ���������� ����� � response
            HttpResponseMessage response = await _httpClient.PostAsync("http://bububu.ru/api/login", jsonContent);

			// ���� ����� ����� 200
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{

				var content = await response.Content.ReadAsStringAsync();
				var result = JsonSerializer.Deserialize<AuthResponse>(content);

				if (result?.Token != null)
				{
					// ���������� ���� ������, ������� ������ ������������ � �����
					return result;
				}

			}
			// ���� ����� 401
			else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			{
				await DisplayAlert("������ �����", "������������ ����� ��� ������", "OK");
			}
			else { 
				await DisplayAlert("������", "��������� ������ �� �������", "OK");
            }
	
		}
		catch (Exception ex) {
			await DisplayAlert("������ ����", ex.Message, "��");
		}
		return null;

	}
}