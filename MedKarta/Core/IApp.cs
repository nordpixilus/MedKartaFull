﻿using MedKarta.UCL.Models;

namespace MedKarta.Core
{
    internal interface IApp
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">MyViewModel: where T : BaseViewModel.</typeparam>
        /// <returns></returns>
        (T BaseViewModel, object View) GetViewModel<T>() where T : BaseViewModel;
    }
}