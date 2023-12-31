using DevExpress.Mvvm;

namespace DataContract.DTO.Messages;

/// <summary>
/// ��������� � ��������� ����������, ������� ������ �����������, ������������ ������
/// </summary>
public class NeedPaymentMessage : BindableBase, IMessage
{
    public int NumberRoom { get; init; }

    /// <summary>
    /// ��������, ������������ ������� ���� �������� ������� � ������.
    /// �������� �� ����� ��������, � ������ ��������� ���.
    /// </summary>
    public double Lived { get; set; }
    
    /// <summary>
    /// ������ ��������� �� �������� ���
    /// </summary>
    public decimal Price { get; init; }

    /// <summary>
    /// ����� ���������� FinanceService, �������� �� ��������� ����� 
    /// </summary>
    public decimal Fines { get; init; }

    /// <summary>
    /// �������������� ����� ���������� ���������� ����� ����� '��������' �������� ������, �������� �� ����������� ������....
    /// </summary>
    public decimal? AdditionalFines { get; set; }
    
    public decimal TotalPrice => Fines + Price + AdditionalFines.GetValueOrDefault();
}