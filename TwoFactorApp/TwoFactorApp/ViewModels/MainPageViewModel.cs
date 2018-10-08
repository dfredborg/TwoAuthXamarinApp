using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoFactorApp.models;
using TwoFactorApp.Rest;

namespace TwoFactorApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _token;
        private string _secretToken;
        private bool _enabled;

        private Timer _timer;
        private TimeSpan _totalSeconds = new TimeSpan(0, 0, 4, 0);

        private TwoFactor twoFactor;
        private TokenService tokenService;
        public DelegateCommand StartCommand { get; private set; }

        public TimeSpan TotalSeconds

        {

            get { return _totalSeconds; }

            set { SetProperty(ref _totalSeconds, value); }

        }

        public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            Enabled = true;
            StartCommand = new DelegateCommand(Init);
            tokenService = new TokenService();
            Title = "Get Token";            
        }

        private async void Init()
        {            
            twoFactor = await tokenService.GetToken(SecretToken);
            if(twoFactor.LoginToken != "Your Secret token " + SecretToken + " is not valid")
            {
                Enabled = false;
                Token = "Your Token is: " + twoFactor.LoginToken;
                _timer = new Timer(TimeSpan.FromSeconds(1), CountDown);

                TotalSeconds = _totalSeconds;
                _timer.Start();
            }
            else
            {
                Token = twoFactor.LoginToken;
            }
            
        }

        private void CountDown()

        {

            if (_totalSeconds.TotalSeconds == 0)

            {
                TotalSeconds = new TimeSpan(0, 0, 4, 0);
                _timer.Stop();
            }
            else
            {
                TotalSeconds = _totalSeconds.Subtract(new TimeSpan(0, 0, 0, 1));
            }
        }

        public string Token
        {
            get { return _token; }
            set
            {
                SetProperty(ref _token, value);
            }
        }

        public string SecretToken
        {
            get { return _secretToken; }
            set
            {
                SetProperty(ref _secretToken, value);
            }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                SetProperty(ref _enabled, value);
            }
        }
    }
}
