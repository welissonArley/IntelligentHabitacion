using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.Template.Informations;
using IntelligentHabitacion.App.UseCases.Friends.GetMyFriends;
using IntelligentHabitacion.App.View.Modal;
using IntelligentHabitacion.Communication.Response;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel.Friends
{
    public class MyFriendsViewModel : BaseViewModel
    {
        private readonly Lazy<IGetMyFriendsUseCase> getMyFriendsUseCase;
        private IGetMyFriendsUseCase _getMyFriendsUseCase => getMyFriendsUseCase.Value;

        private MyFriendsComponent componentToEdit { get; set; }

        public ICommand SearchTextChangedCommand { protected set; get; }
        public ICommand MakePhonecallCommand { protected set; get; }
        public ICommand DetailFriendCommand { protected set; get; }
        public ICommand AddFriendCommand { protected set; get; }

        private ObservableCollection<FriendModel> _friendsList { get; set; }
        public ObservableCollection<FriendModel> FriendsList { get; set; }

        public MyFriendsViewModel(Lazy<IGetMyFriendsUseCase> getMyFriendsUseCase)
        {
            this.getMyFriendsUseCase = getMyFriendsUseCase;

            CurrentState = LayoutState.Loading;

            SearchTextChangedCommand = new Command((value) =>
            {
                OnSearchTextChanged((string)value);
            });
            MakePhonecallCommand = new Command(async (value) =>
            {
                await MakePhonecall((FriendModel)value);
            });
            DetailFriendCommand = new Command(async (value) =>
            {
                componentToEdit = (MyFriendsComponent)value;
                await OnDetailFriend(componentToEdit.Friend);
            });
            AddFriendCommand = new Command(async () =>
            {
                await OnAddFriend();
            });
        }

        private void OnSearchTextChanged(string value)
        {
            FriendsList = new ObservableCollection<FriendModel>(_friendsList.Where(c => c.Name.ToUpper().Contains(value.ToUpper())).ToList());

            OnPropertyChanged(new PropertyChangedEventArgs("FriendsList"));
        }
        private async Task MakePhonecall(FriendModel friend)
        {
            if (string.IsNullOrWhiteSpace(friend.Phonenumber2))
                await MakeCall(friend.Phonenumber1);
            else
            {
                var navigation = Resolver.Resolve<INavigation>();
                await navigation.PushPopupAsync(new ChoosePhonenumberModal(friend.Name, friend.Phonenumber1, friend.Phonenumber2, friend.ProfileColor, MakeCall));
            }
        }
        private async Task OnDetailFriend(FriendModel friend)
        {
            try
            {
                await ShowLoading();
                await Navigation.PushAsync<FriendDetailsViewModel>((viewModel, page) =>
                {
                    viewModel.Model = friend;
                    viewModel.RefreshCallback = new Command(RefreshList);
                    viewModel.DeleteFriendCallback = new Command((friendId)=>
                    {
                        FriendRemoved(friendId.ToString());
                    });
                });
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
        private async Task OnAddFriend()
        {
            try
            {
                await Navigation.PushAsync<AddFriendViewModel>(async(viewModel, _) =>
                {
                    await viewModel.Initialize(new Command((friendModel) =>
                    {
                        CallbackFriendAdded((ResponseFriendJson)friendModel);
                    }));
                });
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        private async Task MakeCall(string number)
        {
            await ShowLoading();
            PhoneCall.MakeCall(number);
            HideLoading();
        }

        private void RefreshList()
        {
            OnPropertyChanged(new PropertyChangedEventArgs("FriendsList"));
            componentToEdit.Refresh();
        }

        private void FriendRemoved(string id)
        {
            var friend = FriendsList.First(c => c.Id.Equals(id));
            FriendsList.Remove(friend);
            OnPropertyChanged(new PropertyChangedEventArgs("FriendsList"));
            OnPropertyChanged(new PropertyChangedEventArgs("FriendsListIsEmpty"));
            Navigation.PopAsync();
        }

        private void CallbackFriendAdded(ResponseFriendJson json)
        {
            var model = new FriendModel
            {
                Id = json.Id,
                JoinedOn = json.JoinedOn,
                Name = json.Name,
                ProfileColor = json.ProfileColor,
                Phonenumber1 = json.Phonenumbers[0].Number,
                Phonenumber2 = json.Phonenumbers.Count > 1 ? json.Phonenumbers[1].Number : null,
                EmergencyContact1 = new EmergencyContactModel
                {
                    Name = json.EmergencyContacts[0].Name,
                    Relationship = json.EmergencyContacts[0].Relationship,
                    PhoneNumber = json.EmergencyContacts[0].Phonenumber
                },
                EmergencyContact2 = json.EmergencyContacts.Count == 1 ? null : new EmergencyContactModel
                {
                    Name = json.EmergencyContacts[1].Name,
                    Relationship = json.EmergencyContacts[1].Relationship,
                    PhoneNumber = json.EmergencyContacts[1].Phonenumber
                }
            };
            _friendsList.Add(model);
            FriendsList.Add(model);
            CurrentState = LayoutState.None;
            OnPropertyChanged(new PropertyChangedEventArgs("FriendsList"));
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
        }

        public async Task Initialize()
        {
            _friendsList = new ObservableCollection<FriendModel>(await _getMyFriendsUseCase.Execute());
            FriendsList = new ObservableCollection<FriendModel>(_friendsList);
            OnPropertyChanged(new PropertyChangedEventArgs("FriendsList"));
            CurrentState = _friendsList.Any() ? LayoutState.None : LayoutState.Empty;
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
        }
    }
}
