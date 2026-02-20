using ESPINOSA_MdtACT1.Data;
using ESPINOSA_MdtACT1.Model;
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
            catch (Exception ex)
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

        private async void OnDeleteUserClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is User userToDelete)
            {
                bool confimed = await DisplayAlertAsync("Confirm Delete", $"Are you sure you want to delete this user {userToDelete.Name}?", "Yes", "No");

                if (!confimed)
                {
                    return;
                }
                try
                {
                    _dataContext.Users.Remove(userToDelete);
                    await _dataContext.SaveChangesAsync();
                    Users.Remove(userToDelete);
                    await DisplayAlertAsync("Success", "User deleted successfully.", "OK");
                }
                catch
                {
                    await DisplayAlertAsync("Error", "Failed to delete user.", "OK");
                    return;
                }
            }
        }


        private async void OnEditUserClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is User userToEdit)
            {
                string newName = await DisplayPromptAsync("Edit User", "Enter new name for the user:", initialValue: userToEdit.Name);
                if (string.IsNullOrWhiteSpace(newName))
                {
                    await DisplayAlertAsync("Error", "Cannot be empty.", "OK");
                    return;
                }
                try
                {
                    userToEdit.Name = newName.Trim();
                    _dataContext.Users.Update(userToEdit);
                    await _dataContext.SaveChangesAsync();

                    var index = Users.IndexOf(userToEdit);
                    if (index != -1)
                    {
                        Users[index] = userToEdit;
                        await DisplayAlertAsync("Success", "User updated successfully.", "OK");
                    }

                }
                catch (Exception ex)
                {
                    await DisplayAlertAsync("Error", $"Failed to update user: {ex.Message}", "OK");
                    return;
                }
            }


        }
    }
}
