using Microsoft.Extensions.DependencyInjection;

namespace Quanlyvadatphonghomestay
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
<<<<<<< HEAD
            return new Window(new DanhGia());
=======
<<<<<<< HEAD
            return new Window(new Modau());
=======
            return new Window(new ThongTinCaNhan());
>>>>>>> e14679408b0c635b74f8737b80017ff0a103a869
>>>>>>> 726fc91e1a92d2ba766659457a8a3be28a60e6e8
        }
    }
}