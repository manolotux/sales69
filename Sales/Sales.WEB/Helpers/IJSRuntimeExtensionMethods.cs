using Microsoft.JSInterop;

namespace Sales.WEB.Helpers
{
    public static class IJSRuntimeExtensionMethods
    {
        private const string KEY = "token";

        public static ValueTask<object> SetLocalStorageItem(this IJSRuntime js, string content) =>
            js.InvokeAsync<object>("localStorage.setItem", KEY, content);

        public static ValueTask<string?> GetLocalStorageItem(this IJSRuntime js) =>
            js.InvokeAsync<string?>("localStorage.getItem", KEY);

        public static ValueTask<object> RemoveLocalStorageItem(this IJSRuntime js) => 
            js.InvokeAsync<object>("localStorage.removeItem", KEY);
    }
}
