using Microsoft.AspNetCore.SignalR;

namespace PRN222.Lab2.ProductManagementASPNETCoreRazorPage.Helps
{
	public class SignalRServer : Hub
	{
		public async Task NotifyProductDeleted(int productId)
		{
			await Clients.All.SendAsync("ProductDeleted", productId);
		}
		public async Task NotifyProductAdded(int productId, string productName, int unitsInStock, decimal unitPrice, string categoryName)
		{
			await Clients.All.SendAsync("ProductAdded", productId, productName, unitsInStock, unitPrice, categoryName);
		}
		public async Task SendEditedProduct(int productId, string productName, int unitsInStock, decimal unitPrice, string categoryName)
		{
			await Clients.All.SendAsync("ProductEdited", productId, productName, unitsInStock, unitPrice, categoryName);
		}
	}
}
	