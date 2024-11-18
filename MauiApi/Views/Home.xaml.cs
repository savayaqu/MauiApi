using MauiApi.Models;

namespace MauiApi.Views;

public partial class Home : ContentPage
{
	private User _user;
	private string _token;
	public Home(User user, string token)
	{
		InitializeComponent();
		_user = user;
		_token = token;

		//Устанавливаем аватар и никнейм
		AvatarHome.Source = $"http://bububu.ru/public/storage/{_user.Avatar}";
		NicknameHome.Text = _user.NickName;

    }
}