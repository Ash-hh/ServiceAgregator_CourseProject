using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAgregator.Command;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Win32;

namespace ServiceAgregator.ViewModel
{
    class RegisterWinViewModel : BaseViewModel
    {

        public RegisterWinViewModel()
        {
            _User = new Models.Users();

            SetDeafultIcon();

            RegisterCommand = new DelegateCommand<Window>(Register, CanExecute);

            LoadImage = new DelegateCommand<Window>(ImageLoad, CanExecute);
            
        }

        private Models.Users _User;

        public Models.Users User { get { return _User;  } set { _User = value; OnPropertyChanged("User"); } }

        public DelegateCommand<Window> RegisterCommand { set; get; }

        public DelegateCommand<Window> LoadImage { set; get; }

        
        private void ImageLoad(Window win)
        {
            string FilePath="";
            if (OpenFile(ref FilePath))
            {
                try
                {
                    User.User_Image = File.ReadAllBytes(FilePath);
                    OnPropertyChanged("User");
                    MessageBox.Show("!!");
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
        }

        private bool OpenFile(ref string FilePath)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        private void SetDeafultIcon()
        {
            try
            {
                User.User_Image = File.ReadAllBytes(@"..\..\View\Resources\UserIcon.png");
                
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void Register(Window win)
        {
            var Register = new DataBase.RegisterQuery(_User);

            if(Register.Succes)
            {
                var NewLogin = new View.LoginWin();
                NewLogin.Show();
                if(win!=null)
                {
                    win.Close();
                }                
            }
           
        }

        private bool CanExecute(Window win)
        {
            return true;
        }


    }
    


}
