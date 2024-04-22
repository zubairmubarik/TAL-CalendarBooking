namespace Application.Common.Extensions
{
    internal static class TaskExtensions
    {
        /// <summary>
        /// This method is used when we want to execute async call 
        /// in a function which is without async [Keyword]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <returns></returns>
        public static T RunSync<T>(this Task<T> task)
        {
            return task.ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }
    }
}
