using System;

namespace MedKarta.Application
{
    public interface IApp
    {
        /// <summary>
        /// Вызов расширения GetViewModel.
        /// </summary>
        /// <typeparam name="T">object MyViewModel.</typeparam>    
        /// <returns>Возвращает MyViewModel, MyView.</returns>
        /// <remarks>Вызов функции разместить в блоке try, catch.</remarks>
        /// <exception cref="ArgumentNullException" />
        (T ViewModel, object View) GetViewModel<T>() where T : class;
    }
}