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
            return new Window(new DanhGia());
        }
    }
}