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
        }

        public DelegateCommand<object> UploadImage { set; get; }

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
                OnPropertyChanged(nameof(_currentUser));
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

        public bool CanExecute(object obj)
        {
            return true;
        }




    }
}
