namespace Quanlyvadatphonghomestay
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            chaygiaodien();
        }

        private async void chaygiaodien()
        {
            await Task.Delay(5000);
            Application.Current.MainPage = new DangNhap();
        }
    }
}
