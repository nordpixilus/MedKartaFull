using CommunityToolkit.Mvvm.ComponentModel;
using MedKarta.Core.Models;
using MedKarta.DAL.Context;
using MedKarta.DAL.Table.User;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MedKarta.Windows.Main.Views.Start;

internal partial class StartViewModel : BaseViewModel
{
    //[ObservableProperty]
    //private List<Person> _People;
    //public ObservableCollection<Person>? People { get; }
    public StartViewModel()
    {
        //medKartaContext.Persons.Load();
        //_People = medKartaContext.Persons.Local.ToObservableCollection();

        //_People = medKartaContext.Persons
        //    .Where(p => p.StepId == 2)
        //    //.Select(p => new { p.Kod, p.FullName })
        //    .ToList()
        //    ;
    }
}