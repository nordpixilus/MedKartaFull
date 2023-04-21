using MedKarta.Core.Helpers;
using MedKarta.DAL.Table.User;
using MedKarta.Interface;
using System.Linq;

namespace MedKarta.Core.Models;

internal class PersonModel
{
    private PersonWork PersonWork = new();

    private readonly IRepository<Person> persons;
    private Person? Person;

    public PersonModel(IRepository<Person> Persons)
    {
        //GetClipBoardText();
        persons = Persons;
    }

    internal string? IsErrorPersonWork()
    {
        return PersonWork.IsErrorFields();
    }

    internal bool IsPersonDB()
    {
        Person = persons.Items.Where(p => p.Kod == PersonWork.Kod).SingleOrDefault();
        if (Person == null)
        {
            return false;
        }
        return true;
    }

    internal string? EquatePerson()
    {
        // TODO: реализовать сравнение полей
        return string.Empty;
    }

    internal bool IsClipBoardText()
    {
        string? fieldsText = ClipBoard.GetFieldsPersonClipBoard();
        if (!string.IsNullOrEmpty(fieldsText))
        {
            PersonWork.SetFields(fieldsText);
            return true;
        }
        return false;
        //else
        //{
            
        //    if (string.IsNullOrEmpty(PersonWork.IsError()))
        //    {
        //        CurrentView = nameof(WorkViewModel);                
        //    }
        //    else
        //    {
        //        CurrentView = nameof(ErrorKodViewModel);
        //    }
        //    // Предположим что данные получены
        //    // TODO: реализовать очередь проверки значений
        //    // проверяем наличие персоны в недоделках
        //    //Person = Person.Items.Where(p => p.StepId == 1)
        //    //    .ToList();
        //    //CurrentView = nameof(StartViewModel);
        //    //Person = persons.Items.Where(p => p.Kod == "70").SingleOrDefault();
            
        //}
    }

    //private async bool SetFiedsPersonAsync()
    //{
    //    string? fieldsText = await ClipBoard.StartMoninitorFieldsPersonAsync();
    //    if (fieldsText != null)
    //    {
    //        //GoToWorkView(fieldsText);
    //    }

    //    return true;
    //}
}
