using DevExpress.Mvvm;

namespace DataContract.ViewModelsDto.Messages;

/// <summary>
/// ��������� ���������� ��� �������, ����� ������� �������� - �������.
/// ������ DTO ����� "��������" ������ ���������� � FinanceService ��� ������ ����� � �������.
/// </summary>
public class PayInformationDto : BindableBase, IMessage
{
    public int NumberRoom { get; init; }

    public decimal Price { get; init; }

    /// <summary>
    /// ����� ���������� FinanceService, �������� �� ��������� ����� 
    /// </summary>
    public decimal Fines { get; init; }

    /// <summary>
    /// �������������� ����� ���������� ���������� ����� ����� '��������' �������� ������, �������� �� ����������� ������....
    /// </summary>
    public decimal AdditionalFines { get; set; }
    
    public decimal TotalPrice => Fines + Price + AdditionalFines;
}