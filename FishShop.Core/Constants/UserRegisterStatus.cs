namespace FishShop.Core.Constants;

public enum UserRegisterStatus
{
    /// <summary>
    /// Не заргестрирован
    /// </summary>
    None = 1,
    
    /// <summary>
    /// Зарегестрирован, но не подтвертил почту
    /// </summary>
    RegisterButNotConfirmed = 2,
    
    /// <summary>
    /// Зарегестрирован и подтвержден
    /// </summary>
    RegisteredAndConfirmed = 3,
}