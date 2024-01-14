namespace ConsoleApp.Models;

public class Voucher
{
    public DateTime DeliveryDate { get; set; }

    public decimal TotalPrice { get; set; }

    public List<VoucherSub> Details { get; set; }

    public Voucher()
    {
        Details = new List<VoucherSub>();
    }

    public static Voucher operator +(Voucher voucher1, Voucher voucher2)
    {
        if (voucher1 == null || voucher2 == null)
        {
            throw new ArgumentNullException("One or both vouchers are null.");
        }

        Voucher result = new Voucher
        {
            DeliveryDate = voucher1.DeliveryDate > voucher2.DeliveryDate ? voucher1.DeliveryDate : voucher2.DeliveryDate
        };

        result.Details.AddRange(voucher1.Details);
        result.Details.AddRange(voucher2.Details);

        result.AggregateDetails();

        result.TotalPrice = result.Details.Sum(detail => detail.Price);

        return result;
    }

    private void AggregateDetails()
    {
        var mergedDetails = Details
            .GroupBy(detail => new { detail.Product, detail.Product.IsMergeable })
            .Select(group =>
            {
                if (group.Key.IsMergeable)
                {
                    var totalQuantity = group.Sum(detail => detail.Quantity);
                    var maxPrice = group.Max(detail => detail.Price);

                    var result = new VoucherSub(Details.Count + 1, group.Key.Product, totalQuantity, maxPrice);
                    return result;
                }
                else
                {
                    return group.First();
                }
            })
            .ToList();

        Details.Clear();
        Details.AddRange(mergedDetails);
    }

    public void InsertVoucherSub(Product product, int quantity, decimal price)
    {
        int newRowNumber = Details.Count + 1;
        Details.Add(new VoucherSub(newRowNumber, product, quantity, price));
    }
}
