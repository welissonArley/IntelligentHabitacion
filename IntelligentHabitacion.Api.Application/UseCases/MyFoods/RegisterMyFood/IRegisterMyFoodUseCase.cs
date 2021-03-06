﻿using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.MyFoods.RegisterMyFood
{
    public interface IRegisterMyFoodUseCase
    {
        Task<ResponseOutput> Execute(RequestProductJson requestMyFood);
    }
}
