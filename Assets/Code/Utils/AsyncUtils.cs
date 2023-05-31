using System.Threading.Tasks;
using UnityEngine;

namespace Code.Utils
{
    public static class AsyncUtils
    {
        public static async Task WaitForSeconds(float seconds)
        {
            var startTime = Time.time;
            var endTime = startTime + seconds;
            
            while (Time.time < endTime)
                await Task.Yield();
        }
    }
}