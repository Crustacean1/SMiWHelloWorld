using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x415

namespace RaspberryHelloWorld
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private const int MAX_NOTE_COUNT = 10;

        private string _selectedString;
        public ObservableCollection<NoteData> Notes { get; } = new ObservableCollection<NoteData>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if(Notes.Count < MAX_NOTE_COUNT){
                Notes.Add(new NoteData() { Content = SubmitField.Text });
                RefreshNoteCount();
            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems is null || e.AddedItems.Count  == 0) { return; }
            _selectedString = (((NoteData)e.AddedItems[0]).Content);
            NoteDisplayField.Text = _selectedString;
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if(_selectedString is null) { return; }
            var noteToRemove = Notes.FirstOrDefault(note => note.Content.Equals(_selectedString));
            if(noteToRemove is null) { return; }
            var result = Notes.Remove(noteToRemove);
            RefreshNoteCount();
        }

        private void RefreshNoteCount()
        {
            NoteCountBar.Value = (100.0/MAX_NOTE_COUNT) * Notes.Count;
        }
    }
    public class NoteData : INotifyPropertyChanged
    {
        private string _content = "Default note";
        public string Content
        {
            get => _content;
            set
            {
                if (_content.Equals(value)) return;
                _content = value;
                this.OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null) => 
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_content));
    }
}
