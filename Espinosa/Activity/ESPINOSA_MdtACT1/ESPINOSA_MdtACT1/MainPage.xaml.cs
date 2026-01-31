using ESPINOSA_MdtACT1.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace ESPINOSA_MdtACT1
{
    public partial class MainPage : ContentPage
    {
        private readonly DataContext _dataContext;

        public ObservableCollection<Model.User> Users { get; set; }

        public MainPage(DataContext dataContext)
        {
            InitializeComponent();
            _dataContext = dataContext;
            Users = new ObservableCollection<Model.User>(_dataContext.Users.ToList());
            BindingContext = this;
            LoadUsers();
        }

        public async void LoadUsers()
        {
            try
            {
                Users.Clear();

                var users = await _dataContext.Users.ToListAsync();
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
            catch(Exception ex)
            {
                await DisplayAlertAsync("Error", $"Failed to Load Users {ex.Message}", "OK");
            }
        }

        private async void OnAddUserClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                await DisplayAlertAsync("Validation Error", "User name cannot be empty.", "OK");
                return;
            }

            try
            {
                var newUser = new Model.User { Name = NameEntry.Text.Trim() };
                _dataContext.Users.Add(newUser);
                await _dataContext.SaveChangesAsync();

                Users.Add(newUser);
                NameEntry.Text = string.Empty;

                await DisplayAlertAsync("Success", "User added successfully.", "OK");
            }
            catch
            {
                await DisplayAlertAsync("Error", "Failed to add user.", "OK");
                return;
            }


        }



    }
}
