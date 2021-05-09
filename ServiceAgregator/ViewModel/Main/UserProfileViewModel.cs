using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Windows;
using ServiceAgregator.ViewModel;
using ServiceAgregator.Models;
using ServiceAgregator.Command;

namespace ServiceAgregator.ViewModel.Main
{
    class UserProfileViewModel : BaseViewModel
    {

        public UserProfileViewModel() { }

        public UserProfileViewModel(Users currentUser)
        {
            _currentUser = currentUser;
            UploadImage = new DelegateCommand<object>(ImageLoad, CanExecute);
            SaveUserProfile = new DelegateCommand<object>(SaveProfile, CanExecute);
        }

        public DelegateCommand<object> UploadImage { set; get; }
        public DelegateCommand<object> SaveUserProfile { set; get; }

        private Users _currentUser;
        public Users CurrentUser
        {
            get
            {
                return _currentUser;
            }
            private set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        private void ImageLoad(object win)
        {
            string FilePath = "";
            if (OpenFile(ref FilePath))
            {
                try
                {
                    CurrentUser.User_Image = File.ReadAllBytes(FilePath);
                    OnPropertyChanged("CurrentUser");                  
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

        private void SaveProfile(object obj)
        {
            new DataBase.UserQuery().UserUpdate(CurrentUser);            
        }

        public bool CanExecute(object obj)
        {
            return true;
        }




    }
}
