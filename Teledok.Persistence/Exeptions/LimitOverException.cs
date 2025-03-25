namespace Teledok.Persistence.Exeptions;

/// <summary>
/// Ошибка превышения допустимого количества лиц
/// </summary>
public class LimitOverException()
    : Exception(message: "The founder of an Individual Entrepreneur can be only 1 person")
{ }
