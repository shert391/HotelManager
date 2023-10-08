namespace DataContract.BusinessModels;

public class PayInformation
{
    public int NumberRoom { get; init; }
    public decimal Price { get; init; }
    public decimal Fines { get; init; }
    public decimal AdditionalFines { get; set; }
    public decimal TotalPrice => Price + Fines + AdditionalFines; 
    
    public DateTime DatePayment { get; init; } = DateTime.Now;
}