using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;

namespace CIMOBProject.Models {
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser, INotifyPropertyChanged {

        public ApplicationUser() {

        }

        public ApplicationUser(ApplicationUser user) {
            Id = user.Id;
            UserName = user.UserName;
            UserFullname = user.UserFullname;
            PostalCode = user.PostalCode;
            BirthDate = user.BirthDate;
            UserAddress = user.UserAddress;
            UserCc = user.UserCc;
            PhoneNumber = user.PhoneNumber;
            IsBanned = user.IsBanned;
            BirthDate = user.BirthDate;
        }

        private string userFullname;
        public string UserFullname {
            get { return userFullname; }
            set {
                userFullname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UserFullname"));
            }
        }

        private string postalCode;
        public String PostalCode {
            get { return postalCode; }
            set {
                postalCode = value;
                OnPropertyChanged(new PropertyChangedEventArgs("PostalCode"));
            }
        }

        private DateTime birthDate;
        public DateTime BirthDate {
            get { return birthDate; }
            set {
                birthDate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("BirthDate"));
            }
        }

        private string userAddress;
        public String UserAddress {
            get { return userAddress; }
            set {
                userAddress = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UserAddress"));
            }
        }

        private string userCc;
        public string UserCc {
            get { return userCc; }
            set {
                userCc = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UserCc"));
            }
        }

        public override string PhoneNumber {
            get => base.PhoneNumber;
            set {
                base.PhoneNumber = value;
                OnPropertyChanged(new PropertyChangedEventArgs("PhoneNumber"));
            }
        }

        public override string UserName { get => base.UserName;
            set {
                base.UserName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UserName"));
            }
        }

        public override string Email {
            get => base.Email;
            set {
                base.Email = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Email"));
            }
        }

        private bool isBanned;
        public bool IsBanned {
            get { return isBanned; }
            set {
                isBanned = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsBanned"));
            }
        }


        public bool IsNotified { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e) {
            PropertyChanged?.Invoke(this, e);
        }

    }
}
