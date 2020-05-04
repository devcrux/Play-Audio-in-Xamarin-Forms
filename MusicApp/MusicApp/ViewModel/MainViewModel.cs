using MusicApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicApp.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            musicList = GetMusics();
            recentMusic = musicList.Where(x => x.IsRecent == true).FirstOrDefault();
        }

        ObservableCollection<Music> musicList;
        public ObservableCollection<Music> MusicList
        {
            get { return musicList; }
            set
            {
                musicList = value;
                OnPropertyChanged();
            }
        }

        private Music recentMusic;
        public Music RecentMusic
        {
            get { return recentMusic; }
            set
            {
                recentMusic = value;
                OnPropertyChanged();
            }
        }

        private Music selectedMusic;
        public Music SelectedMusic
        {
            get { return selectedMusic; }
            set
            {
                selectedMusic = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectionCommand => new Command(PlayMusic);

        private void PlayMusic()
        {
            if (selectedMusic != null)
            {
                var viewModel = new PlayerViewModel(selectedMusic, musicList) ;
                var playerPage = new PlayerPage { BindingContext = viewModel };

                var navigation = Application.Current.MainPage as NavigationPage;
                navigation.PushAsync(playerPage, true);
            }
        }

        private ObservableCollection<Music> GetMusics()
        {
            return new ObservableCollection<Music> 
            { 
                new Music { Title = "Beach Walk", Artist = "Unicorn Heads", Url = "https://devcrux.com/wp-content/uploads/Beach_Walk.mp3", CoverImage = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcRU6FVly4jMTD3AKB_sHxqPofJVQwqqUj5peEvgA1H4XegM3uJ7&usqp=CAU", IsRecent = true},
                new Music { Title = "I'll Follow You", Artist = "Density & Time", Url = "https://devcrux.com/wp-content/uploads/I_ll_Follow_You.mp3", CoverImage = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcRm-su97lHFGZrbR6BkgL32qbzZBj2f3gKGrFR0Pn66ih01SyGj&usqp=CAU"},
                new Music { Title = "Ancient", Artist = "Density & Time", Url = "https://devcrux.com/wp-content/uploads/Ancient.mp3"},
                new Music { Title = "News Room News", Artist = "Spence", Url = "https://devcrux.com/wp-content/uploads/Cats_Searching_for_the_Truth.mp3"},
                new Music { Title = "Bro Time", Artist = "Nat Keefe & BeatMowe", Url = "https://devcrux.com/wp-content/uploads/Bro_Time.mp3"},
                new Music { Title = "Cats Searching for the Truth", Artist = "Nat Keefe & Hot Buttered Rum", Url = "https://devcrux.com/wp-content/uploads/Cats_Searching_for_the_Truth.mp3"}
            };
        }
    }
}
