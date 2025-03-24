using System.ComponentModel.DataAnnotations;

namespace PRN222.lab2.Repositories.Entities;

public partial class Product
{
	public int ProductId { get; set; }

	[Required(ErrorMessage = "Product Name is required.")]
	[MinLength(10, ErrorMessage = "Product Name must be at least 10 characters long.")]
	[MaxLength(100, ErrorMessage = "Product Name cannot exceed 100 characters.")]
	[RegularExpression(@"^([A-Z0-9][a-zA-Z0-9]*\s?)+$", ErrorMessage = "Product Name must start with a capital letter or a number. No special characters (#, @, &, (, )).")]
	public string ProductName { get; set; } = null!;

	[Required(ErrorMessage = "Category is required.")]
	public int? CategoryId { get; set; }

	[Required(ErrorMessage = "Units in stock is required.")]
	[Range(1, short.MaxValue, ErrorMessage = "Units in stock must be greater than 0.")]
	public short? UnitsInStock { get; set; }

	[Required(ErrorMessage = "Unit price is required.")]
	[Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0.")]
	public decimal UnitPrice { get; set; }

	public virtual Category? Category { get; set; }
}
