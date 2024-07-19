using System.ComponentModel;

namespace FishShop.Contracts.Enums;

/// <summary>
/// Категории товаров
/// </summary>
public enum CategoryType
{
    /// <summary>
    /// Рыболовные снасти
    /// </summary>
    [Description("Рыболовные снасти")]
    FishingGear = 1,
    
    /// <summary>
    /// Рыболовные катушки
    /// </summary>
    [Description("Рыболовные катушки")]
    FishingReels = 2,
    
    /// <summary>
    /// Удилища
    /// </summary>
    [Description("Удилища")]
    Rods = 3,
    
    /// <summary>
    /// Приманки
    /// </summary>
    [Description("Приманки")]
    Baits = 4
}