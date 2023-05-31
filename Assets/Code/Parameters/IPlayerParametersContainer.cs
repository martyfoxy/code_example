using System.Collections.Generic;

namespace Code.Parameters
{
    /// <summary>
    /// Общий интерфейс для всех контейнеров параметров (параметры здоровья, боевые параметры итд)
    /// </summary>
    public interface IPlayerParametersContainer
    {
        Dictionary<int, object> GetParameters();
    }
}