using topaicarina_lab7.Models;

namespace topaicarina_lab7;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnChooseButtonClicked(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new ProductPage((ShopList)this.BindingContext)
        {
            BindingContext = new Product()
        });

    }

    async void OnDeleteItemButtonClicked(object sender, EventArgs e)
    {
        if (listView.SelectedItem != null)
        {
            var selectedProduct = listView.SelectedItem as Product;
            var shopList = (ShopList)BindingContext;

            await App.Database.DeleteListProductAsync(shopList.ID, selectedProduct.ID);
            listView.ItemsSource = await App.Database.GetListProductsAsync(shopList.ID);
            listView.SelectedItem = null;
        }
        else
        {
            await DisplayAlert("Eroare", "Selecteaza un produs pentru a-l sterge.", "OK");
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var shopl = (ShopList)BindingContext;

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }
}