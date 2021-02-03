﻿using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.CleanHouse
{
    public class MyTasksViewModel : BaseViewModel
    {
        public string ProfileColor { get; set; }
        public MyTasksCleanHouseModel Model { get; set; }

        public ICommand SeeFriendsTaskCommand { get; private set; }

        public MyTasksViewModel(UserPreferences userPreferences)
        {
            ProfileColor = userPreferences.ProfileColor;

            SeeFriendsTaskCommand = new Command(async () => await SeeFriendsTaskSelected());

            Model = new MyTasksCleanHouseModel
            {
                Name = "Pablo Henrique",
                Month = DateTime.Today,
                Tasks = new System.Collections.ObjectModel.ObservableCollection<MyTasksForTheMonth>
                {
                    new MyTasksForTheMonth
                    {
                        Room = "Área de Serviço",
                        CleaningRecords = 5,
                        LastRecord = DateTime.Today
                    },
                    new MyTasksForTheMonth
                    {
                        Room = "Banheiro",
                        CleaningRecords = 0,
                        LastRecord = null
                    }
                }
            };
        }

        private async Task SeeFriendsTaskSelected()
        {
            try
            {
                await ShowLoading();
                await Navigation.PushAsync<SeeScheduleAllFriendsViewModel>();
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
    }
}