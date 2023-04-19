using CommunityToolkit.Mvvm.ComponentModel;
using MedKarta.Core.Models;
using MedKarta.DAL.Context;
using MedKarta.DAL.Table.User;
using MedKarta.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MedKarta.Windows.Main.Views.Start;

internal partial class StartViewModel : BaseViewModel
{
    [ObservableProperty]
    private List<Person> _People;
    public StartViewModel(IRepository<Person> Person)
    {
        _People = Person.Items
            .Where(p => p.StepId == 1)
            .ToList();
    }
}