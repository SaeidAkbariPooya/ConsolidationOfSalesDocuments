using ConsoleApp.Models;

namespace ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var mergeableProduct = new Product(1, "Mergeable Product", true);
            var nonMergeableProduct = new Product(2, "Non-Mergeable Product", false);


            var voucher1 = new Voucher() { DeliveryDate = DateTime.Now.AddDays(1), TotalPrice = 50 };
            voucher1.Details.Add(new VoucherSub(1, mergeableProduct, 3, 10));
            voucher1.Details.Add(new VoucherSub(2, nonMergeableProduct, 1, 20));

            var voucher2 = new Voucher() { DeliveryDate = DateTime.Now.AddDays(1), TotalPrice = 30 };
            voucher2.Details.Add(new VoucherSub(1, mergeableProduct, 1, 10));
            voucher2.Details.Add(new VoucherSub(2, nonMergeableProduct, 1, 20));

            try
            {
                Voucher result = voucher1 + voucher2;

                result.InsertVoucherSub(new Product(5, "NewItem", true), 2, 40);

                Console.WriteLine($"Result Delivery Date: {result.DeliveryDate}");
                Console.WriteLine($"Result Total Price: {result.TotalPrice}");

                Console.WriteLine("Result Voucher Details:");
                foreach (var voucherSub in result.Details)
                {
                    Console.WriteLine($"RowNumber: {voucherSub.RowNumber}, Product: {voucherSub.Product.Name}, Quantity: {voucherSub.Quantity}, Price: {voucherSub.Price}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadLine();
        }


    }
}
