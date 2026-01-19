using ClearWork.Domain.Constants;

namespace ClearWork.Domain.OwnedTypes;

/// <summary>
/// Ustawienia Pracowniczych Planów Kapitałowych (PPK) dla miejsca pracy.
/// </summary>
public class PpkSetting
{
    /// <summary>
    /// Określa, czy PPK jest aktywne dla danego miejsca pracy.
    /// </summary>
    public bool IsActive { get; set; } = false;

    /// <summary>
    /// Stawka składki PPK finansowanej przez pracownika.
    /// </summary>
    public decimal EmployeeRate { get; set; } = PpkRates.PpkEmployeeRateDefaultValue;

    /// <summary>
    /// Stawka składki PPK finansowanej przez pracodawcę.
    /// </summary>
    public decimal EmployerRate { get; set; } = PpkRates.PpkEmployerRateDefaultValue;
}