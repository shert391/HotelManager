namespace DataContract.BusinessModels;

public class PayInformation
{
    public int NumberRoom { get; init; }
    
    public RoomType Type { get; set; }

    public decimal PricePerDay { get; set; }

    /// <summary>
    /// Значение, показывающее сколько дней жильцами прожито в номере.
    /// Возможно не целое значение, в случае неполного дня.
    /// </summary>
    public double Lived { get; set; }

    public decimal Price { get; init; }
    
    public decimal Fines { get; init; }
    
    public decimal AdditionalFines { get; set; }
    
    public decimal TotalPrice => Price + Fines + AdditionalFines; 
    
    public DateTime DatePayment { get; init; } = DateTime.Now;
}