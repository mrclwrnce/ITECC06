namespace ESPINOSA_ACT2_PTBL
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnElementTapped(object sender, TappedEventArgs e)
        {
            var border = sender as Border;
            var elementName = e.Parameter as string;
            DisplayAlertAsync(elementName, $"You clicked the element {elementName}", "OK");
        }

    }
}
