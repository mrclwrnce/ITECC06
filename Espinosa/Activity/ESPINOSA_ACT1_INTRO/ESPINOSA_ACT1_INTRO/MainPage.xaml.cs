using System.Collections.ObjectModel;

namespace ESPINOSA_ACT1_INTRO
{
    public partial class MainPage : ContentPage
    {   
        public ObservableCollection<string> Items { get; set; }

        public MainPage()
        {
            InitializeComponent();

            Items = new ObservableCollection<string>
            {
                "APPLES",
                "ORANGES",
                "BANANAS",
                "RATBU"
            };

            ItemsList.ItemsSource = Items;

            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TaskEntry.Text))
            {
                Items.Add(TaskEntry.Text);
                TaskEntry.Text = string.Empty;
            }
        }

    }
}
