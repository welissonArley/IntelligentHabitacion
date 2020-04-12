using IntelligentHabitacion.App.Model;
using System.Collections.ObjectModel;

namespace IntelligentHabitacion.App.ViewModel
{
    public class MyFriendsViewModel : BaseViewModel
    {
        public ObservableCollection<FriendModel> FriendsList { get; set; }

        public MyFriendsViewModel()
        {
            FriendsList = new ObservableCollection<FriendModel>
            {
                new FriendModel
                {
                    Name = "Matheus",
                    Phonenumber1 = "(37) 9 9811-1881",
                    Phonenumber2 = "(37) 9 9811-1882",
                    EmergencyContact1 = new EmergencyContactModel
                    {
                        Name = "Zilda",
                        PhoneNumber = "(31) 9 0000-0000",
                        FamilyRelationship = "Mãe"
                    }
                },
                new FriendModel
                {
                    Name = "William",
                    Phonenumber1 = "(37) 9 9811-1881",
                    EmergencyContact1 = new EmergencyContactModel
                    {
                        Name = "Zilda",
                        PhoneNumber = "(31) 9 0000-0000",
                        FamilyRelationship = "Mãe"
                    },
                    EmergencyContact2 = new EmergencyContactModel
                    {
                        Name = "Zilda",
                        PhoneNumber = "(31) 9 0000-0000",
                        FamilyRelationship = "Mãe"
                    }
                }
            };
        }
    }
}
